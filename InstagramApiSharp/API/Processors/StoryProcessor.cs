using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.API.Processors
{
    public class StoryProcessor : IStoryProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public StoryProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger, UserAuthValidate userAuthValidate)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
            _userAuthValidate = userAuthValidate;
        }
        /// <summary>
        ///     Get user story feed (stories from users followed by current user).
        /// </summary>
        public async Task<IResult<InstaStoryFeed>> GetStoryFeedAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var storyFeedUri = UriCreator.GetStoryFeedUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, storyFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaStoryFeed) null);
                var storyFeedResponse = JsonConvert.DeserializeObject<InstaStoryFeedResponse>(json);
                var instaStoryFeed = ConvertersFabric.Instance.GetStoryFeedConverter(storyFeedResponse).Convert();
                return Result.Success(instaStoryFeed);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryFeed>(exception.Message);
            }
        }
        /// <summary>
        ///     Get the story by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        public async Task<IResult<InstaStory>> GetUserStoryAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userStoryUri = UriCreator.GetUserStoryUri(userId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userStoryUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK) Result.UnExpectedResponse<InstaStory>(response, json);
                var userStoryResponse = JsonConvert.DeserializeObject<InstaStoryResponse>(json);
                var userStory = ConvertersFabric.Instance.GetStoryConverter(userStoryResponse).Convert();
                return Result.Success(userStory);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStory>(exception.Message);
            }
        }
        /// <summary>
        ///     Upload story photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(InstaImage image, string caption)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUploadPhotoUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(uploadId), "\"upload_id\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    }
                };
                byte[] imageBytes;
                if (image.ImageBytes == null)
                    imageBytes = File.ReadAllBytes(image.Uri);
                else
                    imageBytes = image.ImageBytes;
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent, "photo", $"pending_media_{ApiRequestMessage.GenerateUploadId()}.jpg");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return await ConfigureStoryPhotoAsync(image, uploadId, caption);
                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception.Message);
            }
        }
        /// <summary>
        ///     Configure story photo
        /// </summary>
        /// <param name="image">Photo to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        private async Task<IResult<InstaStoryMedia>> ConfigureStoryPhotoAsync(InstaImage image, string uploadId,
            string caption)
        {
            try
            {
                var instaUri = UriCreator.GetStoryConfigureUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"source_type", "1"},
                    {"caption", caption},
                    {"upload_id", uploadId},
                    {"edits", new JObject()},
                    {"disable_comments", false},
                    {"configure_mode", 1},
                    {"camera_position", "unknown"}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var mediaResponse = JsonConvert.DeserializeObject<InstaStoryMediaResponse>(json);
                    var converter = ConvertersFabric.Instance.GetStoryMediaConverter(mediaResponse);
                    return Result.Success(converter.Convert());
                }

                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception.Message);
            }
        }
        /// <summary>
        ///     Upload story video
        /// </summary>
        /// <param name="image">video to upload</param>
        /// <param name="caption">Caption</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryVideoAsync(InstaVideoUpload video, string caption)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                
                var uploadId = ApiRequestMessage.GenerateRandomUploadId();
                var videoHashCode = Path.GetFileName(video.Video.Uri).GetHashCode();
                var photoHashCode = Path.GetFileName(video.VideoThumbnail.Uri).GetHashCode();

                var waterfallId = Guid.NewGuid().ToString();
      
                var videoEntityName= string.Format("{0}_0_{1}", uploadId, videoHashCode);
                var videoUri = UriCreator.GetStoryUploadVideoUri(uploadId, videoHashCode);

                var photoEntityName = string.Format("{0}_0_{1}", uploadId, photoHashCode);
                var photoUri = UriCreator.GetStoryUploadPhotoUri(uploadId, photoHashCode);


                var videoMediaInfoData = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"media_info", new JObject
                        {
                              {"capture_mode", "normal"},
                              {"media_type", 2},
                              {"caption", caption},
                              {"mentions", new JArray()},
                              {"hashtags", new JArray()},
                              {"locations", new JArray()},
                              {"stickers", new JArray()},
                        }
                    }
                };

                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, UriCreator.GetStoryMediaInfoUploadUri(), _deviceInfo, videoMediaInfoData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                //{"message": {}, "status": "fail"}



                // prepare video for upload
                var videoUploadParamsObj = new JObject
                {
                    {"upload_media_height", "0"},
                    {"upload_media_width", "0"},
                    {"upload_media_duration_ms", "46000"},
                    {"upload_id", uploadId},
                    {"for_album", "1"},
                    {"retry_context", "{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}"},
                    {"media_type", "2"},
                };
                var videoUploadParams = JsonConvert.SerializeObject(videoUploadParamsObj);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Get, videoUri, _deviceInfo);
                request.Headers.Add("X_FB_VIDEO_WATERFALL_ID", waterfallId);
                request.Headers.Add("X-Instagram-Rupload-Params", videoUploadParams);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                //{"offset":0}



                // upload video
                byte[] videoBytes;
                if (video.Video.VideoBytes == null)
                    videoBytes = File.ReadAllBytes(video.Video.Uri);
                else
                    videoBytes = video.Video.VideoBytes;

                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {new StringContent("\"0\""), "upload_media_height"},
                    {new StringContent("\"0\""), "upload_media_width"},
                    {new StringContent($"\"{uploadId}\""), "upload_id"},
                    {new StringContent("\"2\""), "media_type"},
                    {new StringContent("\"1\""), "for_album"},
                    {new StringContent("\"0\""), "upload_media_duration_ms"},
                    {new StringContent("\"{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}\""), "retry_context"},
                };
                var videoContent = new ByteArrayContent(videoBytes);
                videoContent.Headers.Add("Content-Transfer-Encoding", "binary");
                videoContent.Headers.Add("Content-Type", "application/octet-stream");
                //videoContent.Headers.Add("Content-Disposition", $"attachment; filename=\"{Path.GetFileName(video.Video.Uri)}\"");

                requestContent.Add(videoContent, "video", $"pending_media_{Path.GetFileName(video.Video.Uri)}");
                request = HttpHelper.GetDefaultRequest(HttpMethod.Post, videoUri, _deviceInfo);
                request.Content = requestContent;
                var vidExt = Path.GetExtension(video.Video.Uri).Replace(".", "").ToLower();
                if(vidExt == "mov")
                    request.Headers.Add("X-Entity-Type", "video/quicktime");
                else
                    request.Headers.Add("X-Entity-Type", "video/mp4");
                request.Headers.Add("Offset", "0");
                request.Headers.Add("X-Instagram-Rupload-Params", videoUploadParams);
                request.Headers.Add("X-Entity-Name", videoEntityName);
                request.Headers.Add("X-Entity-Length", videoBytes.Length.ToString());
                request.Headers.Add("X_FB_VIDEO_WATERFALL_ID", waterfallId);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                //{"xsharing_nonces": {}, "status": "ok"}
                
                // video uploaded

                // prepare video thumbnail for upload
                var photoUploadParamsObj = new JObject
                {
                    {"retry_context", "{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}"},
                    {"media_type", "2"},
                    {"upload_id", uploadId},
                    {"image_compression", "{\"lib_name\":\"moz\",\"lib_version\":\"3.1.m\",\"quality\":\"95\"}"},
                };
                var photoUploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Get, photoUri, _deviceInfo);
                request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                //{"offset":0}

                // upload video thumbnail
                requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(uploadId), "\"upload_id\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    },
                    {new StringContent("{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}"), "\"retry_context\""},
                    {new StringContent("2"), "\"media_type\""},
                };
                byte[] imageBytes;
                if (video.VideoThumbnail.ImageBytes == null)
                    imageBytes = File.ReadAllBytes(video.VideoThumbnail.Uri);
                else
                    imageBytes = video.VideoThumbnail.ImageBytes;

                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent/*, "photo", $"cover_photo_{uploadId}.jpg"*/);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Post, photoUri, _deviceInfo);
                request.Content = requestContent;
                request.Headers.Add("X-Entity-Type", "image/jpeg");
                request.Headers.Add("Offset", "0");
                request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
                request.Headers.Add("X-Entity-Name", photoEntityName);
                request.Headers.Add("X-Entity-Length", imageBytes.Length.ToString());
                request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                //{"upload_id": "153559153995740", "xsharing_nonces": {}, "status": "ok"}
                
                // video thumbnail uploaded
                if (response.IsSuccessStatusCode)
                    return await ConfigureStoryVideoAsync(video, uploadId, caption);// not working
                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception.Message);
            }
        }
        /// <summary>
        ///     Configure story video | It's NOT WORKING
        /// </summary>
        /// <param name="video">Video to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        private async Task<IResult<InstaStoryMedia>> ConfigureStoryVideoAsync(InstaVideoUpload video, string uploadId,
            string caption)
        {
            try
            {
                var instaUri = UriCreator.GetVideoStoryConfigureUri(false);
                // NOT WORKING

                //{
                //	"filter_type": "0",
                //	"timezone_offset": "16200",
                //	"_csrftoken": "xzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz",
                //	"client_shared_at": "1535457104",
                //	"media_folder": "FastSave",
                //	"configure_mode": "1",
                //	"source_type": "4",
                //	"video_result": "",
                //	"_uid": "7405924766",
                //	"_uuid": "6324ecb2-e663-4dc8-a3a1-289c699cc876",
                //	"imported_taken_at": "1535453973",
                //	"caption": "Abcdef",
                //	"date_time_original": "19040101T000000.000Z",
                //	"capture_type": "normal",
                //	"mas_opt_in": "NOT_PROMPTED",
                //	"upload_id": "114717810279369",
                //	"client_timestamp": "1535457152",
                //	"device": {
                //		"manufacturer": "HUAWEI",
                //		"model": "PRA-LA1",
                //		"android_version": 24,
                //		"android_release": "7.0"
                //	},
                //	"length": 15.0,
                //	"clips": [{
                //		"length": 15.0,
                //		"source_type": "4"
                //	}],
                //	"extra": {
                //		"source_width": 480,
                //		"source_height": 480
                //	},
                //	"audio_muted": false,
                //	"poster_frame_index": 0
                //}
                Random rnd = new Random();
                var data = new JObject
                {
                    {"filter_type", "0"},
                    {"timezone_offset", "16200"},
                    {"_csrftoken", _user.CsrfToken},
                    {"client_shared_at", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(3,10)).ToString()},
                    {"story_media_creation_date", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(10,20)).ToString()},
                    {"media_folder", "Camera"},
                    {"configure_mode", "1"},
                    {"source_type", "4"},
                    {"video_result", ""},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"caption", caption},
                    //{"date_time_original", DateTime.UtcNow.ToString("yyyyMMdd hmmss")},
                    {"date_time_original", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fffZ")},
                    {"capture_type", "normal"},
                    {"mas_opt_in", "NOT_PROMPTED"},
                    {"upload_id", uploadId},
                    {"client_timestamp", ApiRequestMessage.GenerateUploadId()},
                    {
                        "device", new JObject{
                            {"manufacturer", _deviceInfo.HardwareManufacturer},
                            {"model", _deviceInfo.DeviceModelIdentifier},
                            {"android_release", "7.0"},
                            {"android_version", 24}
                        }
                    },
                    {"length", 0},
                    //{
                    //    "clips", /*JsonConvert.SerializeObject*/(new JArray{
                    //        new JObject
                    //        {
                    //            {"length", 46},
                    //            {"source_type", "4"},
                    //        }
                    //    })
                    //},
                    {
                        "extra", new JObject
                        {
                            {"source_width", 0},
                            {"source_height", 0}
                        }
                    },
                    {"audio_muted", false},
                    {"poster_frame_index", 0},
                };
                Debug.WriteLine(data.ToString(Formatting.Indented));
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                
                var uploadParamsObj = new JObject
                {
                    {"num_step_auto_retry", 0},
                    {"num_reupload", 0},
                    {"num_step_manual_retry", 0}
                };
                var uploadParams = JsonConvert.SerializeObject(uploadParamsObj);
                request.Headers.Add("retry_context", uploadParams);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("JRESP Configure: " + json);
                //{"message": "Unknown Server Error.", "status": "fail"}
                //{"message": "media_needs_reupload", "media": {"device_timestamp": "1535578012"}, "error_title": "staged_upload_not_found extra:{}", "status": "ok"}
                
                
                // every time returns this
                //{"message": "Transcode error: Video file does not contain duration", "status": "fail"}

                if (response.IsSuccessStatusCode)
                {
                    var mediaResponse = JsonConvert.DeserializeObject<InstaStoryMediaResponse>(json);
                    var converter = ConvertersFabric.Instance.GetStoryMediaConverter(mediaResponse);
                    return Result.Success(converter.Convert());
                }

                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception.Message);
            }
        }
        /// <summary>
        ///     Get user story reel feed. Contains user info last story including all story items.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        public async Task<IResult<InstaReelFeed>> GetUserStoryFeedAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var feed = new InstaReelFeed();
            try
            {
                var userFeedUri = UriCreator.GetUserReelFeedUri(userId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaReelFeed>(response, json);
                var feedResponse = JsonConvert.DeserializeObject<InstaReelFeedResponse>(json);
                feed = ConvertersFabric.Instance.GetReelFeedConverter(feedResponse).Convert();
                return Result.Success(feed);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, feed);
            }
        }
        /// <summary>
        ///     Get story media viewers
        /// </summary>
        /// <param name="StoryMediaId">Story media id</param>
        /// <param name="paginationParameters">Pagination parameters</param>
        public async Task<IResult<InstaReelStoryMediaViewers>> GetStoryMediaViewers(string StoryMediaId, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters.MaximumPagesToLoad > 1)
                    throw new Exception("Not supported");
                var directInboxUri = new Uri(InstaApiConstants.BASE_INSTAGRAM_API_URL +$"media/{StoryMediaId}/list_reel_media_viewer/?max_id={paginationParameters.NextId}", UriKind.RelativeOrAbsolute);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaReelStoryMediaViewers>(response, json);
                var threadResponse = JsonConvert.DeserializeObject<InstaReelStoryMediaViewers>(json);

                return Result.Success(threadResponse);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaReelStoryMediaViewers>(exception.Message);
            }
        }
        /// <summary>
        ///     Share story to someone
        /// </summary>
        /// <param name="reelId">Reel id</param>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="threadId">Thread id</param>
        /// <param name="sharingType">Sharing type</param>
        public async Task<IResult<InstaSharing>> ShareStoryAsync(string reelId, string storyMediaId, string threadId, SharingType sharingType = SharingType.Video)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = new Uri(InstaApiConstants.BASE_INSTAGRAM_API_URL + $"direct_v2/threads/broadcast/story_share/?media_type={sharingType.ToString().ToLower()}");
                var data = new JObject
                {
                    {"action", "send_item"},
                    {"thread_ids", $"[{threadId}]"},
                    {"unified_broadcast_format", "1"},
                    {"reel_id", reelId},
                    {"story_media_id", storyMediaId},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (InstaSharing)null);
                var obj = JsonConvert.DeserializeObject<InstaSharing>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaSharing>(exception);
            }
        }
    }
}