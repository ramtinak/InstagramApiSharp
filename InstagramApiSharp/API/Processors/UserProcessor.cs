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

namespace InstagramApiSharp.API.Processors
{
    public class UserProcessor : IUserProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;

        public UserProcessor(AndroidDevice deviceInfo, UserSessionData user, IHttpRequestProcessor httpRequestProcessor,
            IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }

        public async Task<IResult<InstaMediaList>> GetUserMediaAsync(long userId,
            PaginationParameters paginationParameters)
        {
            var mediaList = new InstaMediaList();
            try
            {
                var instaUri = UriCreator.GetUserMediaListUri(userId, paginationParameters.NextId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaList>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());

                mediaList = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse).Convert();
                mediaList.NextId = paginationParameters.NextId = mediaResponse.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextMedia = await GetUserMediaAsync(userId, paginationParameters);
                    if (!nextMedia.Succeeded)
                        return Result.Fail(nextMedia.Info, mediaList);
                    mediaList.NextId = paginationParameters.NextId = nextMedia.Value.NextId;
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

        public async Task<IResult<InstaUser>> GetUserAsync(string username)
        {
            try
            {
                var userUri = UriCreator.GetUserUri(username);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
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
                var user = userInfo.Users?.FirstOrDefault(u => u.UserName == username);
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

        public async Task<IResult<InstaUserInfo>> GetUserInfoByIdAsync(long pk)
        {
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

        public async Task<IResult<InstaUserInfo>> GetUserInfoByUsernameAsync(string username)
        {
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

        private async Task<IResult<InstaUserInfo>> GetUserInfoAsync(Uri userUri)
        {
            try
            {
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
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

        public async Task<IResult<InstaCurrentUser>> GetCurrentUserAsync()
        {
            try
            {
                var instaUri = UriCreator.GetCurrentUserUri();
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
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

        public async Task<IResult<InstaUserShortList>> GetUserFollowersAsync(string username,
            PaginationParameters paginationParameters, string searchQuery)
        {
            var followers = new InstaUserShortList();
            try
            {
                var user = await GetUserAsync(username);
                var userFollowersUri =
                    UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken, searchQuery,
                        paginationParameters.NextId);
                var followersResponse = await GetUserListByUriAsync(userFollowersUri);
                if (!followersResponse.Succeeded)
                    return Result.Fail(followersResponse.Info, (InstaUserShortList) null);
                followers.AddRange(
                    followersResponse.Value.Items?.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                followers.NextId = followersResponse.Value.NextMaxId;

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
                    followers.NextId = followersResponse.Value.NextMaxId;
                }

                return Result.Success(followers);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, followers);
            }
        }

        public async Task<IResult<InstaUserShortList>> GetUserFollowingAsync(string username,
            PaginationParameters paginationParameters, string searchQuery)
        {
            var following = new InstaUserShortList();
            try
            {
                var user = await GetUserAsync(username);
                var uri = UriCreator.GetUserFollowingUri(user.Value.Pk, _user.RankToken, searchQuery,
                    paginationParameters.NextId);
                var userListResponse = await GetUserListByUriAsync(uri);
                if (!userListResponse.Succeeded)
                    return Result.Fail(userListResponse.Info, (InstaUserShortList) null);
                following.AddRange(
                    userListResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                following.NextId = userListResponse.Value.NextMaxId;
                var pages = 1;
                while (!string.IsNullOrEmpty(following.NextId)
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
                    following.NextId = userListResponse.Value.NextMaxId;
                }

                return Result.Success(following);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, following);
            }
        }

        public async Task<IResult<InstaUserShortList>> GetCurrentUserFollowersAsync(
            PaginationParameters paginationParameters)
        {
            return await GetUserFollowersAsync(_user.UserName, paginationParameters, string.Empty);
        }

        public async Task<IResult<InstaMediaList>> GetUserTagsAsync(long userId,
            PaginationParameters paginationParameters)
        {
            var userTags = new InstaMediaList();
            try
            {
                var uri = UriCreator.GetUserTagsUri(userId, _user.RankToken, paginationParameters.NextId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaMediaList) null);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                userTags.AddRange(
                    mediaResponse.Medias.Select(ConvertersFabric.Instance.GetSingleMediaConverter)
                        .Select(converter => converter.Convert()));
                userTags.NextId = paginationParameters.NextId = mediaResponse.NextMaxId;
                paginationParameters.PagesLoaded++;

                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextMedia = await GetUserTagsAsync(userId, paginationParameters);
                    if (!nextMedia.Succeeded)
                        return nextMedia;

                    userTags.AddRange(nextMedia.Value);
                    userTags.NextId = paginationParameters.NextId = nextMedia.Value.NextId;
                }

                return Result.Success(userTags);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, userTags);
            }
        }

        public async Task<IResult<InstaFriendshipStatus>> FollowUserAsync(long userId)
        {
            return await FollowUnfollowUserInternal(userId, UriCreator.GetFollowUserUri(userId));
        }

        public async Task<IResult<InstaFriendshipStatus>> UnFollowUserAsync(long userId)
        {
            return await FollowUnfollowUserInternal(userId, UriCreator.GetUnFollowUserUri(userId));
        }

        public async Task<IResult<InstaFriendshipStatus>> BlockUserAsync(long userId)
        {
            return await BlockUnblockUserInternal(userId, UriCreator.GetBlockUserUri(userId));
        }

        public async Task<IResult<InstaFriendshipStatus>> UnBlockUserAsync(long userId)
        {
            return await BlockUnblockUserInternal(userId, UriCreator.GetUnBlockUserUri(userId));
        }

        public async Task<IResult<InstaFriendshipStatus>> GetFriendshipStatusAsync(long userId)
        {
            try
            {
                var userUri = UriCreator.GetUserFriendshipUri(userId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
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
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
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
                return Result.Fail(exception.Message, (InstaFriendshipStatus) null);
            }
        }

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
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
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
                return Result.Fail(exception.Message, (InstaFriendshipStatus) null);
            }
        }

        private async Task<IResult<InstaUserListShortResponse>> GetUserListByUriAsync(Uri uri)
        {
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.UnExpectedResponse<InstaUserListShortResponse>(response, json);
            var instaUserListResponse = JsonConvert.DeserializeObject<InstaUserListShortResponse>(json);
            if (instaUserListResponse.IsOk())
                return Result.Success(instaUserListResponse);
            return Result.UnExpectedResponse<InstaUserListShortResponse>(response, json);
        }
    }
}