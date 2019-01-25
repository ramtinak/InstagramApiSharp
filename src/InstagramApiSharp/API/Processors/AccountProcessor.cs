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
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using InstagramApiSharp.Helpers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.Models;
using System.Net;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Classes.ResponseWrappers.Business;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Account api functions.
    ///     <para>Note: this is for self account.</para>
    /// </summary>
    internal class AccountProcessor : IAccountProcessor
    {
        #region Properties and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        private readonly HttpHelper _httpHelper;
        public AccountProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
                var hash = CryptoHelper.CalculateHash(_httpHelper._apiVersion.SignatureKey,
                    JsonConvert.SerializeObject(fields));
                var payload = JsonConvert.SerializeObject(fields);
                var signature = $"{hash}.{Uri.EscapeDataString(payload)}";
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
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
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserShort), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserShort>(exception);
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
                var hash = CryptoHelper.CalculateHash(_httpHelper._apiVersion.SignatureKey,
                    JsonConvert.SerializeObject(fields));
                var payload = JsonConvert.SerializeObject(fields);
                var signature = $"{hash}.{Uri.EscapeDataString(payload)}";
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
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
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserShort), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserShort>(exception);
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

                var request = _httpHelper.GetSignedRequest(HttpMethod.Get, changePasswordUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true);
                var error = JsonConvert.DeserializeObject<BadStatusErrorsResponse>(json);
                var errors = "";
                error.Message.Errors.ForEach(errorContent => errors += errorContent + "\n");
                return Result.Fail(errors, false);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception, false);
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
        public async Task<IResult<InstaUserEdit>> EditProfileAsync(string name, string biography, string url, string email, string phone, InstaGenderType? gender, string newUsername = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var editRequest = await GetRequestForEditProfileAsync();
                if(!editRequest.Succeeded)
                    return Result.Fail("Edit request returns badrequest", (InstaUserEdit)null);
                var user = editRequest.Value.Username;

                if (string.IsNullOrEmpty(newUsername))
                    newUsername = user;

                if (name == null)
                    name = editRequest.Value.FullName;

                if (biography == null)
                    biography = editRequest.Value.Biography;

                if (url == null)
                    url = editRequest.Value.ExternalUrl;

                if (email == null)
                    email = editRequest.Value.Email;

                if (phone == null)
                    phone = editRequest.Value.PhoneNumber;

                if (gender == null)
                    gender = editRequest.Value.Gender;

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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserEdit>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaUserEditContainer>(json);

                return Result.Success(obj.User);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserEdit), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserEdit>(exception);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBiography>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBiography>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBiography), ResponseType.NetworkProblem);
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
        public async Task<IResult<InstaUserEdit>> GetRequestForEditProfileAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetRequestForEditProfileUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserEdit>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaUserEditContainer>(json);
                return Result.Success(obj.User);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserEdit), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUserEdit>(exception);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                return Result.Success(false);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(bool), ResponseType.NetworkProblem);
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
        public async Task<IResult<InstaUserEdit>> RemoveProfilePictureAsync()
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUserEdit>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaUserEditContainer>(json);

                return Result.Success(obj.User);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserEdit), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaUserEdit>(exception);
            }
        }
        /// <summary>
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        public async Task<IResult<InstaUserEdit>> ChangeProfilePictureAsync(byte[] pictureBytes)
        {
            return await ChangeProfilePictureAsync(null, pictureBytes);
        }
        /// <summary>
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary> 
        /// <param name="progress">Progress action</param>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        public async Task<IResult<InstaUserEdit>> ChangeProfilePictureAsync(Action<InstaUploaderProgress> progress, byte[] pictureBytes)
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
            
                //if (progressContent.UploaderProgress != null)
                //    upProgress = progressContent.UploaderProgress;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    upProgress.UploadState = InstaUploadState.Error;
                    progress?.Invoke(upProgress);
                    return Result.UnExpectedResponse<InstaUserEdit>(response, json);
                }
                upProgress.UploadState = InstaUploadState.Uploaded;
                progress?.Invoke(upProgress);

                var obj = JsonConvert.DeserializeObject<InstaUserEditContainer>(json);
                upProgress.UploadState = InstaUploadState.Completed;
                progress?.Invoke(upProgress);
                return Result.Success(obj.User);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUserEdit), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                upProgress.UploadState = InstaUploadState.Error;
                progress?.Invoke(upProgress);
                _logger?.LogException(exception);
                return Result.Fail<InstaUserEdit>(exception);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaRequestDownloadData>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaRequestDownloadData), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaRequestDownloadData>(exception);
            }
        }
        /// <summary>
        ///     Upload nametag image
        /// </summary>
        /// <param name="nametagImage">Nametag image</param>
        public async Task<IResult<InstaMedia>> UploadNametagAsync(InstaImage nametagImage)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendMediaPhotoAsync(null, nametagImage.ConvertToImageUpload(), null, null, true);
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
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStorySettings>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaStorySettings>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStorySettings), ResponseType.NetworkProblem);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                return Result.Success(false);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                return Result.Success(false);
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
        ///     Enable Save story to archive.
        /// </summary>
        public async Task<IResult<bool>> EnableSaveStoryToArchiveAsync()
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
                    {"reel_auto_archive", "on"}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaAccountArchiveStory>(json);
                if (obj.ReelAutoArchive.ToLower() == "on")
                    return Result.Success(true);
                return Result.Success(false);

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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountArchiveStory>(json);
                if(obj.ReelAutoArchive.ToLower() == "off")
                    return Result.Success(true);
                return Result.Success(false);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountArchiveStory>(json);
                if (obj.Status.ToLower() == "off")
                    return Result.Success(true);
                return Result.Success(false);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaAccountArchiveStory>(json);
                if (obj.MessagePrefs.ToLower() == "anyone" && repliesType == InstaMessageRepliesType.Everyone)
                    return Result.Success(true);
                if (obj.MessagePrefs.ToLower() == "following" && repliesType == InstaMessageRepliesType.Following)
                    return Result.Success(true);
                if (obj.MessagePrefs.ToLower() == "off" && repliesType == InstaMessageRepliesType.Off)
                    return Result.Success(true);
                return Result.Success(false);
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
        ///     Check username availablity. (for logged in user)
        /// </summary>
        /// <param name="desiredUsername">Desired username</param>
        public async Task<IResult<InstaAccountCheck>> CheckUsernameAsync(string desiredUsername)
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountCheck>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountCheck>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountCheck), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountCheck>(exception);
            }
        }
        #endregion Story settings

        #region two factor authentication enable/disable
        
        /// <summary>
        ///     Get Security settings (two factor authentication and backup codes).
        /// </summary>
        public async Task<IResult<InstaAccountSecuritySettings>> GetSecuritySettingsInfoAsync()
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountSecuritySettings>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountSecuritySettings>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountSecuritySettings), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountSecuritySettings>(exception);
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaDefault>(json);
                if (obj.Status.ToLower() == "ok")
                    return Result.Success(true);
                return Result.Success(false);
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
        ///     Send two factor enable sms.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<InstaAccountTwoFactorSms>> SendTwoFactorEnableSmsAsync(string phoneNumber)
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountTwoFactorSms>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountTwoFactorSms>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountTwoFactorSms), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountTwoFactorSms>(exception);
            }
        }
        /// <summary>
        ///     Verify enable two factor.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        public async Task<IResult<InstaAccountTwoFactor>> TwoFactorEnableAsync(string phoneNumber, string verificationCode)
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountTwoFactor>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountTwoFactor>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountTwoFactor), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountTwoFactor>(exception);
            }
        }
        /// <summary>
        ///     Send confirm email.
        /// </summary>
        public async Task<IResult<InstaAccountConfirmEmail>> SendConfirmEmailAsync()
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountConfirmEmail>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountConfirmEmail>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountConfirmEmail), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountConfirmEmail>(exception);
            }
        }
        /// <summary>
        ///     Send sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<InstaAccountSendSms>> SendSmsCodeAsync(string phoneNumber)
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountSendSms>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountSendSms>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountSendSms), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountSendSms>(exception);
            }
        }
        /// <summary>
        ///     Verify email by verification url
        /// </summary>
        /// <param name="verificationUri">Verification url</param>
        public async Task<IResult<bool>> VerifyEmailByVerificationUriAsync(Uri verificationUri)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (verificationUri == null) throw new ArgumentNullException("Verification uri cannot be null");

                var instaUri = UriCreator.GetVerifyEmailUri(verificationUri);
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                var obj = JsonConvert.DeserializeObject<InstaAccountConfirmEmail>(json);

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<bool>(response, obj.Body, null);

                return obj.Title.ToLower() == "thanks" ? Result.Success(true) : Result.Fail(obj.Body, false);
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
        ///     Verify sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number (ex: +9891234...)</param>
        /// <param name="verificationCode">Verification code</param>
        public async Task<IResult<InstaAccountVerifySms>> VerifySmsCodeAsync(string phoneNumber, string verificationCode)
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
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountVerifySms>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaAccountVerifySms>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountVerifySms), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountVerifySms>(exception);
            }
        }
        /// <summary>
        ///     Regenerate two factor backup codes
        /// </summary>
        public async Task<IResult<TwoFactorRegenBackupCodes>> RegenerateTwoFactorBackupCodesAsync()
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

                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<TwoFactorRegenBackupCodes>(response, json);

                var obj = JsonConvert.DeserializeObject<TwoFactorRegenBackupCodes>(json);
                return obj.Status.ToLower() == "ok" ? Result.Success(obj) : Result.UnExpectedResponse<TwoFactorRegenBackupCodes>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(TwoFactorRegenBackupCodes), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<TwoFactorRegenBackupCodes>(exception);
            }
        }
        #endregion two factor authentication enable/disable

        #region Other functions

        /// <summary>
        ///     Enable presence (people can track your activities and you can see their activies too)
        /// </summary>
        public async Task<IResult<bool>> EnablePresenceAsync()
        {
            return await EnableDisablePresenceAsync(true);
        }

        /// <summary>
        ///     Disable presence (people can't track your activities and you can't see their activies too)
        /// </summary>
        public async Task<IResult<bool>> DisablePresenceAsync()
        {
            return await EnableDisablePresenceAsync(false);
        }

        /// <summary>
        ///     Get presence options (see your presence is disable or not)
        /// </summary>
        public async Task<IResult<InstaPresence>> GetPresenceOptionsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetPresenceUri(_httpHelper._apiVersion.SignatureKey);
                
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaPresence>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaPresenceResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetPresenceConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaPresence), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaPresence>(exception);
            }
        }

        /// <summary>
        ///     Switch to personal account
        /// </summary>
        public async Task<IResult<InstaUser>> SwitchToPersonalAccountAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetConvertToPersonalAccountUri();
                var data = new JObject
                {
                    { "_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUser>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaUserContainerResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetUserConverter(obj.User).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaUser), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaUser>(exception);
            }
        }
        
        /// <summary>
        ///     Switch to business account
        /// </summary>
        public async Task<IResult<InstaBusinessUser>> SwitchToBusinessAccountAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetConvertToBusinessAccountUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBusinessUser>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBusinessUserContainerResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetBusinessUserConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBusinessUser), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessUser>(exception);
            }
        }
        /// <summary>
        ///    [NOT WORKING] Set contact information for business account
        /// </summary>
        /// <param name="categoryId">Category id (Use <see cref="IBusinessProcessor.GetCategoriesAsync"/> to get category id)</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="email">Email address</param>
        public async Task<IResult<InstaBusinessUser>> SetBusinessInfoAsync(string categoryId, string phoneNumber, string email)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                //[NOT WORKING]
                var instaUri = UriCreator.GetCreateBusinessInfoUri();
                
                var publicPhoneContact = new JObject
                {
                    {"public_phone_number", phoneNumber},
                    {"business_contact_method", "CALL"},
                };
                var edit = await GetRequestForEditProfileAsync();
                //{
                //  "set_public": "false",
                //  "entry_point": "setting",
                //  "_csrftoken": "UBPgM6BG1Qr95lO4ofLYpgJXtbVvVnvs",
                //  "public_phone_contact": "{\"public_phone_number\":\"+989174314006\",\"business_contact_method\":\"CALL\"}",
                //  "_uid": "7405924766",
                //  "_uuid": "6324ecb2-e663-4dc8-a3a1-289c699cc876",
                //  "public_email": "ramtinjokar@yahoo.com",
                //  "category_id": "2700"
                //}
                var pub = edit.Value.IsPrivate;
                
                var data = new JObject
                {
                    {"set_public", pub.ToString().ToLower()},
                    {"entry_point", "setting"},
                    {"_csrftoken", _user.CsrfToken},
                    {"public_phone_contact", publicPhoneContact.ToString(Formatting.None)},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"public_email", email ?? string.Empty},
                    {"category_id", categoryId},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBusinessUser>(response, json);
                //{"message": "Business details are malformed", "error_identifier": "BUSINESS_ID", "status": "fail"}
                //{"message": "Can not convert to business, Try again later", "error_identifier": "CANNOT_CONVERT", "status": "fail"}
                var obj = JsonConvert.DeserializeObject<InstaBusinessUserContainerResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetBusinessUserConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBusinessUser), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessUser>(exception);
            }
        }


        private async Task<IResult<bool>> EnableDisablePresenceAsync(bool enable)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetAccountSetPresenseDisabledUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"disabled", enable ? "0" : "1"},
                    { "_csrftoken", _user.CsrfToken}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
        #endregion Other functions

        #region NOT COMPLETE FUNCTIONS


        //NOT COMPLETE
        private async Task<IResult<object>> GetCommentFilterAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = new Uri(InstaApiConstants.BASE_INSTAGRAM_API_URL + $"accounts/get_comment_filter/");
                Debug.WriteLine(instaUri.ToString());

             
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);

                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(json);
                //if (response.StatusCode != HttpStatusCode.OK)
                //    return Result.UnExpectedResponse<object>(response, json);
                //{"config_value": 0, "status": "ok"}
                return null;
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(object), ResponseType.NetworkProblem);
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
