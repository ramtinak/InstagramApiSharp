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
    ///     User api functions.
    /// </summary>
    internal class UserProcessor : IUserProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public UserProcessor(AndroidDevice deviceInfo, UserSessionData user, IHttpRequestProcessor httpRequestProcessor,
            IInstaLogger logger, UserAuthValidate userAuthValidate, InstaApi instaApi,
            HttpHelper httpHelper)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
            _userAuthValidate = userAuthValidate;
            _instaApi = instaApi;
            _httpHelper = httpHelper;
        }
        #region public parts
        /// <summary>
        ///     Accept user friendship requst.
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<InstaFriendshipStatus>> AcceptFriendshipRequestAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetAcceptFriendshipUri(userId);
                var fields = new Dictionary<string, string>
                {
                    {"user_id", userId.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                   new InstaFriendShipDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFriendshipStatus), ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail<InstaFriendshipStatus>(ex);
            }
        }

        /// <summary>
        ///     Add new best friend (besties)
        /// </summary>
        /// <param name="userIds">User ids (pk) to add</param>
        public async Task<IResult<InstaFriendshipShortStatusList>> AddBestFriendsAsync(params long[] userIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            if (userIds?.Length == 0)
                return Result.Fail<InstaFriendshipShortStatusList>("At least 1 user id is require");

            return await AddBestFriends(userIds, null);
        }

        /// <summary>
        ///     Delete an user from your best friend (besties) lists
        /// </summary>
        /// <param name="userIds">User ids (pk) to add</param>
        public async Task<IResult<InstaFriendshipShortStatusList>> DeleteBestFriendsAsync(params long[] userIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            if (userIds?.Length == 0)
                return Result.Fail<InstaFriendshipShortStatusList>("At least 1 user id is require");

            return await AddBestFriends(null, userIds);
        }
        /// <summary>
        ///     Block user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipFullStatus>> BlockUserAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await BlockUnblockUserInternal(userId, UriCreator.GetBlockUserUri(userId));
        }

        /// <summary>
        ///     Favorite user (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<bool>> FavoriteUserAsync(long userId)
        {
            return await FavoriteUnfavoriteUser(UriCreator.GetFavoriteUserUri(userId), userId);
        }

        /// <summary>
        ///     Favorite user stories (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<bool>> FavoriteUserStoriesAsync(long userId)
        {
            return await FavoriteUnfavoriteUser(UriCreator.GetFavoriteForUserStoriesUri(userId), userId);
        }

        /// <summary>
        ///     Follow user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipFullStatus>> FollowUserAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await FollowUnfollowUserInternal(userId, UriCreator.GetFollowUserUri(userId));
        }

        /// <summary>
        ///     Get self best friends (besties)
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaUserShortList>> GetBestFriendsAsync(PaginationParameters paginationParameters)
        {
            return await GetBesties(paginationParameters);
        }

        /// <summary>
        ///     Get best friends (besties) suggestions
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaUserShortList>> GetBestFriendsSuggestionsAsync(PaginationParameters paginationParameters)
        {
            return await GetBesties(paginationParameters, true);
        }

        /// <summary>
        ///     Get blocked users
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaBlockedUsers>> GetBlockedUsersAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaBlockedUsers Convert(InstaBlockedUsersResponse instaBlockedUsers)
                {
                    return ConvertersFabric.Instance.GetBlockedUsersConverter(instaBlockedUsers).Convert();
                }
                var blockedUsersResponse = await GetBlockedUsers(paginationParameters?.NextMaxId);
                if (!blockedUsersResponse.Succeeded)
                {
                    if (blockedUsersResponse.Value != null)
                        return Result.Fail(blockedUsersResponse.Info, Convert(blockedUsersResponse.Value));
                    else
                        return Result.Fail(blockedUsersResponse.Info, default(InstaBlockedUsers));
                }
                paginationParameters.NextMaxId = blockedUsersResponse.Value.MaxId;

                paginationParameters.PagesLoaded++;
                while (!string.IsNullOrEmpty(paginationParameters.NextMaxId)
                     && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var moreUsers = await GetBlockedUsers(paginationParameters.NextMaxId);
                    if (!moreUsers.Succeeded)
                        return Result.Fail(moreUsers.Info, Convert(blockedUsersResponse.Value));

                    blockedUsersResponse.Value.BlockedList.AddRange(moreUsers.Value.BlockedList);
                    blockedUsersResponse.Value.PageSize = moreUsers.Value.PageSize;
                    blockedUsersResponse.Value.BigList = moreUsers.Value.BigList;
                    blockedUsersResponse.Value.MaxId = paginationParameters.NextMaxId = moreUsers.Value.MaxId;
                    paginationParameters.PagesLoaded++;
                }
                return Result.Success(Convert(blockedUsersResponse.Value));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBlockedUsers), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBlockedUsers>(exception);
            }
        }

        /// <summary>
        ///     Get currently logged in user info asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaCurrentUser" />
        /// </returns>
        public async Task<IResult<InstaCurrentUser>> GetCurrentUserAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetCurrentUserUri();
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCurrentUser>(response, json);
                var user = JsonConvert.DeserializeObject<InstaCurrentUserResponse>(json,
                    new InstaCurrentUserDataConverter());
                if (user.Pk < 1)
                    Result.Fail<InstaCurrentUser>("Pk is incorrect");
                var converter = ConvertersFabric.Instance.GetCurrentUserConverter(user);
                var userConverted = converter.Convert();
                return Result.Success(userConverted);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaCurrentUser), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCurrentUser>(exception);
            }
        }

        /// <summary>
        ///     Get followers list for currently logged in user asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaUserShortList>> GetCurrentUserFollowersAsync(
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await GetUserFollowersAsync(_user.UserName, paginationParameters, string.Empty);
        }

        public async Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityFeedAsync(PaginationParameters paginationParameters)
        {
            var uri = UriCreator.GetFollowingRecentActivityUri(paginationParameters.NextMaxId);
            return await GetRecentActivityInternalAsync(uri, paginationParameters);
        }

        /// <summary>
        ///     Get friendship status for given user id.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        /// <returns>
        ///     <see cref="InstaStoryFriendshipStatus" />
        /// </returns>
        public async Task<IResult<InstaStoryFriendshipStatus>> GetFriendshipStatusAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetUserFriendshipUri(userId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStoryFriendshipStatus>(response, json);
                var friendshipStatusResponse = JsonConvert.DeserializeObject<InstaStoryFriendshipStatusResponse>(json);
                var converter = ConvertersFabric.Instance.GetStoryFriendshipStatusConverter(friendshipStatusResponse);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryFriendshipStatus), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStoryFriendshipStatus>(exception);
            }
        }
        /// <summary>
        ///     Get friendship status for multiple user ids.
        /// </summary>
        /// <param name="userIds">Array of user identifier (PK)</param>
        /// <returns>
        ///     <see cref="InstaFriendshipShortStatusList" />
        /// </returns>
        public async Task<IResult<InstaFriendshipShortStatusList>> GetFriendshipStatusesAsync(params long[] userIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (userIds == null || userIds != null && !userIds.Any())
                    throw new ArgumentException("At least one user id is require.");

                var userUri = UriCreator.GetFriendshipShowManyUri();

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"user_ids", string.Join(",", userIds)},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, userUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFriendshipShortStatusList>(response, json);

                var friendshipStatusesResponse = JsonConvert.DeserializeObject<InstaFriendshipShortStatusListResponse>(json,
                    new InstaFriendShipShortDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendshipShortStatusListConverter(friendshipStatusesResponse);

                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFriendshipShortStatusList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipShortStatusList>(exception);
            }
        }

        /// <summary>
        ///     Get full user info (user info, feeds, stories, broadcasts)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<InstaFullUserInfo>> GetFullUserInfoAsync(long userId)
        {
            try
            {
                var instaUri = UriCreator.GetFullUserInfoUri(userId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFullUserInfo>(response, json);
                var fullUserInfoResponse = JsonConvert.DeserializeObject<InstaFullUserInfoResponse>(json);
                var converter = ConvertersFabric.Instance.GetFullUserInfoConverter(fullUserInfoResponse);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFullUserInfo), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFullUserInfo>(exception);
            }
        }

        /// <summary>
        ///     Get pending friendship requests.
        /// </summary>
        public async Task<IResult<InstaPendingRequest>> GetPendingFriendRequestsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                        .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? string.Empty;
                _user.CsrfToken = csrftoken;
                var instaUri = UriCreator.GetFriendshipPendingRequestsUri(_user.RankToken);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var JRes = JsonConvert.DeserializeObject<InstaPendingRequest>(json);
                    return Result.Success(JRes);
                }

                return Result.Fail<InstaPendingRequest>(response.StatusCode.ToString());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaPendingRequest), ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail<InstaPendingRequest>(ex);
            }
        }

        public async Task<IResult<InstaActivityFeed>> GetRecentActivityFeedAsync(
                    PaginationParameters paginationParameters)
        {
            var uri = UriCreator.GetRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, paginationParameters);
        }

        /// <summary>
        ///     Get suggestion details
        /// </summary>
        /// <param name="userIds">List of user ids (pk)</param>
        public async Task<IResult<InstaSuggestionItemList>> GetSuggestionDetailsAsync(params long[] userIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (userIds == null || userIds != null && !userIds.Any())
                    throw new ArgumentException("At least one user id is require.");
                var instaUri = UriCreator.GetDiscoverSuggestionDetailsUri(_user.LoggedInUser.Pk, userIds.ToList());

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaSuggestionItemList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaSuggestionItemListResponse>(json,
                    new InstaSuggestionUserDetailDataConverter());
                return Result.Success(ConvertersFabric.Instance.GetSuggestionItemListConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSuggestionItemList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSuggestionItemList>(exception);
            }
        }

        /// <summary>
        ///     Get suggestion users
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaSuggestions>> GetSuggestionUsersAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaSuggestions Convert(InstaSuggestionUserContainerResponse suggestResponse)
                {
                    return ConvertersFabric.Instance.GetSuggestionsConverter(suggestResponse).Convert();
                }
                var suggestionsResponse = await GetSuggestionUsers(paginationParameters);
                if (!suggestionsResponse.Succeeded)
                {
                    if (suggestionsResponse.Value != null)
                        return Result.Fail(suggestionsResponse.Info, Convert(suggestionsResponse.Value));
                    else
                        return Result.Fail(suggestionsResponse.Info, default(InstaSuggestions));
                }
                paginationParameters.NextMaxId = suggestionsResponse.Value.MaxId;

                paginationParameters.PagesLoaded++;
                while (suggestionsResponse.Value.MoreAvailable
                     && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                     && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var moreSuggestions = await GetSuggestionUsers(paginationParameters);
                    if (!moreSuggestions.Succeeded)
                        return Result.Fail(moreSuggestions.Info, Convert(suggestionsResponse.Value));

                    suggestionsResponse.Value.NewSuggestedUsers.Suggestions.AddRange(moreSuggestions.Value.NewSuggestedUsers.Suggestions);
                    suggestionsResponse.Value.SuggestedUsers.Suggestions.AddRange(moreSuggestions.Value.SuggestedUsers.Suggestions);
                    suggestionsResponse.Value.MoreAvailable = moreSuggestions.Value.MoreAvailable;
                    suggestionsResponse.Value.MaxId = paginationParameters.NextMaxId = moreSuggestions.Value.MaxId;
                    paginationParameters.PagesLoaded++;
                }
                return Result.Success(Convert(suggestionsResponse.Value));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSuggestions), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSuggestions>(exception);
            }
        }

        /// <summary>
        ///     Get user info by its user name asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        public async Task<IResult<InstaUser>> GetUserAsync(string username)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetUserUri(username);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE,
                    InstaApiConstants.TIMEZONE_OFFSET.ToString()));
                request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_COUNT, "1"));
                request.Properties.Add(
                    new KeyValuePair<string, object>(InstaApiConstants.HEADER_RANK_TOKEN, _user.RankToken));
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUser>(response, json);
                var userInfo = JsonConvert.DeserializeObject<InstaSearchUserResponse>(json);
                var user = userInfo.Users?.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower().Replace("@", ""));
                if (user == null)
                {
                    var errorMessage = $"Can't find this user: {username}";
                    _logger?.LogInfo(errorMessage);
                    return Result.Fail<InstaUser>(errorMessage);
                }

                if (user.Pk < 1)
                    Result.Fail<InstaUser>("Pk is incorrect");
                var converter = ConvertersFabric.Instance.GetUserConverter(user);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUser), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUser>(exception);
            }
        }

        /// <summary>
        ///     Get user from a nametag image
        /// </summary>
        /// <param name="nametagImage">Nametag image</param>
        public async Task<IResult<InstaUser>> GetUserFromNametagAsync(InstaImage nametagImage)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUsersNametagLookupUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"gallery", "true"},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"waterfall_id", Guid.NewGuid().ToString()},
                };
                var signedBody = _httpHelper.GetSignature(data);

                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(InstaApiConstants.IG_SIGNATURE_KEY_VERSION), InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION},
                    {new StringContent(signedBody), "signed_body"},
                };
                byte[] fileBytes;
                if (nametagImage.ImageBytes == null)
                    fileBytes = File.ReadAllBytes(nametagImage.Uri);
                else
                    fileBytes = nametagImage.ImageBytes;

                var imageContent = new ByteArrayContent(fileBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent, "photo_0", "photo_0");
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail<InstaUser>("User not found.");
                //{"message": "No user found for 3", "username": "3", "failure_code": 602, "failure_reason": "user_not_found_for_username", "status": "fail"}
                //{"message": "Scan was below 95, got 90.0779664516449", "confidence": 90.0779664516449, "username": "9", "failure_code": 601, "failure_reason": "confidence_below_threshold", "status": "fail"}

                var obj = JsonConvert.DeserializeObject<InstaUserContainerResponse>(json);

                var converter = ConvertersFabric.Instance.GetUserConverter(obj.User);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUser), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUser>(exception);
            }
        }
        /// <summary>
        ///     Get followers list by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followers</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaUserShortList>> GetUserFollowersAsync(string username,
            PaginationParameters paginationParameters, string searchQuery, bool mutualsfirst = false)
        {
            try
            {
                var user = await GetUserAsync(username);
                if (user.Succeeded)
                {
                    if (user.Value.FriendshipStatus.IsPrivate && user.Value.UserName != _user.LoggedInUser.UserName && !user.Value.FriendshipStatus.Following)
                        return Result.Fail("You must be a follower of private accounts to be able to get user's followers", default(InstaUserShortList));

                    return await GetUserFollowersByIdAsync(user.Value.Pk, paginationParameters, searchQuery, mutualsfirst);
                }
                else
                    return Result.Fail(user.Info, default(InstaUserShortList));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserShortList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, default(InstaUserShortList));
            }
        }

        /// <summary>
        ///     Get followers list by user id(pk) asynchronously
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followers</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaUserShortList>> GetUserFollowersByIdAsync(long userId,
            PaginationParameters paginationParameters, string searchQuery, bool mutualsfirst = false)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var followers = new InstaUserShortList();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                var userFollowersUri =
                    UriCreator.GetUserFollowersUri(userId, _user.RankToken, searchQuery, mutualsfirst,
                        paginationParameters.NextMaxId);
                var followersResponse = await GetUserListByUriAsync(userFollowersUri);
                if (!followersResponse.Succeeded)
                    return Result.Fail(followersResponse.Info, (InstaUserShortList)null);
                followers.AddRange(
                    followersResponse.Value.Items?.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                paginationParameters.NextMaxId = followers.NextMaxId = followersResponse.Value.NextMaxId;

                var pagesLoaded = 1;
                while (!string.IsNullOrEmpty(followersResponse.Value.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFollowersUri =
                        UriCreator.GetUserFollowersUri(userId, _user.RankToken, searchQuery, mutualsfirst,
                            followersResponse.Value.NextMaxId);
                    followersResponse = await GetUserListByUriAsync(nextFollowersUri);
                    if (!followersResponse.Succeeded)
                        return Result.Fail(followersResponse.Info, followers);
                    followers.AddRange(
                        followersResponse.Value.Items?.Select(ConvertersFabric.Instance.GetUserShortConverter)
                            .Select(converter => converter.Convert()));
                    pagesLoaded++;
                    paginationParameters.PagesLoaded = pagesLoaded;
                    paginationParameters.NextMaxId = followers.NextMaxId = followersResponse.Value.NextMaxId;
                }

                return Result.Success(followers);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, followers, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, followers);
            }
        }

        /// <summary>
        ///     Get following list by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followings</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaUserShortList>> GetUserFollowingAsync(string username,
            PaginationParameters paginationParameters, string searchQuery)
        {
            try
            {
                var user = await GetUserAsync(username);
                if (user.Succeeded)
                {
                    if (user.Value.FriendshipStatus.IsPrivate && user.Value.UserName != _user.LoggedInUser.UserName && !user.Value.FriendshipStatus.Following)
                        return Result.Fail("You must be a follower of private accounts to be able to get user's followings", default(InstaUserShortList));

                    return await GetUserFollowingByIdAsync(user.Value.Pk, paginationParameters, searchQuery);
                }
                else
                    return Result.Fail(user.Info, default(InstaUserShortList));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserShortList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, default(InstaUserShortList));
            }
        }
        /// <summary>
        ///     Get following list by user id(pk) asynchronously
        /// </summary>
        /// <param name="userId">User id(pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followings</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        public async Task<IResult<InstaUserShortList>> GetUserFollowingByIdAsync(long userId,
            PaginationParameters paginationParameters, string searchQuery)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var following = new InstaUserShortList();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                var uri = UriCreator.GetUserFollowingUri(userId, _user.RankToken, searchQuery,
                    paginationParameters.NextMaxId);
                var userListResponse = await GetUserListByUriAsync(uri);
                if (!userListResponse.Succeeded)
                    return Result.Fail(userListResponse.Info, (InstaUserShortList)null);
                following.AddRange(
                    userListResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                paginationParameters.NextMaxId = following.NextMaxId = userListResponse.Value.NextMaxId;
                var pages = 1;
                while (!string.IsNullOrEmpty(following.NextMaxId)
                       && pages < paginationParameters.MaximumPagesToLoad)
                {
                    var nextUri =
                        UriCreator.GetUserFollowingUri(userId, _user.RankToken, searchQuery,
                            userListResponse.Value.NextMaxId);
                    userListResponse = await GetUserListByUriAsync(nextUri);
                    if (!userListResponse.Succeeded)
                        return Result.Fail(userListResponse.Info, following);
                    following.AddRange(
                        userListResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                            .Select(converter => converter.Convert()));
                    pages++;
                    paginationParameters.PagesLoaded = pages;
                    paginationParameters.NextMaxId = following.NextMaxId = userListResponse.Value.NextMaxId;
                }

                return Result.Success(following);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, following, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, following);
            }
        }

        /// <summary>
        ///     Gets the user extended information (followers count, following count, bio, etc) by user identifier.
        /// </summary>
        /// <param name="pk">User Id, like "123123123"</param>
        /// <returns></returns>
        public async Task<IResult<InstaUserInfo>> GetUserInfoByIdAsync(long pk)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetUserInfoByIdUri(pk);
                return await GetUserInfoAsync(userUri);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserInfo), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserInfo>(exception);
            }
        }

        /// <summary>
        ///     Gets the user extended information (followers count, following count, bio, etc) by username.
        /// </summary>
        /// <param name="username">Username, like "instagram"</param>
        /// <returns></returns>
        public async Task<IResult<InstaUserInfo>> GetUserInfoByUsernameAsync(string username)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetUserInfoByUsernameUri(username);
                return await GetUserInfoAsync(userUri);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserInfo), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserInfo>(exception);
            }
        }

        /// <summary>
        ///     Get all user media by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetUserMediaAsync(string username,
             PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var user = await GetUserAsync(username);
            if (!user.Succeeded)
                return Result.Fail<InstaMediaList>("Unable to get user to load media");
            return await GetUserMediaByIdAsync(user.Value.Pk, paginationParameters);
        }

        /// <summary>
        ///     Get all user media by user id (pk) asynchronously
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetUserMediaByIdAsync(long userId,
                                                    PaginationParameters paginationParameters)
        {
            var mediaList = new InstaMediaList();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaMediaList Convert(InstaMediaListResponse mediaListResponse)
                {
                    return ConvertersFabric.Instance.GetMediaListConverter(mediaListResponse).Convert();
                }

                var mediaResult = await GetUserMedia(userId, paginationParameters);
                if (!mediaResult.Succeeded)
                {
                    if (mediaResult.Value != null)
                        return Result.Fail(mediaResult.Info, Convert(mediaResult.Value));
                    else
                        return Result.Fail(mediaResult.Info, default(InstaMediaList));
                }
                var mediaResponse = mediaResult.Value;

                mediaList = Convert(mediaResponse);
                mediaList.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {

                    var nextMedia = await GetUserMedia(userId, paginationParameters);
                    if (!nextMedia.Succeeded)
                        return Result.Fail(nextMedia.Info, mediaList);
                    mediaResponse.MoreAvailable = nextMedia.Value.MoreAvailable;
                    mediaResponse.ResultsCount += nextMedia.Value.ResultsCount;
                    mediaList.NextMaxId = mediaResponse.NextMaxId = paginationParameters.NextMaxId = nextMedia.Value.NextMaxId;
                    mediaList.AddRange(Convert(nextMedia.Value));
                    paginationParameters.PagesLoaded++;
                }

                mediaList.Pages = paginationParameters.PagesLoaded;
                mediaList.PageSize = mediaResponse.ResultsCount;
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
                return Result.Fail(exception, mediaList);
            }
        }

        /// <summary>
        ///     Get all user shoppable media by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetUserShoppableMediaAsync(string username,
            PaginationParameters paginationParameters)
        {
            return await _instaApi.ShoppingProcessor.GetUserShoppableMediaAsync(username, paginationParameters);
        }

        /// <summary>
        ///     Get user tags by username asynchronously
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetUserTagsAsync(string username,
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var user = await GetUserAsync(username);
            if (!user.Succeeded)
                return Result.Fail($"Unable to get user {username} to get tags", (InstaMediaList)null);
            return await GetUserTagsAsync(user.Value.Pk, paginationParameters);
        }
        /// <summary>
        ///     Get user tags by username asynchronously
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetUserTagsAsync(long userId,
            PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var userTags = new InstaMediaList();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                IEnumerable<InstaMedia> Convert(InstaMediaListResponse mediaListResponse)
                {
                    return mediaListResponse.Medias.Select(ConvertersFabric.Instance.GetSingleMediaConverter)
                        .Select(converter => converter.Convert());
                }
                var mediaTags = await GetUserTags(userId, paginationParameters);
                if (!mediaTags.Succeeded)
                {
                    if (mediaTags.Value != null)
                    {
                        userTags.AddRange(Convert(mediaTags.Value));
                        return Result.Fail(mediaTags.Info, userTags);
                    }
                    else
                        return Result.Fail(mediaTags.Info, default(InstaMediaList));
                }
                var mediaResponse = mediaTags.Value;
                userTags.AddRange(Convert(mediaResponse));
                userTags.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextMedia = await GetUserTags(userId, paginationParameters);
                    if (!nextMedia.Succeeded)
                        return Result.Fail(nextMedia.Info, userTags);

                    userTags.AddRange(Convert(nextMedia.Value));
                    userTags.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId = nextMedia.Value.NextMaxId;
                    mediaResponse.AutoLoadMoreEnabled = nextMedia.Value.AutoLoadMoreEnabled;
                    mediaResponse.MoreAvailable = nextMedia.Value.MoreAvailable;
                    mediaResponse.RankToken = nextMedia.Value.RankToken;
                    mediaResponse.TotalCount += nextMedia.Value.TotalCount;
                    mediaResponse.ResultsCount += nextMedia.Value.ResultsCount;
                }
                userTags.PageSize = mediaResponse.ResultsCount;
                userTags.Pages = paginationParameters.PagesLoaded;
                return Result.Success(userTags);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, userTags, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, userTags);
            }
        }
        /// <summary>
        ///     Ignore user friendship requst.
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<InstaFriendshipFullStatus>> IgnoreFriendshipRequestAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDenyFriendshipUri(userId);
                var fields = new Dictionary<string, string>
                {
                    {"user_id", userId.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFriendshipFullStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipFullStatusContainerResponse>(json);
                var converter = ConvertersFabric.Instance.GetFriendshipFullStatusConverter(friendshipStatus.FriendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFriendshipFullStatus), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaFriendshipFullStatus>(exception);
            }
        }

        /// <summary>
        ///     Hide my story from specific user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaStoryFriendshipStatus>> HideMyStoryFromUserAsync(long userId)
        {
            return await HideUnhideMyStoryFromUser(UriCreator.GetHideMyStoryFromUserUri(userId));
        }

        /// <summary>
        ///     Mark user as overage
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<bool>> MarkUserAsOverageAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMarkUserOverageUri(userId);

                var data = new JObject
                {
                    {"user_id", userId.ToString()},
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
                    return Result.UnExpectedResponse<bool>(response, obj.Message, null);

                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, obj.Message, null);
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

        /// <summary>
        ///     Mute friend's stories, so you won't see their stories in latest stories tab
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<InstaStoryFriendshipStatus>> MuteFriendStoryAsync(long userId)
        {
            return await MuteUnMuteFriendStory(UriCreator.GetMuteFriendStoryUri(userId));
        }

        /// <summary>
        ///     Mute user media (story, post or all)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="unmuteOption">Unmute option</param>
        public async Task<IResult<InstaStoryFriendshipStatus>> MuteUserMediaAsync(long userId, InstaMuteOption unmuteOption)
        {
            return await MuteUnMuteUserMedia(UriCreator.GetMuteUserMediaStoryUri(userId), userId, unmuteOption);
        }
        /// <summary>
        ///     Report user
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<bool>> ReportUserAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetReportUserUri(userId);
                var fields = new Dictionary<string, string>
                {
                    {"user_id", userId.ToString()},
                    {"source_name", "profile"},
                    {"reason", "1"},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"is_spam", "true"}
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
        ///     Stop block user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipFullStatus>> UnBlockUserAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await BlockUnblockUserInternal(userId, UriCreator.GetUnBlockUserUri(userId));
        }

        /// <summary>
        ///     Unfavorite user (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<bool>> UnFavoriteUserAsync(long userId)
        {
            return await FavoriteUnfavoriteUser(UriCreator.GetUnFavoriteUserUri(userId), userId);
        }

        /// <summary>
        ///     Unfavorite user stories (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<bool>> UnFavoriteUserStoriesAsync(long userId)
        {
            return await FavoriteUnfavoriteUser(UriCreator.GetUnFavoriteForUserStoriesUri(userId), userId);
        }

        /// <summary>
        ///     Stop follow user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipFullStatus>> UnFollowUserAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await FollowUnfollowUserInternal(userId, UriCreator.GetUnFollowUserUri(userId));
        }

        /// <summary>
        ///     Unhide my story from specific user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaStoryFriendshipStatus>> UnHideMyStoryFromUserAsync(long userId)
        {
            return await HideUnhideMyStoryFromUser(UriCreator.GetUnHideMyStoryFromUserUri(userId));
        }

        /// <summary>
        ///     Unmute friend's stories, so you will be able to see their stories in latest stories tab once again
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<InstaStoryFriendshipStatus>> UnMuteFriendStoryAsync(long userId)
        {
            return await MuteUnMuteFriendStory(UriCreator.GetUnMuteFriendStoryUri(userId));
        }

        /// <summary>
        ///     Unmute user media (story, post or all)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="unmuteOption">Unmute option</param>
        public async Task<IResult<InstaStoryFriendshipStatus>> UnMuteUserMediaAsync(long userId, InstaMuteOption unmuteOption)
        {
            return await MuteUnMuteUserMedia(UriCreator.GetUnMuteUserMediaStoryUri(userId), userId, unmuteOption);
        }

        /// <summary>
        ///     Remove an follower from your followers
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<InstaFriendshipStatus>> RemoveFollowerAsync(long userId)
        {
            try
            {
                var instaUri = UriCreator.GetRemoveFollowerUri(userId);

                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"user_id", userId.ToString()},
                    {"radio_type", "wifi-none"},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };

                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(json))
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                    new InstaFriendShipDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFriendshipStatus), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipStatus>(exception);
            }
        }

        /// <summary>
        ///     Translate biography of someone
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        public async Task<IResult<string>> TranslateBiographyAsync(long userId)
        {
            try
            {
                var instaUri = UriCreator.GetTranslateBiographyUri(userId);

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(json))
                    return Result.UnExpectedResponse<string>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaTranslateBioResponse>(json);

                return obj.Status.ToLower() == "ok" ? Result.Success(obj.Translation) : Result.Fail<string>(obj.Message);
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
        #endregion public parts

        #region private parts

        private async Task<IResult<InstaFriendshipShortStatusList>> AddBestFriends(long[] userIdsToAdd, long[] userIdsToRemove)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSetBestFriendsUri();

                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"module", "favorites_home_list"},
                    {"source", "audience_manager"}
                };
                if (userIdsToAdd?.Length > 0)
                {
                    var jArr = new JArray
                    {
                        userIdsToAdd
                    };
                    data.Add("add", jArr);
                    data.Add("remove", new JArray());
                }
                else
                {
                    var jArr = new JArray
                    {
                        userIdsToRemove
                    };
                    data.Add("add", new JArray());
                    data.Add("remove", jArr);
                }
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFriendshipShortStatusList>(response, json);

                var friendshipStatusesResponse = JsonConvert.DeserializeObject<InstaFriendshipShortStatusListResponse>(json,
                    new InstaFriendShipShortDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendshipShortStatusListConverter(friendshipStatusesResponse);

                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFriendshipShortStatusList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipShortStatusList>(exception);
            }
        }
        private async Task<IResult<InstaFriendshipFullStatus>> BlockUnblockUserInternal(long userId, Uri instaUri)
        {
            try
            {
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"user_id", userId.ToString()},
                    {"radio_type", "wifi-none"}
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(json))
                    return Result.UnExpectedResponse<InstaFriendshipFullStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipFullStatusContainerResponse>(json);
                var converter = ConvertersFabric.Instance.GetFriendshipFullStatusConverter(friendshipStatus.FriendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFriendshipFullStatus), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipFullStatus>(exception);
            }
        }

        private async Task<IResult<InstaFriendshipFullStatus>> FollowUnfollowUserInternal(long userId, Uri instaUri)
        {
            try
            {
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"user_id", userId.ToString()},
                    {"radio_type", "wifi-none"}
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(json))
                    return Result.UnExpectedResponse<InstaFriendshipFullStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipFullStatusContainerResponse>(json);
                var converter = ConvertersFabric.Instance.GetFriendshipFullStatusConverter(friendshipStatus.FriendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFriendshipFullStatus), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipFullStatus>(exception);
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
                    new Converters.Json.InstaRecentActivityConverter());
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
                    new Converters.Json.InstaRecentActivityConverter());
                activityFeed.IsOwnActivity = feedPage.IsOwnActivity;
                var nextId = feedPage.NextMaxId;
                activityFeed.Items.AddRange(
                    feedPage.Stories.Select(ConvertersFabric.Instance.GetSingleRecentActivityConverter)
                        .Select(converter => converter.Convert()));
                paginationParameters.PagesLoaded++;
                activityFeed.NextMaxId = paginationParameters.NextMaxId = feedPage.NextMaxId;
                while (!string.IsNullOrEmpty(nextId)
                       && paginationParameters.PagesLoaded <= paginationParameters.MaximumPagesToLoad)
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

        private async Task<IResult<InstaBlockedUsersResponse>> GetBlockedUsers(string maxId)
        {
            try
            {
                var instaUri = UriCreator.GetBlockedUsersUri(maxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBlockedUsersResponse>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBlockedUsersResponse>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBlockedUsersResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBlockedUsersResponse>(exception);
            }
        }

        private async Task<IResult<InstaSuggestionUserContainerResponse>> GetSuggestionUsers(PaginationParameters paginationParameters)
        {
            try
            {
                var instaUri = UriCreator.GetDiscoverPeopleUri();

                var data = new Dictionary<string, string>
                {
                    {"phone_id", _deviceInfo.PhoneGuid.ToString()},
                    {"module", "discover_people"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"paginate", "true"},
                };

                if (paginationParameters != null && !string.IsNullOrEmpty(paginationParameters.NextMaxId))
                    data.Add("max_id", paginationParameters.NextMaxId);

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaSuggestionUserContainerResponse>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaSuggestionUserContainerResponse>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSuggestionUserContainerResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSuggestionUserContainerResponse>(exception);
            }
        }

        private async Task<IResult<InstaUserInfo>> GetUserInfoAsync(Uri userUri)
        {
            try
            {
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserInfo>(response, json);
                var userInfo = JsonConvert.DeserializeObject<InstaUserInfoContainerResponse>(json);
                var converter = ConvertersFabric.Instance.GetUserInfoConverter(userInfo);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserInfo), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserInfo>(exception);
            }
        }

        private async Task<IResult<InstaUserShortList>> GetBesties(PaginationParameters paginationParameters, bool suggested = false)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var besties = new InstaUserShortList();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                Uri bestiesUri = UriCreator.GetBestFriendsUri(paginationParameters.NextMaxId);
                if(suggested)
                    bestiesUri = UriCreator.GetBestiesSuggestionUri(paginationParameters.NextMaxId);

                var bestiesResponse = await GetUserListByUriAsync(bestiesUri);
                if (!bestiesResponse.Succeeded)
                    return Result.Fail(bestiesResponse.Info, (InstaUserShortList)null);
                besties.AddRange(
                    bestiesResponse.Value.Items?.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                paginationParameters.NextMaxId = besties.NextMaxId = bestiesResponse.Value.NextMaxId;

                var pagesLoaded = 1;
                while (!string.IsNullOrEmpty(bestiesResponse.Value.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextBestiesUri = UriCreator.GetBestFriendsUri(bestiesResponse.Value.NextMaxId);
                    if (suggested)
                        nextBestiesUri = UriCreator.GetBestiesSuggestionUri(bestiesResponse.Value.NextMaxId);
                    bestiesResponse = await GetUserListByUriAsync(nextBestiesUri);
                    if (!bestiesResponse.Succeeded)
                        return Result.Fail(bestiesResponse.Info, besties);
                    besties.AddRange(
                        bestiesResponse.Value.Items?.Select(ConvertersFabric.Instance.GetUserShortConverter)
                            .Select(converter => converter.Convert()));
                    pagesLoaded++;
                    paginationParameters.PagesLoaded = pagesLoaded;
                    paginationParameters.NextMaxId = besties.NextMaxId = bestiesResponse.Value.NextMaxId;
                }

                return Result.Success(besties);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, besties, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, besties);
            }
        }

        private async Task<IResult<InstaUserListShortResponse>> GetUserListByUriAsync(Uri uri)
        {
            try
            {
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserListShortResponse>(response, json);
                var instaUserListResponse = JsonConvert.DeserializeObject<InstaUserListShortResponse>(json);
                if (instaUserListResponse.IsOk())
                    return Result.Success(instaUserListResponse);
                return Result.UnExpectedResponse<InstaUserListShortResponse>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserListShortResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserListShortResponse>(exception);
            }
        }

        private async Task<IResult<InstaMediaListResponse>> GetUserMedia(long userId,
                                             PaginationParameters paginationParameters)
        {
            try
            {
                var instaUri = UriCreator.GetUserMediaListUri(userId, paginationParameters.NextMaxId);
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
                return Result.Fail(exception, default(InstaMediaListResponse));
            }
        }
        private async Task<IResult<bool>> FavoriteUnfavoriteUser(Uri instaUri, long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var data = new JObject
                {
                    {"user_id", userId.ToString()},
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
                    return Result.UnExpectedResponse<bool>(response, obj.Message, null);

                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, obj.Message, null);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail<bool>(ex);
            }
        }

        private async Task<IResult<InstaStoryFriendshipStatus>> MuteUnMuteUserMedia(Uri instaUri, long userId, InstaMuteOption muteUnmuteOption)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                };
                switch (muteUnmuteOption)
                {
                    case InstaMuteOption.All:
                        data.Add("target_reel_author_id", userId.ToString());
                        data.Add("target_posts_author_id", userId.ToString());
                        break;
                    case InstaMuteOption.Post:
                        data.Add("target_posts_author_id", userId.ToString());
                        break;
                    case InstaMuteOption.Story:
                        data.Add("target_reel_author_id", userId.ToString());
                        break;
                }
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStoryFriendshipStatus>(response, obj.Message, null);

                var friendshipStatus = JsonConvert.DeserializeObject<InstaStoryFriendshipStatusContainerResponse>(json);
                var converter = ConvertersFabric.Instance.GetStoryFriendshipStatusConverter(friendshipStatus.FriendshipStatus);

                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryFriendshipStatus), ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail<InstaStoryFriendshipStatus>(ex);
            }
        }
        private async Task<IResult<InstaStoryFriendshipStatus>> HideUnhideMyStoryFromUser(Uri instaUri)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var data = new JObject
                {
                    {"source", "profile"},
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
                    return Result.UnExpectedResponse<InstaStoryFriendshipStatus>(response, obj.Message, null);

                var friendshipStatus = JsonConvert.DeserializeObject<InstaStoryFriendshipStatusContainerResponse>(json);
                var converter = ConvertersFabric.Instance.GetStoryFriendshipStatusConverter(friendshipStatus.FriendshipStatus);

                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryFriendshipStatus), ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail<InstaStoryFriendshipStatus>(ex);
            }
        }

        private async Task<IResult<InstaStoryFriendshipStatus>> MuteUnMuteFriendStory(Uri instaUri)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
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
                    return Result.UnExpectedResponse<InstaStoryFriendshipStatus>(response, obj.Message, null);

                var friendshipStatus = JsonConvert.DeserializeObject<InstaStoryFriendshipStatusContainerResponse>(json);
                var converter = ConvertersFabric.Instance.GetStoryFriendshipStatusConverter(friendshipStatus.FriendshipStatus);

                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStoryFriendshipStatus), ResponseType.NetworkProblem);
            }
            catch (Exception ex)
            {
                return Result.Fail<InstaStoryFriendshipStatus>(ex);
            }
        }

        private async Task<IResult<InstaMediaListResponse>> GetUserTags(long userId,
            PaginationParameters paginationParameters)
        {
            try
            {
                var uri = UriCreator.GetUserTagsUri(userId, _user.RankToken, paginationParameters?.NextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<InstaMediaListResponse>(response, json);
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
                return Result.Fail(exception, default(InstaMediaListResponse));
            }
        }
        #endregion private parts

    }
}