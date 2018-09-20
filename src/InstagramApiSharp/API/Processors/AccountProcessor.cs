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
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Enums;

namespace InstagramApiSharp.API.Processors
{
    internal class AccountProcessor : IAccountProcessor
    {
        #region Properties and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        public AccountProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger,
            UserAuthValidate userAuthValidate, InstaApi instaApi)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
            _userAuthValidate = userAuthValidate;
            _instaApi = instaApi;
        }
        #endregion Properties and constructor

        #region Profile edit
        /// <summary>
        ///     Set current account private
        /// </summary>
        public async Task<IResult<InstaUserShort>> SetAccountPrivateAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUriSetAccountPrivate();
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                    JsonConvert.SerializeObject(fields));
                var payload = JsonConvert.SerializeObject(fields);
                var signature = $"{hash}.{Uri.EscapeDataString(payload)}";
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserShort>(response, json);
                var userInfoUpdated =
                    JsonConvert.DeserializeObject<InstaUserShortResponse>(json, new InstaUserShortDataConverter());
                if (userInfoUpdated.Pk < 1)
                    return Result.Fail<InstaUserShort>("Pk is null or empty");
                var converter = ConvertersFabric.Instance.GetUserShortConverter(userInfoUpdated);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaUserShort)null);
            }
        }
        /// <summary>
        ///     Set current account public
        /// </summary>
        public async Task<IResult<InstaUserShort>> SetAccountPublicAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUriSetAccountPublic();
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                    JsonConvert.SerializeObject(fields));
                var payload = JsonConvert.SerializeObject(fields);
                var signature = $"{hash}.{Uri.EscapeDataString(payload)}";
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var userInfoUpdated =
                        JsonConvert.DeserializeObject<InstaUserShortResponse>(json, new InstaUserShortDataConverter());
                    if (userInfoUpdated.Pk < 1)
                        return Result.Fail<InstaUserShort>("Pk is incorrect");
                    var converter = ConvertersFabric.Instance.GetUserShortConverter(userInfoUpdated);
                    return Result.Success(converter.Convert());
                }

                return Result.UnExpectedResponse<InstaUserShort>(response, json);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaUserShort)null);
            }
        }
        /// <summary>
        ///     Change password
        /// </summary>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">
        ///     The new password (shouldn't be the same old password, and should be a password you never used
        ///     here)
        /// </param>
        /// <returns>Return true if the password is changed</returns>
        public async Task<IResult<bool>> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            if (oldPassword == newPassword)
                return Result.Fail("The old password should not the same of the new password", false);

            try
            {
                var changePasswordUri = UriCreator.GetChangePasswordUri();

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"old_password", oldPassword},
                    {"new_password1", newPassword},
                    {"new_password2", newPassword}
                };

                var request = HttpHelper.GetSignedRequest(HttpMethod.Get, changePasswordUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true);
                var error = JsonConvert.DeserializeObject<BadStatusErrorsResponse>(json);
                var errors = "";
                error.Message.Errors.ForEach(errorContent => errors += errorContent + "\n");
                return Result.Fail(errors, false);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }
        /// <summary>
        ///     Edit profile
        /// </summary>
        /// <param name="name">Name (leave null if you don't want to change it)</param>
        /// <param name="biography">Biography (leave null if you don't want to change it)</param>
        /// <param name="url">Url (leave null if you don't want to change it)</param>
        /// <param name="email">Email (leave null if you don't want to change it)</param>
        /// <param name="phone">Phone number (leave null if you don't want to change it)</param>
        /// <param name="gender">Gender type (leave null if you don't want to change it)</param>
        /// <param name="newUsername">New username (optional) (leave null if you don't want to change it)</param>
        public async Task<IResult<InstaAccountUserResponse>> EditProfileAsync(string name, string biography, string url, string email, string phone, InstaGenderType? gender, string newUsername = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var editRequest = await GetRequestForEditProfileAsync();
                if(!editRequest.Succeeded)
                    return Result.Fail("Edit request returns badrequest", (InstaAccountUserResponse)null);
                var user = editRequest.Value.User.Username;

                if (string.IsNullOrEmpty(newUsername))
                    newUsername = user;

                if (name == null)
                    name = editRequest.Value.User.FullName;

                if (biography == null)
                    biography = editRequest.Value.User.Biography;

                if (url == null)
                    url = editRequest.Value.User.ExternalUrl;

                if (email == null)
                    email = editRequest.Value.User.Email;

                if (phone == null)
                    phone = editRequest.Value.User.PhoneNumber;

                if (gender == null)
                    gender = editRequest.Value.User.Gender;

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
                    return Result.UnExpectedResponse<InstaAccountUserResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountUserResponse>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountUserResponse>(exception);
            }
        }
        /// <summary>
        ///     Set biography (support hashtags and user mentions)
        /// </summary>
        /// <param name="bio">Biography text, hashtags or user mentions</param>
        public async Task<IResult<InstaBiography>> SetBiographyAsync(string bio)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var editRequest = await GetRequestForEditProfileAsync();
                if (!editRequest.Succeeded)
                    return Result.Fail("Edit request returns badrequest.\r\nPlease try again.", (InstaBiography)null);

                var instaUri = UriCreator.GetSetBiographyUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    { "raw_text", bio}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBiography>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBiography>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaBiography>(exception);
            }
        }
        /// <summary>
        ///     Get request for edit profile.
        /// </summary>
        public async Task<IResult<InstaAccountUserResponse>> GetRequestForEditProfileAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetRequestForEditProfileUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountUserResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountUserResponse>(json);
                return Result.Success(obj);            
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountUserResponse>(exception);
            }
        }
        /// <summary>
        ///     Set name and phone number.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<bool>> SetNameAndPhoneNumberAsync(string name, string phoneNumber = "")
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
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
        ///     Remove profile picture.
        /// </summary>
        public async Task<IResult<InstaAccountUserResponse>> RemoveProfilePictureAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<InstaAccountUserResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountUserResponse>(json);

                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountUserResponse>(exception);
            }
        }
        /// <summary>
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        public async Task<IResult<InstaAccountUserResponse>> ChangeProfilePictureAsync(byte[] pictureBytes)
        {
            return await ChangeProfilePictureAsync(null, pictureBytes);
        }
        /// <summary>
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary> 
        /// <param name="progress">Progress action</param>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        public async Task<IResult<InstaAccountUserResponse>> ChangeProfilePictureAsync(Action<InstaUploaderProgress> progress, byte[] pictureBytes)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var upProgress = new InstaUploaderProgress
            {
                Caption = string.Empty,
                UploadState = InstaUploadState.Preparing
            };
            try
            {
                var instaUri = UriCreator.GetChangeProfilePictureUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                upProgress.UploadId = uploadId;
                progress?.Invoke(upProgress);
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.LoggedInUser.Pk.ToString()), "\"_uid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""}
                };
                var imageContent = new ByteArrayContent(pictureBytes);
                requestContent.Add(imageContent, "profile_pic", $"r{ApiRequestMessage.GenerateUploadId()}.jpg");
                var progressContent = new ProgressableStreamContent(requestContent, 4096, progress)
                {
                    UploaderProgress = upProgress
                };
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = progressContent;
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (progressContent.UploaderProgress != null)
                    upProgress = progressContent.UploaderProgress;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaAccountUserResponse>(response, json);
                }

                var obj = JsonConvert.DeserializeObject<InstaAccountUserResponse>(json);
                upProgress.UploadState = InstaUploadState.Completed;
                progress?.Invoke(upProgress);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountUserResponse>(exception);
            }
        }
        /// <summary>
        ///     Get request for download backup account data.
        /// </summary>
        /// <param name="email">Email</param>
        public async Task<IResult<InstaRequestDownloadData>> GetRequestForDownloadAccountDataAsync(string email)
        {
            return await GetRequestForDownloadAccountDataAsync(email, null);
        }
        /// <summary>
        ///     Get request for download backup account data.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password (only for facebook logins)</param>
        public async Task<IResult<InstaRequestDownloadData>> GetRequestForDownloadAccountDataAsync(string email, string password)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (string.IsNullOrEmpty(password))
                    password = _user.Password;

                var instaUri = UriCreator.GetRequestForDownloadDataUri();
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"email", email},
                    {"password", password}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaRequestDownloadData>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaRequestDownloadData>(exception);
            }
        }
        #endregion Profile edit

        #region Story settings
        // Story settings
        /// <summary>
        ///     Get story settings.
        /// </summary>
        public async Task<IResult<InstaStorySettings>> GetStorySettingsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetStorySettingsUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStorySettings>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaStorySettings>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStorySettings>(exception);
            }
        }
        /// <summary>
        ///     Enable Save story to gallery.
        /// </summary>
        public async Task<IResult<bool>> EnableSaveStoryToGalleryAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Disable Save story to gallery.
        /// </summary>
        public async Task<IResult<bool>> DisableSaveStoryToGalleryAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Enable Save story to archive.
        /// </summary>
        public async Task<IResult<bool>> EnableSaveStoryToArchiveAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Disable Save story to archive.
        /// </summary>
        public async Task<IResult<bool>> DisableSaveStoryToArchiveAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Allow story sharing.
        /// </summary>
        /// <param name="allow">Allow or disallow story sharing</param>
        public async Task<IResult<bool>> AllowStorySharingAsync(bool allow = true)
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Allow story message replies.
        /// </summary>
        /// <param name="repliesType">Reply typo</param>
        public async Task<IResult<bool>> AllowStoryMessageRepliesAsync(InstaMessageRepliesType repliesType)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetSetReelSettingsUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"message_prefs", repliesType.ToString().ToLower()}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<AccountArchiveStoryResponse>(json);
                if (obj.MessagePrefs.ToLower() == "anyone" && repliesType == InstaMessageRepliesType.Everyone)
                    return Result.Success(true);
                else if (obj.MessagePrefs.ToLower() == "following" && repliesType == InstaMessageRepliesType.Following)
                    return Result.Success(true);
                else if (obj.MessagePrefs.ToLower() == "off" && repliesType == InstaMessageRepliesType.Off)
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
        ///     Check username availablity. (for logged in user)
        /// </summary>
        /// <param name="desiredUsername">Desired username</param>
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
                    return Result.UnExpectedResponse<AccountCheckResponse>(response, json);
                var obj = JsonConvert.DeserializeObject<AccountCheckResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountCheckResponse>(exception);
            }
        }
        #endregion Story settings

        #region two factor authentication enable/disable
        // two factor authentication enable/disable
        /// <summary>
        ///     Get Security settings (two factor authentication and backup codes).
        /// </summary>
        public async Task<IResult<AccountSecuritySettingsResponse>> GetSecuritySettingsInfoAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<AccountSecuritySettingsResponse>(response, json);
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
        ///     Disable two factor authentication.
        /// </summary>        
        public async Task<IResult<bool>> DisableTwoFactorAuthenticationAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<bool>(response, json);
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
        ///     Send two factor enable sms.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<AccountTwoFactorSmsResponse>> SendTwoFactorEnableSmsAsync(string phoneNumber)
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<AccountTwoFactorSmsResponse>(response, json);
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
        ///     Verify enable two factor.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        public async Task<IResult<AccountTwoFactorResponse>> TwoFactorEnableAsync(string phoneNumber, string verificationCode)
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<AccountTwoFactorResponse>(response, json);
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
        ///     Send confirm email.
        /// </summary>
        public async Task<IResult<AccountConfirmEmailResponse>> SendConfirmEmailAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<AccountConfirmEmailResponse>(response, json);
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
        ///     Send sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<AccountSendSmsResponse>> SendSmsCodeAsync(string phoneNumber)
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<AccountSendSmsResponse>(response, json);
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
        ///     Verify sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number (ex: +9891234...)</param>
        /// <param name="verificationCode">Verification code</param>
        public async Task<IResult<AccountVerifySmsResponse>> VerifySmsCodeAsync(string phoneNumber, string verificationCode)
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<AccountVerifySmsResponse>(response, json);
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
        ///     Regenerate two factor backup codes
        /// </summary>
        public async Task<IResult<TwoFactorRegenBackupCodesResponse>> RegenerateTwoFactorBackupCodesAsync()
        {
            try
            {
                var instaUri = UriCreator.GetRegenerateTwoFactorBackUpCodeUri();
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
                    return Result.UnExpectedResponse<TwoFactorRegenBackupCodesResponse>(response, json);

                var obj = JsonConvert.DeserializeObject<TwoFactorRegenBackupCodesResponse>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(obj) : Result.UnExpectedResponse<TwoFactorRegenBackupCodesResponse>(response, json);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<TwoFactorRegenBackupCodesResponse>(exception);
            }
        }
        #endregion two factor authentication enable/disable




        #region NOT COMPLETE FUNCTIONS
        //NOT COMPLETE
        private async Task<IResult<object>> EnablePresenceAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<object>(response, json);

                return null;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<object>(exception);
            }
        }

        //NOT COMPLETE
        private async Task<IResult<object>> DisablePresenceAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                    return Result.UnExpectedResponse<object>(response, json);


                return null;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<object>(exception);
            }
        }

        //NOT COMPLETE
        private async Task<IResult<object>> GetCommentFilterAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
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
                //    return Result.UnExpectedResponse<object>(response, json);
                //{"config_value": 0, "status": "ok"}
                return null;
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<object>(exception);
            }
        }
        #endregion NOT COMPLETE FUNCTIONS

    }
}
