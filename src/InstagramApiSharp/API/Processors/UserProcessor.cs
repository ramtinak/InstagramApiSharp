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
            catch (Exception ex)
            {
                return Result.Fail<InstaFriendshipStatus>(ex.Message);
            }
        }

        /// <summary>
        ///     Block user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipStatus>> BlockUserAsync(long userId)
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
        ///     Follow user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipStatus>> FollowUserAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await FollowUnfollowUserInternal(userId, UriCreator.GetFollowUserUri(userId));
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCurrentUser>(exception.Message);
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
            var uri = UriCreator.GetFollowingRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, paginationParameters);
        }

        /// <summary>
        ///     Get friendship status for given user id.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        /// <returns>
        ///     <see cref="InstaFriendshipStatus" />
        /// </returns>
        public async Task<IResult<InstaFriendshipStatus>> GetFriendshipStatusAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetUserFriendshipUri(userId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatusResponse = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json);
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatusResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipStatus>(exception.Message);
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipShortStatusList>(exception.Message);
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFullUserInfo>(exception.Message);
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
            catch (Exception ex)
            {
                return Result.Fail<InstaPendingRequest>(ex.Message);
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

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaSuggestionItemList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaSuggestionItemListResponse>(json,
                    new InstaSuggestionUserDetailDataConverter());
                return Result.Success(ConvertersFabric.Instance.GetSuggestionItemListConverter(obj).Convert());
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
                InstaSuggestions Convert(InstaSuggestionUserContainerResponse suggestResponse)
                {
                    return ConvertersFabric.Instance.GetSuggestionsConverter(suggestResponse).Convert();
                }
                var suggestionsResponse = await GetSuggestionUsers(paginationParameters);
                if (!suggestionsResponse.Succeeded)
                    return Result.Fail(suggestionsResponse.Info, Convert(suggestionsResponse.Value));

                paginationParameters.NextMaxId = suggestionsResponse.Value.MaxId;

                paginationParameters.PagesLoaded++;
                while (suggestionsResponse.Value.MoreAvailable
                     && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                     && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var moreSuggestions = await GetSuggestionUsers(paginationParameters);
                    if (!moreSuggestions.Succeeded)
                        return Result.Fail(moreSuggestions.Info, Convert(moreSuggestions.Value));

                    suggestionsResponse.Value.NewSuggestedUsers.Suggestions.AddRange(moreSuggestions.Value.NewSuggestedUsers.Suggestions);
                    suggestionsResponse.Value.SuggestedUsers.Suggestions.AddRange(moreSuggestions.Value.SuggestedUsers.Suggestions);
                    suggestionsResponse.Value.MoreAvailable = moreSuggestions.Value.MoreAvailable;
                    suggestionsResponse.Value.MaxId = paginationParameters.NextMaxId = moreSuggestions.Value.MaxId;
                }
                return Result.Success(Convert(suggestionsResponse.Value));
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
                var user = userInfo.Users?.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower().Replace("@",""));
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUser>(exception.Message);
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUser>(exception.Message);
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
            PaginationParameters paginationParameters, string searchQuery)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var followers = new InstaUserShortList();
            try
            {
                var user = await GetUserAsync(username);
                var userFollowersUri =
                    UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken, searchQuery,
                        paginationParameters.NextMaxId);
                var followersResponse = await GetUserListByUriAsync(userFollowersUri);
                if (!followersResponse.Succeeded)
                    return Result.Fail(followersResponse.Info, (InstaUserShortList)null);
                followers.AddRange(
                    followersResponse.Value.Items?.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                followers.NextMaxId = followersResponse.Value.NextMaxId;

                var pagesLoaded = 1;
                while (!string.IsNullOrEmpty(followersResponse.Value.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFollowersUri =
                        UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken, searchQuery,
                            followersResponse.Value.NextMaxId);
                    followersResponse = await GetUserListByUriAsync(nextFollowersUri);
                    if (!followersResponse.Succeeded)
                        return Result.Fail(followersResponse.Info, followers);
                    followers.AddRange(
                        followersResponse.Value.Items?.Select(ConvertersFabric.Instance.GetUserShortConverter)
                            .Select(converter => converter.Convert()));
                    pagesLoaded++;
                    followers.NextMaxId = followersResponse.Value.NextMaxId;
                }

                return Result.Success(followers);
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
            UserAuthValidator.Validate(_userAuthValidate);
            var following = new InstaUserShortList();
            try
            {
                var user = await GetUserAsync(username);
                var uri = UriCreator.GetUserFollowingUri(user.Value.Pk, _user.RankToken, searchQuery,
                    paginationParameters.NextMaxId);
                var userListResponse = await GetUserListByUriAsync(uri);
                if (!userListResponse.Succeeded)
                    return Result.Fail(userListResponse.Info, (InstaUserShortList)null);
                following.AddRange(
                    userListResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                following.NextMaxId = userListResponse.Value.NextMaxId;
                var pages = 1;
                while (!string.IsNullOrEmpty(following.NextMaxId)
                       && pages < paginationParameters.MaximumPagesToLoad)
                {
                    var nextUri =
                        UriCreator.GetUserFollowingUri(user.Value.Pk, _user.RankToken, searchQuery,
                            userListResponse.Value.NextMaxId);
                    userListResponse = await GetUserListByUriAsync(nextUri);
                    if (!userListResponse.Succeeded)
                        return Result.Fail(userListResponse.Info, following);
                    following.AddRange(
                        userListResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                            .Select(converter => converter.Convert()));
                    pages++;
                    following.NextMaxId = userListResponse.Value.NextMaxId;
                }

                return Result.Success(following);
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserInfo>(exception.Message);
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserInfo>(exception.Message);
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
           return await GetUserMediaAsync(user.Value.Pk, paginationParameters);
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
                var uri = UriCreator.GetUserTagsUri(userId, _user.RankToken, paginationParameters.NextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaMediaList) null);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                userTags.AddRange(
                    mediaResponse.Medias.Select(ConvertersFabric.Instance.GetSingleMediaConverter)
                        .Select(converter => converter.Convert()));
                userTags.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextMedia = await GetUserTagsAsync(userId, paginationParameters);
                    if (!nextMedia.Succeeded)
                        return nextMedia;

                    userTags.AddRange(nextMedia.Value);
                    userTags.NextMaxId = paginationParameters.NextMaxId = nextMedia.Value.NextMaxId;
                }

                return Result.Success(userTags);
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
        public async Task<IResult<InstaFriendshipStatus>> IgnoreFriendshipRequestAsync(long userId)
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
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                    new InstaFriendShipDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (Exception ex)
            {
                return Result.Fail<InstaFriendshipStatus>(ex.Message);
            }
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
            catch (Exception ex)
            {
                return Result.Fail<bool>(ex.Message);
            }
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }

        /// <summary>
        ///     Stop block user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipStatus>> UnBlockUserAsync(long userId)
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
        ///     Stop follow user
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IResult<InstaFriendshipStatus>> UnFollowUserAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await FollowUnfollowUserInternal(userId, UriCreator.GetUnFollowUserUri(userId));
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaFriendshipStatus)null);
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (string)null);
            }
        }
        #endregion public parts

        #region private parts
        private async Task<IResult<InstaFriendshipStatus>> BlockUnblockUserInternal(long userId, Uri instaUri)
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
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                    new InstaFriendShipDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaFriendshipStatus)null);
            }
        }

        private async Task<IResult<InstaFriendshipStatus>> FollowUnfollowUserInternal(long userId, Uri instaUri)
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
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                    new InstaFriendShipDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaFriendshipStatus)null);
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
                new Converters.Json.InstaRecentActivityConverter());
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
                new Converters.Json.InstaRecentActivityConverter());
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserInfo>(exception.Message);
            }
        }

        private async Task<IResult<InstaUserListShortResponse>> GetUserListByUriAsync(Uri uri)
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

        private async Task<IResult<InstaMediaList>> GetUserMediaAsync(long userId,
                                                    PaginationParameters paginationParameters)
        {
            var mediaList = new InstaMediaList();
            try
            {
                var instaUri = UriCreator.GetUserMediaListUri(userId, paginationParameters.NextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaList>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());

                mediaList = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse).Convert();
                mediaList.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId;
                

                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    paginationParameters.PagesLoaded++;
                    var nextMedia = await GetUserMediaAsync(userId, paginationParameters);
                    if (!nextMedia.Succeeded)
                        return Result.Fail(nextMedia.Info, mediaList);
                    mediaList.NextMaxId = paginationParameters.NextMaxId = nextMedia.Value.NextMaxId;
                    mediaList.AddRange(nextMedia.Value);
                }

                mediaList.Pages = paginationParameters.PagesLoaded;
                mediaList.PageSize = mediaResponse.ResultsCount;
                return Result.Success(mediaList);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, mediaList);
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
            catch (Exception ex)
            {
                return Result.Fail<bool>(ex.Message);
            }
        }
        #endregion private parts

    }
}