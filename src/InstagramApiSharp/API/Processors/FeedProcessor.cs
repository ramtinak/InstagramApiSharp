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
                    return Result.Fail(feeds.Info, Convert(feeds.Value));
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
        public async Task<IResult<InstaMediaList>> GetLikeFeedAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var instaUri = UriCreator.GetUserLikeFeedUri(paginationParameters.NextMaxId);
            var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.UnExpectedResponse<InstaMediaList>(response, json);
            var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                new InstaMediaListDataConverter());

            var mediaList = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse).Convert();
            mediaList.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId;
            while (mediaResponse.MoreAvailable
                   && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                   && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
            {
                var result = await GetLikeFeedAsync(paginationParameters);
                if (!result.Succeeded)
                    return Result.Fail(result.Info, mediaList);

                paginationParameters.PagesLoaded++;
                mediaList.NextMaxId = paginationParameters.NextMaxId = result.Value.NextMaxId;
                mediaList.AddRange(result.Value);
            }

            mediaList.PageSize = mediaResponse.ResultsCount;
            mediaList.Pages = paginationParameters.PagesLoaded;
            return Result.Success(mediaList);
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
                var userFeedUri = UriCreator.GetTagFeedUri(tag, paginationParameters.NextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTagFeed>(response, json);
                var feedResponse = JsonConvert.DeserializeObject<InstaTagFeedResponse>(json,
                    new InstaTagFeedDataConverter());
                tagFeed = ConvertersFabric.Instance.GetTagFeedConverter(feedResponse).Convert();

                paginationParameters.NextMaxId = feedResponse.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (feedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFeed = await GetTagFeedAsync(tag, paginationParameters);
                    if (!nextFeed.Succeeded)
                        return Result.Fail(nextFeed.Info, tagFeed);
                    tagFeed.NextMaxId = paginationParameters.NextMaxId = nextFeed.Value.NextMaxId;
                    tagFeed.Medias.AddRange(nextFeed.Value.Medias);
                    tagFeed.Stories.AddRange(nextFeed.Value.Stories);
                }

                return Result.Success(tagFeed);
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
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        public async Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(PaginationParameters paginationParameters, string[] seenMediaIds = null, bool refreshRequest = false)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var feed = new InstaFeed();
            try
            {
                var userFeedUri = UriCreator.GetUserFeedUri(paginationParameters.NextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                if(seenMediaIds != null)
                {
                    string SeendStr = "";
                    for (int i = 0; i < seenMediaIds.Length; i++)
                    {
                        if(i < (seenMediaIds.Length -1))
                        {
                            SeendStr += seenMediaIds[i] + ",";
                        }
                        else
                        {
                            SeendStr += seenMediaIds[i];
                        }
                    }
                    request.Headers.Add("seen_posts", SeendStr);
                }
                if (refreshRequest)
                {
                    request.Headers.Add("reason", "pull_to_refresh");
                    request.Headers.Add("is_pull_to_refresh", "1");
                }
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFeed>(response, json);

                var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json,
                    new InstaFeedResponseDataConverter());
                feed = ConvertersFabric.Instance.GetFeedConverter(feedResponse).Convert();
                paginationParameters.NextMaxId = feed.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (feedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFeed = await GetUserTimelineFeedAsync(paginationParameters);
                    if (!nextFeed.Succeeded)
                        return Result.Fail(nextFeed.Info, feed);

                    feed.Medias.AddRange(nextFeed.Value.Medias);
                    feed.Stories.AddRange(nextFeed.Value.Stories);

                    paginationParameters.NextMaxId = nextFeed.Value.NextMaxId;
                    paginationParameters.PagesLoaded++;
                }

                return Result.Success(feed);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, feed);
            }
        }
        private async Task<IResult<InstaRecentActivityResponse>> GetFollowingActivityWithMaxIdAsync(string maxId)
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

        private async Task<IResult<InstaActivityFeed>> GetRecentActivityInternalAsync(Uri uri,
            PaginationParameters paginationParameters)
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaExploreFeedResponse>(exception);
            }
        }
    }
}