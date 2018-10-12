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
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using InstagramApiSharp.Helpers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.API.Processors
{
    internal class LiveProcessor : ILiveProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public LiveProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        /// Add an broadcast to post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastAddToPostLiveResponse>> AddToPostLiveAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastAddToPostLiveUri(broadcastId);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastAddToPostLiveResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastAddToPostLiveResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastAddToPostLiveResponse>(exception);
            }
        }

        /// <summary>
        /// Post a new comment to broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="commentText">Comment text</param>
        /// <returns></returns>
        public async Task<IResult<InstaComment>> CommentAsync(string broadcastId, string commentText)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastPostCommentUri(broadcastId);
                var breadcrumb = CryptoHelper.GetCommentBreadCrumbEncoded(commentText);
                var data = new JObject
                {
                    {"user_breadcrumb", commentText},
                    {"idempotence_token",  Guid.NewGuid().ToString()},
                    {"comment_text", commentText},
                    {"live_or_vod", "1"},
                    {"offset_to_video_start"," 0"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaComment>(response, json);
                var commentResponse = JsonConvert.DeserializeObject<InstaCommentResponse>(json,
                     new InstaCommentDataConverter());
                var converter = ConvertersFabric.Instance.GetCommentConverter(commentResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaComment>(exception);
            }
        }

        // create, start, end broadcast
        /// <summary>
        /// Create live broadcast. After create an live broadcast you must call StartAsync.
        /// </summary>
        /// <param name="previewWidth">Preview width</param>
        /// <param name="previewHeight">Preview height</param>
        /// <param name="broadcastMessage">Broadcast start message</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastCreateResponse>> CreateAsync(int previewWidth = 720, int previewHeight = 1184, string broadcastMessage = "")
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastCreateUri();
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"preview_height",  previewHeight},
                    {"preview_width",  previewWidth},
                    {"broadcast_message",  broadcastMessage},
                    {"broadcast_type",  "RTMP"},
                    {"internal_only",  0}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastCreateResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastCreateResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastCreateResponse>(exception);
            }
        }

        /// <summary>
        /// Delete an broadcast from post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<bool>> DeletePostLiveAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastDeletePostLiveUri(broadcastId);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        /// <summary>
        /// Disable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastCommentEnableDisableResponse>> DisableCommentsAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastDisableCommenstUri(broadcastId);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastCommentEnableDisableResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastCommentEnableDisableResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastCommentEnableDisableResponse>(exception);
            }
        }

        /// <summary>
        /// Enable broadcast comments.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastCommentEnableDisableResponse>> EnableCommentsAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastEnableCommenstUri(broadcastId);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastCommentEnableDisableResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastCommentEnableDisableResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastCommentEnableDisableResponse>(exception);
            }
        }

        /// <summary>
        /// End live broadcast
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="endAfterCopyrightWarning">Copyright warning</param>
        /// <returns></returns>
        public async Task<IResult<bool>> EndAsync(string broadcastId, bool endAfterCopyrightWarning = false)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastEndUri(broadcastId);
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.UserName},
                    {"end_after_copyright_warning", endAfterCopyrightWarning.ToString()},
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }

        /// <summary>
        /// Get broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="lastCommentTs">Last comment time stamp</param>
        /// <param name="commentsRequested">Comments requested count</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastCommentResponse>> GetCommentsAsync(string broadcastId, int lastCommentTs = 0, int commentsRequested = 4)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastCommentUri(broadcastId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastCommentResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastCommentResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastCommentResponse>(exception);
            }
        }

        /// <summary>
        /// Get discover top live.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<InstaDiscoverTopLiveResponse>> GetDiscoverTopLiveAsync()
        {
            try
            {
                var instaUri = UriCreator.GetDiscoverTopLiveUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDiscoverTopLiveResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDiscoverTopLiveResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDiscoverTopLiveResponse>(exception);
            }
        }

        /// <summary>
        /// Get final viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastFinalViewerListResponse>> GetFinalViewerListAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetLiveFinalViewerListUri(broadcastId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastFinalViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastFinalViewerListResponse>(json);
                return Result.Success(obj);

            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastFinalViewerListResponse>(exception);
            }
        }

        /// <summary>
        /// Get heart beat and viewer count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastLiveHeartBeatViewerCountResponse>> GetHeartBeatAndViewerCountAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetLiveHeartbeatAndViewerCountUri(broadcastId);
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent("offset_to_video_start"),"30"}
                };
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastLiveHeartBeatViewerCountResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastLiveHeartBeatViewerCountResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastLiveHeartBeatViewerCountResponse>(exception);
            }
        }
        /// <summary>
        /// Get broadcast information.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastInfoResponse>> GetInfoAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastInfoUri(broadcastId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastInfoResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastInfoResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastInfoResponse>(exception);
            }
        }

        /// <summary>
        ///     Get join requests to current live broadcast
        /// </summary>
        /// <param name="broadcastId">Broadcast</param>
        public async Task<IResult<InstaBroadcastFinalViewerListResponse>> GetJoinRequestsAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastJoinRequestsUri(broadcastId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastFinalViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastFinalViewerListResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastFinalViewerListResponse>(exception);
            }
        }

        /// <summary>
        /// Get broadcast like count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeTs">Like time stamp</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastLikeResponse>> GetLikeCountAsync(string broadcastId, int likeTs = 0)
        {
            try
            {
                var instaUri = UriCreator.GetLiveLikeCountUri(broadcastId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastLikeResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastLikeResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastLikeResponse>(exception);
            }
        }

        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<object>> GetPostLiveCommentsAsync(string broadcastId, int startingOffset = 0, string encodingTag = "instagram_dash_remuxed")
        {
            try
            {
                // kamel nist
                var instaUri = UriCreator.GetBroadcastPostLiveCommentUri(broadcastId, startingOffset, encodingTag);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadCastNotifyFriendsResponse>(json);
                return Result.Success(json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<string>(exception);
            }
        }

        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<object>> GetPostLiveLikesAsync(string broadcastId, int startingOffset = 0, string encodingTag = "instagram_dash_remuxed")
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastPostLiveLikesUri(broadcastId, startingOffset, encodingTag);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadCastNotifyFriendsResponse>(json);
                return Result.Success(json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<string>(exception);
            }
        }

        /// <summary>
        /// Get post live viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="maxId">Max id</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastViewerListResponse>> GetPostLiveViewerListAsync(string broadcastId, int? maxId = null)
        {
            try
            {
                var instaUri = UriCreator.GetPostLiveViewersListUri(broadcastId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastViewerListResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastViewerListResponse>(exception);
            }
        }

        /// <summary>
        /// Get suggested broadcasts
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastSuggestedResponse>> GetSuggestedBroadcastsAsync()
        {
            try
            {
                var instaUri = UriCreator.GetSuggestedBroadcastsUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastSuggestedResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastSuggestedResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastSuggestedResponse>(exception);
            }
        }
        /// <summary>
        /// Get top live status.
        /// </summary>
        /// <param name="broadcastIds">Broadcast ids</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastTopLiveStatusResponse>> GetTopLiveStatusAsync(params string[] broadcastIds)
        {
            if(broadcastIds == null)
                return Result.Fail<InstaBroadcastTopLiveStatusResponse>("broadcast ids must be set");
            try
            {
                var instaUri = UriCreator.GetDiscoverTopLiveStatusUri();
                var data = new JObject
                {
                    {"broadcast_ids", new JArray(broadcastIds)},
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastTopLiveStatusResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastTopLiveStatusResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastTopLiveStatusResponse>(exception);
            }
        }
        /// <summary>
        /// Get broadcast viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastViewerListResponse>> GetViewerListAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastViewerListUri(broadcastId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastViewerListResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastViewerListResponse>(exception);
            }
        }
        /// <summary>
        /// Like broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeCount">Like count (from 1 to 6)</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastLikeResponse>> LikeAsync(string broadcastId, int likeCount = 1)
        {
            try
            {
                var instaUri = UriCreator.GetLikeLiveUri(broadcastId);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"user_like_count", likeCount}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastLikeResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastLikeResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastLikeResponse>(exception);
            }
        }

        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<object>> NotifyToFriendsAsync()
        {
            try
            {
                var instaUri = UriCreator.GetLiveNotifyToFriendsUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadCastNotifyFriendsResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadCastNotifyFriendsResponse>(exception);
            }
        }

        /// <summary>
        /// Pin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastPinUnpinResponse>> PinCommentAsync(string broadcastId, string commentId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastPinCommentUri(broadcastId);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"comment_id", commentId},
                    {"offset_to_video_start", 0}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastPinUnpinResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastPinUnpinResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastPinUnpinResponse>(exception);
            }
        }
        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<object>> SeenBroadcastAsync(string broadcastId, string pk)
        {
            try
            {
                var instaUri = new Uri(InstaApiConstants.BASE_INSTAGRAM_API_URL + $"media/seen/?reel=1&live_vod=0");
                Debug.WriteLine(instaUri.ToString());

                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"live_vods_skipped",  new JObject()},
                    {"nuxes_skipped",  new JObject()},
                    {"nuxes",  new JObject()},
                    {"reels",  new JObject{ { broadcastId, new JArray(pk) } } },
                    {"live_vods",  new JObject()},
                    {"reel_media_skipped",  new JObject()},

                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastViewerListResponse>(response, json);
                return Result.Success(json);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastLiveHeartBeatViewerCountResponse>(exception);
            }
        }

        /// <summary>
        /// Start live broadcast. NOTE: YOU MUST CREATE AN BROADCAST FIRST(CreateAsync) AND THEN CALL THIS METHOD. 
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="sendNotifications">Send notifications</param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastStartResponse>> StartAsync(string broadcastId, bool sendNotifications)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastStartUri(broadcastId);
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"should_send_notifications",  sendNotifications}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaBroadcastStartResponse>(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastStartResponse>(response, json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastStartResponse>(exception);
            }
        }

        /// <summary>
        /// Unpin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<IResult<InstaBroadcastPinUnpinResponse>> UnPinCommentAsync(string broadcastId, string commentId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastUnPinCommentUri(broadcastId);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"comment_id", commentId},
                    {"offset_to_video_start", 0}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBroadcastPinUnpinResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaBroadcastPinUnpinResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBroadcastPinUnpinResponse>(exception);
            }
        }
    }
}
