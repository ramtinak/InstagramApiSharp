/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
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
    ///     Messaging (direct) api functions.
    /// </summary>
    internal class MessagingProcessor : IMessagingProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public MessagingProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor,
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
        /// <summary>
        ///     Add users to group thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="userIds">User ids (pk)</param>
        public async Task<IResult<InstaDirectInboxThread>> AddUserToGroupThreadAsync(string threadId, params long[] userIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (userIds == null || userIds != null && !userIds.Any())
                    throw new ArgumentException("UserIds cannot be null or empty.\nAt least one user id require.");

                var instaUri = UriCreator.GetAddUserToDirectThreadUri(threadId);

                var data = new Dictionary<string, string>
                {
                    {"use_unified_inbox", "true"},
                    {"user_ids", $"[{userIds.EncodeList()}]"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThread>(response, json);
                var threadResponse = JsonConvert.DeserializeObject<InstaDirectInboxThreadResponse>(json,
                             new InstaThreadDataConverter());

                //Reverse for Chat Order
                threadResponse.Items.Reverse();
                var converter = ConvertersFabric.Instance.GetDirectThreadConverter(threadResponse);


                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxThread), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThread>(exception);
            }
        }

        /// <summary>
        ///     Approve direct pending request
        /// </summary>
        /// <param name="threadIds">Thread id</param>
        public async Task<IResult<bool>> ApproveDirectPendingRequestAsync(params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                Uri instaUri;
                if (threadIds.Length == 1)
                    instaUri = UriCreator.GetApprovePendingDirectRequestUri(threadIds.FirstOrDefault());
                else
                {
                    instaUri = UriCreator.GetApprovePendingMultipleDirectRequestUri();
                    data.Add("thread_ids", threadIds.EncodeList(false));
                }
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                if (obj.IsSucceed)
                    return Result.Success(true);
                return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Decline all direct pending requests
        /// </summary>
        public async Task<IResult<bool>> DeclineAllDirectPendingRequestsAsync()
        {
            return await DeclineDirectPendingRequests(true);
        }
        /// <summary>
        ///     Decline direct pending requests
        /// </summary>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> DeclineDirectPendingRequestsAsync(params string[] threadIds)
        {
            return await DeclineDirectPendingRequests(false, threadIds);
        }

        /// <summary>
        ///     Delete direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> DeleteDirectThreadAsync(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetHideDirectThreadUri(threadId);

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"use_unified_inbox", "true"}
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
        ///     Delete self message in direct
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> DeleteSelfMessageAsync(string threadId, string itemId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDeleteDirectMessageUri(threadId, itemId);

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
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
        ///     Get direct inbox threads for current user asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaDirectInboxContainer" />
        /// </returns>
        public async Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaDirectInboxContainer Convert(InstaDirectInboxContainerResponse inboxContainerResponse)
                {
                    return ConvertersFabric.Instance.GetDirectInboxConverter(inboxContainerResponse).Convert();
                }

                var inbox = await GetDirectInbox(paginationParameters.NextMaxId);
                if (!inbox.Succeeded)
                    return Result.Fail(inbox.Info, default(InstaDirectInboxContainer));
                var inboxResponse = inbox.Value;
                paginationParameters.NextMaxId = inboxResponse.Inbox.OldestCursor;
                var pagesLoaded = 1;
                while (inboxResponse.Inbox.HasOlder
                      && !string.IsNullOrEmpty(inboxResponse.Inbox.OldestCursor)
                      && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextInbox = await GetDirectInbox(inboxResponse.Inbox.OldestCursor);

                    if (!nextInbox.Succeeded)
                        return Result.Fail(nextInbox.Info, Convert(nextInbox.Value));

                    inboxResponse.Inbox.OldestCursor = paginationParameters.NextMaxId = nextInbox.Value.Inbox.OldestCursor;
                    inboxResponse.Inbox.HasOlder = nextInbox.Value.Inbox.HasOlder;
                    inboxResponse.Inbox.BlendedInboxEnabled = nextInbox.Value.Inbox.BlendedInboxEnabled;
                    inboxResponse.Inbox.UnseenCount = nextInbox.Value.Inbox.UnseenCount;
                    inboxResponse.Inbox.UnseenCountTs = nextInbox.Value.Inbox.UnseenCountTs;
                    inboxResponse.Inbox.Threads.AddRange(nextInbox.Value.Inbox.Threads);
                    pagesLoaded++;
                }

                return Result.Success(ConvertersFabric.Instance.GetDirectInboxConverter(inboxResponse).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxContainer), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxContainer>(exception);
            }
        }
        /// <summary>
        ///     Get direct inbox thread by its id asynchronously
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        public async Task<IResult<InstaDirectInboxThread>> GetDirectInboxThreadAsync(string threadId, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                var thread = await GetDirectInboxThread(threadId, paginationParameters.NextMaxId);
                if (!thread.Succeeded)
                    return Result.Fail(thread.Info, default(InstaDirectInboxThread));
                InstaDirectInboxThread Convert(InstaDirectInboxThreadResponse inboxThreadResponse)
                {
                    return ConvertersFabric.Instance.GetDirectThreadConverter(inboxThreadResponse).Convert();
                }

                var threadResponse = thread.Value;
                paginationParameters.NextMaxId = threadResponse.OldestCursor;
                var pagesLoaded = 1;

                while (threadResponse.HasOlder
                      && !string.IsNullOrEmpty(threadResponse.OldestCursor)
                      && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextThread = await GetDirectInboxThread(threadId, threadResponse.OldestCursor);

                    if (!nextThread.Succeeded)
                        return Result.Fail(nextThread.Info, Convert(nextThread.Value));

                    threadResponse.OldestCursor = paginationParameters.NextMaxId = nextThread.Value.OldestCursor;
                    threadResponse.HasOlder = nextThread.Value.HasOlder;
                    threadResponse.Canonical = nextThread.Value.Canonical;
                    threadResponse.ExpiringMediaReceiveCount = nextThread.Value.ExpiringMediaReceiveCount;
                    threadResponse.ExpiringMediaSendCount = nextThread.Value.ExpiringMediaSendCount;
                    threadResponse.HasNewer = nextThread.Value.HasNewer;
                    threadResponse.LastActivity = nextThread.Value.LastActivity;
                    threadResponse.LastSeenAt = nextThread.Value.LastSeenAt;
                    threadResponse.ReshareReceiveCount = nextThread.Value.ReshareReceiveCount;
                    threadResponse.ReshareSendCount = nextThread.Value.ReshareSendCount;
                    threadResponse.Status = nextThread.Value.Status;
                    threadResponse.Title = nextThread.Value.Title;
                    threadResponse.IsGroup = nextThread.Value.IsGroup;
                    threadResponse.IsSpam = nextThread.Value.IsSpam;
                    threadResponse.IsPin = nextThread.Value.IsPin;
                    threadResponse.Muted = nextThread.Value.Muted;
                    threadResponse.PendingScore = nextThread.Value.PendingScore;
                    threadResponse.Pending = nextThread.Value.Pending;
                    threadResponse.Users = nextThread.Value.Users;
                    threadResponse.ValuedRequest = nextThread.Value.ValuedRequest;
                    threadResponse.VCMuted = nextThread.Value.VCMuted;
                    threadResponse.VieweId = nextThread.Value.VieweId;
                    threadResponse.Items.AddRange(nextThread.Value.Items);
                    pagesLoaded++;
                }

                //Reverse for Chat Order
                threadResponse.Items.Reverse();
                var converter = ConvertersFabric.Instance.GetDirectThreadConverter(threadResponse);


                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxThread), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThread>(exception);
            }
        }

        /// <summary>
        ///     Get direct pending inbox threads for current user asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaDirectInboxContainer" />
        /// </returns>
        public async Task<IResult<InstaDirectInboxContainer>> GetPendingDirectAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaDirectInboxContainer Convert(InstaDirectInboxContainerResponse inboxContainerResponse)
                {
                    return ConvertersFabric.Instance.GetDirectInboxConverter(inboxContainerResponse).Convert();
                }

                var inbox = await GetPendingDirect(paginationParameters.NextMaxId);
                if (!inbox.Succeeded)
                    return Result.Fail(inbox.Info, default(InstaDirectInboxContainer));
                var inboxResponse = inbox.Value;
                paginationParameters.NextMaxId = inboxResponse.Inbox.OldestCursor;
                var pagesLoaded = 1;
                while (inboxResponse.Inbox.HasOlder
                      && !string.IsNullOrEmpty(inboxResponse.Inbox.OldestCursor)
                      && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextInbox = await GetPendingDirect(inboxResponse.Inbox.OldestCursor);

                    if (!nextInbox.Succeeded)
                        return Result.Fail(nextInbox.Info, Convert(nextInbox.Value));

                    inboxResponse.Inbox.OldestCursor = paginationParameters.NextMaxId = nextInbox.Value.Inbox.OldestCursor;
                    inboxResponse.Inbox.HasOlder = nextInbox.Value.Inbox.HasOlder;
                    inboxResponse.Inbox.Threads.AddRange(nextInbox.Value.Inbox.Threads);
                    inboxResponse.Inbox.BlendedInboxEnabled = nextInbox.Value.Inbox.BlendedInboxEnabled;
                    inboxResponse.Inbox.UnseenCount = nextInbox.Value.Inbox.UnseenCount;
                    inboxResponse.Inbox.UnseenCountTs = nextInbox.Value.Inbox.UnseenCountTs;
                    pagesLoaded++;
                }
                return Result.Success(ConvertersFabric.Instance.GetDirectInboxConverter(inboxResponse).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxContainer), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxContainer>(exception);
            }
        }

        /// <summary>
        ///     Get ranked recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        public async Task<IResult<InstaRecipients>> GetRankedRecipientsAsync()
        {
            return await GetRankedRecipientsByUsernameAsync(null);
        }

        /// <summary>
        ///     Get ranked recipients (threads and users) asynchronously
        ///     <para>Note: Some recipient has User, some recipient has Thread</para>
        /// </summary>
        /// <param name="username">Username to search</param>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        public async Task<IResult<InstaRecipients>> GetRankedRecipientsByUsernameAsync(string username)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                Uri instaUri;
                if (string.IsNullOrEmpty(username))
                    instaUri = UriCreator.GetRankedRecipientsUri();
                else
                    instaUri = UriCreator.GetRankRecipientsByUserUri(username);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaRecipients>(response, json);
                var responseRecipients = JsonConvert.DeserializeObject<InstaRankedRecipientsResponse>(json);
                var converter = ConvertersFabric.Instance.GetRecipientsConverter(responseRecipients);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaRecipients), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaRecipients>(exception);
            }
        }

        /// <summary>
        ///     Get recent recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        public async Task<IResult<InstaRecipients>> GetRecentRecipientsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetRecentRecipientsUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaRecipients>(response, json);
                var responseRecipients = JsonConvert.DeserializeObject<InstaRecentRecipientsResponse>(json);
                var converter = ConvertersFabric.Instance.GetRecipientsConverter(responseRecipients);
                return Result.Success(converter.Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaRecipients), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaRecipients>(exception);
            }
        }

        /// <summary>
        ///     Get direct users presence
        ///     <para>Note: You can use this function to find out who is online and who isn't.</para>
        /// </summary>
        public async Task<IResult<InstaUserPresenceList>> GetUsersPresenceAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDirectPresenceUri();

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserPresenceList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaUserPresenceContainerResponse>(json,
                    new InstaUserPresenceContainerDataConverter());
                return Result.Success(ConvertersFabric.Instance.GetUserPresenceListConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserPresenceList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserPresenceList>(exception);
            }
        }

        /// <summary>
        ///     Leave from group thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> LeaveGroupThreadAsync(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetLeaveThreadUri(threadId);
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
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
        ///     Like direct message in a thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="itemId">Item id (message id)</param>
        public async Task<IResult<bool>> LikeThreadMessageAsync(string threadId, string itemId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetLikeUnlikeDirectMessageUri();

                var data = new Dictionary<string, string>
                {
                    {"item_type", "reaction"},
                    {"reaction_type", "like"},
                    {"action", "send_item"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"thread_ids", $"[{threadId}]"},
                    {"client_context", Guid.NewGuid().ToString()},
                    {"node_type", "item"},
                    {"reaction_status", "created"},
                    {"item_id", itemId}
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
        ///     Mark direct message as seen
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="itemId">Message id (item id)</param>
        public async Task<IResult<bool>> MarkDirectThreadAsSeenAsync(string threadId, string itemId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDirectThreadSeenUri(threadId, itemId);

                var data = new Dictionary<string, string>
                {
                    {"thread_id", threadId},
                    {"action", "mark_seen"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"item_id", itemId},
                    {"use_unified_inbox", "true"},
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
        ///     Mute direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> MuteDirectThreadAsync(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMuteDirectThreadUri(threadId);

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
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
        ///     Send disappearing photo to direct thread (video will remove after user saw it)
        /// </summary>
        /// <param name="image">Image to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectDisappearingPhotoAsync(InstaImage image,
            InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds)
        {
            return await SendDirectDisappearingPhotoAsync(null, image, viewMode, threadIds);
        }

        /// <summary>
        ///     Send disappearing photo to direct thread (video will remove after user saw it) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Image to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectDisappearingPhotoAsync(Action<InstaUploaderProgress> progress, InstaImage image,
            InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendPhotoAsync(progress, false, true, "", viewMode, InstaStoryType.Direct, null, threadIds.EncodeList(), image);
        }

        /// <summary>
        ///     Send disappearing video to direct thread (video will remove after user saw it)
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectDisappearingVideoAsync(InstaVideoUpload video,
            InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds)
        {
            return await SendDirectDisappearingVideoAsync(null, video, viewMode, threadIds);
        }

        /// <summary>
        ///     Send disappearing video to direct thread (video will remove after user saw it) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectDisappearingVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video,
            InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendVideoAsync(progress, false, true, "", viewMode, InstaStoryType.Direct, null, threadIds.EncodeList(), video);
        }

        /// <summary>
        ///     Send hashtag to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="hashtag">Hashtag to send</param>
        /// <param name="threadIds">Thread ids</param>
        /// <returns>Returns True if hashtag sent</returns>
        public async Task<IResult<bool>> SendDirectHashtagAsync(string text, string hashtag, params string[] threadIds)
        {
            return await SendDirectHashtagAsync(text, hashtag, threadIds, null);
        }

        /// <summary>
        ///     Send hashtag to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="hashtag">Hashtag to send</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="recipients">Recipients ids</param>
        /// <returns>Returns True if hashtag sent</returns>
        public async Task<IResult<bool>> SendDirectHashtagToRecipientsAsync(string text, string hashtag, params string[] recipients)
        {
            return await SendDirectHashtagAsync(text, hashtag, null, recipients);
        }

        /// <summary>
        ///     Send hashtag to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="hashtag">Hashtag to send</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="recipients">Recipients ids</param>
        /// <returns>Returns True if hashtag sent</returns>
        public async Task<IResult<bool>> SendDirectHashtagAsync(string text, string hashtag, string[] threadIds, string[] recipients)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSendDirectHashtagUri();
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"text", text ?? string.Empty},
                    {"hashtag", hashtag},
                    {"action", "send_item"},
                    {"client_context", clientContext},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                if (threadIds?.Length > 0)
                {
                    data.Add("thread_ids", $"[{threadIds.EncodeList(false)}]");
                }
                if (recipients?.Length > 0)
                {
                    data.Add("recipient_users", "[[" + recipients.EncodeList(false) + "]]");
                }
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
        ///     Send link address to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="link">Link to send</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectLinkAsync(string text, string link, params string[] threadIds)
        {
            return await SendDirectLinkAsync(text, link, threadIds, null);
        }

        /// <summary>
        ///     Send link address to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="link">Link to send</param>
        /// <param name="recipients">Recipients ids</param>
        public async Task<IResult<bool>> SendDirectLinkToRecipientsAsync(string text, string link, params string[] recipients)
        {
            return await SendDirectLinkAsync(text, link, null, recipients);
        }


        /// <summary>
        ///     Send link address to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="link">Link to send</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="recipients">Recipients ids</param>
        public async Task<IResult<bool>> SendDirectLinkAsync(string text, string link, string[] threadIds, string[] recipients)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSendDirectLinkUri();
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"link_text", text ?? string.Empty},
                    {"link_urls", $"[{ExtensionHelper.EncodeList(new[]{ link })}]"},
                    {"action", "send_item"},
                    {"client_context", clientContext},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                if (threadIds?.Length > 0)
                {
                    data.Add("thread_ids", $"[{threadIds.EncodeList(false)}]");
                }
                if (recipients?.Length > 0)
                {
                    data.Add("recipient_users", "[[" + recipients.EncodeList(false) + "]]");
                }
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
        ///     Send location to direct thread
        /// </summary>
        /// <param name="externalId">External id (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectLocationAsync(string externalId, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSendDirectLocationUri();
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"venue_id", externalId},
                    {"action", "send_item"},
                    {"thread_ids", $"[{threadIds.EncodeList(false)}]"},
                    {"client_context", clientContext},
                    {"_csrftoken", _user.CsrfToken},
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
        ///     Send photo to direct thread (single)
        /// </summary>
        /// <param name="image">Image to upload</param>
        /// <param name="threadId">Thread id</param>
        /// <returns>Returns True is sent</returns>
        public async Task<IResult<bool>> SendDirectPhotoAsync(InstaImage image, string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await SendDirectPhotoAsync(null, image, threadId);
        }

        /// <summary>
        ///     Send photo to direct thread (single) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Image to upload</param>
        /// <param name="threadId">Thread id</param>
        /// <returns>Returns True is sent</returns>
        public async Task<IResult<bool>> SendDirectPhotoAsync(Action<InstaUploaderProgress> progress, InstaImage image, string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await SendDirectPhoto(progress, null, threadId, image);
        }

        /// <summary>
        ///     Send photo to multiple recipients (multiple user)
        /// </summary>
        /// <param name="image">Image to upload</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        /// <returns>Returns True is sent</returns>
        public async Task<IResult<bool>> SendDirectPhotoToRecipientsAsync(InstaImage image, params string[] recipients)
        {
            return await SendDirectPhotoToRecipientsAsync(null, image, recipients);
        }

        /// <summary>
        ///     Send photo to multiple recipients (multiple user) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Image to upload</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        /// <returns>Returns True is sent</returns>
        public async Task<IResult<bool>> SendDirectPhotoToRecipientsAsync(Action<InstaUploaderProgress> progress, InstaImage image, params string[] recipients)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await SendDirectPhoto(progress, string.Join(",", recipients), null, image);
        }

        /// <summary>
        ///     Send profile to direct thrad
        /// </summary>
        /// <param name="userIdToSend">User id to send</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectProfileAsync(long userIdToSend, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSendDirectProfileUri();
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"profile_user_id", userIdToSend.ToString()},
                    {"action", "send_item"},
                    {"thread_ids", $"[{threadIds.EncodeList(false)}]"},
                    {"client_context", clientContext},
                    {"_csrftoken", _user.CsrfToken},
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
        ///     Send profile to direct thrad
        /// </summary>
        /// <param name="userIdToSend">User id to send</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectProfileToRecipientsAsync(long userIdToSend, string recipients)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSendDirectProfileUri();
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"profile_user_id", userIdToSend.ToString()},
                    {"action", "send_item"},
                    {"recipient_users", $"[[" + recipients + "]]"},
                    {"client_context", clientContext},
                    {"_csrftoken", _user.CsrfToken},
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
        ///     Send direct text message to provided users and threads
        /// </summary>
        /// <param name="recipients">Comma-separated users PK</param>
        /// <param name="threadIds">Message thread ids</param>
        /// <param name="text">Message text</param>
        /// <returns>List of threads</returns>
        public async Task<IResult<InstaDirectInboxThreadList>> SendDirectTextAsync(string recipients, string threadIds,
            string text)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var threads = new InstaDirectInboxThreadList();
            try
            {
                var directSendMessageUri = UriCreator.GetDirectSendMessageUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, directSendMessageUri, _deviceInfo);
                var fields = new Dictionary<string, string> { { "text", text } };
                if (!string.IsNullOrEmpty(recipients))
                    fields.Add("recipient_users", "[[" + recipients + "]]");
                else
                    fields.Add("recipient_users", "[]");

                if (!string.IsNullOrEmpty(threadIds))
                    fields.Add("thread_ids", "[" + threadIds + "]");

                request.Content = new FormUrlEncodedContent(fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);
                var result = JsonConvert.DeserializeObject<InstaSendDirectMessageResponse>(json);
                if (!result.IsOk()) return Result.Fail<InstaDirectInboxThreadList>(result.Status);
                threads.AddRange(result.Threads.Select(thread =>
                    ConvertersFabric.Instance.GetDirectThreadConverter(thread).Convert()));
                return Result.Success(threads);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxThreadList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThreadList>(exception);
            }
        }

        /// <summary>
        ///     Send video to direct thread (single)
        /// </summary>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> SendDirectVideoAsync(InstaVideoUpload video, string threadId)
        {
            return await SendDirectVideoAsync(null, video, threadId);
        }

        /// <summary>
        ///     Send video to direct thread (single) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> SendDirectVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendVideoAsync(progress, true, false, "", InstaViewMode.Replayable, InstaStoryType.Both, null, threadId, video);
        }

        /// <summary>
        ///     Send video to multiple recipients (multiple user)
        /// </summary>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        public async Task<IResult<bool>> SendDirectVideoToRecipientsAsync(InstaVideoUpload video, params string[] recipients)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await SendDirectVideoToRecipientsAsync(null, video, recipients);
        }

        /// <summary>
        ///     Send video to multiple recipients (multiple user) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        public async Task<IResult<bool>> SendDirectVideoToRecipientsAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, params string[] recipients)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendVideoAsync(progress, true, false, "", InstaViewMode.Replayable, InstaStoryType.Both, recipients.EncodeList(false), null, video);
        }

        /// <summary>
        ///     Share media to direct thread
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="mediaType">Media type</param>
        /// <param name="text">Text to send</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> ShareMediaToThreadAsync(string mediaId, InstaMediaType mediaType, string text, params string[] threadIds)
        {
            try
            {
                if (threadIds == null || threadIds != null && !threadIds.Any())
                    throw new ArgumentException("At least one thread id required");

                return await ShareMedia(mediaId, mediaType, text, threadIds, null);
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
        ///     Share media to user id
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="mediaType">Media type</param>
        /// <param name="text">Text to send</param>
        /// <param name="userIds">User ids (pk)</param>
        public async Task<IResult<bool>> ShareMediaToUserAsync(string mediaId, InstaMediaType mediaType, string text, params long[] userIds)
        {
            try
            {
                if (userIds == null || userIds != null && !userIds.Any())
                    throw new ArgumentException("At least one user id required");

                return await ShareMedia(mediaId, mediaType, text, null, userIds);
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

        private async Task<IResult<bool>> ShareMedia(string mediaId, InstaMediaType mediaType, string text, string[] threadIds, long[] userIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMediaShareUri(mediaType);
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"action", "send_item"},
                    {"client_context", clientContext},
                    {"media_id", mediaId},
                    {"_csrftoken", _user.CsrfToken},
                    {"unified_broadcast_format", "1"},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"text", text ?? string.Empty}
                };
                if (threadIds != null)
                    data.Add("thread_ids", $"[{threadIds.EncodeList(false)}]");
                else
                    data.Add("recipient_users", $"[{userIds.EncodeRecipients()}]");

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
        ///     Share an user
        /// </summary>
        /// <param name="userIdToSend">User id(PK)</param>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<InstaSharing>> ShareUserAsync(string userIdToSend, string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetShareUserUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(userIdToSend), "\"profile_user_id\""},
                    {new StringContent("1"), "\"unified_broadcast_format\""},
                    {new StringContent("send_item"), "\"action\""},
                    {new StringContent($"[{threadId}]"), "\"thread_ids\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.LoggedInUser.Pk.ToString()), "\"_uid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""}

                };
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
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
        ///     UnLike direct message in a thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="itemId">Item id (message id)</param>
        public async Task<IResult<bool>> UnLikeThreadMessageAsync(string threadId, string itemId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetLikeUnlikeDirectMessageUri();

                var data = new Dictionary<string, string>
                {
                    {"item_type", "reaction"},
                    {"reaction_type", "like"},
                    {"action", "send_item"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"thread_ids", $"[{threadId}]"},
                    {"client_context", Guid.NewGuid().ToString()},
                    {"node_type", "item"},
                    {"reaction_status", "deleted"},
                    {"item_id", itemId}
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
        ///     Unmute direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> UnMuteDirectThreadAsync(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUnMuteDirectThreadUri(threadId);

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
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
        ///     Update direct thread title (for groups)
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="title">New title</param>
        public async Task<IResult<bool>> UpdateDirectThreadTitleAsync(string threadId, string title)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDirectThreadUpdateTitleUri(threadId);

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"title", title},
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
        private async Task<IResult<bool>> DeclineDirectPendingRequests(bool all, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDeclineAllPendingDirectRequestsUri();

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                if (!all)
                {
                    if (threadIds.Length == 1)
                        instaUri = UriCreator.GetDeclinePendingDirectRequestUri(threadIds.FirstOrDefault());
                    else
                    {
                        instaUri = UriCreator.GetDeclineMultplePendingDirectRequestsUri();
                        data.Add("thread_ids", threadIds.EncodeList(false));
                    }
                }
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                if (obj.IsSucceed)
                    return Result.Success(true);
                return Result.Fail("Error: " + obj.Message, false);
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
        ///     Send a like to the conversation
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> SendDirectLikeAsync(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDirectThreadBroadcastLikeUri();

                var data = new Dictionary<string, string>
                {
                    {"action", "send_item"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"thread_id", $"{threadId}"},
                    {"client_context", Guid.NewGuid().ToString()}
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
        private async Task<IResult<bool>> SendDirectPhoto(Action<InstaUploaderProgress> progress, string recipients, string threadId, InstaImage image)
        {
            var upProgress = new InstaUploaderProgress
            {
                Caption = string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                var instaUri = UriCreator.GetDirectSendPhotoUri();
                var uploadId = ApiRequestMessage.GenerateRandomUploadId();
                var clientContext = Guid.NewGuid();
                upProgress.UploadId = uploadId;
                progress?.Invoke(upProgress);
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent("send_item"), "\"action\""},
                    {new StringContent(clientContext.ToString()), "\"client_context\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""}
                };
                if (!string.IsNullOrEmpty(recipients))
                    requestContent.Add(new StringContent($"[[{recipients}]]"), "recipient_users");
                else
                    requestContent.Add(new StringContent($"[{threadId}]"), "thread_ids");
                byte[] fileBytes;
                if (image.ImageBytes == null)
                    fileBytes = File.ReadAllBytes(image.Uri);
                else
                    fileBytes = image.ImageBytes;
                var imageContent = new ByteArrayContent(fileBytes);
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent, "photo",
                    $"direct_temp_photo_{ApiRequestMessage.GenerateUploadId()}.jpg");
                //var progressContent = new ProgressableStreamContent(requestContent, 4096, progress)
                //{
                //    UploaderProgress = upProgress
                //};
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                upProgress.UploadState = InstaUploadState.Uploading;
                progress?.Invoke(upProgress);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<bool>(response, json);
                }
                upProgress.UploadState = InstaUploadState.Uploaded;
                progress?.Invoke(upProgress);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                if (obj.Status.ToLower() == "ok")
                {
                    upProgress.UploadState = InstaUploadState.Completed;
                    progress?.Invoke(upProgress);
                    return Result.Success(true);
                }

                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                return Result.UnExpectedResponse<bool>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        private async Task<IResult<InstaDirectInboxContainerResponse>> GetDirectInbox(string maxId = null)
        {
            try
            {
                var directInboxUri = UriCreator.GetDirectInboxUri(maxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxContainerResponse>(response, json);
                var inboxResponse = JsonConvert.DeserializeObject<InstaDirectInboxContainerResponse>(json);
                return Result.Success(inboxResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxContainerResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxContainerResponse>(exception);
            }
        }
        private async Task<IResult<InstaDirectInboxThreadResponse>> GetDirectInboxThread(string threadId, string maxId = null)
        {
            try
            {
                var directInboxUri = UriCreator.GetDirectInboxThreadUri(threadId, maxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadResponse>(response, json);
                var threadResponse = JsonConvert.DeserializeObject<InstaDirectInboxThreadResponse>(json,
                    new InstaThreadDataConverter());

                return Result.Success(threadResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxThreadResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThreadResponse>(exception);
            }
        }
        private async Task<IResult<InstaDirectInboxContainerResponse>> GetPendingDirect(string maxId = null)
        {
            try
            {
                var directInboxUri = UriCreator.GetDirectPendingInboxUri(maxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxContainerResponse>(response, json);
                var inboxResponse = JsonConvert.DeserializeObject<InstaDirectInboxContainerResponse>(json);
                return Result.Success(inboxResponse);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDirectInboxContainerResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxContainerResponse>(exception);
            }
        }
    }
}