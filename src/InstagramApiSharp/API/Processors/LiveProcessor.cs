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
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        public LiveProcessor(AndroidDevice deviceInfo, UserSessionData user,
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

        /// <summary>
        /// Get heart beat and viewer count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastLiveHeartBeatViewerCountResponse>> GetHeartBeatAndViewerCountAsync(string broadcastId)
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastLiveHeartBeatViewerCountResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastLiveHeartBeatViewerCountResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastLiveHeartBeatViewerCountResponse>(exception);
            }
        }
        /// <summary>
        /// Get final viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastFinalViewerListResponse>> GetFinalViewerListAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetLiveFinalViewerListUri(broadcastId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastFinalViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastFinalViewerListResponse>(json);
                return Result.Success(obj);

            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastFinalViewerListResponse>(exception);
            }
        }
        /// <summary>
        /// Get suggested broadcasts
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<BroadcastSuggestedResponse>> GetSuggestedBroadcastsAsync()
        {
            try
            {
                var instaUri = UriCreator.GetSuggestedBroadcastsUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastSuggestedResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastSuggestedResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastSuggestedResponse>(exception);
            }
        }
        /// <summary>
        /// Get discover top live.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<DiscoverTopLiveResponse>> GetDiscoverTopLiveAsync()
        {
            try
            {
                var instaUri = UriCreator.GetDiscoverTopLiveUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<DiscoverTopLiveResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<DiscoverTopLiveResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<DiscoverTopLiveResponse>(exception);
            }
        }
        /// <summary>
        /// Get top live status.
        /// </summary>
        /// <param name="broadcastIds">Broadcast ids</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastTopLiveStatusResponse>> GetTopLiveStatusAsync(params string[] broadcastIds)
        {
            if(broadcastIds == null)
                return Result.Fail<BroadcastTopLiveStatusResponse>("broadcast ids must be set");
            try
            {
                var instaUri = UriCreator.GetDiscoverTopLiveStatusUri();
                var data = new JObject
                {
                    {"broadcast_ids", new JArray(broadcastIds)},
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastTopLiveStatusResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastTopLiveStatusResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastTopLiveStatusResponse>(exception);
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
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
        /// Get broadcast viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastViewerListResponse>> GetViewerListAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastViewerListUri(broadcastId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastViewerListResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastViewerListResponse>(exception);
            }
        }
        /// <summary>
        /// Get post live viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="maxId">Max id</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastViewerListResponse>> GetPostLiveViewerListAsync(string broadcastId, int? maxId = null)
        {
            try
            {
                var instaUri = UriCreator.GetPostLiveViewersListUri(broadcastId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastViewerListResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastViewerListResponse>(exception);
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
        /// <summary>
        /// Pin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<IResult<BroadcastPinUnpinResponse>> PinCommentAsync(string broadcastId, string commentId)
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastPinUnpinResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastPinUnpinResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastPinUnpinResponse>(exception);
            }
        }
        /// <summary>
        /// Unpin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<IResult<BroadcastPinUnpinResponse>> UnPinCommentAsync(string broadcastId, string commentId)
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastPinUnpinResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastPinUnpinResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastPinUnpinResponse>(exception);
            }
        }
        /// <summary>
        /// Get broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="lastCommentTs">Last comment time stamp</param>
        /// <param name="commentsRequested">Comments requested count</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastCommentResponse>> GetCommentsAsync(string broadcastId, int lastCommentTs = 0, int commentsRequested =4)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastCommentUri(broadcastId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastCommentResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastCommentResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastCommentResponse>(exception);
            }
        }
        /// <summary>
        /// Enable broadcast comments.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <returns></returns>
        public async Task<IResult<BroadcastCommentEnableDisableResponse>> EnableCommentsAsync(string broadcastId)
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastCommentEnableDisableResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastCommentEnableDisableResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastCommentEnableDisableResponse>(exception);
            }
        }
        /// <summary>
        /// Disable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastCommentEnableDisableResponse>> DisableCommentsAsync(string broadcastId)
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastCommentEnableDisableResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastCommentEnableDisableResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastCommentEnableDisableResponse>(exception);
            }
        }
        /// <summary>
        /// Like broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeCount">Like count (from 1 to 6)</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastLikeResponse>> LikeAsync(string broadcastId, int likeCount = 1)
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastLikeResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastLikeResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastLikeResponse>(exception);
            }
        }
        /// <summary>
        /// Get broadcast like count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeTs">Like time stamp</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastLikeResponse>> GetLikeCountAsync(string broadcastId, int likeTs = 0)
        {
            try
            {
                var instaUri = UriCreator.GetLiveLikeCountUri(broadcastId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastLikeResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastLikeResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastLikeResponse>(exception);
            }
        }
        /// <summary>
        /// Add an broadcast to post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastAddToPostLiveResponse>> AddToPostLiveAsync(string broadcastId)
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastAddToPostLiveResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastAddToPostLiveResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastAddToPostLiveResponse>(exception);
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
        ///     Get join requests to current live broadcast
        /// </summary>
        /// <param name="broadcastId">Broadcast</param>
        public async Task<IResult<BroadcastFinalViewerListResponse>> GetJoinRequestsAsync(string broadcastId)
        {
            try
            {
                var instaUri = UriCreator.GetBroadcastJoinRequestsUri(broadcastId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastFinalViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastFinalViewerListResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastFinalViewerListResponse>(exception);
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
        public async Task<IResult<BroadcastCreateResponse>> CreateAsync(int previewWidth = 720, int previewHeight = 1184, string broadcastMessage = "")
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo,data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastCreateResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadcastCreateResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastCreateResponse>(exception);
            }
        }
        /// <summary>
        /// Start live broadcast. NOTE: YOU MUST CREATE AN BROADCAST FIRST(CreateAsync) AND THEN CALL THIS METHOD. 
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="sendNotifications">Send notifications</param>
        /// <returns></returns>
        public async Task<IResult<BroadcastStartResponse>> StartAsync(string broadcastId, bool sendNotifications)
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<BroadcastStartResponse>(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastStartResponse>(response, json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadcastStartResponse>(exception);
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status == "ok" ? Result.Success(true): Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadCastNotifyFriendsResponse>(json);
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
        public async Task<IResult<object>> GetPostLiveCommentsAsync(string broadcastId, int startingOffset = 0, string encodingTag = "instagram_dash_remuxed")
        {
            try
            {
                // kamel nist
                var instaUri = UriCreator.GetBroadcastPostLiveCommentUri(broadcastId, startingOffset, encodingTag);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadCastNotifyFriendsResponse>(json);
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
        public async Task<IResult<object>> NotifyToFriendsAsync()
        {
            try
            {
                var instaUri = UriCreator.GetLiveNotifyToFriendsUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastViewerListResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<BroadCastNotifyFriendsResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<BroadCastNotifyFriendsResponse>(exception);
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
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<BroadcastViewerListResponse>(response, json);
                return Result.Success(json);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<BroadcastLiveHeartBeatViewerCountResponse>(exception);
            }
        }
    }
}
