using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    ///     Story api functions.
    /// </summary>
    internal class StoryProcessor : IStoryProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public StoryProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        ///     Respond to an story question
        /// </summary>
        /// <param name="storyId">Story id (<see cref="InstaStoryItem.Id"/>)</param>
        /// <param name="questionId">Question id (<see cref="InstaStoryQuestionStickerItem.QuestionId"/>)</param>
        /// <param name="responseText">Text to respond</param>
        public async Task<IResult<bool>> AnswerToStoryQuestionAsync(string storyId, long questionId, string responseText)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetStoryQuestionResponseUri(storyId, questionId);
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"response", responseText ?? string.Empty},
                    {"type", "text"}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, obj.Message, null);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, false, ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex, false);
            }
        }

        /// <summary>
        ///     Append to existing highlight
        /// </summary>
        /// <param name="highlightId">Highlight id</param>
        /// <param name="mediaId">Media id (CoverMedia.MediaId)</param>
        public async Task<IResult<bool>> AppendToHighlightFeedAsync(string highlightId, string mediaId)
        {
            return await AppendOrDeleteHighlight(highlightId, mediaId, false);
        }

        /// <summary>
        ///     Create new highlight
        /// </summary>
        /// <param name="mediaId">Story media id</param>
        /// <param name="title">Highlight title</param>
        /// <param name="cropWidth">Crop width It depends on the aspect ratio/size of device display and the aspect ratio of story uploaded. must be in a range of 0-1, i.e: 0.19545822</param>
        /// <param name="cropHeight">Crop height It depends on the aspect ratio/size of device display and the aspect ratio of story uploaded. must be in a range of 0-1, i.e: 0.8037307</param>
        public async Task<IResult<InstaHighlightFeed>> CreateHighlightFeedAsync(string mediaId, string title, float cropWidth, float cropHeight)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var cover = new JObject
                {
                    {"media_id", mediaId},
                    {"crop_rect", new JArray { 0.0, cropWidth, 1.0, cropHeight }.ToString(Formatting.None) }
                }.ToString(Formatting.None);
                var data = new JObject
                {
                    {"source", "self_profile"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"cover", cover},
                    {"title", title},
                    {"media_ids", $"[{ExtensionHelper.EncodeList(new[] { mediaId })}]"}
                };

                var instaUri = UriCreator.GetHighlightCreateUri();
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<InstaHighlightFeed>(response, json);

                var highlightFeedResponse = JsonConvert.DeserializeObject<InstaHighlightReelResponse>(json,
                    new InstaHighlightReelDataConverter());
                var highlightStoryFeed = ConvertersFabric.Instance.GetHighlightReelConverter(highlightFeedResponse).Convert();
                return Result.Success(highlightStoryFeed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHighlightFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHighlightFeed>(exception);
            }
        }

        /// <summary>
        ///     Delete highlight feed
        /// </summary>
        /// <param name="highlightId">Highlight id</param>
        /// <param name="mediaId">Media id (CoverMedia.MediaId)</param>
        public async Task<IResult<bool>> DeleteHighlightFeedAsync(string highlightId, string mediaId)
        {
            return await AppendOrDeleteHighlight(highlightId, mediaId, true);
        }

        /// <summary>
        ///     Delete a media story (photo or video)
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="sharingType">The type of the media</param>
        /// <returns>Return true if the story media is deleted</returns>
        public async Task<IResult<bool>> DeleteStoryAsync(string storyMediaId, InstaSharingType sharingType = InstaSharingType.Video)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var deleteMediaUri = UriCreator.GetDeleteStoryMediaUri(storyMediaId, sharingType);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_id", storyMediaId}
                };

                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, deleteMediaUri, _deviceInfo, data);
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
        ///     Follow countdown stories
        /// </summary>
        /// <param name="countdownId">Countdown id (<see cref="InstaStoryCountdownStickerItem.CountdownId"/>)</param>
        public async Task<IResult<bool>> FollowCountdownStoryAsync(long countdownId)
        {
            return await FollowUnfollowCountdown(UriCreator.GetStoryFollowCountdownUri(countdownId));
        }

        /// <summary>
        ///     Get list of users that blocked from seeing your stories
        /// </summary>
        public async Task<IResult<InstaUserShortList>> GetBlockedUsersFromStoriesAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var list = new InstaUserShortList();
            try
            {
                var instaUri = UriCreator.GetBlockedStoriesUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserShortList>(response, json);

                var usersResponse = JsonConvert.DeserializeObject<InstaUserListShortResponse>(json);
                list.AddRange(
                    usersResponse.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                return Result.Success(list);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, list, ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex, list);
            }
        }

        /// <summary>
        ///     Get stories countdowns for self accounts
        /// </summary>
        public async Task<IResult<InstaStoryCountdownList>> GetCountdownsStoriesAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetStoryCountdownMediaUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStoryCountdownList>(response, json);
                var countdownListResponse = JsonConvert.DeserializeObject<InstaStoryCountdownListResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetStoryCountdownListConverter(countdownListResponse).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryCountdownList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryCountdownList>(exception);
            }
        }

        /// <summary>
        ///     Get user highlight feeds by user id (pk)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<InstaHighlightFeeds>> GetHighlightFeedsAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetHighlightFeedsUri(userId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<InstaHighlightFeeds>(response, json);
                var highlightFeedResponse = JsonConvert.DeserializeObject<InstaHighlightFeedsResponse>(json);
                var highlightStoryFeed = ConvertersFabric.Instance.GetHighlightFeedsConverter(highlightFeedResponse).Convert();
                return Result.Success(highlightStoryFeed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHighlightFeeds), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHighlightFeeds>(exception);
            }
        }

        /// <summary>
        ///     Get user highlights archive
        ///     <para>Note: Use <see cref="IStoryProcessor.GetHighlightsArchiveMediasAsync(string)"/> to get hightlight medias of an specific day.</para>
        /// </summary>
        public async Task<IResult<InstaHighlightShortList>> GetHighlightsArchiveAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetHighlightsArchiveUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<InstaHighlightShortList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaHighlightShortListResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetHighlightShortListConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHighlightShortList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHighlightShortList>(exception);
            }
        }

        /// <summary>
        ///     Get highlights archive medias
        ///     <para>Note: get highlight id from <see cref="IStoryProcessor.GetHighlightsArchiveAsync"/></para>
        /// </summary>
        /// <param name="highlightId">Highlight id (Get it from <see cref="IStoryProcessor.GetHighlightsArchiveAsync"/>)</param>
        public async Task<IResult<InstaHighlightSingleFeed>> GetHighlightsArchiveMediasAsync(string highlightId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (string.IsNullOrEmpty(highlightId))
                    throw new ArgumentNullException("highlightId cannot be null or empty");

                var instaUri = UriCreator.GetReelMediaUri();

                var data = new JObject
                {
                    {InstaApiConstants.SUPPORTED_CAPABALITIES_HEADER, InstaApiConstants.SupportedCapabalities.ToString(Formatting.None)},
                    {"source", "reel_highlights_gallery"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"user_ids", new JArray(highlightId)}
                };

                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<InstaHighlightSingleFeed>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaHighlightReelResponse>(json,
                    new InstaHighlightReelsListDataConverter());

                return obj?.Reel != null ? Result.Success(ConvertersFabric.Instance.GetHighlightReelConverter(obj).Convert()) : Result.Fail<InstaHighlightSingleFeed>("No reels found");
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHighlightSingleFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHighlightSingleFeed>(exception);
            }
        }

        /// <summary>
        ///     Get single highlight medias
        ///     <para>Note: get highlight id from <see cref="IStoryProcessor.GetHighlightFeedsAsync(long)"/></para>
        /// </summary>
        /// <param name="highlightId">Highlight id (Get it from <see cref="IStoryProcessor.GetHighlightFeedsAsync(long)"/>)</param>
        public async Task<IResult<InstaHighlightSingleFeed>> GetHighlightMediasAsync(string highlightId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (string.IsNullOrEmpty(highlightId))
                    throw new ArgumentNullException("highlightId cannot be null or empty");

                var instaUri = UriCreator.GetReelMediaUri();
                var data = new JObject
                {
                    {InstaApiConstants.SUPPORTED_CAPABALITIES_HEADER, InstaApiConstants.SupportedCapabalities.ToString(Formatting.None)},
                    {"source", "profile"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"user_ids", new JArray(highlightId)}
                };

                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<InstaHighlightSingleFeed>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaHighlightReelResponse>(json,
                    new InstaHighlightReelsListDataConverter());

                return obj?.Reel != null ? Result.Success(ConvertersFabric.Instance.GetHighlightReelConverter(obj).Convert()) : Result.Fail<InstaHighlightSingleFeed>("No reels found");
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaHighlightSingleFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHighlightSingleFeed>(exception);
            }
        }

        /// <summary>
        ///     Get user story feed (stories from users followed by current user).
        /// </summary>
        public async Task<IResult<InstaStoryFeed>> GetStoryFeedAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            { 
                var storyFeedUri = UriCreator.GetStoryFeedUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, storyFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<InstaStoryFeed>(response, json);
                var storyFeedResponse = JsonConvert.DeserializeObject<InstaStoryFeedResponse>(json);
                var instaStoryFeed = ConvertersFabric.Instance.GetStoryFeedConverter(storyFeedResponse).Convert();
                return Result.Success(instaStoryFeed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryFeed>(exception);
            }
        }
        /// <summary>
        ///     Get story media viewers
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="paginationParameters">Pagination parameters</param>
        public async Task<IResult<InstaReelStoryMediaViewers>> GetStoryMediaViewersAsync(string storyMediaId, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaReelStoryMediaViewers Convert(InstaReelStoryMediaViewersResponse reelResponse)
                {
                    return ConvertersFabric.Instance.GetReelStoryMediaViewersConverter(reelResponse).Convert();
                }

                var storyMediaViewersResult = await GetStoryMediaViewers(storyMediaId, paginationParameters?.NextMaxId);

                if (!storyMediaViewersResult.Succeeded)
                    return Result.Fail(storyMediaViewersResult.Info, default(InstaReelStoryMediaViewers));

                var storyMediaViewersResponse = storyMediaViewersResult.Value;
                paginationParameters.NextMaxId = storyMediaViewersResponse.NextMaxId;

                while (!string.IsNullOrEmpty(paginationParameters.NextMaxId)
                    && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    paginationParameters.PagesLoaded++;
                    var nextStoryViewers = await GetStoryMediaViewers(storyMediaId, paginationParameters.NextMaxId);
                    if (!nextStoryViewers.Succeeded)
                        return Result.Fail(nextStoryViewers.Info, Convert(nextStoryViewers.Value));
                    storyMediaViewersResponse.NextMaxId = paginationParameters.NextMaxId = nextStoryViewers.Value.NextMaxId;
                    storyMediaViewersResponse.Users.AddRange(nextStoryViewers.Value.Users);
                }

                return Result.Success(Convert(storyMediaViewersResponse));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaReelStoryMediaViewers), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaReelStoryMediaViewers>(exception);
            }
        }

        /// <summary>
        ///     Get story poll voters
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="pollId">Story poll id</param>
        /// <param name="paginationParameters">Pagination parameters</param>
        public async Task<IResult<InstaStoryPollVotersList>> GetStoryPollVotersAsync(string storyMediaId, string pollId, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaStoryPollVotersList Convert(InstaStoryPollVotersListResponse storyVotersResponse)
                {
                    return ConvertersFabric.Instance.GetStoryPollVotersListConverter(storyVotersResponse).Convert();
                }

                var votersResult = await GetStoryPollVoters(storyMediaId, pollId, paginationParameters?.NextMaxId);

                if (!votersResult.Succeeded)
                    return Result.Fail(votersResult.Info, default(InstaStoryPollVotersList));

                var votersResponse = votersResult.Value;
                paginationParameters.NextMaxId = votersResponse.MaxId;

                while (votersResponse.MoreAvailable &&
                    !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                    && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    paginationParameters.PagesLoaded++;
                    var nextVoters = await GetStoryPollVoters(storyMediaId, pollId, paginationParameters.NextMaxId);
                    if (!nextVoters.Succeeded)
                        return Result.Fail(nextVoters.Info, Convert(nextVoters.Value));
                    votersResponse.MaxId = paginationParameters.NextMaxId = nextVoters.Value.MaxId;
                    votersResponse.Voters.AddRange(nextVoters.Value.Voters);
                    votersResponse.LatestPollVoteTime = nextVoters.Value.LatestPollVoteTime;
                    votersResponse.MoreAvailable = nextVoters.Value.MoreAvailable;
                }

                return Result.Success(Convert(votersResponse));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryPollVotersList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryPollVotersList>(exception);
            }
        }


        /// <summary>
        ///     Get the story by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        public async Task<IResult<InstaStory>> GetUserStoryAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userStoryUri = UriCreator.GetUserStoryUri(userId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userStoryUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK) Result.UnExpectedResponse<InstaStory>(response, json);
                var userStoryResponse = JsonConvert.DeserializeObject<InstaStoryResponse>(json);
                var userStory = ConvertersFabric.Instance.GetStoryConverter(userStoryResponse).Convert();
                return Result.Success(userStory);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStory), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStory>(exception);
            }
        }
        /// <summary>
        ///     Get user story reel feed. Contains user info last story including all story items.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        public async Task<IResult<InstaReelFeed>> GetUserStoryFeedAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var feed = new InstaReelFeed();
            try
            {
                var userFeedUri = UriCreator.GetUserReelFeedUri(userId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaReelFeed>(response, json);
                var feedResponse = JsonConvert.DeserializeObject<InstaReelFeedResponse>(json);
                feed = ConvertersFabric.Instance.GetReelFeedConverter(feedResponse).Convert();
                return Result.Success(feed);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaReelFeed), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, feed);
            }
        }

        /// <summary>
        ///     Seen story
        /// </summary>
        /// <param name="storyMediaId">Story media identifier</param>
        /// <param name="takenAtUnix">Taken at unix</param>
        public async Task<IResult<bool>> MarkStoryAsSeenAsync(string storyMediaId, long takenAtUnix)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSeenMediaStoryUri();
                var storyId = $"{storyMediaId}_{storyMediaId.Split('_')[1]}";
                var dateTimeUnix = DateTime.UtcNow.ToUnixTime();
                var reel = new JObject
                {
                    { storyId, new JArray($"{takenAtUnix}_{dateTimeUnix}") }
                };
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"container_module", "feed_timeline"},
                    {"live_vods_skipped", new JObject()},
                    {"nuxes_skipped", new JObject()},
                    {"nuxes", new JObject()},
                    {"reels", reel},
                    {"live_vods", new JObject()},
                    {"reel_media_skipped", new JObject()}
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
        ///     Seen highlight
        ///     <para>Get media id from <see cref="InstaHighlightFeed.CoverMedia.MediaId"/></para>
        /// </summary>
        /// <param name="mediaId">Media identifier (get it from <see cref="InstaHighlightFeed.CoverMedia.MediaId"/>)</param>
        /// <param name="highlightId">Highlight id</param>
        /// <param name="takenAtUnix">Taken at unix</param>
        public async Task<IResult<bool>> MarkHighlightAsSeenAsync(string mediaId, string highlightId, long takenAtUnix)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSeenMediaStoryUri();
                var reelId = $"{mediaId}_{highlightId}";
                var dateTimeUnix = DateTime.UtcNow.ToUnixTime();

                var reel = new JObject
                {
                    { reelId, new JArray($"{takenAtUnix}_{dateTimeUnix}") }
                };
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"container_module", "profile"},
                    {"live_vods_skipped", new JObject()},
                    {"nuxes_skipped", new JObject()},
                    {"nuxes", new JObject()},
                    {"reels", reel},
                    {"live_vods", new JObject()},
                    {"reel_media_skipped", new JObject()}
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
        ///     Share an media to story
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="mediaStoryUpload">
        ///     Media options
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </param>
        public async Task<IResult<InstaStoryMedia>> ShareMediaAsStoryAsync(InstaImage image, InstaMediaStoryUpload mediaStoryUpload)
        {
            return await ShareMediaAsStoryAsync(null, image, mediaStoryUpload);
        }

        /// <summary>
        ///     Share an media to story with progress
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="mediaStoryUpload">
        ///     Media options
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </param>
        public async Task<IResult<InstaStoryMedia>> ShareMediaAsStoryAsync(Action<InstaUploaderProgress> progress, InstaImage image,
            InstaMediaStoryUpload mediaStoryUpload)
        {
            if (image == null)
                return Result.Fail<InstaStoryMedia>("Image cannot be null");

            if (mediaStoryUpload == null)
                return Result.Fail<InstaStoryMedia>("Media story upload option cannot be null");

            return await UploadStoryPhotoWithUrlAsync(progress, image, string.Empty, null, new InstaStoryUploadOptions { MediaStory = mediaStoryUpload });
        }

        /// <summary>
        ///     Share story to someone
        /// </summary>
        /// <param name="reelId">Reel id</param>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="threadId">Thread id</param>
        /// <param name="text">Text to send (optional</param>
        /// <param name="sharingType">Sharing type</param>
        public async Task<IResult<InstaSharing>> ShareStoryAsync(string reelId, string storyMediaId, string threadId, string text, InstaSharingType sharingType = InstaSharingType.Video)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetStoryShareUri(sharingType.ToString().ToLower());
                var data = new JObject
                {
                    {"action", "send_item"},
                    {"thread_ids", $"[{threadId}]"},
                    {"unified_broadcast_format", "1"},
                    {"reel_id", reelId},
                    {"text", text ?? ""},
                    {"story_media_id", storyMediaId},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaSharing>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaSharing>(json);

                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSharing), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSharing>(exception);
            }
        }

        /// <summary>
        ///     Reply to story
        ///     <para>Note: Get story media id from <see cref="InstaMedia.InstaIdentifier"/></para>
        /// </summary>
        /// <param name="storyMediaId">Media id (get it from <see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <param name="userId">Story owner user pk (get it from <see cref="InstaMedia.User.Pk"/>)</param>
        /// <param name="text">Text to send</param>
        public async Task<IResult<bool>> ReplyToStoryAsync(string storyMediaId, long userId, string text)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetBroadcastReelShareUri();
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"recipient_users", $"[[{userId}]]"},
                    {"action", "send_item"},
                    {"client_context", clientContext},
                    {"media_id", storyMediaId},
                    {"_csrftoken", _user.CsrfToken},
                    {"text", text ?? string.Empty},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
        ///     UnFollow countdown stories
        /// </summary>
        /// <param name="countdownId">Countdown id (<see cref="InstaStoryCountdownStickerItem.CountdownId"/>)</param>
        public async Task<IResult<bool>> UnFollowCountdownStoryAsync(long countdownId)
        {
            return await FollowUnfollowCountdown(UriCreator.GetStoryUnFollowCountdownUri(countdownId));
        }

        /// <summary>
        ///     Upload story photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(InstaImage image, string caption,
            InstaStoryUploadOptions uploadOptions = null)
        {
            return await UploadStoryPhotoAsync(null, image, caption, uploadOptions);
        }
        /// <summary>
        ///     Upload story photo with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(Action<InstaUploaderProgress> progress, InstaImage image, string caption,
            InstaStoryUploadOptions uploadOptions = null)
        {
            return await UploadStoryPhotoWithUrlAsync(progress, image, caption, null, uploadOptions);
        }
        /// <summary>
        ///     Upload story photo with adding link address
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryPhotoWithUrlAsync(InstaImage image, string caption, Uri uri,
            InstaStoryUploadOptions uploadOptions = null)
        {
            return await UploadStoryPhotoWithUrlAsync(null, image, caption, uri, uploadOptions);
        }
        /// <summary>
        ///     Upload story photo with adding link address (with progress)
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryPhotoWithUrlAsync(Action<InstaUploaderProgress> progress,
            InstaImage image, string caption, Uri uri, InstaStoryUploadOptions uploadOptions = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var upProgress = new InstaUploaderProgress
            {
                Caption = caption ?? string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                if (uploadOptions?.Mentions?.Count > 0)
                {
                    var currentDelay = _instaApi.GetRequestDelay();
                    _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
                    foreach (var t in uploadOptions.Mentions)
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
                if(uploadOptions?.Questions?.Count > 0)
                {
                    try
                    {
                        bool tried = false;
                        var profilePicture = string.Empty;
                    TryToGetMyUser:
                        // get latest profile picture
                        var myUser = await _instaApi.UserProcessor.GetUserAsync(_user.UserName.ToLower());
                        if (!myUser.Succeeded)
                        {
                            if (!tried)
                            {
                                tried = true;
                                goto TryToGetMyUser;
                            }
                            else
                                profilePicture = _user.LoggedInUser.ProfilePicture;
                        }
                        else
                            profilePicture = myUser.Value.ProfilePicture;


                        foreach (var question in uploadOptions.Questions)
                            question.ProfilePicture = profilePicture;
                    }
                    catch { }
                }

                var uploadId = ApiRequestMessage.GenerateRandomUploadId();
                var photoHashCode = Path.GetFileName(image.Uri ?? $"C:\\{13.GenerateRandomString()}.jpg").GetHashCode();

                var waterfallId = Guid.NewGuid().ToString();

                var photoEntityName = $"{uploadId}_0_{photoHashCode}";
                var photoUri = UriCreator.GetStoryUploadPhotoUri(uploadId, photoHashCode);

                upProgress.UploadId = uploadId;
                progress?.Invoke(upProgress);
                var videoMediaInfoData = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"media_info", new JObject
                        {
                              {"capture_mode", "normal"},
                              {"media_type", 1},
                              {"caption", caption},
                              {"mentions", new JArray()},
                              {"hashtags", new JArray()},
                              {"locations", new JArray()},
                              {"stickers", new JArray()},
                        }
                    }
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, UriCreator.GetStoryMediaInfoUploadUri(), _deviceInfo, videoMediaInfoData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
               
                var photoUploadParamsObj = new JObject
                {
                    {"upload_id", uploadId},
                    {"media_type", "1"},
                    {"retry_context", "{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}"},

                    {"image_compression", "{\"lib_name\":\"moz\",\"lib_version\":\"3.1.m\",\"quality\":\"95\"}"}
                };
                var photoUploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                request = _httpHelper.GetDefaultRequest(HttpMethod.Get, photoUri, _deviceInfo);
                request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
                }

                upProgress.UploadState = InstaUploadState.Uploading;
                progress?.Invoke(upProgress);
                var imageBytes = image.ImageBytes ?? File.ReadAllBytes(image.Uri);
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                request = _httpHelper.GetDefaultRequest(HttpMethod.Post, photoUri, _deviceInfo);
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
                {
                    upProgress.UploadState = InstaUploadState.Uploaded;
                    progress?.Invoke(upProgress);
                    await Task.Delay(5000);
                    return await ConfigureStoryPhotoAsync(progress, upProgress, image, uploadId, caption, uri, uploadOptions);
                }
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception);
            }
        }

        /// <summary>
        ///     Upload story video (to self story)
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryVideoAsync(InstaVideoUpload video, string caption, InstaStoryUploadOptions uploadOptions = null)
        {
            return await UploadStoryVideoAsync(null, video, caption, uploadOptions);
        }

        /// <summary>
        ///     Upload story video (to self story) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, 
            string caption, InstaStoryUploadOptions uploadOptions = null)
        {
            return await UploadStoryVideoWithUrlAsync(progress, video, caption, null, uploadOptions);
        }

        /// <summary>
        ///     Upload story video (to self story)
        /// </summary>
        /// <param name="video">Video to upload</param>
        public async Task<IResult<bool>> UploadStoryVideoAsync(InstaVideoUpload video,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendVideoAsync(null, false, false, "", InstaViewMode.Replayable, storyType, null, threadIds.EncodeList(), video, null, uploadOptions);
        }

        /// <summary>
        ///     Upload story video (to self story) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<bool>> UploadStoryVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendVideoAsync(progress, false, false, "", InstaViewMode.Replayable, storyType, null, threadIds.EncodeList(), video, null, uploadOptions);
        }

        /// <summary>
        ///     Upload story video (to self story) with adding link address
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryVideoWithUrlAsync(InstaVideoUpload video, string caption, Uri uri,
            InstaStoryUploadOptions uploadOptions = null)
        {
            return await UploadStoryVideoWithUrlAsync(null, video, caption, uri, uploadOptions);
        }

        /// <summary>
        ///     Upload story video (to self story) with adding link address (with progress)
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<InstaStoryMedia>> UploadStoryVideoWithUrlAsync(Action<InstaUploaderProgress> progress,
            InstaVideoUpload video, string caption, Uri uri, InstaStoryUploadOptions uploadOptions = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var upProgress = new InstaUploaderProgress
            {
                Caption = caption ?? string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                var uploadId = ApiRequestMessage.GenerateRandomUploadId();
                var videoHashCode = Path.GetFileName(video.Video.Uri ?? $"C:\\{13.GenerateRandomString()}.mp4").GetHashCode();
                var photoHashCode = Path.GetFileName(video.VideoThumbnail.Uri ?? $"C:\\{13.GenerateRandomString()}.jpg").GetHashCode();

                var waterfallId = Guid.NewGuid().ToString();

                var videoEntityName = $"{uploadId}_0_{videoHashCode}";
                var videoUri = UriCreator.GetStoryUploadVideoUri(uploadId, videoHashCode);

                var photoEntityName = $"{uploadId}_0_{photoHashCode}";
                var photoUri = UriCreator.GetStoryUploadPhotoUri(uploadId, photoHashCode);

                upProgress.UploadId = uploadId;
                progress?.Invoke(upProgress);
                var videoMediaInfoData = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"media_info", new JObject
                        {
                              {"capture_mode", "normal"},
                              {"media_type", 2},
                              {"caption", caption},
                              {"mentions", new JArray()},
                              {"hashtags", new JArray()},
                              {"locations", new JArray()},
                              {"stickers", new JArray()},
                        }
                    }
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, UriCreator.GetStoryMediaInfoUploadUri(), _deviceInfo, videoMediaInfoData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                var videoUploadParamsObj = new JObject
                {
                    {"upload_media_height", "0"},
                    {"upload_media_width", "0"},
                    {"upload_media_duration_ms", "46000"},
                    {"upload_id", uploadId},
                    {"for_album", "1"},
                    {"retry_context", "{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}"},
                    {"media_type", "2"},
                };
                var videoUploadParams = JsonConvert.SerializeObject(videoUploadParamsObj);
                request = _httpHelper.GetDefaultRequest(HttpMethod.Get, videoUri, _deviceInfo);
                request.Headers.Add("X_FB_VIDEO_WATERFALL_ID", waterfallId);
                request.Headers.Add("X-Instagram-Rupload-Params", videoUploadParams);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
                }


                var videoBytes = video.Video.VideoBytes ?? File.ReadAllBytes(video.Video.Uri);
                var videoContent = new ByteArrayContent(videoBytes);
                videoContent.Headers.Add("Content-Transfer-Encoding", "binary");
                videoContent.Headers.Add("Content-Type", "application/octet-stream");
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
                    request.Headers.Add("X-Entity-Type", "image/quicktime");
                else
                    request.Headers.Add("X-Entity-Type", "image/mp4");
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
                    return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
                }
                upProgress.UploadState = InstaUploadState.Uploaded;
                progress?.Invoke(upProgress);
                var photoUploadParamsObj = new JObject
                {
                    {"retry_context", "{\"num_step_auto_retry\":0,\"num_reupload\":0,\"num_step_manual_retry\":0}"},
                    {"media_type", "2"},
                    {"upload_id", uploadId},
                    {"image_compression", "{\"lib_name\":\"moz\",\"lib_version\":\"3.1.m\",\"quality\":\"95\"}"},
                };
                var photoUploadParams = JsonConvert.SerializeObject(photoUploadParamsObj);
                request = _httpHelper.GetDefaultRequest(HttpMethod.Get, photoUri, _deviceInfo);
                request.Headers.Add("X_FB_PHOTO_WATERFALL_ID", waterfallId);
                request.Headers.Add("X-Instagram-Rupload-Params", photoUploadParams);
                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
                }

                upProgress.UploadState = InstaUploadState.UploadingThumbnail;
                progress?.Invoke(upProgress);
                var imageBytes = video.VideoThumbnail.ImageBytes ?? File.ReadAllBytes(video.VideoThumbnail.Uri);
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                request = _httpHelper.GetDefaultRequest(HttpMethod.Post, photoUri, _deviceInfo);
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
                {
                    //upProgress = progressContent?.UploaderProgress;
                    upProgress.UploadState = InstaUploadState.ThumbnailUploaded;
                    progress?.Invoke(upProgress);
                    await Task.Delay(30000);
                    return await ConfigureStoryVideoAsync(progress, upProgress, video, uploadId, caption, uri, uploadOptions);
                }
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception);
            }
        }

        /// <summary>
        ///     Upload story video [to self story, to direct threads or both(self and direct)] with adding link address
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="storyType">Story type</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<bool>> UploadStoryVideoWithUrlAsync(InstaVideoUpload video, Uri uri,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendVideoAsync(null, false, false, "", InstaViewMode.Replayable, storyType, null, threadIds.EncodeList(), video, uri, uploadOptions);
        }

        /// <summary>
        ///     Upload story video (to self story) with adding link address (with progress)
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="storyType">Story type</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        public async Task<IResult<bool>> UploadStoryVideoWithUrlAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, Uri uri,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendVideoAsync(progress, false, false, "", InstaViewMode.Replayable, storyType, null, threadIds.EncodeList(), video, uri, uploadOptions);
        }

        /// <summary>
        ///     Validate url for adding to story link
        /// </summary>
        /// <param name="url">Url address</param>
        public async Task<IResult<bool>> ValidateUrlAsync(string url)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (string.IsNullOrEmpty(url))
                    return Result.Fail("Url cannot be null or empty.", false);

                var instaUri = UriCreator.GetValidateReelLinkAddressUri();
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"url", url},
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
        ///     Vote to an story poll
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="pollId">Story poll id</param>
        /// <param name="pollVote">Your poll vote</param>
        public async Task<IResult<InstaStoryItem>> VoteStoryPollAsync(string storyMediaId, string pollId, InstaStoryPollVoteType pollVote)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetStoryPollVoteUri(storyMediaId, pollId);
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"radio_type", "wifi-none"},
                    {"vote", ((int)pollVote).ToString()},
                };

                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStoryItem>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaReelStoryMediaViewersResponse>(json);
                var covertedObj = ConvertersFabric.Instance.GetReelStoryMediaViewersConverter(obj).Convert();

                return Result.Success(covertedObj.UpdatedMedia);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryItem), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryItem>(exception);
            }
        }

        /// <summary>
        ///     Vote to an story slider
        ///     <para>Note: slider vote must be between 0 and 1</para>
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="pollId">Story poll id</param>
        /// <param name="sliderVote">Your slider vote (from 0 to 1)</param>
        public async Task<IResult<InstaStoryItem>> VoteStorySliderAsync(string storyMediaId, string pollId, double sliderVote = 0.5)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (sliderVote > 1)
                    return Result.Fail<InstaStoryItem>("sliderVote cannot be more than 1.\r\nIt must be between 0 and 1");
                if(sliderVote < 0)
                    return Result.Fail<InstaStoryItem>("sliderVote cannot be less than 0.\r\nIt must be between 0 and 1");

                var instaUri = UriCreator.GetVoteStorySliderUri(storyMediaId, pollId);
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"vote", sliderVote.ToString()},
                };

                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStoryItem>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaReelStoryMediaViewersResponse>(json);
                var covertedObj = ConvertersFabric.Instance.GetReelStoryMediaViewersConverter(obj).Convert();

                return Result.Success(covertedObj.UpdatedMedia);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryItem), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryItem>(exception);
            }
        }

        private async Task<IResult<bool>> AppendOrDeleteHighlight(string highlightId, string mediaId, bool delete)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var data = new JObject
                {
                    {"source", "story_viewer"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                if (delete)
                {
                    data.Add("added_media_ids", "[]");
                    data.Add("removed_media_ids", $"[{ExtensionHelper.EncodeList(new[] { mediaId })}]");
                }
                else
                {
                    data.Add("added_media_ids", $"[{ExtensionHelper.EncodeList(new[] { mediaId })}]");
                    data.Add("removed_media_ids", "[]");
                }
                var instaUri = UriCreator.GetHighlightEditUri(highlightId);
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Configure story photo
        /// </summary>
        /// <param name="image">Photo to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        private async Task<IResult<InstaStoryMedia>> ConfigureStoryPhotoAsync(Action<InstaUploaderProgress> progress, InstaUploaderProgress upProgress, InstaImage image, string uploadId,
            string caption, Uri uri, InstaStoryUploadOptions uploadOptions = null)
        {
            try
            {
                upProgress.UploadState = InstaUploadState.Configuring;
                progress?.Invoke(upProgress);
                var instaUri = UriCreator.GetVideoStoryConfigureUri();// UriCreator.GetStoryConfigureUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"source_type", "3"},
                    {"caption", caption},
                    {"upload_id", uploadId},
                    {"edits", new JObject()},
                    {"disable_comments", false},
                    {"configure_mode", 1},
                    {"camera_position", "unknown"}
                };
                if (uri != null)
                {
                    var webUri = new JArray
                    {
                        new JObject
                        {
                            {"webUri", uri.ToString()}
                        }
                    };
                    var storyCta = new JArray
                    {
                        new JObject
                        {
                            {"links",  webUri}
                        }
                    };
                    data.Add("story_cta", storyCta.ToString(Formatting.None));
                }
                if (uploadOptions != null)
                {
                    if (uploadOptions.Hashtags?.Count > 0)
                    {
                        var hashtagArr = new JArray();
                        foreach (var item in uploadOptions.Hashtags)
                            hashtagArr.Add(item.ConvertToJson());

                        data.Add("story_hashtags", hashtagArr.ToString(Formatting.None));
                    }

                    if (uploadOptions.Locations?.Count > 0)
                    {
                        var locationArr = new JArray();
                        foreach (var item in uploadOptions.Locations)
                            locationArr.Add(item.ConvertToJson());

                        data.Add("story_locations", locationArr.ToString(Formatting.None));
                    }
                    if (uploadOptions.Slider != null)
                    {
                        var sliderArr = new JArray
                        {
                            uploadOptions.Slider.ConvertToJson()
                        };

                        data.Add("story_sliders", sliderArr.ToString(Formatting.None));
                        if (uploadOptions.Slider.IsSticker)
                            data.Add("story_sticker_ids", $"{uploadOptions.Slider.Emoji}");
                    }
                    else
                    {
                        if (uploadOptions.Polls?.Count > 0)
                        {
                            var pollArr = new JArray();
                            foreach (var item in uploadOptions.Polls)
                                pollArr.Add(item.ConvertToJson());

                            data.Add("story_polls", pollArr.ToString(Formatting.None));
                        }
                        if (uploadOptions.Questions?.Count > 0)
                        {
                            var questionArr = new JArray();
                            foreach (var item in uploadOptions.Questions)
                                questionArr.Add(item.ConvertToJson());

                            data.Add("story_questions", questionArr.ToString(Formatting.None));
                        }
                    }
                    if (uploadOptions.MediaStory != null)
                    {
                        var mediaStory = new JArray
                        {
                            uploadOptions.MediaStory.ConvertToJson()
                        };

                        data.Add("attached_media", mediaStory.ToString(Formatting.None));
                    }

                    if (uploadOptions.Mentions?.Count > 0)
                    {
                        var mentionArr = new JArray();
                        foreach (var item in uploadOptions.Mentions)
                            mentionArr.Add(item.ConvertToJson());

                        data.Add("reel_mentions", mentionArr.ToString(Formatting.None));
                    }
                    if (uploadOptions.Countdown != null)
                    {
                        var countdownArr = new JArray
                        {
                            uploadOptions.Countdown.ConvertToJson()
                        };

                        data.Add("story_countdowns", countdownArr.ToString(Formatting.None));
                        data.Add("story_sticker_ids", "countdown_sticker_time");
                    }
                }
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var mediaResponse = JsonConvert.DeserializeObject<InstaStoryMediaResponse>(json);
                    var converter = ConvertersFabric.Instance.GetStoryMediaConverter(mediaResponse);
                    var obj = converter.Convert();
                    upProgress.UploadState = InstaUploadState.Configured;
                    progress?.Invoke(upProgress);

                    upProgress.UploadState = InstaUploadState.Completed;
                    progress?.Invoke(upProgress);
                    return Result.Success(obj);
                }
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception);
            }
        }
        /// <summary>
        ///     Configure story video
        /// </summary>
        /// <param name="video">Video to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        private async Task<IResult<InstaStoryMedia>> ConfigureStoryVideoAsync(Action<InstaUploaderProgress> progress, InstaUploaderProgress upProgress, InstaVideoUpload video, string uploadId,
            string caption, Uri uri, InstaStoryUploadOptions uploadOptions = null)
        {
            try
            {
                upProgress.UploadState = InstaUploadState.Configuring;
                progress?.Invoke(upProgress);
                var instaUri = UriCreator.GetVideoStoryConfigureUri(false);
                var rnd = new Random();
                var data = new JObject
                {
                    {"filter_type", "0"},
                    {"timezone_offset", "16200"},
                    {"_csrftoken", _user.CsrfToken},
                    {"client_shared_at", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(25,55)).ToString()},
                    {"story_media_creation_date", (long.Parse(ApiRequestMessage.GenerateUploadId())- rnd.Next(50,70)).ToString()},
                    {"media_folder", "Camera"},
                    {"configure_mode", "1"},
                    {"source_type", "4"},
                    {"video_result", ""},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"caption", caption},
                    {"date_time_original", DateTime.Now.ToString("yyyy-dd-MMTh:mm:ss-0fffZ")},
                    {"capture_type", "normal"},
                    {"mas_opt_in", "NOT_PROMPTED"},
                    {"upload_id", uploadId},
                    {"client_timestamp", ApiRequestMessage.GenerateUploadId()},
                    {
                        "device", new JObject{
                            {"manufacturer", _deviceInfo.HardwareManufacturer},
                            {"model", _deviceInfo.DeviceModelIdentifier},
                            {"android_release", _deviceInfo.AndroidVer.VersionNumber},
                            {"android_version", _deviceInfo.AndroidVer.APILevel}
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
                if (uri != null)
                {
                    var webUri = new JArray
                    {
                        new JObject
                        {
                            {"webUri", uri.ToString()}
                        }
                    };
                    var storyCta = new JArray
                    {
                        new JObject
                        {
                            {"links",  webUri}
                        }
                    };
                    data.Add("story_cta", storyCta.ToString(Formatting.None));
                }
                if (uploadOptions != null)
                {
                    if (uploadOptions.Hashtags?.Count > 0)
                    {
                        var hashtagArr = new JArray();
                        foreach (var item in uploadOptions.Hashtags)
                            hashtagArr.Add(item.ConvertToJson());

                        data.Add("story_hashtags", hashtagArr.ToString(Formatting.None));
                    }

                    if (uploadOptions.Locations?.Count > 0)
                    {
                        var locationArr = new JArray();
                        foreach (var item in uploadOptions.Locations)
                            locationArr.Add(item.ConvertToJson());

                        data.Add("story_locations", locationArr.ToString(Formatting.None));
                    }
                    if (uploadOptions.Slider != null)
                    {
                        var sliderArr = new JArray
                        {
                            uploadOptions.Slider.ConvertToJson()
                        };

                        data.Add("story_sliders", sliderArr.ToString(Formatting.None));
                        if (uploadOptions.Slider.IsSticker)
                            data.Add("story_sticker_ids", $"emoji_slider_{uploadOptions.Slider.Emoji}");
                    }
                    else
                    {
                        if (uploadOptions.Polls?.Count > 0)
                        {
                            var pollArr = new JArray();
                            foreach (var item in uploadOptions.Polls)
                                pollArr.Add(item.ConvertToJson());

                            data.Add("story_polls", pollArr.ToString(Formatting.None));
                        }
                        if (uploadOptions.Questions?.Count > 0)
                        {
                            var questionArr = new JArray();
                            foreach (var item in uploadOptions.Questions)
                                questionArr.Add(item.ConvertToJson());

                            data.Add("story_questions", questionArr.ToString(Formatting.None));
                        }
                    }

                    if (uploadOptions.Countdown != null)
                    {
                        var countdownArr = new JArray
                        {
                            uploadOptions.Countdown.ConvertToJson()
                        };

                        data.Add("story_countdowns", countdownArr.ToString(Formatting.None));
                        data.Add("story_sticker_ids", "countdown_sticker_time");
                    }
                }
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var uploadParamsObj = new JObject
                {
                    {"num_step_auto_retry", 0},
                    {"num_reupload", 0},
                    {"num_step_manual_retry", 0}
                };
                var uploadParams = JsonConvert.SerializeObject(uploadParamsObj);
                request.Headers.Add("retry_context", uploadParams);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var mediaResponse = JsonConvert.DeserializeObject<InstaStoryMediaResponse>(json);
                    var converter = ConvertersFabric.Instance.GetStoryMediaConverter(mediaResponse);
                    var obj = Result.Success(converter.Convert());
                    upProgress.UploadState = InstaUploadState.Configured;
                    progress?.Invoke(upProgress);
                    upProgress.UploadState = InstaUploadState.Completed;
                    progress?.Invoke(upProgress);
                    return obj;
                }
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception);
            }
        }

        private async Task<IResult<InstaReelStoryMediaViewersResponse>> GetStoryMediaViewers(string storyMediaId, string maxId)
        {
            try
            {
                var directInboxUri = UriCreator.GetStoryMediaViewersUri(storyMediaId, maxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaReelStoryMediaViewersResponse>(response, json);

                var storyMediaViewersResponse = JsonConvert.DeserializeObject<InstaReelStoryMediaViewersResponse>(json);

                return Result.Success(storyMediaViewersResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaReelStoryMediaViewersResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaReelStoryMediaViewersResponse>(exception);
            }
        }

        private async Task<IResult<InstaStoryPollVotersListResponse>> GetStoryPollVoters(string storyMediaId, string pollId, string maxId)
        {
            try
            {
                var directInboxUri = UriCreator.GetStoryPollVotersUri(storyMediaId, pollId, maxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStoryPollVotersListResponse>(response, json);

                var storyVotersResponse = JsonConvert.DeserializeObject<InstaStoryPollVotersListContainerResponse>(json);

                return Result.Success(storyVotersResponse.VoterInfo);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryPollVotersListResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryPollVotersListResponse>(exception);
            }
        }
        public async Task<IResult<bool>> FollowUnfollowCountdown(Uri instaUri)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                };

                var request =  _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                var resp = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);

                return resp.IsSucceed ? Result.Success(true) : Result.Fail<bool>("");
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

        #region Old functions

        private async Task<IResult<InstaStoryMedia>> UploadStoryPhotoWithUrlAsyncOLD(Action<InstaUploaderProgress> progress, InstaImage image,
            string caption, Uri uri,
            InstaStoryUploadOptions uploadOptions = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var upProgress = new InstaUploaderProgress
            {
                Caption = caption ?? string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                if (uploadOptions?.Mentions?.Count > 0)
                {
                    var currentDelay = _instaApi.GetRequestDelay();
                    _instaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
                    foreach (var t in uploadOptions.Mentions)
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
                var instaUri = UriCreator.GetUploadPhotoUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                upProgress.UploadId = uploadId;
                progress?.Invoke(upProgress);
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(uploadId), "\"upload_id\""},
                    //{new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    //{new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    }
                };
                byte[] imageBytes;
                if (image.ImageBytes == null)
                    imageBytes = File.ReadAllBytes(image.Uri);
                else
                    imageBytes = image.ImageBytes;
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");

                //var progressContent = new ProgressableStreamContent(imageContent, 4096, progress)
                //{
                //    UploaderProgress = upProgress
                //};
                upProgress.UploadState = InstaUploadState.Uploading;
                progress?.Invoke(upProgress);
                requestContent.Add(imageContent, "photo", $"pending_media_{ApiRequestMessage.GenerateUploadId()}.jpg");
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    upProgress.UploadState = InstaUploadState.Uploaded;
                    progress?.Invoke(upProgress);
                    //upProgress = progressContent?.UploaderProgress;
                    return await ConfigureStoryPhotoAsync(progress, upProgress, image, uploadId, caption, uri, uploadOptions);
                }
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<InstaStoryMedia>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryMedia), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryMedia>(exception);
            }
        }

        #endregion Old functions
    }
}