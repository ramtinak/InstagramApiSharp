/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Discover api functions.
    /// </summary>
    internal class DiscoverProcessor : IDiscoverProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public DiscoverProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        ///     Clear Recent searches
        /// </summary>
        public async Task<IResult<bool>> ClearRecentSearchsAsync()
        {
            try
            {
                var instaUri = UriCreator.GetClearSearchHistoryUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
        ///     Get discover user chaining list 
        /// </summary>
        public async Task<IResult<InstaUserChainingList>> GetChainingUsersAsync()
        {
            try
            {
                var instaUri = UriCreator.GetDiscoverChainingUri(_user.LoggedInUser.Pk);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserChainingList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaUserChainingContainerResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetUserChainingListConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserChainingList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserChainingList>(exception);
            }
        }

        /// <summary>
        ///     Get recent searches
        /// </summary>
        public async Task<IResult<InstaDiscoverRecentSearches>> GetRecentSearchesAsync()
        {
            try
            {
                var instaUri = UriCreator.GetRecentSearchUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDiscoverRecentSearches>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaDiscoverRecentSearchesResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetDiscoverRecentSearchesConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDiscoverRecentSearches), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDiscoverRecentSearches>(exception);
            }
        }

        /// <summary>
        /// Get top searches
        /// </summary>
        /// <param name="querry">querry string of the search</param>
        /// <param name="searchType">Search type(only blended and users works)</param>
        /// <param name="timezone_offset">Timezone offset of the search region (GMT Offset * 60 * 60 - Like Tehran GMT +3:30 = 3.5* 60*60 = 12600)</param>
        /// <returns></returns>
        public async Task<IResult<InstaDiscoverTopSearches>> GetTopSearchesAsync(string querry = "", InstaDiscoverSearchType searchType = InstaDiscoverSearchType.Users, int timezone_offset = 12600)
        {
            try
            {
                var instaUri = UriCreator.GetTopSearchUri(_user.RankToken, querry, searchType, timezone_offset);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDiscoverTopSearches>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaDiscoverTopSearchesResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetDiscoverTopSearchesConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDiscoverTopSearches), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDiscoverTopSearches>(exception);
            }
        }

        /// <summary>
        ///     Get suggested searches
        /// </summary>
        /// <param name="searchType">Search type(only blended and users works)</param>
        public async Task<IResult<InstaDiscoverSuggestedSearches>> GetSuggestedSearchesAsync(InstaDiscoverSearchType searchType =
            InstaDiscoverSearchType.Users)
        {
            try
            {
                var instaUri = UriCreator.GetSuggestedSearchUri(searchType);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDiscoverSuggestedSearches>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaDiscoverSuggestedSearchesResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetDiscoverSuggestedSearchesConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDiscoverSuggestedSearches), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDiscoverSuggestedSearches>(exception);
            }
        }
        /// <summary>
        ///     Search user people
        /// </summary>
        /// <param name="query">Text to search</param>
        /// <param name="count">Count</param>
        public async Task<IResult<InstaDiscoverSearchResult>> SearchPeopleAsync(string query, int count = 30)
        {
            try
            {
                var instaUri = UriCreator.GetSearchUserUri(query, count);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDiscoverSearchResult>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaDiscoverSearchResultResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetDiscoverSearchResultConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDiscoverSearchResult), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDiscoverSearchResult>(exception);
            }
        }
        #region Other functions

        /// <summary>
        ///     Sync your phone contact list to instagram
        ///     <para>Note:You can find your friends in instagram with this function</para>
        /// </summary>
        /// <param name="instaContacts">Contact list</param>
        public async Task<IResult<InstaContactUserList>> SyncContactsAsync(params InstaContact[] instaContacts)
        {
            try
            {
                var contacts = new InstaContactList();
                contacts.AddRange(instaContacts);
                return await SyncContactsAsync(contacts);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaContactUserList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaContactUserList>(exception);
            }
        }
        /// <summary>
        ///     Sync your phone contact list to instagram
        ///     <para>Note:You can find your friends in instagram with this function</para>
        /// </summary>
        /// <param name="instaContacts">Contact list</param>
        public async Task<IResult<InstaContactUserList>> SyncContactsAsync(InstaContactList instaContacts)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSyncContactsUri();

                var jsonContacts = JsonConvert.SerializeObject(instaContacts);

                var fields = new Dictionary<string, string>
                {
                    {"contacts", jsonContacts}
                };

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);

                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaContactUserList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaContactUserListResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetUserContactListConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaContactUserList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaContactUserList>(exception);
            }
        }

        #endregion Other functions




        /// <summary>
        ///     NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        private async Task<IResult<object>> DiscoverPeopleAsync()
        {
            try
            {
                var instaUri = UriCreator.GetDiscoverPeopleUri();
                Debug.WriteLine(instaUri.ToString());

                var data = new JObject
                {
                    { "phone_id", _deviceInfo.DeviceGuid.ToString()},
                    { "module","discover_people"},
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    { "paginate","true"}
                    //{"_uid", _user.LoggedInUder.Pk.ToString()},
                };

                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Host = "i.instagram.com";
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDefaultResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDefaultResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDefaultResponse>(exception);
            }
        }
    }
}
