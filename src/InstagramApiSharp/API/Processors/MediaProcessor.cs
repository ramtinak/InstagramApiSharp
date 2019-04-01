using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Media api functions.
    /// </summary>
    internal class MediaProcessor : IMediaProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public MediaProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger,
            UserAuthValidate userAuthValidate, InstaApi instaApi, HttpHelper httpHelper)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
            _userAuthValidate = userAuthValidate;
            _instaApi = instaApi;
            _httpHelper = httpHelper;
        }

        /// <summary>
        ///     Add an post to archive list (this will show the post only for you!)
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <returns>Return true if the media is archived</returns>
        public async Task<IResult<bool>> ArchiveMediaAsync(string mediaId)
        {
            return await LikeUnlikeArchiveUnArchiveMediaInternal(mediaId, UriCreator.GetArchiveMediaUri(mediaId));
        }

        /// <summary>
        ///     Delete a media (photo, video or album)
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <param name="mediaType">The type of the media</param>
        /// <returns>Return true if the media is deleted</returns>
        public async Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var deleteMediaUri = UriCreator.GetDeleteMediaUri(mediaId, mediaType);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_id", mediaId}
                };

                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Get, deleteMediaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                var deletedResponse = JsonConvert.DeserializeObject<DeleteResponse>(json);
                return Result.Success(deletedResponse.IsDeleted);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        /// <summary>
        ///     Edit the caption/location of the media (photo/video/album)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="caption">The new caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        /// <param name="userTags">User tags => Optional</param>
        /// <returns>Return true if everything is ok</returns>
        public async Task<IResult<InstaMedia>> EditMediaAsync(string mediaId, string caption, InstaLocationShort location = null, InstaUserTagUpload[] userTags = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var editMediaUri = UriCreator.GetEditMediaUri(mediaId);

                var currentMedia = await GetMediaByIdAsync(mediaId);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"caption_text", caption ?? string.Empty}
                };
                if (location != null)
                {
                    data.Add("location", location.GetJson());
                }

                var removeArr = new JArray();
                if (currentMedia.Succeeded)
                {
                    if (currentMedia.Value?.UserTags?.Count > 0)
                    {
                        foreach (var user in currentMedia.Value.UserTags)
                            removeArr.Add(user.User.Pk.ToString());
                    }
                }
                if (userTags?.Length > 0)
                {
                    var currentDelay = _instaApi.GetRequestDelay();
                    _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));

                    var tagArr = new JArray();

                    foreach (var tag in userTags)
                    {
                        try
                        {
                            bool tried = false;
                        TryLabel:
                            var u = await _instaApi.UserProcessor.GetUserAsync(tag.Username);
                            if (!u.Succeeded)
                            {
                                if (!tried)
                                {
                                    tried = true;
                                    goto TryLabel;
                                }
                            }
                            else
                            {
                                var position = new JArray(tag.X, tag.Y);
                                var singleTag = new JObject
                                {
                                    {"user_id", u.Value.Pk},
                                    {"position", position}
                                };
                                tagArr.Add(singleTag);
                            }

                        }
                        catch { }
                    }
          
                    _instaApi.SetRequestDelay(currentDelay);
                    var root = new JObject
                    {
                        {"in",  tagArr}
                    };
                    if (removeArr.Any())
                        root.Add("removed", removeArr);

                    data.Add("usertags", root.ToString(Formatting.None));
                }
                else
                {
                    if (removeArr.Any())
                    {
                        var root = new JObject
                        {
                            {"removed", removeArr}
                        };
                        data.Add("usertags", root.ToString(Formatting.None));
                    }
                }
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, editMediaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var mediaResponse = JsonConvert.DeserializeObject<InstaMediaItemResponse>(json,
                        new InstaMediaDataConverter());
                    var converter = ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse);
                    return Result.Success(converter.Convert());
                }
                var error = JsonConvert.DeserializeObject<BadStatusResponse>(json);
                return Result.Fail(error.Message, (InstaMedia)null);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        public async Task<IResult<InstaMediaList>> GetArchivedMediaAsync(PaginationParameters paginationParameters)
        {
            var mediaList = new InstaMediaList();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaMediaList Convert(InstaMediaListResponse instaMediaListResponse)
                {
                    return ConvertersFabric.Instance.GetMediaListConverter(instaMediaListResponse).Convert();
                }

                var archivedPostsResult = await GetArchivedMedia(paginationParameters?.NextMaxId);
                if (!archivedPostsResult.Succeeded)
                    return Result.Fail(archivedPostsResult.Info, mediaList);
                var archivedResponse = archivedPostsResult.Value;

                mediaList = Convert(archivedResponse);
                mediaList.NextMaxId = paginationParameters.NextMaxId = archivedResponse.NextMaxId;

                paginationParameters.PagesLoaded++;
                while (archivedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    paginationParameters.PagesLoaded++;
                    var nextMedia = await GetArchivedMedia(paginationParameters.NextMaxId);
                    if (!nextMedia.Succeeded)
                        return Result.Fail(nextMedia.Info, mediaList);
                    mediaList.NextMaxId = paginationParameters.NextMaxId = nextMedia.Value.NextMaxId;
                    archivedResponse.MoreAvailable = nextMedia.Value.MoreAvailable;
                    archivedResponse.ResultsCount += nextMedia.Value.ResultsCount;
                    mediaList.AddRange(Convert(nextMedia.Value));
                    paginationParameters.PagesLoaded++;
                }

                mediaList.Pages = paginationParameters.PagesLoaded;
                mediaList.PageSize = archivedResponse.ResultsCount;
                return Result.Success(mediaList);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, mediaList, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, mediaList);
            }
        }

        /// <summary>
        ///     Get blocked medias
        ///     <para>Note: returns media ids!</para>
        /// </summary>
        public async Task<IResult<InstaMediaIdList>> GetBlockedMediasAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var mediaIds = new InstaMediaIdList();
            try
            {
                var mediaUri = UriCreator.GetBlockedMediaUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, mediaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaIdList>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaMediaIdsResponse>(json);

                return Result.Success(obj.MediaIds);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, mediaIds, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, mediaIds);
            }
        }

        /// <summary>
        ///     Get multiple media by its multiple ids asynchronously
        /// </summary>
        /// <param name="mediaIds">Media ids</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetMediaByIdsAsync(params string[] mediaIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var mediaList = new InstaMediaList();
            try
            {
                if (mediaIds?.Length == 0)
                    throw new ArgumentNullException("At least one media id is required");

                var instaUri = UriCreator.GetMediaInfoByMultipleMediaIdsUri(mediaIds,_deviceInfo.DeviceGuid.ToString(), _user.CsrfToken);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaList>(response, json);

                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                mediaList = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse).Convert();    
                
                return Result.Success(mediaList);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, mediaList, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, mediaList);
            }
        }
        /// <summary>
        ///     Get media by its id asynchronously
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier>"/>)</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        public async Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var mediaUri = UriCreator.GetMediaUri(mediaId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, mediaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMedia>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                if (mediaResponse.Medias?.Count > 1)
                {
                    var errorMessage = $"Got wrong media count for request with media id={mediaId}";
                    _logger?.LogInfo(errorMessage);
                    return Result.Fail<InstaMedia>(errorMessage);
                }

                var converter =
                    ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse.Medias.FirstOrDefault());
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        /// <summary>
        ///     Get media ID from an url (got from "share link")
        /// </summary>
        /// <param name="uri">Uri to get media ID</param>
        /// <returns>Media ID</returns>
        public async Task<IResult<string>> GetMediaIdFromUrlAsync(Uri uri)
        {
            try
            {
                var collectionUri = UriCreator.GetMediaIdFromUrlUri(uri);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, collectionUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<string>(response, json);

                var data = JsonConvert.DeserializeObject<InstaOembedUrlResponse>(json);
                return Result.Success(data.MediaId);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(string), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<string>(exception);
            }
        }
        /// <summary>
        ///     Get users (short) who liked certain media. Normaly it return around 1000 last users.
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<InstaLikersList>> GetMediaLikersAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var likers = new InstaLikersList();
                var likersUri = UriCreator.GetMediaLikersUri(mediaId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, likersUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaLikersList>(response, json);
                var mediaLikersResponse = JsonConvert.DeserializeObject<InstaMediaLikersResponse>(json);
                likers.UsersCount = mediaLikersResponse.UsersCount;
                if (mediaLikersResponse.UsersCount < 1) return Result.Success(likers);
                likers.AddRange(
                    mediaLikersResponse.Users.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                return Result.Success(likers);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaLikersList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaLikersList>(exception);
            }
        }

        /// <summary>
        ///     Get share link from media Id
        /// </summary>
        /// <param name="mediaId">media ID</param>
        /// <returns>Share link as Uri</returns>
        public async Task<IResult<Uri>> GetShareLinkFromMediaIdAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var collectionUri = UriCreator.GetShareLinkFromMediaId(mediaId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, collectionUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<Uri>(response, json);

                var data = JsonConvert.DeserializeObject<InstaPermalinkResponse>(json);
                return Result.Success(new Uri(data.Permalink));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(Uri), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<Uri>(exception);
            }
        }

        /// <summary>
        ///     Like media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> LikeMediaAsync(string mediaId)
        {
            return await LikeUnlikeArchiveUnArchiveMediaInternal(mediaId, UriCreator.GetLikeMediaUri(mediaId));
        }

        /// <summary>
        ///     Report media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> ReportMediaAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetReportMediaUri(mediaId);
                var fields = new Dictionary<string, string>
                {
                    {"media_id", mediaId},
                    {"reason", "1"},
                    {"source_name", "photo_view_profile"},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, false);
            }
        }

        /// <summary>
        ///     Save media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> SaveMediaAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSaveMediaUri(mediaId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, false);
            }
        }

        /// <summary>
        ///     Remove an post from archive list (this will show the post for everyone!)
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <returns>Return true if the media is unarchived</returns>
        public async Task<IResult<bool>> UnArchiveMediaAsync(string mediaId)
        {
            return await LikeUnlikeArchiveUnArchiveMediaInternal(mediaId, UriCreator.GetUnArchiveMediaUri(mediaId));
        }

        /// <summary>
        ///     Remove like from media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> UnLikeMediaAsync(string mediaId)
        {
            return await LikeUnlikeArchiveUnArchiveMediaInternal(mediaId, UriCreator.GetUnLikeMediaUri(mediaId));
        }

        /// <summary>
        ///     Unsave media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> UnSaveMediaAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUnSaveMediaUri(mediaId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, false);
            }
        }

        /// <summary>
        ///     Upload album (videos and photos)
        /// </summary>
        /// <param name="images">Array of photos to upload</param>
        /// <param name="videos">Array of videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadAlbumAsync(InstaImageUpload[] images, InstaVideoUpload[] videos, string caption, InstaLocationShort location = null)
        {
            return await UploadAlbumAsync(null, images, videos, caption, location);
        }

        /// <summary>
        ///     Upload album (videos and photos)
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="images">Array of photos to upload</param>
        /// <param name="videos">Array of videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadAlbumAsync(Action<InstaUploaderProgress> progress, InstaImageUpload[] images, InstaVideoUpload[] videos, string caption, InstaLocationShort location = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var upProgress = new InstaUploaderProgress
            {
                Caption = caption ?? string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                upProgress.Name = "Album upload";
                progress?.Invoke(upProgress);
                var imagesUploadIds = new Dictionary<string, InstaImageUpload>();
                var index = 1;
                if (images?.Length > 0)
                {
                    foreach (var image in images)
                    {
                        if (image.UserTags?.Count > 0)
                        {
                            var currentDelay = _instaApi.GetRequestDelay();
                            _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
                            foreach (var t in image.UserTags)
                            {
                                try
                                {
                                    bool tried = false;
                                TryLabel:
                                    var u = await _instaApi.UserProcessor.GetUserAsync(t.Username);
                                    if (!u.Succeeded)
                                    {
                                        if (!tried)
                                        {
                                            tried = true;
                                            goto TryLabel;
                                        }
                                    }
                                    else
                                        t.Pk = u.Value.Pk;
                                }
                                catch { }
                            }
                            _instaApi.SetRequestDelay(currentDelay);
                        }
                    }
                    foreach (var image in images)
                    {
                        upProgress.Name = $"[Album] Photo uploading {index}/{images.Length}";
                        upProgress.UploadState = InstaUploadState.Uploading;
                        progress?.Invoke(upProgress);
                        upProgress.UploadState = InstaUploadState.Uploading;
                        progress?.Invoke(upProgress);
                        var uploadId = await UploadSinglePhoto(progress, image, upProgress);
                        if(uploadId.Succeeded)
                        {
                            upProgress.UploadState = InstaUploadState.Uploaded;
                            progress?.Invoke(upProgress);
                            imagesUploadIds.Add(uploadId.Value, image);
                        }
                        else
                        {
                            upProgress.UploadState = InstaUploadState.Error;
                            progress?.Invoke(upProgress);
                            return Result.Fail<InstaMedia>(uploadId.Info.Message);
                        }
                    }
                }

                var videosDic = new Dictionary<string, InstaVideoUpload>();
                var vidIndex = 1;
                if (videos?.Length > 0)
                {
                    foreach (var video in videos)
                    {
                        foreach (var t in video.UserTags)
                        {
                            var currentDelay = _instaApi.GetRequestDelay();
                            _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
                            if (t.Pk <= 0)
                            {
                                try
                                {
                                    bool tried = false;
                                TryLabel:
                                    var u = await _instaApi.UserProcessor.GetUserAsync(t.Username);
                                    if (!u.Succeeded)
                                    {
                                        if (!tried)
                                        {
                                            tried = true;
                                            goto TryLabel;
                                        }
                                    }
                                    else
                                        t.Pk = u.Value.Pk;
                                }
                                catch { }
                            }
                            _instaApi.SetRequestDelay(currentDelay);
                        }
                    }
                    
                    foreach (var video in videos)
                    {
                        upProgress.Name = $"[Album] Video uploading {vidIndex}/{videos.Length}";
                        upProgress.UploadState = InstaUploadState.Uploading;
                        progress?.Invoke(upProgress);
                        var uploadId = await UploadSingleVideo(progress, video, upProgress);
                        var thumb = await UploadSinglePhoto(progress, video.VideoThumbnail.ConvertToImageUpload(), upProgress, uploadId.Value);
                        videosDic.Add(uploadId.Value, video);

                        upProgress.UploadState = InstaUploadState.Uploaded;
                        progress?.Invoke(upProgress);
                        vidIndex++;
                    }
                }
                var config = await ConfigureAlbumAsync(progress, upProgress, imagesUploadIds, videosDic, caption, location);
                return config;
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        /// <summary>
        ///     Upload album (videos and photos)
        /// </summary>
        /// <param name="album">Array of photos or videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadAlbumAsync(InstaAlbumUpload[] album, string caption, InstaLocationShort location = null)
        {
            return await UploadAlbumAsync(null, album, caption, location);
        }

        /// <summary>
        ///     Upload album (videos and photos) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="album">Array of photos or videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadAlbumAsync(Action<InstaUploaderProgress> progress, InstaAlbumUpload[] album, string caption, InstaLocationShort location = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var upProgress = new InstaUploaderProgress
            {
                Caption = caption ?? string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                upProgress.Name = "Album upload";
                progress?.Invoke(upProgress);
                var uploadIds = new Dictionary<string, InstaAlbumUpload>();
                var index = 1;

                foreach (var al in album)
                {
                    if (al.IsImage)
                    {
                        var image = al.ImageToUpload;
                        if (image.UserTags?.Count > 0)
                        {
                            var currentDelay = _instaApi.GetRequestDelay();
                            _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
                            foreach (var t in image.UserTags)
                            {
                                if (t.Pk <= 0)
                                {
                                    try
                                    {
                                        bool tried = false;
                                    TryLabel:
                                        var u = await _instaApi.UserProcessor.GetUserAsync(t.Username);
                                        if (!u.Succeeded)
                                        {
                                            if (!tried)
                                            {
                                                tried = true;
                                                goto TryLabel;
                                            }
                                        }
                                        else
                                            t.Pk = u.Value.Pk;
                                    }
                                    catch { }
                                }
                            }
                            _instaApi.SetRequestDelay(currentDelay);
                        }
                    }
                    else if(al.IsVideo)
                    {
                        var video = al.VideoToUpload;
                        if (video.UserTags?.Count > 0)
                        {
                            var currentDelay = _instaApi.GetRequestDelay();
                            _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
                            foreach (var t in video.UserTags)
                            {
                                if (t.Pk <= 0)
                                {
                                    try
                                    {
                                        bool tried = false;
                                    TryLabel:
                                        var u = await _instaApi.UserProcessor.GetUserAsync(t.Username);
                                        if (!u.Succeeded)
                                        {
                                            if (!tried)
                                            {
                                                tried = true;
                                                goto TryLabel;
                                            }
                                        }
                                        else
                                            t.Pk = u.Value.Pk;
                                    }
                                    catch { }
                                }
                            }
                            _instaApi.SetRequestDelay(currentDelay);
                        }
                    }
                }
                foreach (var al in album)
                {
                    if (al.IsImage)
                    {
                        upProgress.Name = $"[Album] uploading {index}/{album.Length}";
                        upProgress.UploadState = InstaUploadState.Uploading;
                        progress?.Invoke(upProgress);
                        var image = await UploadSinglePhoto(progress, al.ImageToUpload, upProgress);
                        if (image.Succeeded)
                            uploadIds.Add(image.Value, al);
                    }
                    else if (al.IsVideo)
                    {
                        upProgress.Name = $"[Album] uploading {index}/{album.Length}";
                        upProgress.UploadState = InstaUploadState.Uploading;
                        progress?.Invoke(upProgress);
                        var video = await UploadSingleVideo(progress, al.VideoToUpload, upProgress);
                        if (video.Succeeded)
                        {
                            var image = await UploadSinglePhoto(progress, al.VideoToUpload.VideoThumbnail.ConvertToImageUpload(), upProgress, video.Value);
                            uploadIds.Add(video.Value, al);
                        }
                    }
                    index++;
                }
                var config = await ConfigureAlbumAsync(progress, upProgress, uploadIds, caption, location);
                return config;
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }
        
        private async Task<IResult<string>> UploadSinglePhoto(Action<InstaUploaderProgress> progress, InstaImageUpload image, InstaUploaderProgress upProgress, string uploadId = null, bool album = true)
        {
            if (string.IsNullOrEmpty(uploadId))
                uploadId = ApiRequestMessage.GenerateUploadId();
            var photoHashCode = Path.GetFileName(image.Uri ?? $"C:\\{13.GenerateRandomString()}.jpg").GetHashCode();
            var photoEntityName = $"{uploadId}_0_{photoHashCode}";
            var photoUri = UriCreator.GetStoryUploadPhotoUri(uploadId, photoHashCode);
            var photoUploadParamsObj = new JObject
            {
                {"upload_id", uploadId},
                {"media_type", "1"},
                {"retry_context", HelperProcessor.GetRetryContext()},
                {"image_compression", "{\"lib_name\":\"moz\",\"lib_version\":\"3.1.m\",\"quality\":\"95\"}"},
                {"xsharing_user_ids", "[]"},
            };
            if (album)
                photoUploadParamsObj.Add("is_sidecar", "1");
            upProgress.UploadState = InstaUploadState.UploadingThumbnail;
            progress?.Invoke(upProgress);
            var photoUploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
            var imageBytes = image.ImageBytes ?? File.ReadAllBytes(image.Uri);
            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
            imageContent.Headers.Add("Content-Type", "application/octet-stream");
            var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, photoUri, _deviceInfo);
            request.Content = imageContent;
            request.Headers.Add("X-Entity-Type", "image/jpeg");
            request.Headers.Add("Offset", "0");
            request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
            request.Headers.Add("X-Entity-Name", photoEntityName);
            request.Headers.Add("X-Entity-Length", imageBytes.Length.ToString());
            request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", Guid.NewGuid().ToString());
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //upProgress = progressContent?.UploaderProgress;
                upProgress.UploadState = InstaUploadState.Uploaded;
                progress?.Invoke(upProgress);
                return Result.Success(uploadId);
            }
            else
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.Fail<string>("NO UPLOAD ID");
            }
        }

        private async Task<IResult<string>> UploadSingleVideo(Action<InstaUploaderProgress> progress, InstaVideoUpload video, InstaUploaderProgress upProgress, bool album = true)
        {
            var uploadId = ApiRequestMessage.GenerateRandomUploadId();
            var videoHashCode = Path.GetFileName(video.Video.Uri ?? $"C:\\{13.GenerateRandomString()}.mp4").GetHashCode();
            var waterfallId = Guid.NewGuid().ToString();
            var videoEntityName = $"{uploadId}_0_{videoHashCode}";
            var videoUri = UriCreator.GetStoryUploadVideoUri(uploadId, videoHashCode);
            var retryContext = HelperProcessor.GetRetryContext();
            HttpRequestMessage request = null;
            HttpResponseMessage response = null;
            string videoUploadParams = null;
            string json = null;

            var videoUploadParamsObj = new JObject
            {
                {"upload_media_height", "0"},
                {"upload_media_width", "0"},
                {"upload_media_duration_ms", "0"},
                {"upload_id", uploadId},
                {"retry_context", retryContext},
                {"media_type", "2"},
                {"xsharing_user_ids", "[]"}
            };
            if (album)
                videoUploadParamsObj.Add("is_sidecar", "1");

            videoUploadParams = JsonConvert.SerializeObject(videoUploadParamsObj);
            request = _httpHelper.GetDefaultRequest(HttpMethod.Get, videoUri, _deviceInfo);
            request.Headers.Add("X_FB_VIDEO_WATERFALL_ID", waterfallId);
            request.Headers.Add("X-Instagram-Rupload-Params", videoUploadParams);
            response = await _httpRequestProcessor.SendAsync(request);
            json = await response.Content.ReadAsStringAsync();


            if (response.StatusCode != HttpStatusCode.OK)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<string>(response, json);
            }
            
            var videoBytes = video.Video.VideoBytes ?? File.ReadAllBytes(video.Video.Uri);

            var videoContent = new ByteArrayContent(videoBytes);
            //var progressContent = new ProgressableStreamContent(videoContent, 4096, progress)
            //{
            //    UploaderProgress = upProgress
            //};
            request = _httpHelper.GetDefaultRequest(HttpMethod.Post, videoUri, _deviceInfo);
            request.Content = videoContent;
            upProgress.UploadState = InstaUploadState.Uploading;
            progress?.Invoke(upProgress);
            var vidExt = Path.GetExtension(video.Video.Uri ?? $"C:\\{13.GenerateRandomString()}.mp4").Replace(".", "").ToLower();
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
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<string>(response, json);
            }
            return Result.Success(uploadId);
        }

        private async Task<IResult<InstaMedia>> ConfigureAlbumAsync(Action<InstaUploaderProgress> progress, InstaUploaderProgress upProgress, Dictionary<string, InstaAlbumUpload> album, string caption, InstaLocationShort location)
        {
            try
            {
                upProgress.Name = "Album upload";
                upProgress.UploadState = InstaUploadState.Configuring;
                progress?.Invoke(upProgress);
                var instaUri = UriCreator.GetMediaAlbumConfigureUri();
                var clientSidecarId = ApiRequestMessage.GenerateUploadId();
                var childrenArray = new JArray();
                
                foreach(var al in album)
                {
                    if (al.Value.IsImage)
                        childrenArray.Add(GetImageConfigure(al.Key, al.Value.ImageToUpload));
                    else if (al.Value.IsVideo)
                        childrenArray.Add(GetVideoConfigure(al.Key, al.Value.VideoToUpload));
                }

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"caption", caption},
                    {"client_sidecar_id", clientSidecarId},
                    {"upload_id", clientSidecarId},
                    {"timezone_offset", InstaApiConstants.TIMEZONE_OFFSET.ToString()},
                    {"source_type", "4"},
                    {"device_id", _deviceInfo.DeviceId},
                    {"creation_logger_session_id", Guid.NewGuid().ToString()},
                    {
                        "device", new JObject
                        {
                            {"manufacturer", _deviceInfo.HardwareManufacturer},
                            {"model", _deviceInfo.DeviceModelIdentifier},
                            {"android_release", _deviceInfo.AndroidVer.VersionNumber},
                            {"android_version", _deviceInfo.AndroidVer.APILevel}
                        }
                    },
                    {"children_metadata", childrenArray},
                };
                if (location != null)
                {
                    data.Add("location", location.GetJson());
                    data.Add("date_time_digitalized", DateTime.Now.ToString("yyyy:dd:MM+h:mm:ss"));
                }
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaMedia>(response, json);
                }
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaAlbumResponse>(json);
                var converter = ConvertersFabric.Instance.GetSingleMediaFromAlbumConverter(mediaResponse);
                var obj = converter.Convert();
                if (obj.Caption == null && !string.IsNullOrEmpty(caption))
                {
                    var editedMedia = await _instaApi.MediaProcessor.EditMediaAsync(obj.InstaIdentifier, caption, location);
                    if (editedMedia.Succeeded)
                    {
                        upProgress.UploadState = InstaUploadState.Configured;
                        progress?.Invoke(upProgress);
                        upProgress.UploadState = InstaUploadState.Completed;
                        progress?.Invoke(upProgress);
                        return Result.Success(editedMedia.Value);
                    }
                }
                upProgress.UploadState = InstaUploadState.Configured;
                progress?.Invoke(upProgress);
                upProgress.UploadState = InstaUploadState.Completed;
                progress?.Invoke(upProgress);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }




        /// <summary>
        ///     Upload photo [Supports user tags]
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImageUpload image, string caption, InstaLocationShort location = null)
        {
            return await UploadPhotoAsync(null, image, caption, location);
        }

        /// <summary>
        ///     Upload photo with progress [Supports user tags]
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadPhotoAsync(Action<InstaUploaderProgress> progress, InstaImageUpload image, string caption,
            InstaLocationShort location = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendMediaPhotoAsync(progress, image, caption, location);
        }

        /// <summary>
        ///     Upload video [Supports user tags]
        /// </summary>
        /// <param name="video">Video and thumbnail to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideoUpload video, string caption, InstaLocationShort location = null)
        {
            return await UploadVideoAsync(null, video, caption, location);
        }
        /// <summary>
        ///     Upload video with progress [Supports user tags]
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video and thumbnail to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        public async Task<IResult<InstaMedia>> UploadVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, string caption, InstaLocationShort location = null)
        {
            var upProgress = new InstaUploaderProgress
            {
                Caption = caption ?? string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                if (video?.UserTags?.Count > 0)
                {
                    var currentDelay = _instaApi.GetRequestDelay();
                    _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
                    foreach (var t in video.UserTags)
                    {
                        if (t.Pk <= 0)
                        {
                            try
                            {
                                bool tried = false;
                            TryLabel:
                                var u = await _instaApi.UserProcessor.GetUserAsync(t.Username);
                                if (!u.Succeeded)
                                {
                                    if (!tried)
                                    {
                                        tried = true;
                                        goto TryLabel;
                                    }
                                }
                                else
                                    t.Pk = u.Value.Pk;
                            }
                            catch { }
                        }
                    }
                    _instaApi.SetRequestDelay(currentDelay);
                }
                upProgress.UploadState = InstaUploadState.Uploading;
                progress?.Invoke(upProgress);
                var uploadVideo = await UploadSingleVideo(progress, video, upProgress, false);

                if (!uploadVideo.Succeeded)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.Fail<InstaMedia>(uploadVideo.Info.Message);
                }
                upProgress.UploadState = InstaUploadState.Uploaded;
                progress?.Invoke(upProgress);

                upProgress.UploadState = InstaUploadState.UploadingThumbnail;
                progress?.Invoke(upProgress);
                
                var uploadPhoto = await UploadSinglePhoto(progress, video.VideoThumbnail.ConvertToImageUpload(), upProgress, uploadVideo.Value, false);

                if (uploadPhoto.Succeeded)
                {
                    //upProgress = progressContent?.UploaderProgress;
                    upProgress.UploadState = InstaUploadState.ThumbnailUploaded;
                    progress?.Invoke(upProgress);
                    return await ConfigureVideoAsync(progress, upProgress, video, uploadVideo.Value, caption, location);
                }
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.Fail<InstaMedia>(uploadPhoto.Value);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        private async Task<IResult<InstaMedia>> ConfigureAlbumAsync(Action<InstaUploaderProgress> progress, InstaUploaderProgress upProgress, Dictionary<string, InstaImageUpload> imagesUploadIds, Dictionary<string, InstaVideoUpload> videos, string caption, InstaLocationShort location)
        {
            try
            {
                upProgress.Name = "Album upload";
                upProgress.UploadState = InstaUploadState.Configuring;
                progress?.Invoke(upProgress);
                var instaUri = UriCreator.GetMediaAlbumConfigureUri();
                var clientSidecarId = ApiRequestMessage.GenerateUploadId();
                var childrenArray = new JArray();
                if (imagesUploadIds != null && imagesUploadIds.Any())
                {
                    foreach (var img in imagesUploadIds)
                    {
                        childrenArray.Add(GetImageConfigure(img.Key, img.Value));
                    }
                }
                if (videos != null && videos.Any())
                {
                    foreach (var id in videos)
                    {
                        childrenArray.Add(GetVideoConfigure(id.Key, id.Value));
                    }
                }
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"caption", caption},
                    {"client_sidecar_id", clientSidecarId},
                    {"upload_id", clientSidecarId},
                    {
                        "device", new JObject
                        {
                            {"manufacturer", _deviceInfo.HardwareManufacturer},
                            {"model", _deviceInfo.DeviceModelIdentifier},
                            {"android_release", _deviceInfo.AndroidVer.VersionNumber},
                            {"android_version", _deviceInfo.AndroidVer.APILevel}
                        }
                    },
                    {"children_metadata", childrenArray},
                };
                if (location != null)
                {
                    data.Add("location", location.GetJson());
                    data.Add("date_time_digitalized", DateTime.Now.ToString("yyyy:dd:MM+h:mm:ss"));
                }
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaMedia>(response, json);
                }
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaAlbumResponse>(json);
                var converter = ConvertersFabric.Instance.GetSingleMediaFromAlbumConverter(mediaResponse);
                var obj = converter.Convert();
                if (obj.Caption == null && !string.IsNullOrEmpty(caption))
                {
                    var editedMedia = await _instaApi.MediaProcessor.EditMediaAsync(obj.InstaIdentifier, caption, location);
                    if (editedMedia.Succeeded)
                    {
                        upProgress.UploadState = InstaUploadState.Configured;
                        progress?.Invoke(upProgress);
                        upProgress.UploadState = InstaUploadState.Completed;
                        progress?.Invoke(upProgress);
                        return Result.Success(editedMedia.Value);
                    }
                }
                upProgress.UploadState = InstaUploadState.Configured;
                progress?.Invoke(upProgress);
                upProgress.UploadState = InstaUploadState.Completed;
                progress?.Invoke(upProgress);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        private async Task<IResult<InstaMedia>> ConfigureVideoAsync(Action<InstaUploaderProgress> progress, InstaUploaderProgress upProgress, InstaVideoUpload video, string uploadId, string caption, InstaLocationShort location)
        {
            try
            {
                upProgress.UploadState = InstaUploadState.Configuring;
                progress?.Invoke(upProgress);
                var instaUri = UriCreator.GetMediaConfigureUri(true);
                var data = new JObject
                {
                    {"caption", caption ?? string.Empty},
                    {"upload_id", uploadId},
                    {"source_type", "4"},
                    {"camera_position", "unknown"},
                    {"creation_logger_session_id", Guid.NewGuid().ToString()},
                    {"timezone_offset", InstaApiConstants.TIMEZONE_OFFSET.ToString()},
                    {"date_time_original", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fffZ")},
                    {
                        "extra", new JObject
                        {
                            {"source_width", 0},
                            {"source_height", 0}
                        }
                    },
                    {
                        "clips", new JArray{
                            new JObject
                            {
                                {"length", 0},
                                {"creation_date", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fff")},
                                {"source_type", "3"},
                                {"camera_position", "back"}
                            }
                        }
                    },
                    {"poster_frame_index", 0},
                    {"audio_muted", false},
                    {"filter_type", "0"},
                    {"video_result", ""},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.UserName}
                };
                if (location != null)
                {
                    data.Add("location", location.GetJson());
                    data.Add("date_time_digitalized", DateTime.Now.ToString("yyyy:dd:MM+h:mm:ss"));
                }
                if (video.UserTags?.Count > 0)
                {
                    var tagArr = new JArray();
                    foreach (var tag in video.UserTags)
                    {
                        if (tag.Pk != -1)
                        {
                            var position = new JArray(0.0, 0.0);
                            var singleTag = new JObject
                            {
                                {"user_id", tag.Pk},
                                {"position", position}
                            };
                            tagArr.Add(singleTag);
                        }
                    }

                    var root = new JObject
                    {
                        {"in",  tagArr}
                    };
                    data.Add("usertags", root.ToString(Formatting.None));
                }
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, UriCreator.GetMediaUploadFinishUri(), _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaMedia>(response, json);
                }
                upProgress.UploadState = InstaUploadState.Configured;
                progress?.Invoke(upProgress);

                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaItemResponse>(json,
                                    new InstaMediaDataConverter());
                var converter = ConvertersFabric.Instance.GetSingleMediaConverter(mediaResponse);
                var obj = converter.Convert();
                if (obj.Caption == null && !string.IsNullOrEmpty(caption))
                {
                    var editedMedia = await _instaApi.MediaProcessor.EditMediaAsync(obj.InstaIdentifier, caption, location);
                    if (editedMedia.Succeeded)
                        return Result.Success(editedMedia.Value);
                }
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaMedia>(exception);
            }
        }

        private async Task<IResult<bool>> LikeUnlikeArchiveUnArchiveMediaInternal(string mediaId, Uri instaUri)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_id", mediaId},
                    {"radio_type", "wifi-none"}
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                return Result.Fail<bool>(exception);
            }
        }

        private async Task<IResult<bool>> UploadVideoThumbnailAsync(Action<InstaUploaderProgress> progress, InstaUploaderProgress upProgress, InstaImage image, string uploadId)
        {
            try
            {
                var instaUri = UriCreator.GetUploadPhotoUri();
                upProgress.UploadState = InstaUploadState.UploadingThumbnail;
                progress?.Invoke(upProgress);
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
                byte[] fileBytes;
                if (image.ImageBytes == null)
                    fileBytes = File.ReadAllBytes(image.Uri);
                else
                    fileBytes = image.ImageBytes;

                var imageContent = new ByteArrayContent(fileBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent, "photo", $"pending_media_{uploadId}.jpg");
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var imgResp = JsonConvert.DeserializeObject<ImageThumbnailResponse>(json);
                if (imgResp.Status.ToLower() == "ok")
                {
                    upProgress.UploadState = InstaUploadState.ThumbnailUploaded;
                    progress?.Invoke(upProgress);
                    return Result.Success(true);
                }

                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.Fail<bool>("Could not upload thumbnail");
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        private async Task<IResult<InstaMediaListResponse>> GetArchivedMedia(string nextMaxId)
        {
            var mediaList = new InstaMediaList();
            try
            {
                var instaUri = UriCreator.GetArchivedMediaFeedsListUri(nextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaListResponse>(response, json);
                var archivedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json);
                return Result.Success(archivedResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMediaListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail<InstaMediaListResponse>(ex);
            }
        }

        JObject GetImageConfigure(string uploadId, InstaImageUpload image)
        {
            var imgData = new JObject
            {
                {"timezone_offset", InstaApiConstants.TIMEZONE_OFFSET.ToString()},
                {"source_type", "4"},
                {"upload_id", uploadId},
                {"caption", ""},
                {
                    "extra", JsonConvert.SerializeObject(new JObject
                    {
                        {"source_width", 0},
                        {"source_height", 0}
                    })
                },
                {
                    "device", JsonConvert.SerializeObject(new JObject{
                        {"manufacturer", _deviceInfo.HardwareManufacturer},
                        {"model", _deviceInfo.DeviceModelIdentifier},
                        {"android_release", _deviceInfo.AndroidVer.VersionNumber},
                        {"android_version", _deviceInfo.AndroidVer.APILevel}
                    })
                }
            };
            if (image.UserTags?.Count > 0)
            {
                var tagArr = new JArray();
                foreach (var tag in image.UserTags)
                {
                    if (tag.Pk != -1)
                    {
                        var position = new JArray(tag.X, tag.Y);
                        var singleTag = new JObject
                                    {
                                        {"user_id", tag.Pk},
                                        {"position", position}
                                    };
                        tagArr.Add(singleTag);
                    }
                }

                var root = new JObject
                {
                    {"in",  tagArr}
                };
                imgData.Add("usertags", root.ToString(Formatting.None));
            }
            return imgData;
        }

        JObject GetVideoConfigure(string uploadId, InstaVideoUpload video)
        {
            var vidData = new JObject
            {
                {"timezone_offset", InstaApiConstants.TIMEZONE_OFFSET.ToString()},
                {"caption", ""},
                {"upload_id", uploadId},
                {"date_time_original", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fffZ")},
                {"source_type", "4"},
                {
                    "extra", JsonConvert.SerializeObject(new JObject
                    {
                        {"source_width", 0},
                        {"source_height", 0}
                    })
                },
                {
                    "clips", JsonConvert.SerializeObject(new JArray{
                        new JObject
                        {
                            {"length", video.Video.Length},
                            {"source_type", "4"},
                        }
                    })
                },
                {
                    "device", JsonConvert.SerializeObject(new JObject{
                        {"manufacturer", _deviceInfo.HardwareManufacturer},
                        {"model", _deviceInfo.DeviceModelIdentifier},
                        {"android_release", _deviceInfo.AndroidVer.VersionNumber},
                        {"android_version", _deviceInfo.AndroidVer.APILevel}
                    })
                },
                {"length", video.Video.Length.ToString()},
                {"poster_frame_index", "0"},
                {"audio_muted", "false"},
                {"filter_type", "0"},
                {"video_result", ""},
            };
            if (video.UserTags?.Count > 0)
            {
                var tagArr = new JArray();
                foreach (var tag in video.UserTags)
                {
                    if (tag.Pk != -1)
                    {
                        var position = new JArray(0.0, 0.0);
                        var singleTag = new JObject
                        {
                            {"user_id", tag.Pk},
                            {"position", position}
                        };
                        tagArr.Add(singleTag);
                    }
                }

                var root = new JObject
                {
                    {"in",  tagArr}
                };
                vidData.Add("usertags", root.ToString(Formatting.None));
            }
            return vidData;
        }
    }
}