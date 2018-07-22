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
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using InstagramApiSharp.Helpers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.Models;
using System.Net;

namespace InstagramApiSharp.API.Processors
{
    internal class AccountProcessor : IAccountProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;

        public AccountProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }


        /// <summary>
        /// Edit profile.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="phone">Phone number</param>
        /// <param name="name">Name</param>
        /// <param name="biography">Biography</param>
        /// <param name="email">Email</param>
        /// <param name="gender">Gender type</param>
        /// <param name="newUsername">New username (optional)</param>
        /// <returns></returns>
        public async Task<IResult<AccountUserResponse>> EditProfileAsync(string url, string phone, string name, string biography, string email, GenderType gender, string newUsername = null)
        {
            try
            {
                var editRequest = await GetRequestForEditProfileAsync();
                if(!editRequest.Succeeded)
                    return Result.Fail("Edit request returns badrequest", (AccountUserResponse)null);
                var user = editRequest.Value.User.Username;

                if (string.IsNullOrEmpty(newUsername))
                    newUsername = user;
                var instaUri = UriCreator.GetEditProfileUri();

                var data = new JObject
                {
                    {"external_url", url},
                    {"gender", ((int)gender).ToString()},
                    {"phone_number", phone},
                    {"_csrftoken", _user.CsrfToken},
                    {"username", newUsername},
                    {"first_name", name},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"biography", biography},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"email", email},
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountUserResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountUserResponse>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountUserResponse>(exception);
            }
        }
        /// <summary>
        /// Get request for edit profile.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<AccountUserResponse>> GetRequestForEditProfileAsync()
        {
            try
            {
                var instaUri = UriCreator.GetRequestForEditProfileUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountUserResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountUserResponse>(json);                
                return Result.Success(obj);            
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountUserResponse>(exception);
            }
        }
        /// <summary>
        /// Set name and phone number.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns></returns>
        public async Task<IResult<bool>> SetNameAndPhoneNumberAsync(string name, string phoneNumber = "")
        {
            try
            {
                var instaUri = UriCreator.GetProfileSetPhoneAndNameUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    { "_csrftoken", _user.CsrfToken},
                    {"first_name", name},
                    {"phone_number", phoneNumber}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                var obj = JsonConvert.DeserializeObject<AccountDefaultResponse>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                else
                    return Result.Success(false);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Remove profile picture.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<AccountUserResponse>> RemoveProfilePictureAsync()
        {
            try
            {
                var instaUri = UriCreator.GetRemoveProfilePictureUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    { "_csrftoken", _user.CsrfToken}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountUserResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountUserResponse>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<AccountUserResponse>(exception);
            }
        }
        /// <summary>
        /// Change profile picture(only jpg and jpeg formats).
        /// </summary>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        /// <returns></returns>
        public async Task<IResult<AccountUserResponse>> ChangeProfilePictureAsync(byte[] pictureBytes)
        {
            try
            {
                var instaUri = UriCreator.GetChangeProfilePictureUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.LoggedInUser.Pk.ToString()), "\"_uid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""}
                };
                var imageContent = new ByteArrayContent(pictureBytes);
                requestContent.Add(imageContent, "profile_pic", $"r{ApiRequestMessage.GenerateUploadId()}.jpg");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountUserResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountUserResponse>(json);       
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountUserResponse>(exception);
            }
        }

        
        // Story settings
        /// <summary>
        /// Get story settings.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<AccountSettingsResponse>> GetStorySettingsAsync()
        {
            try
            {
                var instaUri = UriCreator.GetStorySettingsUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountSettingsResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountSettingsResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountSettingsResponse>(exception);
            }
        }
        /// <summary>
        /// Enable Save story to gallery.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<bool>> EnableSaveStoryToGalleryAsync()
        {
            try
            {            
                var instaUri = UriCreator.GetSetReelSettingsUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"save_to_camera_roll", 1.ToString()}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                var obj = JsonConvert.DeserializeObject<AccountArchiveStoryResponse>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                else
                    return Result.Success(false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Disable Save story to gallery.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<bool>> DisableSaveStoryToGalleryAsync()
        {
            try
            {
                var instaUri = UriCreator.GetSetReelSettingsUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"save_to_camera_roll", 0.ToString()}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                var obj = JsonConvert.DeserializeObject<AccountArchiveStoryResponse>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                else
                    return Result.Success(false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Enable Save story to archive.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<bool>> EnableSaveStoryToArchiveAsync()
        {
            try
            {
                //POST /api/v1/users/set_reel_settings/ HTTP/1.1
                var instaUri = UriCreator.GetSetReelSettingsUri();
                Debug.WriteLine(instaUri.ToString());

                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"reel_auto_archive", "on"}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                Debug.WriteLine(json);
                var obj = JsonConvert.DeserializeObject<AccountArchiveStoryResponse>(json);
                //{"reel_auto_archive": "on", "message_prefs": null, "status": "ok"}
                if (obj.ReelAutoArchive.ToLower() == "on")
                    return Result.Success(true);
                else
                    return Result.Success(false);

            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Disable Save story to archive.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<bool>> DisableSaveStoryToArchiveAsync()
        {
            try
            {
                var instaUri = UriCreator.GetSetReelSettingsUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"check_pending_archive", "1"},
                    {"reel_auto_archive", "off"}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                var obj = JsonConvert.DeserializeObject<AccountArchiveStoryResponse>(json);
                if(obj.ReelAutoArchive.ToLower() == "off")
                    return Result.Success(true);
                else
                    return Result.Success(false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Allow story sharing.
        /// </summary>
        /// <param name="allow"></param>
        /// <returns></returns>
        public async Task<IResult<bool>> AllowStorySharingAsync(bool allow = true)
        {
            try
            {
                var instaUri = UriCreator.GetSetReelSettingsUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                };
                if (allow)
                    data.Add("allow_story_reshare", "1");
                else
                    data.Add("allow_story_reshare", "0");
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                var obj = JsonConvert.DeserializeObject<AccountArchiveStoryResponse>(json);
                if (obj.Status.ToLower() == "off")
                    return Result.Success(true);
                else
                    return Result.Success(false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Allow story message replies.
        /// </summary>
        /// <param name="repliesType">Reply typo</param>
        /// <returns></returns>
        public async Task<IResult<bool>> AllowStoryMessageRepliesAsync(MessageRepliesType repliesType)
        {
            try
            {
                var instaUri = UriCreator.GetSetReelSettingsUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                };
                switch (repliesType)
                {
                    case MessageRepliesType.Everyone:
                        data.Add("message_prefs", "anyone");
                        break;
                    case MessageRepliesType.Following:
                        data.Add("message_prefs", "following");
                        break;
                    case MessageRepliesType.Off:
                        data.Add("message_prefs", "off");
                        break;
                }
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                var obj = JsonConvert.DeserializeObject<AccountArchiveStoryResponse>(json);
                if (obj.MessagePrefs.ToLower() == "anyone" && repliesType == MessageRepliesType.Everyone)
                    return Result.Success(true);
                else if (obj.MessagePrefs.ToLower() == "following" && repliesType == MessageRepliesType.Following)
                    return Result.Success(true);
                else if (obj.MessagePrefs.ToLower() == "off" && repliesType == MessageRepliesType.Off)
                    return Result.Success(true);
                else
                    return Result.Success(false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Check username availablity.
        /// </summary>
        /// <param name="desiredUsername">Desired username</param>
        /// <returns></returns>
        public async Task<IResult<AccountCheckResponse>> CheckUsernameAsync(string desiredUsername)
        {
            try
            {
                var instaUri = UriCreator.GetCheckUsernameUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"username", desiredUsername}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountCheckResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountCheckResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountCheckResponse>(exception);
            }
        }
        

        // two factor authentication enable/disable
        /// <summary>
        /// Get Security settings (two factor authentication and backup codes).
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<AccountSecuritySettingsResponse>> GetSecuritySettingsInfoAsync()
        {
            try
            {
                var instaUri = UriCreator.GetAccountSecurityInfoUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountSecuritySettingsResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountSecuritySettingsResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountSecuritySettingsResponse>(exception);
            }
        }
        /// <summary>
        /// Disable two factor authentication.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<bool>> DisableTwoFactorAuthenticationAsync()
        {
            try
            {
                var instaUri = UriCreator.GetDisableSmsTwoFactorUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, false);
                var obj = JsonConvert.DeserializeObject<AccountCheckResponse>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                else
                    return Result.Success(false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        /// Send two factor enable sms.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns></returns>
        public async Task<IResult<AccountTwoFactorSmsResponse>> SendTwoFactorEnableSmsAsync(string phoneNumber)
        {
            try
            {
                var instaUri = UriCreator.GetSendTwoFactorEnableSmsUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    { "device_id", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    { "phone_number", phoneNumber}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountTwoFactorSmsResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountTwoFactorSmsResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountTwoFactorSmsResponse>(exception);
            }
        }
        /// <summary>
        /// Verify enable two factor.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        /// <returns></returns>
        public async Task<IResult<AccountTwoFactorResponse>> TwoFactorEnableAsync(string phoneNumber, string verificationCode)
        {
            try
            {
                var instaUri = UriCreator.GetEnableSmsTwoFactorUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    { "device_id", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    { "phone_number", phoneNumber},
                    { "verification_code", verificationCode}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountTwoFactorResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountTwoFactorResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountTwoFactorResponse>(exception);
            }
        }
        /// <summary>
        /// Send confirm email.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<AccountConfirmEmailResponse>> SendConfirmEmailAsync()
        {
            try
            {
                var instaUri = UriCreator.GetAccountSendConfirmEmailUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"send_source", "edit_profile"}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountConfirmEmailResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountConfirmEmailResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountConfirmEmailResponse>(exception);
            }
        }
        /// <summary>
        /// Send sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns></returns>
        public async Task<IResult<AccountSendSmsResponse>> SendSmsCodeAsync(string phoneNumber)
        {
            try
            {
                var instaUri = UriCreator.GetAccountSendSmsCodeUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    { "device_id", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    { "phone_number", phoneNumber}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountSendSmsResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountSendSmsResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountSendSmsResponse>(exception);
            }
        }
        /// <summary>
        /// Verify sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number (ex: +9891234...)</param>
        /// <param name="verificationCode">Verification code</param>
        /// <returns></returns>
        public async Task<IResult<AccountVerifySmsResponse>> VerifySmsCodeAsync(string phoneNumber, string verificationCode)
        {
            try
            {
                var instaUri = UriCreator.GetAccountVerifySmsCodeUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    { "device_id", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    { "phone_number", phoneNumber},
                    { "verification_code", verificationCode}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountVerifySmsResponse)null);
                var obj = JsonConvert.DeserializeObject<AccountVerifySmsResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountVerifySmsResponse>(exception);
            }
        }



        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <param name="bio"></param>
        /// <returns></returns>
        public async Task<IResult<object>> SetBiographyAsync(string bio)
        {
            try
            {
                //POST /api/v1/accounts/set_biography/ HTTP/1.1

                var instaUri = new Uri(InstaApiConstants.BASE_INSTAGRAM_API_URL + $"accounts/set_biography/");
                Debug.WriteLine(instaUri.ToString());

                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    { "raw_text", bio}
                };
                Debug.WriteLine("-----------------------");
                Debug.WriteLine(JsonConvert.SerializeObject(data));
                Debug.WriteLine("--");

                Debug.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

                Debug.WriteLine("-----------------------");
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(response.StatusCode);
                // hamash NotFound return mikone:|
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (BroadcastCommentResponse)null);
                Debug.WriteLine(json);
                var obj = JsonConvert.DeserializeObject<BroadcastCommentResponse>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<BroadcastCommentResponse>(exception);
            }
        }
        //NOT COMPLETE
        public async Task<IResult<object>> EnablePresenceAsync()
        {
            try
            {
                var instaUri = UriCreator.GetAccountSetPresenseDisabledUri();
                Debug.WriteLine(instaUri.ToString());

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"disabled", "0"},
                    { "_csrftoken", _user.CsrfToken}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);

                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(response.StatusCode);

                Debug.WriteLine(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountBesties)null);

                return null;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<object>(exception);
            }
        }

        //NOT COMPLETE
        public async Task<IResult<object>> DisablePresenceAsync()
        {
            try
            {
                var instaUri = UriCreator.GetAccountSetPresenseDisabledUri();
                Debug.WriteLine(instaUri.ToString());

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"disabled", "1"},
                    { "_csrftoken", _user.CsrfToken}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);

                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail("Status code: " + response.StatusCode, (AccountBesties)null);


                return null;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<object>(exception);
            }
        }

        //NOT COMPLETE
        public async Task<IResult<object>> GetCommentFilterAsync()
        {
            try
            {
                var instaUri = new Uri(InstaApiConstants.BASE_INSTAGRAM_API_URL + $"accounts/get_comment_filter/");
                Debug.WriteLine(instaUri.ToString());

             
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);

                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(json);
                //if (response.StatusCode != HttpStatusCode.OK)
                //    return Result.Fail("Status code: " + response.StatusCode, false);
                //{"config_value": 0, "status": "ok"}
                return null;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<object>(exception);
            }
        }

    }
}
