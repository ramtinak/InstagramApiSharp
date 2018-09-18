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
    internal class MessagingProcessor : IMessagingProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public MessagingProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor,
            IInstaLogger logger, UserAuthValidate userAuthValidate, InstaApi instaApi)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
            _userAuthValidate = userAuthValidate;
            _instaApi = instaApi;
        }
        /// <summary>
        ///     Approve direct pending request
        /// </summary>
        /// <param name="threadId">Thread id</param>
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
        public async Task<IResult<bool>> DeclineAllDirectPendingRequestsAsync()
        {
            return await DeclineDirectPendingRequests(true);
        }

        public async Task<IResult<bool>> DeclineDirectPendingRequestsAsync(params string[] threadIds)
        {
            return await DeclineDirectPendingRequests(false, threadIds);
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
                //Debug.WriteLine(json);
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
        ///     Like direct message in a thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="itemId">Item id (message id)</param>
        public async Task<IResult<bool>> LikeThreadMessageAsync(string threadId, string itemId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetLikeDirectMessageUri();
                //item_type=reaction&reaction_type=like&action=send_item&thread_ids=[340282366841710300949128132202173515958]&client_context=95ffd205-3764-49ac-9529-98b0b5843259&_csrftoken=hbBLcOgXuVNDEOD180DwiaMrcdRUgKmm&_uuid=6324ecb2-e663-4dc8-a3a1-289c699cc876&node_type=item&reaction_status=created&item_id=28340859537572358811808588978192384


                //item_type=reaction&
                //reaction_type=like&
                //action=send_item&
                //thread_ids=[340282366841710300949128132202173515958]&
                //client_context=95ffd205-3764-49ac-9529-98b0b5843259&
                //_csrftoken=hbBLcOgXuVNDEOD180DwiaMrcdRUgKmm&
                //_uuid=6324ecb2-e663-4dc8-a3a1-289c699cc876&
                //node_type=item&
                //reaction_status=created&
                //item_id=28340859537572358811808588978192384
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
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                var instaUri = UriCreator.GetDirectThreadSeenUri(threadId);

                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"item_ids", $"[{itemId}]"},
                };
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
        ///     Send link address to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="link">Link to send</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> SendDirectLinkAsync(string text, string link, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSendDirectLinkUri();
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"link_text", text},
                    {"link_urls", $"[{ExtensionHelper.EncodeList(new string[]{ link })}]"},
                    {"action", "send_item"},
                    {"thread_ids", $"[{threadIds.EncodeList(false)}]"},
                    {"client_context", clientContext},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };

                var request =
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, directSendMessageUri, _deviceInfo);
                var fields = new Dictionary<string, string> { { "text", text } };
                if (!string.IsNullOrEmpty(recipients))
                    fields.Add("recipient_users", "[[" + recipients + "]]");
                else
                    fields.Add("recipient_users", "[]");
                //else
                //    return Result.Fail<InstaDirectInboxThreadList>("Please provide at least one recipient.");
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
            return await _instaApi.HelperProcessor.SendVideoAsync(progress,true, false, "", InstaViewMode.Replayable, InstaStoryType.Both, null, threadId, video);
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
        ///     Send new direct message. (use this function, if you didn't send any message to this user before)
        /// </summary>
        /// <param name="username">Username to send</param>
        /// <param name="text">Message text</param>
        /// <returns>List of threads</returns>
        public async Task<IResult<InstaDirectInboxThreadList>> SendNewDirectMessageAsync(string username, string text)
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
                if (firstRecipient == null)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);

                instaUri = UriCreator.GetParticipantRecipientUserUri(firstRecipient.Pk);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);

                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);

                var respParticipant = JsonConvert.DeserializeObject<InstaDefault>(json);

                if (respParticipant.Status.ToLower() != "ok")
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);



                instaUri = UriCreator.GetParticipantRecipientUserUri(firstRecipient.Pk);
                request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);

                response = await _httpRequestProcessor.SendAsync(request);
                json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDirectInboxThreadList>(response, json);


                var result = await SendDirectTextAsync(firstRecipient.Pk.ToString(), null, text);

                return result;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDirectInboxThreadList>(exception);
            }
        }
        /// <summary>
        ///     Share media to direct thread
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="mediaType">Media id</param>
        /// <param name="threadIds">Thread ids</param>
        public async Task<IResult<bool>> ShareMediaToThreadAsync(string mediaId, InstaMediaType mediaType, params string[] threadIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMediaShareUri(mediaType);
                var clientContext = Guid.NewGuid().ToString();
                var data = new Dictionary<string, string>
                {
                    {"action", "send_item"},
                    {"thread_ids", $"[{threadIds.EncodeList(false)}]"},
                    {"client_context", clientContext},
                    {"media_id", mediaId},
                    {"_csrftoken", _user.CsrfToken},
                    {"unified_broadcast_format", "1"},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };
                var request =
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaSharing>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaSharing>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {

                _logger?.LogException(exception);
                return Result.Fail<InstaSharing>(exception);
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
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
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
                    HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
                if(!all)
                {
                    if(threadIds.Length == 1)
                        instaUri = UriCreator.GetDeclinePendingDirectRequestUri(threadIds.FirstOrDefault());
                    else
                    {
                        instaUri = UriCreator.GetDeclineMultplePendingDirectRequestsUri();
                        data.Add("thread_ids", threadIds.EncodeList(false));
                    }
                }
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
                var progressContent = new ProgressableStreamContent(imageContent, 4096, progress)
                {
                    UploaderProgress = upProgress
                };
                requestContent.Add(progressContent, "photo",
                    $"direct_temp_photo_{ApiRequestMessage.GenerateUploadId()}.jpg");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<bool>(response, json);
                }

                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                if (obj.Status.ToLower() == "ok")
                {
                    upProgress.UploadState = InstaUploadState.Completed;
                    progress?.Invoke(upProgress);
                    return Result.Success(true);
                }
                else
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<bool>(response, json);
                }
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
    }
}