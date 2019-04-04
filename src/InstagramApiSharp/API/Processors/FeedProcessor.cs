using System;
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
using InstaRecentActivityConverter = InstagramApiSharp.Converters.Json.InstaRecentActivityConverter;
using System.Diagnostics;
using System.Collections.Generic;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Feed api functions.
    /// </summary>
    internal class FeedProcessor : IFeedProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public FeedProcessor(AndroidDevice deviceInfo, UserSessionData user, IHttpRequestProcessor httpRequestProcessor,
            IInstaLogger logger, UserAuthValidate userAuthValidate, InstaApi instaApi, HttpHelper httpHelper)
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
        ///     Get user explore feed (Explore tab info) asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns><see cref="InstaExploreFeed" /></returns>
        public async Task<IResult<InstaExploreFeed>> GetExploreFeedAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var exploreFeed = new InstaExploreFeed();
            try
            {
                InstaExploreFeed Convert(InstaExploreFeedResponse exploreFeedResponse)
                {
                    return ConvertersFabric.Instance.GetExploreFeedConverter(exploreFeedResponse).Convert();
                }
                var feeds = await GetExploreFeed(paginationParameters);
                if (!feeds.Succeeded)
                {
                    if (feeds.Value != null)
                        return Result.Fail(feeds.Info, Convert(feeds.Value));
                    else
                        return Result.Fail(feeds.Info, (InstaExploreFeed)null);
                }
                var feedResponse = feeds.Value;
                paginationParameters.NextMaxId = feedResponse.MaxId;
                paginationParameters.RankToken = feedResponse.RankToken;

                while (feedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFeed = await GetExploreFeed(paginationParameters);
                    if (!nextFeed.Succeeded)
                        return Result.Fail(nextFeed.Info, Convert(feeds.Value));

                    feedResponse.NextMaxId = paginationParameters.NextMaxId = nextFeed.Value.MaxId;
                    feedResponse.RankToken = paginationParameters.RankToken = nextFeed.Value.RankToken;
                    feedResponse.MoreAvailable = nextFeed.Value.MoreAvailable;
                    feedResponse.AutoLoadMoreEnabled = nextFeed.Value.AutoLoadMoreEnabled;
                    feedResponse.NextMaxId = nextFeed.Value.NextMaxId;
                    feedResponse.ResultsCount = nextFeed.Value.ResultsCount;
                    feedResponse.Items.Channel = nextFeed.Value.Items.Channel;
                    feedResponse.Items.Medias.AddRange(nextFeed.Value.Items.Medias);
                    if (nextFeed.Value.Items.StoryTray == null)
                        feedResponse.Items.StoryTray = nextFeed.Value.Items.StoryTray;
                    else
                    {
                        feedResponse.Items.StoryTray.Id = nextFeed.Value.Items.StoryTray.Id;
                        feedResponse.Items.StoryTray.IsPortrait = nextFeed.Value.Items.StoryTray.IsPortrait;

                        feedResponse.Items.StoryTray.Tray.AddRange(nextFeed.Value.Items.StoryTray.Tray);
                        if (nextFeed.Value.Items.StoryTray.TopLive == null)
                            feedResponse.Items.StoryTray.TopLive = nextFeed.Value.Items.StoryTray.TopLive;
                        else
                        {
                            feedResponse.Items.StoryTray.TopLive.RankedPosition = nextFeed.Value.Items.StoryTray.TopLive.RankedPosition;
                            feedResponse.Items.StoryTray.TopLive.BroadcastOwners.AddRange(nextFeed.Value.Items.StoryTray.TopLive.BroadcastOwners);
                        }
                    }

                    paginationParameters.PagesLoaded++;
                }
                exploreFeed = Convert(feedResponse);
                exploreFeed.Medias.Pages = paginationParameters.PagesLoaded;
                exploreFeed.Medias.PageSize = feedResponse.ResultsCount;
                return Result.Success(exploreFeed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaExploreFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, exploreFeed);
            }
        }

        /// <summary>
        ///     Get activity of following asynchronously
        /// </summary>
        /// <param name="paginationParameters"></param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaActivityFeed" />
        /// </returns>
        public async Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityFeedAsync(
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var uri = UriCreator.GetFollowingRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, paginationParameters);
        }

        /// <summary>
        ///     Get feed of media your liked.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetLikedFeedAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaMediaList Convert(InstaMediaListResponse mediaListResponse)
                {
                    return ConvertersFabric.Instance.GetMediaListConverter(mediaListResponse).Convert();
                }
                var mediaResult = await GetAnyFeeds(UriCreator.GetUserLikeFeedUri(paginationParameters.NextMaxId));
                if (!mediaResult.Succeeded)
                {
                    if (mediaResult.Value != null)
                        return Result.Fail(mediaResult.Info, Convert(mediaResult.Value));
                    else
                        return Result.Fail(mediaResult.Info, default(InstaMediaList));
                }
                var mediaResponse = mediaResult.Value;
                var mediaList = Convert(mediaResponse);
                mediaList.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId;
                paginationParameters.PagesLoaded++;
                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var result = await GetAnyFeeds(UriCreator.GetUserLikeFeedUri(paginationParameters.NextMaxId));
                    if (!result.Succeeded)
                        return Result.Fail(result.Info, mediaList);

                    var convertedResult = Convert(result.Value);
                    paginationParameters.PagesLoaded++;
                    mediaList.NextMaxId = paginationParameters.NextMaxId = result.Value.NextMaxId;
                    mediaResponse.MoreAvailable = result.Value.MoreAvailable;
                    mediaResponse.ResultsCount += result.Value.ResultsCount;
                    mediaResponse.TotalCount += result.Value.TotalCount;
                    mediaList.AddRange(convertedResult);
                }

                mediaList.PageSize = mediaResponse.ResultsCount;
                mediaList.Pages = paginationParameters.PagesLoaded;
                return Result.Success(mediaList);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMediaList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMediaList>(exception);
            }
        }

        /// <summary>
        ///     Get recent activity info asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaActivityFeed" />
        /// </returns>
        public async Task<IResult<InstaActivityFeed>> GetRecentActivityFeedAsync(
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var uri = UriCreator.GetRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, paginationParameters);
        }


        /// <summary>
        ///     Get saved media feeds asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetSavedFeedAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaMediaList Convert(InstaMediaListResponse mediaListResponse)
                {
                    return ConvertersFabric.Instance.GetMediaListConverter(mediaListResponse).Convert();
                }
                var mediaFeedsResult = await GetAnyFeeds(UriCreator.GetSavedFeedUri(paginationParameters?.NextMaxId));
                if (!mediaFeedsResult.Succeeded)
                {
                    if (mediaFeedsResult.Value != null)
                        return Result.Fail(mediaFeedsResult.Info, Convert(mediaFeedsResult.Value));
                    else
                        return Result.Fail(mediaFeedsResult.Info, default(InstaMediaList));
                }
                var mediaResponse = mediaFeedsResult.Value;
                paginationParameters.NextMaxId = mediaResponse.NextMaxId;
                paginationParameters.PagesLoaded++;
                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var result = await GetAnyFeeds(UriCreator.GetSavedFeedUri(paginationParameters?.NextMaxId));
                    if (!result.Succeeded)
                        return Result.Fail(result.Info, Convert(mediaResponse));

                    mediaResponse.NextMaxId = paginationParameters.NextMaxId = result.Value.NextMaxId;
                    mediaResponse.MoreAvailable = result.Value.MoreAvailable;
                    mediaResponse.AutoLoadMoreEnabled = result.Value.AutoLoadMoreEnabled;
                    mediaResponse.ResultsCount += result.Value.ResultsCount;
                    mediaResponse.TotalCount += result.Value.TotalCount;
                    mediaResponse.Medias.AddRange(result.Value.Medias);
                    paginationParameters.PagesLoaded++;
                }
                var mediaList = Convert(mediaResponse);
                mediaList.PageSize = mediaResponse.ResultsCount;
                mediaList.Pages = paginationParameters.PagesLoaded;
                return Result.Success(mediaList);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMediaList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMediaList>(exception);
            }
        }

        /// <summary>
        ///     Get tag feed by tag value asynchronously
        /// </summary>
        /// <param name="tag">Tag value</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaTagFeed" />
        /// </returns>
        public async Task<IResult<InstaTagFeed>> GetTagFeedAsync(string tag, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var tagFeed = new InstaTagFeed();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                
                InstaTagFeed Convert(InstaTagFeedResponse instaTagFeedResponse)
                {
                    return ConvertersFabric.Instance.GetTagFeedConverter(instaTagFeedResponse).Convert();
                }

                var tags = await GetTagFeed(tag, paginationParameters);
                if (!tags.Succeeded)
                {
                    if (tags.Value != null)
                        return Result.Fail(tags.Info, Convert(tags.Value));
                    else
                        return Result.Fail(tags.Info, default(InstaTagFeed));
                }
                var feedResponse = tags.Value;

                tagFeed = Convert(feedResponse);

                paginationParameters.NextMaxId = feedResponse.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (feedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFeed = await GetTagFeed(tag, paginationParameters);
                    if (!nextFeed.Succeeded)
                        return Result.Fail(nextFeed.Info, tagFeed);

                    var convertedFeeds = Convert(nextFeed.Value);
                    tagFeed.NextMaxId = paginationParameters.NextMaxId = nextFeed.Value.NextMaxId;
                    tagFeed.Medias.AddRange(convertedFeeds.Medias);
                    tagFeed.Stories.AddRange(convertedFeeds.Stories);
                    feedResponse.MoreAvailable = nextFeed.Value.MoreAvailable;
                    paginationParameters.PagesLoaded++;
                }
                return Result.Success(tagFeed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTagFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, tagFeed);
            }
        }
        /// <summary>
        ///     Get user timeline feed (feed of recent posts from users you follow) asynchronously.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="seenMediaIds">Id of the posts seen till now</param>
        /// <param name="refreshRequest">Request refresh feeds</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        public async Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(PaginationParameters paginationParameters, string[] seenMediaIds = null, bool refreshRequest = false)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var feed = new InstaFeed();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaFeed Convert(InstaFeedResponse instaFeedResponse)
                {
                    return ConvertersFabric.Instance.GetFeedConverter(instaFeedResponse).Convert();
                }
                var timelineFeeds = await GetUserTimelineFeed(paginationParameters, seenMediaIds, refreshRequest);
                if (!timelineFeeds.Succeeded)
                    return Result.Fail(timelineFeeds.Info, feed);

                var feedResponse = timelineFeeds.Value;

                feed = Convert(feedResponse);
                paginationParameters.NextMaxId = feed.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (feedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFeed = await GetUserTimelineFeed(paginationParameters);
                    if (!nextFeed.Succeeded)
                        return Result.Fail(nextFeed.Info, feed);

                    var convertedFeed = Convert(nextFeed.Value);
                    feed.Medias.AddRange(convertedFeed.Medias);
                    feed.Stories.AddRange(convertedFeed.Stories);
                    feedResponse.MoreAvailable = nextFeed.Value.MoreAvailable;
                    paginationParameters.NextMaxId = nextFeed.Value.NextMaxId;
                    paginationParameters.PagesLoaded++;
                }
                
                return Result.Success(feed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, feed);
            }
        }

        /// <summary>
        ///     Get user topical explore feeds asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="clusterId">Cluster id</param>
        /// <returns><see cref="InstaTopicalExploreFeed" /></returns>
        public async Task<IResult<InstaTopicalExploreFeed>> GetTopicalExploreFeedAsync(PaginationParameters paginationParameters, string clusterId = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var topicalExploreFeed = new InstaTopicalExploreFeed();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaTopicalExploreFeed Convert(InstaTopicalExploreFeedResponse topicalExploreFeedResponse)
                {
                    return ConvertersFabric.Instance.GetTopicalExploreFeedConverter(topicalExploreFeedResponse).Convert();
                }

                var feeds = await GetTopicalExploreFeed(paginationParameters, clusterId);
                if (!feeds.Succeeded)
                {
                    if (feeds.Value != null)
                        return Result.Fail(feeds.Info, Convert(feeds.Value));
                    else
                        return Result.Fail(feeds.Info, (InstaTopicalExploreFeed)null);
                }
                var feedResponse = feeds.Value;
                paginationParameters.NextMaxId = feedResponse.MaxId;
                paginationParameters.RankToken = feedResponse.RankToken;

                while (feedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFeed = await GetTopicalExploreFeed(paginationParameters, clusterId);
                    if (!nextFeed.Succeeded)
                        return Result.Fail(nextFeed.Info, Convert(feeds.Value));

                    feedResponse.NextMaxId = paginationParameters.NextMaxId = nextFeed.Value.MaxId;
                    feedResponse.RankToken = paginationParameters.RankToken = nextFeed.Value.RankToken;
                    feedResponse.MoreAvailable = nextFeed.Value.MoreAvailable;
                    feedResponse.AutoLoadMoreEnabled = nextFeed.Value.AutoLoadMoreEnabled;
                    feedResponse.NextMaxId = nextFeed.Value.NextMaxId;
                    feedResponse.ResultsCount = nextFeed.Value.ResultsCount;
                    feedResponse.Channel = nextFeed.Value.Channel;
                    feedResponse.Medias.AddRange(nextFeed.Value.Medias);
                    feedResponse.TVChannels.AddRange(nextFeed.Value.TVChannels);
                    feedResponse.Clusters.AddRange(nextFeed.Value.Clusters);
                    feedResponse.MaxId = nextFeed.Value.MaxId;
                    feedResponse.HasShoppingChannelContent = nextFeed.Value.HasShoppingChannelContent;
                    paginationParameters.PagesLoaded++;
                }
                topicalExploreFeed = Convert(feedResponse);
                topicalExploreFeed.Medias.Pages = paginationParameters.PagesLoaded;
                topicalExploreFeed.Medias.PageSize = feedResponse.ResultsCount;
                return Result.Success(topicalExploreFeed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTopicalExploreFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, topicalExploreFeed);
            }
        }






        private async Task<IResult<InstaRecentActivityResponse>> GetFollowingActivityWithMaxIdAsync(string maxId)
        {
            try
            {
                var uri = UriCreator.GetFollowingRecentActivityUri(maxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaRecentActivityResponse>(response, json);
                var followingActivity = JsonConvert.DeserializeObject<InstaRecentActivityResponse>(json,
                    new InstaRecentActivityConverter());
                return Result.Success(followingActivity);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaRecentActivityResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaRecentActivityResponse>(exception);
            }
        }

        private async Task<IResult<InstaActivityFeed>> GetRecentActivityInternalAsync(Uri uri,
            PaginationParameters paginationParameters)
        {
            try
            {
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                var activityFeed = new InstaActivityFeed();
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaActivityFeed>(response, json);
                var feedPage = JsonConvert.DeserializeObject<InstaRecentActivityResponse>(json,
                    new InstaRecentActivityConverter());
                activityFeed.IsOwnActivity = feedPage.IsOwnActivity;
                var nextId = feedPage.NextMaxId;
                activityFeed.Items.AddRange(
                    feedPage.Stories.Select(ConvertersFabric.Instance.GetSingleRecentActivityConverter)
                        .Select(converter => converter.Convert()));
                paginationParameters.PagesLoaded++;
                activityFeed.NextMaxId = paginationParameters.NextMaxId = feedPage.NextMaxId;
                while (!string.IsNullOrEmpty(nextId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFollowingFeed = await GetFollowingActivityWithMaxIdAsync(nextId);
                    if (!nextFollowingFeed.Succeeded)
                        return Result.Fail(nextFollowingFeed.Info, activityFeed);
                    nextId = nextFollowingFeed.Value.NextMaxId;
                    activityFeed.Items.AddRange(
                        feedPage.Stories.Select(ConvertersFabric.Instance.GetSingleRecentActivityConverter)
                            .Select(converter => converter.Convert()));
                    paginationParameters.PagesLoaded++;
                    activityFeed.NextMaxId = paginationParameters.NextMaxId = nextId;
                }

                return Result.Success(activityFeed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaActivityFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaActivityFeed>(exception);
            }
        }

        private async Task<IResult<InstaExploreFeedResponse>> GetExploreFeed(PaginationParameters paginationParameters)
        {
            try
            {
                var exploreUri = UriCreator.GetExploreUri(paginationParameters.NextMaxId, paginationParameters.RankToken);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, exploreUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaExploreFeedResponse>(response, json);
                var feedResponse = JsonConvert.DeserializeObject<InstaExploreFeedResponse>(json,
                    new InstaExploreFeedDataConverter());
                return Result.Success(feedResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaExploreFeedResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaExploreFeedResponse>(exception);
            }
        }

        private async Task<IResult<InstaFeedResponse>> GetUserTimelineFeed(PaginationParameters paginationParameters, string[] seenMediaIds = null, bool refreshRequest = false)
        {
            try
            {
                var userFeedUri = UriCreator.GetUserFeedUri(paginationParameters?.NextMaxId);

                var data = new Dictionary<string, string>
                {
                    {"is_prefetch", "0"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"device_id", _deviceInfo.PhoneGuid.ToString()},
                    {"phone_id", _deviceInfo.RankToken.ToString()},
                    {"client_session_id", Guid.NewGuid().ToString()},
                    {"timezone_offset", _instaApi.GetTimezoneOffset().ToString()},
                    {"rti_delivery_backend", "0"}
                };

                if (seenMediaIds != null)
                    data.Add("seen_posts", seenMediaIds.EncodeList(false));

                if (refreshRequest)
                {
                    data.Add("reason", "pull_to_refresh");
                    data.Add("is_pull_to_refresh", "1");
                }
                else
                    data.Add("reason", "warm_start_fetch");

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, userFeedUri, _deviceInfo, data);
                request.Headers.Add("X-Ads-Opt-Out", "0");
                request.Headers.Add("X-Google-AD-ID", _deviceInfo.GoogleAdId.ToString());
                request.Headers.Add("X-DEVICE-ID", _deviceInfo.DeviceGuid.ToString());

                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFeedResponse>(response, json);

                var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json,
                    new InstaFeedResponseDataConverter());
                return Result.Success(feedResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFeedResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, default(InstaFeedResponse));
            }
        }
        
        private async Task<IResult<InstaTagFeedResponse>> GetTagFeed(string tag, PaginationParameters paginationParameters)
        {
            try
            {
                var userFeedUri = UriCreator.GetTagFeedUri(tag, paginationParameters?.NextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTagFeedResponse>(response, json);
                var feedResponse = JsonConvert.DeserializeObject<InstaTagFeedResponse>(json,
                    new InstaTagFeedDataConverter());
                return Result.Success(feedResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTagFeedResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, default(InstaTagFeedResponse));
            }
        }

        private async Task<IResult<InstaMediaListResponse>> GetAnyFeeds(Uri instaUri)
        {
            try
            {
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaListResponse>(response, json);

                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                
                return Result.Success(mediaResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMediaListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMediaListResponse>(exception);
            }
        }

        private async Task<IResult<InstaTopicalExploreFeedResponse>> GetTopicalExploreFeed(PaginationParameters paginationParameters, string clusterId)
        {
            try
            {
                if (string.IsNullOrEmpty(clusterId))
                    clusterId = "explore_all:0";

                var exploreUri = UriCreator.GetTopicalExploreUri(_deviceInfo.GoogleAdId.ToString(), paginationParameters?.NextMaxId, clusterId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, exploreUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTopicalExploreFeedResponse>(response, json);

                var feedResponse = JsonConvert.DeserializeObject<InstaTopicalExploreFeedResponse>(json,
                    new InstaTopicalExploreFeedDataConverter());

                return Result.Success(feedResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTopicalExploreFeedResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaTopicalExploreFeedResponse>(exception);
            }
        }
    }
}