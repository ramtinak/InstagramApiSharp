/*
* Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
*  
* IRANIAN DEVELOPERS
*/

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    internal class ReelProcessor : IReelProcessor
    {

        #region Fields and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        private readonly HttpHelper _httpHelper;

        public ReelProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        #endregion Properties and constructor


        public async Task<IResult<string>> GetClipsCreationInterestPickerAsync()
        {
            return await _instaApi.SendGetRequestAsync(new Uri("https://i.instagram.com/api/v1/clips/creation_interest_picker/"));
        }
        public async Task<IResult<string>> GetClipsInfoForCreationAsync(string mediaId = null)
        {
            if (mediaId == null)
                return await _instaApi.SendGetRequestAsync(new Uri("https://i.instagram.com/api/v1/clips/clips_info_for_creation/"));
            else
                return await _instaApi.SendGetRequestAsync(new Uri($"https://i.instagram.com/api/v1/clips/clips_info_for_creation/?m_pk={mediaId}"));
        }
        /// <summary>
        ///     Get user's reels clips (medias)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaReelsMediaList>> GetUserReelsClipsAsync(long userId,
            PaginationParameters paginationParameters) =>
            await GetReelsClips(paginationParameters, userId).ConfigureAwait(false);

        /// <summary>
        ///     Get user's reels clips (medias)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<IResult<InstaReelsMediaList>> GetUserReelsClipsAsync(long userId,
            PaginationParameters paginationParameters, CancellationToken cancellationToken) =>
            await GetReelsClips(paginationParameters, userId, cancellationToken).ConfigureAwait(false);

        /// <summary>
        ///     Mark reel feed as seen
        /// </summary>
        /// <param name="mediaPkImpression">Media pk (from <see cref="InstaMedia.Pk"/> )</param>
        public async Task<IResult<bool>> MarkReelAsSeenAsync(string mediaPkImpression)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMarkReelAsSeenUri();
                var impression = new JObject();

                if (!string.IsNullOrEmpty(mediaPkImpression))
                    impression.Add(mediaPkImpression);

                var data = new JObject
                {
                    {"impressions", impression.ToString(Formatting.None)},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };

                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                return obj.IsSucceed ? Result.Success(true) : Result.Fail<bool>(obj.Message);
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
        ///     Explore reel feeds
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaReelsMediaList>> GetReelsClipsAsync(PaginationParameters paginationParameters) =>
            await GetReelsClips(paginationParameters).ConfigureAwait(false);

        /// <summary>
        ///     Explore reel feeds
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<IResult<InstaReelsMediaList>> GetReelsClipsAsync(PaginationParameters paginationParameters,
            CancellationToken cancellationToken) =>
            await GetReelsClips(paginationParameters, null, cancellationToken).ConfigureAwait(false);


        private async Task<IResult<InstaReelsMediaList>> GetReelsClips(PaginationParameters paginationParameters,
            long? userId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var reelFeeds = new InstaReelsMediaList();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaReelsMediaList Convert(InstaReelsMediaListResponse instaReelFeedResponse)
                {
                    return ConvertersFabric.Instance.GetReelsMediaListConverter(instaReelFeedResponse).Convert();
                }
                var timelineFeeds = await GetReels(paginationParameters, userId);
                if (!timelineFeeds.Succeeded)
                    return Result.Fail(timelineFeeds.Info, reelFeeds);

                var reelFeedResponse = timelineFeeds.Value;

                reelFeeds = Convert(reelFeedResponse);
                paginationParameters.NextMaxId = reelFeeds.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (reelFeeds.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded <= paginationParameters.MaximumPagesToLoad)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var nextFeed = await GetReels(paginationParameters, userId);
                    if (!nextFeed.Succeeded)
                        return Result.Fail(nextFeed.Info, reelFeeds);

                    var convertedFeed = Convert(nextFeed.Value);
                    reelFeeds.Medias.AddRange(convertedFeed.Medias);
                    reelFeeds.MoreAvailable = nextFeed.Value.PagingInfo?.MoreAvailable ?? false;
                    reelFeeds.NextMaxId = paginationParameters.NextMaxId = nextFeed.Value.PagingInfo?.MaxId;
                    paginationParameters.PagesLoaded++;
                }

                return Result.Success(reelFeeds);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, reelFeeds, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, reelFeeds);
            }
        }

        private async Task<IResult<InstaReelsMediaListResponse>> GetReels(PaginationParameters paginationParameters,
            long? userId = null)
        {
            try
            {
                Uri instaUri;
                Dictionary<string, string> data;
                if (userId.HasValue)
                {
                    instaUri = UriCreator.GetUserReelsClipsUri();
                    data = new Dictionary<string, string>
                    {
                        {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                        {"target_user_id", userId.ToString()},
                    };
                }
                else
                {
                    instaUri = UriCreator.GetReelsClipsUri();

                    var sessionInfo = new JObject
                    {
                        {"session_id", Guid.NewGuid().ToString()},
                        {"media_info", new JObject()},
                    };
                    data = new Dictionary<string, string>
                    {
                        {"seen_reels", "0"},
                        {"pct_reels", "0"},
                        {"tab_type", "clips_tab"},
                        {"session_info" ,sessionInfo.ToString(Formatting.None)},
                        {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    };
                }
                if (!string.IsNullOrEmpty(paginationParameters.NextMaxId))
                    data.Add("max_id", paginationParameters.NextMaxId);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaReelsMediaListResponse>(response, json);

                return Result.Success(JsonConvert.DeserializeObject<InstaReelsMediaListResponse>(json));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaReelsMediaListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, default(InstaReelsMediaListResponse));
            }
        }

    }
}
