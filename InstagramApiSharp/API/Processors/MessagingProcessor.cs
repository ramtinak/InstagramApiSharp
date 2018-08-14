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
using System.Diagnostics;
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
    public class MessagingProcessor : IMessagingProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public MessagingProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor,
            IInstaLogger logger, UserAuthValidate userAuthValidate)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
            _userAuthValidate = userAuthValidate;
        }
        /// <summary>
        ///     Get direct inbox threads for current user asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaDirectInboxContainer" />
        /// </returns>
        public async Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync(string nextOrCursorId = "")
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var directInboxUri = UriCreator.GetDirectInboxUri(nextOrCursorId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxContainer>(response, json);
                var inboxResponse = JsonConvert.DeserializeObject<InstaDirectInboxContainerResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetDirectInboxConverter(inboxResponse).Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxContainer>(exception.Message);
            }
        }
        /// <summary>
        ///     Get direct inbox thread by its id asynchronously
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        public async Task<IResult<InstaDirectInboxThread>> GetDirectInboxThreadAsync(string threadId, string nextOrCursorId = "")
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var directInboxUri = UriCreator.GetDirectInboxThreadUri(threadId, nextOrCursorId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThread>(response, json);
                var threadResponse = JsonConvert.DeserializeObject<InstaDirectInboxThreadResponse>(json,
                    new InstaThreadDataConverter());

                //Reverse for Chat Order
                threadResponse.Items.Reverse();
                //
                var converter = ConvertersFabric.Instance.GetDirectThreadConverter(threadResponse);

                
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThread>(exception.Message);
            }
        }

        /// <summary>
        ///     Send new direct message. (use this function, if you didn't send any message to this user before)
        /// </summary>
        /// <param name="username">Username to send</param>
        /// <param name="text">Message text</param>
        /// <returns>List of threads</returns>
        public async Task<IResult<InstaDirectInboxThreadList>> SendNewDirectMessage(string username, string text)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetRankRecipientsByUserUri(username);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
      
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);


                var responseRecipients = JsonConvert.DeserializeObject<InstaRankedRecipientsResponse>(json);
                var converter = ConvertersFabric.Instance.GetRecipientsConverter(responseRecipients);
                var recipients = converter.Convert();

                var firstRecipient = recipients.Users?.FirstOrDefault(rec => rec?.UserName.ToLower() == username.ToLower());
                if(firstRecipient == null)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);

                instaUri = UriCreator.GetParticipantRecipientUserUri(firstRecipient.Pk);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);

                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);

                var respParticipant = JsonConvert.DeserializeObject<InstaDefault>(json);

                if(respParticipant.Status.ToLower() != "ok")
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);



                instaUri = UriCreator.GetParticipantRecipientUserUri(firstRecipient.Pk);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);

                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);


                var result = await SendDirectMessage(firstRecipient.Pk.ToString(), null, text);
               
                return result;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThreadList>(exception);
            }
        }
        /// <summary>
        ///     Send direct message to provided users and threads
        /// </summary>
        /// <param name="recipients">Comma-separated users PK</param>
        /// <param name="threadIds">Message thread ids</param>
        /// <param name="text">Message text</param>
        /// <returns>List of threads</returns>
        public async Task<IResult<InstaDirectInboxThreadList>> SendDirectMessage(string recipients, string threadIds,
            string text)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var threads = new InstaDirectInboxThreadList();
            try
            {
                var directSendMessageUri = UriCreator.GetDirectSendMessageUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, directSendMessageUri, _deviceInfo);
                var fields = new Dictionary<string, string> {{"text", text}};
                if (!string.IsNullOrEmpty(recipients))
                    fields.Add("recipient_users", "[[" + recipients + "]]");
                else
                    return Result.Fail<InstaDirectInboxThreadList>("Please provide at least one recipient.");
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
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThreadList>(exception);
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaRecipients>(response, json);
                var responseRecipients = JsonConvert.DeserializeObject<InstaRecentRecipientsResponse>(json);
                var converter = ConvertersFabric.Instance.GetRecipientsConverter(responseRecipients);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaRecipients>(exception.Message);
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
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var userUri = UriCreator.GetRankedRecipientsUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaRecipients>(response, json);
                var responseRecipients = JsonConvert.DeserializeObject<InstaRankedRecipientsResponse>(json);
                var converter = ConvertersFabric.Instance.GetRecipientsConverter(responseRecipients);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaRecipients>(exception.Message);
            }
        }
        /// <summary>
        ///     Approve direct pending request
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> ApproveDirectPendingRequest(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetApprovePendingDirectRequestUri(threadId);

                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                if (obj.IsSucceed)
                    return Result.Success(true);
                else
                    return Result.Fail("Error: " + obj.Message, false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception.Message);
            }
        }
        /// <summary>
        ///     Decline all direct pending requests
        /// </summary>
        public async Task<IResult<bool>> DeclineAllDirectPendingRequests()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetDeclineAllPendingDirectRequestsUri();

                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                if (obj.IsSucceed)
                    return Result.Success(true);
                else
                    return Result.Fail("Error: " + obj.Message, false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception.Message);
            }
        }
        /// <summary>
        ///     Get direct pending inbox threads for current user asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaDirectInboxContainer" />
        /// </returns>
        public async Task<IResult<InstaDirectInboxContainer>> GetPendingDirectAsync(string nextOrCursorId = "")
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var directInboxUri = UriCreator.GetDirectPendingInboxUri(nextOrCursorId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxContainer>(response, json);
                var inboxResponse = JsonConvert.DeserializeObject<InstaDirectInboxContainerResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetDirectInboxConverter(inboxResponse).Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxContainer>(exception.Message);
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (InstaSharing)null);
                var obj = JsonConvert.DeserializeObject<InstaSharing>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaSharing>(exception);
            }
        }


    }
}