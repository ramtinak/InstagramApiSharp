/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using InstagramApiSharp.Helpers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.Models;
using System.Net;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Enums;
using System.IO;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Helper processor for other processors
    /// </summary>
    internal class HelperProcessor
    {
        #region Properties and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        public HelperProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger,
            UserAuthValidate userAuthValidate, InstaApi instaApi)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
            _userAuthValidate = userAuthValidate;
            _instaApi = instaApi;
        }
        #endregion Properties and constructor

        /// <summary>
        ///     Send video story, direct video, disappearing video
        /// </summary>
        /// <param name="isDirectVideo">Direct video</param>
        /// <param name="isDisappearingVideo">Disappearing video</param>
        public async Task<IResult<bool>> SendVideoAsync(bool isDirectVideo,bool isDisappearingVideo,string caption, InstaViewMode viewMode, InstaStoryType storyType,  string recipients, string threadId, InstaVideoUpload video)
        {
            try
            {
                var uploadId = ApiRequestMessage.GenerateRandomUploadId();
                var videoHashCode = Path.GetFileName(video.Video.Uri).GetHashCode();
                var waterfallId = Guid.NewGuid().ToString();
                var videoEntityName = string.Format("{0}_0_{1}", uploadId, videoHashCode);
                var videoUri = UriCreator.GetStoryUploadVideoUri(uploadId, videoHashCode);
                var retryContext = GetRetryContext();
                HttpRequestMessage request = null;
                HttpResponseMessage response = null;
                string videoUploadParams = null;
                string json = null;
                var videoUploadParamsObj = new JObject();
                if (isDirectVideo)
                {
                    videoUploadParamsObj = new JObject
                    {
                        {"upload_media_height", "0"},
                        {"direct_v2", "1"},
                        {"upload_media_width", "0"},
                        {"upload_media_duration_ms", "0"},
                        {"upload_id", uploadId},
                        {"retry_context", retryContext},
                        {"media_type", "2"}
                    };

                    videoUploadParams = JsonConvert.SerializeObject(videoUploadParamsObj);
                    request = HttpHelper.GetDefaultRequest(HttpMethod.Get, videoUri, _deviceInfo);
                    request.Headers.Add("X_FB_VIDEO_WATERFALL_ID", waterfallId);
                    request.Headers.Add("X-Instagram-Rupload-Params", videoUploadParams);
                    response = await _httpRequestProcessor.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode != HttpStatusCode.OK)
                        return Result.UnExpectedResponse<bool>(response, json);
                }
                else
                {
                    videoUploadParamsObj = new JObject
                    {
                        {"_csrftoken", _user.CsrfToken},
                        {"_uid", _user.LoggedInUser.Pk},
                        {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                        {"media_info", new JObject
                            {
                                    {"capture_mode", "normal"},
                                    {"media_type", 2},
                                    {"caption", caption ?? string.Empty},
                                    {"mentions", new JArray()},
                                    {"hashtags", new JArray()},
                                    {"locations", new JArray()},
                                    {"stickers", new JArray()},
                            }
                        }
                    };
                    request = HttpHelper.GetSignedRequest(HttpMethod.Post, UriCreator.GetStoryMediaInfoUploadUri(), _deviceInfo, videoUploadParamsObj);
                    response = await _httpRequestProcessor.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();


                    videoUploadParamsObj = new JObject
                    {
                        {"upload_media_height", "0"},
                        {"upload_media_width", "0"},
                        {"upload_media_duration_ms", "0"},
                        {"upload_id", uploadId},
                        {"retry_context", "{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}"},
                        {"media_type", "2"}
                    };
                    if (isDisappearingVideo)
                    {
                        videoUploadParamsObj.Add("for_direct_story", "1");
                    }
                    else
                    {
                        switch (storyType)
                        {
                            case InstaStoryType.SelfStory:
                            default:
                                videoUploadParamsObj.Add("for_album", "1");
                                break;
                            case InstaStoryType.Direct:
                                videoUploadParamsObj.Add("for_direct_story", "1");
                                break;
                            case InstaStoryType.Both:
                                videoUploadParamsObj.Add("for_album", "1");
                                videoUploadParamsObj.Add("for_direct_story", "1");
                                break;
                        }
                    }
                    videoUploadParams = JsonConvert.SerializeObject(videoUploadParamsObj);
                    request = HttpHelper.GetDefaultRequest(HttpMethod.Get, videoUri, _deviceInfo);
                    request.Headers.Add("X_FB_VIDEO_WATERFALL_ID", waterfallId);
                    request.Headers.Add("X-Instagram-Rupload-Params", videoUploadParams);
                    response = await _httpRequestProcessor.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();


                    if (response.StatusCode != HttpStatusCode.OK)
                        return Result.UnExpectedResponse<bool>(response, json);
                }

                // video part
                byte[] videoBytes;
                if (video.Video.VideoBytes == null)
                    videoBytes = File.ReadAllBytes(video.Video.Uri);
                else
                    videoBytes = video.Video.VideoBytes;

                var videoContent = new ByteArrayContent(videoBytes);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Post, videoUri, _deviceInfo);
                request.Content = videoContent;
                var vidExt = Path.GetExtension(video.Video.Uri).Replace(".", "").ToLower();
                if (vidExt == "mov")
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


                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                if (!isDirectVideo)
                {
                    var photoHashCode = Path.GetFileName(video.VideoThumbnail.Uri).GetHashCode();
                    var photoEntityName = string.Format("{0}_0_{1}", uploadId, photoHashCode);
                    var photoUri = UriCreator.GetStoryUploadPhotoUri(uploadId, photoHashCode);
                    var photoUploadParamsObj = new JObject
                    {
                        {"retry_context", retryContext},
                        {"media_type", "2"},
                        {"upload_id", uploadId},
                        {"image_compression", "{\"lib_name\":\"moz\",\"lib_version\":\"3.1.m\",\"quality\":\"95\"}"},
                    };

                    var photoUploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                    byte[] imageBytes;
                    if (video.VideoThumbnail.ImageBytes == null)
                        imageBytes = File.ReadAllBytes(video.VideoThumbnail.Uri);
                    else
                        imageBytes = video.VideoThumbnail.ImageBytes;
                    var imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                    imageContent.Headers.Add("Content-Type", "application/octet-stream");
                    request = HttpHelper.GetDefaultRequest(HttpMethod.Post, photoUri, _deviceInfo);
                    request.Content = imageContent;
                    request.Headers.Add("X-Entity-Type", "image/jpeg");
                    request.Headers.Add("Offset", "0");
                    request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
                    request.Headers.Add("X-Entity-Name", photoEntityName);
                    request.Headers.Add("X-Entity-Length", imageBytes.Length.ToString());
                    request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                    response = await _httpRequestProcessor.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();

                }
                return await ConfigureVideo(uploadId, isDirectVideo, isDisappearingVideo,caption, viewMode,storyType, recipients, threadId);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }

        }

        private async Task<IResult<bool>> ConfigureVideo(string uploadId, bool isDirectVideo, bool isDisappearingVideo, string caption, InstaViewMode viewMode, InstaStoryType storyType, string recipients, string threadId)
        {
            try
            {

                Uri instaUri = UriCreator.GetDirectConfigVideoUri();
                var retryContext = GetRetryContext();
                var clientContext = Guid.NewGuid().ToString();
                
                if (isDirectVideo)
                {
                    var data = new Dictionary<string, string>()
                    {
                         {"action","send_item"},
                         {"client_context",clientContext.ToString()},
                         {"_csrftoken",_user.CsrfToken},
                         {"video_result",""},
                         {"_uuid",_deviceInfo.DeviceGuid.ToString()},
                         {"upload_id",uploadId}
                    };
                    if (!string.IsNullOrEmpty(recipients))
                        data.Add("recipient_users", $"[[{recipients}]]");
                    else
                        data.Add("thread_ids", $"[{threadId}]");

                    instaUri = UriCreator.GetDirectConfigVideoUri();
                    var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                    request.Content = new FormUrlEncodedContent(data);
                    request.Headers.Add("retry_context", retryContext);
                    var response = await _httpRequestProcessor.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode != HttpStatusCode.OK)
                        return Result.UnExpectedResponse<bool>(response, json);

                    var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                    return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
                }
                else
                {
                    Random rnd = new Random();
                    var data = new JObject
                    {
                        {"filter_type", "0"},
                        {"timezone_offset", "16200"},
                        {"_csrftoken", _user.CsrfToken},
                        {"client_shared_at", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(25,55)).ToString()},
                        {"story_media_creation_date", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(50,70)).ToString()},
                        {"media_folder", "Camera"},
                        {"source_type", "4"},
                        {"video_result", ""},
                        {"_uid", _user.LoggedInUser.Pk.ToString()},
                        {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                        {"caption", caption ?? string.Empty},
                        {"date_time_original", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fffZ")},
                        {"capture_type", "normal"},
                        {"mas_opt_in", "NOT_PROMPTED"},
                        {"upload_id", uploadId},
                        {"client_timestamp", ApiRequestMessage.GenerateUploadId()},
                        {
                            "device", new JObject{
                                {"manufacturer", _deviceInfo.HardwareManufacturer},
                                {"model", _deviceInfo.DeviceModelIdentifier},
                                {"android_release", _deviceInfo.AndroidVersion.VersionNumber},
                                {"android_version", _deviceInfo.AndroidVersion.APILevel}
                            }
                        },
                        {"length", 0},
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
                    if (isDisappearingVideo)
                    {
                        data.Add("view_mode", viewMode.ToString().ToLower());
                        data.Add("configure_mode", "2");
                        data.Add("recipient_users", "[]");
                        data.Add("thread_ids", $"[{threadId}]");
                    }
                    else
                    {
                        switch (storyType)
                        {
                            case InstaStoryType.SelfStory:
                            default:
                                data.Add("configure_mode", "1");
                                break;
                            case InstaStoryType.Direct:
                                data.Add("configure_mode", "2");
                                data.Add("view_mode", "replayable");
                                data.Add("recipient_users", "[]");
                                data.Add("thread_ids", $"[{threadId}]");
                                break;
                            case InstaStoryType.Both:
                                data.Add("configure_mode", "3");
                                data.Add("view_mode", "replayable");
                                data.Add("recipient_users", "[]");
                                data.Add("thread_ids", $"[{threadId}]");
                                break;
                        }
                    }
                    instaUri = UriCreator.GetVideoStoryConfigureUri(true);
                    var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                 
                    request.Headers.Add("retry_context", retryContext);
                    var response = await _httpRequestProcessor.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var mediaResponse = JsonConvert.DeserializeObject<InstaDefault>(json);

                        return mediaResponse.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
                    }
                    return Result.UnExpectedResponse<bool>(response, json);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }



        public async Task<IResult<bool>> SendPhotoAsync(bool isDirectPhoto, bool isDisappearingPhoto, string caption, InstaViewMode viewMode, InstaStoryType storyType, string recipients, string threadId, InstaImage image)
        {
            try
            {
                var uploadId = ApiRequestMessage.GenerateRandomUploadId();
                var photoHashCode = Path.GetFileName(image.Uri).GetHashCode();
                var photoEntityName = string.Format("{0}_0_{1}", uploadId, photoHashCode);
                var photoUri = UriCreator.GetStoryUploadPhotoUri(uploadId, photoHashCode);
                var waterfallId = Guid.NewGuid().ToString();
                var retryContext = GetRetryContext();
                HttpRequestMessage request = null;
                HttpResponseMessage response = null;
                string json = null;
                var photoUploadParamsObj = new JObject
                {
                    {"retry_context", retryContext},
                    {"media_type", "1"},
                    {"upload_id", uploadId},
                    {"image_compression", "{\"lib_name\":\"moz\",\"lib_version\":\"3.1.m\",\"quality\":\"95\"}"},
                };
                //if (isDirectPhoto)
                //{
                //}
                //else
                {
                    var uploadParamsObj = new JObject
                    {
                        {"_csrftoken", _user.CsrfToken},
                        {"_uid", _user.LoggedInUser.Pk},
                        {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                        {"media_info", new JObject
                            {
                                    {"capture_mode", "normal"},
                                    {"media_type", 1},
                                    {"caption", caption ?? string.Empty},
                                    {"mentions", new JArray()},
                                    {"hashtags", new JArray()},
                                    {"locations", new JArray()},
                                    {"stickers", new JArray()},
                            }
                        }
                    };
                    request = HttpHelper.GetSignedRequest(HttpMethod.Post, UriCreator.GetStoryMediaInfoUploadUri(), _deviceInfo, uploadParamsObj);
                    response = await _httpRequestProcessor.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(json);

                    
                    var uploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                    request = HttpHelper.GetDefaultRequest(HttpMethod.Get, photoUri, _deviceInfo);
                    request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                    request.Headers.Add("X-Instagram-Rupload-Params", uploadParams);
                    response = await _httpRequestProcessor.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(json);


                    if (response.StatusCode != HttpStatusCode.OK)
                        return Result.UnExpectedResponse<bool>(response, json);
                }
                
                //if (!isDirectPhoto)
                {
                    var photoUploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                    byte[] imageBytes;
                    if (image.ImageBytes == null)
                        imageBytes = File.ReadAllBytes(image.Uri);
                    else
                        imageBytes = image.ImageBytes;
                    var imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                    imageContent.Headers.Add("Content-Type", "application/octet-stream");
                    request = HttpHelper.GetDefaultRequest(HttpMethod.Post, photoUri, _deviceInfo);
                    request.Content = imageContent;
                    request.Headers.Add("X-Entity-Type", "image/jpeg");
                    request.Headers.Add("Offset", "0");
                    request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
                    request.Headers.Add("X-Entity-Name", photoEntityName);
                    request.Headers.Add("X-Entity-Length", imageBytes.Length.ToString());
                    request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                    response = await _httpRequestProcessor.SendAsync(request);
                    json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(json);

                }
                return await ConfigurePhoto(uploadId, isDirectPhoto, isDisappearingPhoto, caption, viewMode, storyType, recipients, threadId);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }

        }

        private async Task<IResult<bool>> ConfigurePhoto(string uploadId, bool isDirectPhoto, bool isDisappearingPhoto, string caption, InstaViewMode viewMode, InstaStoryType storyType, string recipients, string threadId)
        {
            try
            {

                Uri instaUri = UriCreator.GetDirectConfigVideoUri();
                var retryContext = GetRetryContext();
                var clientContext = Guid.NewGuid().ToString();

                //if (isDirectVideo)
                //{
           
                //}
                //else
                {

                    //{
                    //	"recipient_users": "[]",
                    //	"view_mode": "permanent",
                    //	"thread_ids": "[\"340282366841710300949128132202173515958\"]",
                    //	"timezone_offset": "16200",
                    //	"_csrftoken": "gRMgctLzzC9MfJBQTz3MzxeYMtBxCY4s",
                    //	"client_shared_at": "1536323374",
                    //	"configure_mode": "2",
                    //	"source_type": "3",
                    //	"_uid": "7405924766",
                    //	"_uuid": "6324ecb2-e663-4dc8-a3a1-289c699cc876",
                    //	"capture_type": "normal",
                    //	"mas_opt_in": "NOT_PROMPTED",
                    //	"upload_id": "469885239145487",
                    //	"client_timestamp": "1536323328",
                    //	"device": {
                    //		"manufacturer": "HUAWEI",
                    //		"model": "PRA-LA1",
                    //		"android_version": 24,
                    //		"android_release": "7.0"
                    //	},
                    //	"edits": {
                    //		"crop_original_size": [2240.0, 3968.0],
                    //		"crop_center": [0.0, -2.5201612E-4],
                    //		"crop_zoom": 1.0595461
                    //	},
                    //	"extra": {
                    //		"source_width": 2232,
                    //		"source_height": 3745
                    //	}
                    //}
                    Random rnd = new Random();
                    var data = new JObject
                    {
                        {"timezone_offset", "16200"},
                        {"_csrftoken", _user.CsrfToken},
                        {"client_shared_at", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(25,55)).ToString()},
                        {"story_media_creation_date", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(50,70)).ToString()},
                        {"media_folder", "Camera"},
                        {"source_type", "3"},
                        {"video_result", ""},
                        {"_uid", _user.LoggedInUser.Pk.ToString()},
                        {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                        {"caption", caption ?? string.Empty},
                        {"date_time_original", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fffZ")},
                        {"capture_type", "normal"},
                        {"mas_opt_in", "NOT_PROMPTED"},
                        {"upload_id", uploadId},
                        {"client_timestamp", ApiRequestMessage.GenerateUploadId()},
                        {
                            "device", new JObject{
                                {"manufacturer", _deviceInfo.HardwareManufacturer},
                                {"model", _deviceInfo.DeviceModelIdentifier},
                                {"android_release", _deviceInfo.AndroidVersion.VersionNumber},
                                {"android_version", _deviceInfo.AndroidVersion.APILevel}
                            }
                        },
                        {
                            "extra", new JObject
                            {
                                {"source_width", 0},
                                {"source_height", 0}
                            }
                        }
                    };
                    if (isDisappearingPhoto)
                    {
                        data.Add("view_mode", viewMode.ToString().ToLower());
                        data.Add("configure_mode", "2");
                        data.Add("recipient_users", "[]");
                        data.Add("thread_ids", $"[{threadId}]");
                    }
                    else
                    {
                        switch (storyType)
                        {
                            case InstaStoryType.SelfStory:
                            default:
                                data.Add("configure_mode", "1");
                                break;
                            case InstaStoryType.Direct:
                                data.Add("configure_mode", "2");
                                data.Add("view_mode", "replayable");
                                data.Add("recipient_users", "[]");
                                data.Add("thread_ids", $"[{threadId}]");
                                break;
                            case InstaStoryType.Both:
                                data.Add("configure_mode", "3");
                                data.Add("view_mode", "replayable");
                                data.Add("recipient_users", "[]");
                                data.Add("thread_ids", $"[{threadId}]");
                                break;
                        }
                    }
                    instaUri = UriCreator.GetVideoStoryConfigureUri(false);
                    var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);

                    request.Headers.Add("retry_context", retryContext);
                    var response = await _httpRequestProcessor.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var mediaResponse = JsonConvert.DeserializeObject<InstaDefault>(json);

                        return mediaResponse.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
                    }
                    return Result.UnExpectedResponse<bool>(response, json);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }


        public async Task<IResult<InstaMedia>> SendMediaPhotoAsync(InstaImage image, string caption, InstaLocationShort location)
        {
            try
            {
                var uploadId = ApiRequestMessage.GenerateRandomUploadId();
                var photoHashCode = Path.GetFileName(image.Uri).GetHashCode();
                var photoEntityName = string.Format("{0}_0_{1}", uploadId, photoHashCode);
                var photoUri = UriCreator.GetStoryUploadPhotoUri(uploadId, photoHashCode);
                var waterfallId = Guid.NewGuid().ToString();
                var retryContext = GetRetryContext();
                HttpRequestMessage request = null;
                HttpResponseMessage response = null;
                string json = null;
                var photoUploadParamsObj = new JObject
                {
                    {"retry_context", retryContext},
                    {"media_type", "1"},
                    {"upload_id", uploadId},
                    {"image_compression", "{\"lib_name\":\"moz\",\"lib_version\":\"3.1.m\",\"quality\":\"95\"}"},
                };
                var uploadParamsObj = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"media_info", new JObject
                        {
                                {"capture_mode", "normal"},
                                {"media_type", 1},
                                {"caption", caption ?? string.Empty},
                                {"mentions", new JArray()},
                                {"hashtags", new JArray()},
                                {"locations", new JArray()},
                                {"stickers", new JArray()},
                        }
                    }
                };
                request = HttpHelper.GetSignedRequest(HttpMethod.Post, UriCreator.GetStoryMediaInfoUploadUri(), _deviceInfo, uploadParamsObj);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();


                var uploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Get, photoUri, _deviceInfo);
                request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                request.Headers.Add("X-Instagram-Rupload-Params", uploadParams);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();


                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMedia>(response, json);


                var photoUploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                byte[] imageBytes;
                if (image.ImageBytes == null)
                    imageBytes = File.ReadAllBytes(image.Uri);
                else
                    imageBytes = image.ImageBytes;
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                request = HttpHelper.GetDefaultRequest(HttpMethod.Post, photoUri, _deviceInfo);
                request.Content = imageContent;
                request.Headers.Add("X-Entity-Type", "image/jpeg");
                request.Headers.Add("Offset", "0");
                request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
                request.Headers.Add("X-Entity-Name", photoEntityName);
                request.Headers.Add("X-Entity-Length", imageBytes.Length.ToString());
                request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return await ConfigureMediaPhotoAsync(uploadId, caption, location);
                return Result.UnExpectedResponse<InstaMedia>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }

        }
        private async Task<IResult<InstaMedia>> ConfigureMediaPhotoAsync(string uploadId, string caption, InstaLocationShort location)
        {
            try
            {

                Uri instaUri = UriCreator.GetMediaConfigureUri();
                var retryContext = GetRetryContext();
                var clientContext = Guid.NewGuid().ToString();
                Random rnd = new Random();
                var data = new JObject
                {
                    {"timezone_offset", "16200"},
                    {"_csrftoken", _user.CsrfToken},
                    {"client_shared_at", (long.Parse(ApiRequestMessage.GenerateUploadId()) - rnd.Next(25,55)).ToString()},
                    {"story_media_creation_date", (long.Parse(ApiRequestMessage.GenerateUploadId()) - rnd.Next(50,70)).ToString()},
                    {"media_folder", "Camera"},
                    {"source_type", "3"},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"caption", caption ?? string.Empty},
                    {"date_time_original", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fffZ")},
                    {"capture_type", "normal"},
                    {"mas_opt_in", "NOT_PROMPTED"},
                    {"upload_id", uploadId},
                    {"client_timestamp", ApiRequestMessage.GenerateUploadId()},
                    {
                        "device", new JObject{
                            {"manufacturer", _deviceInfo.HardwareManufacturer},
                            {"model", _deviceInfo.DeviceModelIdentifier},
                            {"android_release", _deviceInfo.AndroidVersion.VersionNumber},
                            {"android_version", _deviceInfo.AndroidVersion.APILevel}
                        }
                    },
                    {
                        "extra", new JObject
                        {
                            {"source_width", 0},
                            {"source_height", 0}
                        }
                    }
                };
                if (location != null)
                {
                    data.Add("location", location.GetJson());
                    data.Add("date_time_digitalized", DateTime.Now.ToString("yyyy:dd:MM+h:mm:ss"));
                }

                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("retry_context", retryContext);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                var mediaResponse =
                     JsonConvert.DeserializeObject<InstaMediaItemResponse>(json, new InstaMediaDataConverter());
                var converter = ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        private string GetRetryContext()
        {
            return new JObject
                {
                    {"num_step_auto_retry", 0},
                    {"num_reupload", 0},
                    {"num_step_manual_retry", 0}
                }.ToString(Formatting.None);
        }
    }
}
