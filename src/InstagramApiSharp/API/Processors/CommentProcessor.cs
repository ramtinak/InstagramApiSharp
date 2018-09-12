using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using System.Diagnostics;
namespace InstagramApiSharp.API.Processors
{
    internal class CommentProcessor : ICommentProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        public CommentProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        ///     Get media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaInlineCommentList>> GetMediaCommentsAsync(string mediaId,
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var commentsUri = UriCreator.GetMediaCommentsUri(mediaId, paginationParameters.NextId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaInlineCommentList>(response, json);
                var commentListResponse = JsonConvert.DeserializeObject<InstaInlineCommentListResponse>(json);

                var pagesLoaded = 1;

                InstaInlineCommentList Convert(InstaInlineCommentListResponse commentsResponse)
                {
                    return ConvertersFabric.Instance.GetInlineCommentsConverter(commentsResponse).Convert();
                }

                while (commentListResponse.HasMoreComments
                       && !string.IsNullOrEmpty(commentListResponse.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextComments = await GetCommentListWithMaxIdAsync(mediaId, commentListResponse.NextMinId);
                    if (!nextComments.Succeeded)
                        return Result.Fail(nextComments.Info, Convert(commentListResponse));
                    commentListResponse.NextMaxId = nextComments.Value.NextMaxId;
                    commentListResponse.HasMoreComments = nextComments.Value.HasMoreComments;
                    commentListResponse.Comments.AddRange(nextComments.Value.Comments);
                    pagesLoaded++;
                }
                var comments = ConvertersFabric.Instance.GetInlineCommentsConverter(commentListResponse).Convert();
                return Result.Success(comments);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaInlineCommentList>(exception);
            }
        }
        /// <summary>
        ///     Get media inline comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="targetCommentId">Target comment id</param>
        /// <param name="paginationParameters">Maximum amount of pages to load and start id</param>
        /// <returns></returns>
        public async Task<IResult<InstaInlineCommentList>> GetMediaRepliesCommentsAsync(string mediaId, string targetCommentId,
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var commentsUri = UriCreator.GetMediaInlineCommentsUri(mediaId, targetCommentId, paginationParameters.NextId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
               
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaInlineCommentList>(response, json);
                var commentListResponse = JsonConvert.DeserializeObject<InstaInlineCommentListResponse>(json);

                var pagesLoaded = 1;

                InstaInlineCommentList Convert(InstaInlineCommentListResponse commentsResponse)
                {
                    return ConvertersFabric.Instance.GetInlineCommentsConverter(commentsResponse).Convert();
                }

                while (commentListResponse.HasMoreComments
                       && !string.IsNullOrEmpty(commentListResponse.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextComments = await GetCommentListWithMaxIdAsync(mediaId, commentListResponse.NextMinId);
                    if (!nextComments.Succeeded)
                        return Result.Fail(nextComments.Info, Convert(commentListResponse));
                    commentListResponse.NextMaxId = nextComments.Value.NextMaxId;
                    commentListResponse.HasMoreComments = nextComments.Value.HasMoreComments;
                    commentListResponse.Comments.AddRange(nextComments.Value.Comments);
                    pagesLoaded++;
                }
                var comments = ConvertersFabric.Instance.GetInlineCommentsConverter(commentListResponse).Convert();
                return Result.Success(comments);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaInlineCommentList>(exception);
            }
        }
        /// <summary>
        ///     Comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="text">Comment text</param>
        public async Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetPostCommetUri(mediaId);
                var breadcrumb = CryptoHelper.GetCommentBreadCrumbEncoded(text);
                var fields = new Dictionary<string, string>
                {
                    {"user_breadcrumb", breadcrumb},
                    {"idempotence_token", Guid.NewGuid().ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"comment_text", text},
                    {"containermodule", "comments_feed_timeline"},
                    {"radio_type", "wifi-none"}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
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
                return Result.Fail(exception.Message, (InstaComment) null);
            }
        }
        /// <summary>
        ///     Inline comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="targetCommentId">Target comment id</param>
        /// <param name="text">Comment text</param>
        public async Task<IResult<InstaComment>> ReplyCommentMediaAsync(string mediaId, string targetCommentId, string text)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetPostCommetUri(mediaId);
                var breadcrumb = CryptoHelper.GetCommentBreadCrumbEncoded(text);
                var fields = new Dictionary<string, string>
                {
                    {"user_breadcrumb", breadcrumb},
                    {"idempotence_token", Guid.NewGuid().ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"replied_to_comment_id", targetCommentId},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"comment_text", text},
                    {"containermodule", "comments_feed_timeline"},
                    {"radio_type", "wifi-none"}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
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
                return Result.Fail(exception.Message, (InstaComment)null);
            }
        }
        /// <summary>
        ///     Delete media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        public async Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDeleteCommentUri(mediaId, commentId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
        /// <summary>
        ///     Delete media comments(multiple)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentIds">Comment id</param>
        public async Task<IResult<bool>> DeleteMultipleCommentsAsync(string mediaId, params string[] commentIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDeleteMultipleCommentsUri(mediaId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"comment_ids_to_delete", commentIds.EncodeList(false)}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
        private async Task<IResult<InstaInlineCommentListResponse>> GetCommentListWithMaxIdAsync(string mediaId,
            string nextId)
        {
            var commentsUri = UriCreator.GetMediaCommentsUri(mediaId, nextId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
           
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.UnExpectedResponse<InstaInlineCommentListResponse>(response, json);
            var comments = JsonConvert.DeserializeObject<InstaInlineCommentListResponse>(json);
            return Result.Success(comments);
        }

        private async Task<IResult<InstaInlineCommentListResponse>> GetInlineCommentListWithMaxIdAsync(string mediaId,
    string targetCommandId,
    string nextId)
        {
            var commentsUri = UriCreator.GetMediaInlineCommentsUri(mediaId, targetCommandId, nextId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.UnExpectedResponse<InstaInlineCommentListResponse>(response, json);
            var commentListResponse = JsonConvert.DeserializeObject<InstaInlineCommentListResponse>(json);

            //var comments = ConvertersFabric.Instance.GetInlineCommentsConverter(commentListResponse).Convert();
            return Result.Success(commentListResponse);
        }
        /// <summary>
        ///     Allow media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> EnableMediaCommentAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetAllowMediaCommetsUri(mediaId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
        /// <summary>
        ///     Disable media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> DisableMediaCommentAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDisableMediaCommetsUri(mediaId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
               
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
        /// <summary>
        ///     Get media comments likers
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<bool>> GetMediaCommentLikersAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMediaCommetLikersUri(mediaId);
                var request =
                    HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
        /// <summary>
        ///     Report media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        public async Task<IResult<bool>> ReportCommentAsync(string mediaId, string commentId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            { 
                var instaUri = UriCreator.GetReportCommetUri(mediaId, commentId);
                var fields = new Dictionary<string, string>
                {
                    {"media_id", mediaId},
                    {"comment_id", commentId},
                    {"reason", "1"},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }


        /// <summary>
        ///     Like media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        public async Task<IResult<bool>> LikeCommentAsync(string mediaId, string commentId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetLikeCommentUri(mediaId, commentId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
        /// <summary>
        ///     Unlike media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        public async Task<IResult<bool>> UnlikeCommentAsync(string mediaId, string commentId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUnLikeCommentUri(mediaId, commentId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Success(true)
                    : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
    }
}