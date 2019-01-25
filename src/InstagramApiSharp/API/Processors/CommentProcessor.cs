using System;
using System.Collections.Generic;
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
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Comments api functions.
    /// </summary>
    internal class CommentProcessor : ICommentProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public CommentProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        ///     Block an user from commenting to medias
        /// </summary>
        /// <param name="userIds">User ids (pk)</param>
        public async Task<IResult<bool>> BlockUserCommentingAsync(params long[] userIds)
        {
            return await BlockUnblockCommenting(true, userIds);
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
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaComment>(response, json);
                var commentResponse = JsonConvert.DeserializeObject<InstaCommentResponse>(json,
                    new InstaCommentDataConverter());
                var converter = ConvertersFabric.Instance.GetCommentConverter(commentResponse);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaComment), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaComment>(exception);
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
        ///     Get blocked users from commenting
        /// </summary>
        public async Task<IResult<InstaUserShortList>> GetBlockedCommentersAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetBlockedCommentersUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserShortList>(response, json);
                
                var obj = JsonConvert.DeserializeObject<InstaBlockedCommentersResponse>(json);
                
                return Result.Success(ConvertersFabric.Instance.GetBlockedCommentersConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserShortList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserShortList>(exception);
            }
        }

        /// <summary>
        ///     Get media comments likers
        /// </summary>
        /// <param name="mediaId">Media id</param>
        public async Task<IResult<InstaLikersList>> GetMediaCommentLikersAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMediaCommetLikersUri(mediaId);
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaLikersList>(response, json);

                var likers = new InstaLikersList();
                var likersResponse = JsonConvert.DeserializeObject<InstaMediaLikersResponse>(json);
                likers.UsersCount = likersResponse.UsersCount;
                likers.AddRange(
                    likersResponse.Users.Select(ConvertersFabric.Instance.GetUserShortConverter)
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
        ///     Get media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaCommentList>> GetMediaCommentsAsync(string mediaId,
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                var commentsUri = UriCreator.GetMediaCommentsUri(mediaId, paginationParameters.NextMaxId);
                if (!string.IsNullOrEmpty(paginationParameters.NextMinId))
                    commentsUri = UriCreator.GetMediaCommentsMinIdUri(mediaId, paginationParameters.NextMinId);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCommentList>(response, json);
                var commentListResponse = JsonConvert.DeserializeObject<InstaCommentListResponse>(json);
                var pagesLoaded = 1;
                InstaCommentList Convert(InstaCommentListResponse commentsResponse)
                {
                    return ConvertersFabric.Instance.GetCommentListConverter(commentsResponse).Convert();
                }

                while (commentListResponse.MoreCommentsAvailable
                       && !string.IsNullOrEmpty(commentListResponse.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad ||

                       commentListResponse.MoreHeadLoadAvailable
                       && !string.IsNullOrEmpty(commentListResponse.NextMinId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    IResult<InstaCommentListResponse> nextComments;
                    if(!string.IsNullOrEmpty(commentListResponse.NextMaxId))
                        nextComments = await GetCommentListWithMaxIdAsync(mediaId, commentListResponse.NextMaxId,null);
                    else 
                        nextComments = await GetCommentListWithMaxIdAsync(mediaId,null, commentListResponse.NextMinId);

                    if (!nextComments.Succeeded)
                        return Result.Fail(nextComments.Info, Convert(commentListResponse));
                    commentListResponse.NextMaxId = nextComments.Value.NextMaxId;
                    commentListResponse.NextMinId = nextComments.Value.NextMinId;
                    commentListResponse.MoreCommentsAvailable = nextComments.Value.MoreCommentsAvailable;
                    commentListResponse.MoreHeadLoadAvailable = nextComments.Value.MoreHeadLoadAvailable;
                    commentListResponse.Comments.AddRange(nextComments.Value.Comments);
                    paginationParameters.NextMaxId = nextComments.Value.NextMaxId;
                    paginationParameters.NextMinId = nextComments.Value.NextMinId;
                    pagesLoaded++;
                }
                paginationParameters.NextMaxId = commentListResponse.NextMaxId;
                paginationParameters.NextMinId = commentListResponse.NextMinId;
                var converter = ConvertersFabric.Instance.GetCommentListConverter(commentListResponse);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaCommentList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCommentList>(exception);
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
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                var commentsUri = UriCreator.GetMediaInlineCommentsUri(mediaId, targetCommentId, paginationParameters.NextMaxId);
                if (!string.IsNullOrEmpty(paginationParameters.NextMinId))
                    commentsUri = UriCreator.GetMediaInlineCommentsWithMinIdUri(mediaId, targetCommentId, paginationParameters.NextMinId);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
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
                while (commentListResponse.HasMoreTailChildComments
                       && !string.IsNullOrEmpty(commentListResponse.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad ||
                       commentListResponse.HasMoreHeadChildComments
                       && !string.IsNullOrEmpty(commentListResponse.NextMinId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    IResult<InstaInlineCommentListResponse> nextComments;
                    if (!string.IsNullOrEmpty(commentListResponse.NextMaxId))
                        nextComments = await GetInlineCommentListWithMaxIdAsync(mediaId, targetCommentId, commentListResponse.NextMaxId, null);
                    else
                        nextComments = await GetInlineCommentListWithMaxIdAsync(mediaId, targetCommentId, null, commentListResponse.NextMinId);
                    if (!nextComments.Succeeded)
                        return Result.Fail(nextComments.Info, Convert(commentListResponse));
                    commentListResponse.NextMaxId = nextComments.Value.NextMaxId;
                    commentListResponse.NextMinId = nextComments.Value.NextMinId;
                    commentListResponse.HasMoreHeadChildComments = nextComments.Value.HasMoreHeadChildComments;
                    commentListResponse.HasMoreTailChildComments = nextComments.Value.HasMoreTailChildComments;
                    commentListResponse.ChildComments.AddRange(nextComments.Value.ChildComments);
                    paginationParameters.NextMaxId = nextComments.Value.NextMaxId;
                    paginationParameters.NextMinId = nextComments.Value.NextMinId;
                    pagesLoaded++;
                }
                paginationParameters.NextMaxId = commentListResponse.NextMaxId;
                paginationParameters.NextMinId = commentListResponse.NextMinId;
                var comments = ConvertersFabric.Instance.GetInlineCommentsConverter(commentListResponse).Convert();
                return Result.Success(comments);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaInlineCommentList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaInlineCommentList>(exception);
            }
        }
        /// <summary>
        ///     Like media comment
        /// </summary>
        /// <param name="commentId">Comment id</param>
        public async Task<IResult<bool>> LikeCommentAsync(string commentId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetLikeCommentUri(commentId);
                var fields = new Dictionary<string, string>
                {
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
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaComment>(response, json);
                var commentResponse = JsonConvert.DeserializeObject<InstaCommentResponse>(json,
                    new InstaCommentDataConverter());
                var converter = ConvertersFabric.Instance.GetCommentConverter(commentResponse);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaComment), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaComment>(exception);
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
        ///     Unblock an user from commenting to medias
        /// </summary>
        /// <param name="userIds">User ids (pk)</param>
        public async Task<IResult<bool>> UnblockUserCommentingAsync(params long[] userIds)
        {
            return await BlockUnblockCommenting(false, userIds);
        }

        /// <summary>
        ///     Unlike media comment
        /// </summary>
        /// <param name="commentId">Comment id</param>
        public async Task<IResult<bool>> UnlikeCommentAsync(string commentId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUnLikeCommentUri(commentId);
                var fields = new Dictionary<string, string>
                {
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
        ///     Translate comment or captions
        ///     <para>Note: use this function to translate captions too! (i.e: <see cref="InstaCaption.Pk"/>)</para>
        /// </summary>
        /// <param name="commentIds">Comment id(s) (Array of <see cref="InstaComment.Pk"/>)</param>
        public async Task<IResult<InstaTranslateList>> TranslateCommentAsync(params long[] commentIds)
        {
            try
            {
                if (commentIds == null || commentIds != null && !commentIds.Any())
                    throw new ArgumentException("At least one comment id require");

                var instaUri = UriCreator.GetTranslateCommentsUri(string.Join(",", commentIds));

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(json))
                    return Result.UnExpectedResponse<InstaTranslateList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaTranslateContainerResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetTranslateContainerConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTranslateList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaTranslateList>(exception);
            }
        }

        private async Task<IResult<InstaCommentListResponse>> GetCommentListWithMaxIdAsync(string mediaId,
                            string nextMaxId, string nextMinId)
        {
            try
            {
                var commentsUri = UriCreator.GetMediaCommentsUri(mediaId, nextMaxId);
                if (!string.IsNullOrEmpty(nextMinId))
                    commentsUri = UriCreator.GetMediaCommentsMinIdUri(mediaId, nextMinId);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCommentListResponse>(response, json);
                var comments = JsonConvert.DeserializeObject<InstaCommentListResponse>(json);
                return Result.Success(comments);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaCommentListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCommentListResponse>(exception);
            }
        }

        private async Task<IResult<InstaInlineCommentListResponse>> GetInlineCommentListWithMaxIdAsync(string mediaId,
    string targetCommandId,
    string nextMaxId, string nextMinId)
        {
            try
            {
                var commentsUri = UriCreator.GetMediaInlineCommentsUri(mediaId, targetCommandId, nextMaxId);
                if (!string.IsNullOrEmpty(nextMinId))
                    commentsUri = UriCreator.GetMediaInlineCommentsWithMinIdUri(mediaId, targetCommandId, nextMinId);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaInlineCommentListResponse>(response, json);
                var commentListResponse = JsonConvert.DeserializeObject<InstaInlineCommentListResponse>(json);
                return Result.Success(commentListResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaInlineCommentListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaInlineCommentListResponse>(exception);
            }
        }

        private async Task<IResult<bool>> BlockUnblockCommenting(bool block, long[] userIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (userIds == null || userIds?.Length == 0)
                    Result.Fail<bool>("At least one user id (pk) is require");

                var instaUri = UriCreator.GetSetBlockedCommentersUri();
                //var blockedUsersResponse = await GetBlockedCommentersAsync();
                //var blockedUsers = new List<long>();
                //if (blockedUsersResponse.Succeeded && blockedUsersResponse.Value?.Count > 0)
                //{
                //    foreach (var u in blockedUsersResponse.Value)
                //    {
                //        foreach (var id in userIds)
                //            if (u.Pk == id)
                //                blockedUsers.Add(u.Pk);
                //    }
                //}

                //{
                //	"_csrftoken": "UBPgM6BG1Qr95lO4ofLYpgJXtbVvVnvs",
                //	"_uid": "7405924766",
                //	"_uuid": "6324ecb2-e663-4dc8-a3a1-289c699cc876",
                //	"commenter_block_status": {
                //		"block": [9013775990, 9013775990],
                //		"unblock": [9013775990]
                //	}
                //}
                var commenterBlockStatus = new JObject();
                if (block)
                {
                    commenterBlockStatus.Add("block", new JArray(userIds));
                    commenterBlockStatus.Add("unblock", new JArray());
                }
                else
                {
                    commenterBlockStatus.Add("block", new JArray());
                    commenterBlockStatus.Add("unblock", new JArray(userIds));
                }

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"commenter_block_status", commenterBlockStatus}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
    }
}