using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.Models.Hashtags;
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
    ///     Hashtag api functions.
    /// </summary>
    internal class HashtagProcessor : IHashtagProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public HashtagProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        ///     Follow a hashtag
        /// </summary>
        /// <param name="tagname">Tag name</param>
        public async Task<IResult<bool>> FollowHashtagAsync(string tagname)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetFollowHashtagUri(tagname);

                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
        ///     Get following hashtags information
        /// </summary>
        /// <param name="userId">User identifier (pk)</param>
        /// <returns>
        ///     List of hashtags
        /// </returns>
        public async Task<IResult<InstaHashtagSearch>> GetFollowingHashtagsInfoAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var tags = new InstaHashtagSearch();
            try
            {
                var userUri = UriCreator.GetFollowingTagsInfoUri(userId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaHashtagSearch>(response, json);

                var tagsResponse = JsonConvert.DeserializeObject<InstaHashtagSearchResponse>(json,
                    new InstaHashtagSuggestedDataConverter());

                tags = ConvertersFabric.Instance.GetHashTagsSearchConverter(tagsResponse).Convert();
                return Result.Success(tags);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHashtagSearch), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, tags);
            }
        }

        /// <summary>
        ///     Gets the hashtag information by user tagname.
        /// </summary>
        /// <param name="tagname">Tagname</param>
        /// <returns>Hashtag information</returns>
        public async Task<IResult<InstaHashtag>> GetHashtagInfoAsync(string tagname)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetTagInfoUri(tagname);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaHashtag>(response, json);

                var tagInfoResponse = JsonConvert.DeserializeObject<InstaHashtagResponse>(json);
                var tagInfo = ConvertersFabric.Instance.GetHashTagConverter(tagInfoResponse).Convert();

                return Result.Success(tagInfo);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHashtag), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHashtag>(exception);
            }
        }

        /// <summary>
        ///     Get stories of an hashtag
        /// </summary>
        /// <param name="tagname">Tag name</param>
        public async Task<IResult<InstaHashtagStory>> GetHashtagStoriesAsync(string tagname)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetHashtagStoryUri(tagname);

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaHashtagStory>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaHashtagStoryContainerResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetHashtagStoryConverter(obj.Story).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHashtagStory), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHashtagStory>(exception);
            }
        }

        /// <summary>
        ///     Get recent hashtag media list
        /// </summary>
        /// <param name="tagname">Tag name</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaSectionMedia>> GetRecentHashtagMediaListAsync(string tagname,
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaSectionMedia Convert(InstaSectionMediaListResponse hashtagMediaListResponse)
                {
                    return ConvertersFabric.Instance.GetHashtagMediaListConverter(hashtagMediaListResponse).Convert();
                }
                var mediaResponse = await GetHashtagSection(tagname,
                     Guid.NewGuid().ToString(),
                    paginationParameters.NextMaxId, true);
                if (!mediaResponse.Succeeded)
                {
                    if (mediaResponse.Value != null)
                        Result.Fail(mediaResponse.Info, Convert(mediaResponse.Value));
                    else
                        Result.Fail(mediaResponse.Info, default(InstaSectionMedia));
                }
                paginationParameters.NextMediaIds = mediaResponse.Value.NextMediaIds;
                paginationParameters.NextPage = mediaResponse.Value.NextPage;
                paginationParameters.NextMaxId = mediaResponse.Value.NextMaxId;
                while (mediaResponse.Value.MoreAvailable
                    && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                    && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var moreMedias = await GetHashtagSection(tagname, Guid.NewGuid().ToString(),
                        paginationParameters.NextMaxId, true);
                    if (!moreMedias.Succeeded)
                    {
                        if (mediaResponse.Value.Sections != null && mediaResponse.Value.Sections.Any())
                            return Result.Success(Convert(mediaResponse.Value));
                        else
                            return Result.Fail(moreMedias.Info, Convert(mediaResponse.Value));
                    }

                    mediaResponse.Value.MoreAvailable = moreMedias.Value.MoreAvailable;
                    mediaResponse.Value.NextMaxId = paginationParameters.NextMaxId = moreMedias.Value.NextMaxId;
                    mediaResponse.Value.AutoLoadMoreEnabled = moreMedias.Value.AutoLoadMoreEnabled;
                    mediaResponse.Value.NextMediaIds = paginationParameters.NextMediaIds = moreMedias.Value.NextMediaIds;
                    mediaResponse.Value.NextPage = paginationParameters.NextPage = moreMedias.Value.NextPage;
                    mediaResponse.Value.Sections.AddRange(moreMedias.Value.Sections);
                    paginationParameters.PagesLoaded++;
                }

                return Result.Success(ConvertersFabric.Instance.GetHashtagMediaListConverter(mediaResponse.Value).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSectionMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSectionMedia>(exception);
            }
        }

        /// <summary>
        ///     Get suggested hashtags
        /// </summary>
        /// <returns>
        ///     List of hashtags
        /// </returns>
        public async Task<IResult<InstaHashtagSearch>> GetSuggestedHashtagsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var tags = new InstaHashtagSearch();
            try
            {
                var userUri = UriCreator.GetSuggestedTagsUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaHashtagSearch>(response, json);

                var tagsResponse = JsonConvert.DeserializeObject<InstaHashtagSearchResponse>(json,
                    new InstaHashtagSuggestedDataConverter());

                tags = ConvertersFabric.Instance.GetHashTagsSearchConverter(tagsResponse).Convert();
                return Result.Success(tags);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHashtagSearch), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, tags);
            }
        }

        /// <summary>
        ///     Get top (ranked) hashtag media list
        /// </summary>
        /// <param name="tagname">Tag name</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaSectionMedia>> GetTopHashtagMediaListAsync(string tagname,
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaSectionMedia Convert(InstaSectionMediaListResponse hashtagMediaListResponse)
                {
                    return ConvertersFabric.Instance.GetHashtagMediaListConverter(hashtagMediaListResponse).Convert();
                }
                var mediaResponse = await GetHashtagSection(tagname,
                    Guid.NewGuid().ToString(),
                    paginationParameters.NextMaxId);

                if (!mediaResponse.Succeeded)
                {
                    if (mediaResponse.Value != null)
                        Result.Fail(mediaResponse.Info, Convert(mediaResponse.Value));
                    else
                        Result.Fail(mediaResponse.Info, default(InstaSectionMedia));
                }
                paginationParameters.NextMediaIds = mediaResponse.Value.NextMediaIds;
                paginationParameters.NextPage = mediaResponse.Value.NextPage;
                paginationParameters.NextMaxId = mediaResponse.Value.NextMaxId;
                while (mediaResponse.Value.MoreAvailable
                    && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                    && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var moreMedias = await GetHashtagSection(tagname, Guid.NewGuid().ToString(),
                        paginationParameters.NextMaxId);
                    if (!moreMedias.Succeeded)
                    {
                        if (mediaResponse.Value.Sections != null && mediaResponse.Value.Sections.Any())
                            return Result.Success(Convert(mediaResponse.Value));
                        else
                            return Result.Fail(moreMedias.Info, Convert(mediaResponse.Value));
                    }

                    mediaResponse.Value.MoreAvailable = moreMedias.Value.MoreAvailable;
                    mediaResponse.Value.NextMaxId = paginationParameters.NextMaxId = moreMedias.Value.NextMaxId;
                    mediaResponse.Value.AutoLoadMoreEnabled = moreMedias.Value.AutoLoadMoreEnabled;
                    mediaResponse.Value.NextMediaIds = paginationParameters.NextMediaIds = moreMedias.Value.NextMediaIds;
                    mediaResponse.Value.NextPage = paginationParameters.NextPage = moreMedias.Value.NextPage;
                    mediaResponse.Value.Sections.AddRange(moreMedias.Value.Sections);
                    paginationParameters.PagesLoaded++;
                }

                return Result.Success(ConvertersFabric.Instance.GetHashtagMediaListConverter(mediaResponse.Value).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSectionMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSectionMedia>(exception);
            }
        }

        /// <summary>
        ///     Searches for specific hashtag by search query.
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="excludeList">Array of numerical hashtag IDs (ie "17841562498105353") to exclude from the response, allowing you to skip tags from a previous call to get more results</param>
        /// <param name="rankToken">The rank token from the previous page's response</param>
        /// <returns>
        ///     List of hashtags
        /// </returns>
        public async Task<IResult<InstaHashtagSearch>> SearchHashtagAsync(string query, IEnumerable<long> excludeList, string rankToken)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var RequestHeaderFieldsTooLarge = (HttpStatusCode)431;
            var count = 50;
            var tags = new InstaHashtagSearch();

            try
            {
                var userUri = UriCreator.GetSearchTagUri(query, count, excludeList, rankToken);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == RequestHeaderFieldsTooLarge)
                    return Result.Success(tags);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaHashtagSearch>(response, json);

                var tagsResponse = JsonConvert.DeserializeObject<InstaHashtagSearchResponse>(json,
                    new InstaHashtagSearchDataConverter());
                tags = ConvertersFabric.Instance.GetHashTagsSearchConverter(tagsResponse).Convert();

                if (tags.Any() && excludeList != null && excludeList.Contains(tags.First().Id))
                    tags.RemoveAt(0);

                if (!tags.Any())
                    tags = new InstaHashtagSearch();

                return Result.Success(tags);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHashtagSearch), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, tags);
            }
        }
        /// <summary>
        ///     Unfollow a hashtag
        /// </summary>
        /// <param name="tagname">Tag name</param>
        public async Task<IResult<bool>> UnFollowHashtagAsync(string tagname)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUnFollowHashtagUri(tagname);

                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
        private async Task<IResult<InstaSectionMediaListResponse>> GetHashtagRecentMedia(string tagname,
            string rankToken = null,
            string maxId = null,
            int? page = null,
            List<long> nextMediaIds = null)
        {
            try
            {
                var instaUri = UriCreator.GetHashtagRecentMediaUri(tagname, rankToken,
                    maxId, page, nextMediaIds);

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaSectionMediaListResponse>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaSectionMediaListResponse>(json);

                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSectionMediaListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSectionMediaListResponse>(exception);
            }
        }

        private async Task<IResult<InstaSectionMediaListResponse>> GetHashtagSection(string tagname,
            string rankToken = null,
            string maxId = null, bool recent = false)
        {
            try
            {
                var instaUri = UriCreator.GetHashtagSectionUri(tagname);

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"include_persistent", !recent ? "true" : "false"},
                    {"rank_token", rankToken},
                };
                if (recent)
                    data.Add("tab", "recent");
                else
                    data.Add("supported_tabs", new JArray("top", "recent", "places", "discover").ToString());

                if (!string.IsNullOrEmpty(maxId))
                    data.Add("max_id", maxId);
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaSectionMediaListResponse>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaSectionMediaListResponse>(json);

                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSectionMediaListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSectionMediaListResponse>(exception);
            }
        }
    }
}