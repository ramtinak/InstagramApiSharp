/*
 * This file is a part of private version of InstagramApiSharp's project
 * 
 * 
 * Developer: Ramtin Jokar [ Ramtinak@live.com ]
 * 
 * 
 * IRANIAN DEVELOPERS (c) 2021
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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Services
{
    internal class RegistrationService : IRegistrationService
    {
#region Properties

        /// <summary>
        ///     Waterfall id for registration
        /// </summary>
        public string RegistrationWaterfallId { get; set; }
        /// <summary>
        ///     Signup code from Registration via Email
        /// </summary>
        public string ForceSignupCode { get; set; }
        /// <summary>
        ///     Birthday for age consent
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        ///     Check Email Registration response
        /// </summary>
        public InstaCheckEmailRegistration InstaCheckEmailRegistration { get; set; }
        /// <summary>
        ///     Sms verification code
        /// </summary>
        public string SmsVerificationCode { get; set; }
        /// <summary>
        ///     Registration phone number response
        /// </summary>
        public InstaAccountRegistrationPhoneNumberNew AccountRegistrationPhoneNumber { get; set; }

#endregion Properties

#region Fields and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
#pragma warning disable IDE0052 // Remove unread private members
        private readonly UserAuthValidate _userAuthValidate;
#pragma warning restore IDE0052 // Remove unread private members
        private readonly InstaApi _instaApi;
        private readonly HttpHelper _httpHelper;

        public RegistrationService(AndroidDevice deviceInfo, UserSessionData user,
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
            RegistrationWaterfallId = Guid.NewGuid().ToString();
            Birthday = GenerateRandomBirthday();
        }
#endregion Properties and constructor

#region Private functions

        void ValidateUser(InstaUserShortResponse user) =>
            _user.LoggedInUser = ConvertersFabric.Instance.GetUserShortConverter(user)?.Convert();

        private async Task<IResult<bool>> GetResultAsync(Uri instaUri, Dictionary<string, string> data = null, bool signedRequest = false, bool setCsrfToken = false)
        {
            try
            {
                HttpRequestMessage request = null;
                if (data?.Count > 0)
                    request = signedRequest ? _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data) :
                        _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                else
                    request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (setCsrfToken)
                    _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);
                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);

                return obj.IsSucceed ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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

        private async Task<IResult<bool>> GetResultAsync(Uri instaUri, JObject data, bool setCsrfToken)
        {
            try
            {
                HttpRequestMessage request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (setCsrfToken)
                    _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);

                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);

                return obj.IsSucceed ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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

#endregion Private functions

#region Public functions

        /// <summary>
        ///     Generate random birthday
        /// </summary>
        public DateTime GenerateRandomBirthday() =>
            ExtensionHelper.GenerateRandomBirthday();

#endregion

#region Public Async Functions

        /// <summary>
        ///     Get first contactpoint prefill [ sends before new registration account ]
        /// </summary>
        public async Task<IResult<bool>> GetFirstContactPointPrefillAsync()
        {
            var data = new Dictionary<string, string>
            {
                {"phone_id",            _deviceInfo.PhoneGuid.ToString()},
                {"usage",               "prefill"},
            };
            return await GetResultAsync(UriCreator.GetContactPointPrefillUri(true), data, true, true).ConfigureAwait(false);
        }

        /// <summary>
        ///     First launcher sync [ sends before new registration account ]
        /// </summary>
        public async Task<IResult<bool>> FirstLauncherSyncAsync()
        {
            var data = new JObject
            {
                {"id",                          _deviceInfo.DeviceGuid.ToString()},
                {"server_config_retrieval",     "1"}
            };
            return await GetResultAsync(UriCreator.GetLauncherSyncUri(), data, true).ConfigureAwait(false);
        }

        /// <summary>
        ///     First Qe sync [ sends before new registration account ]
        /// </summary>
        public async Task<IResult<bool>> FirstQeSyncAsync()
        {
            var data = new JObject
            {
                {"id",                          _deviceInfo.DeviceGuid.ToString()},
                {"server_config_retrieval",     "1"},
                {"experiments",                 "ig_android_reg_nux_headers_cleanup_universe,ig_android_device_detection_info_upload,ig_android_gmail_oauth_in_reg,ig_android_device_info_foreground_reporting,ig_android_device_verification_fb_signup,ig_android_passwordless_account_password_creation_universe,ig_android_direct_add_direct_to_android_native_photo_share_sheet,ig_growth_android_profile_pic_prefill_with_fb_pic_2,ig_account_identity_logged_out_signals_global_holdout_universe,ig_android_quickcapture_keep_screen_on,ig_android_device_based_country_verification,ig_android_login_identifier_fuzzy_match,ig_android_reg_modularization_universe,ig_android_security_intent_switchoff,ig_android_device_verification_separate_endpoint,ig_android_suma_landing_page,ig_android_sim_info_upload,ig_android_fb_account_linking_sampling_freq_universe,ig_android_retry_create_account_universe,ig_android_caption_typeahead_fix_on_o_universe"},
            };

            return await GetResultAsync(UriCreator.GetQeSyncUri(), data, true).ConfigureAwait(false);
        }

        /// <summary>
        ///     Check username availablity
        /// </summary>
        /// <param name="username">Username</param>
        public async Task<IResult<InstaAccountCheck>> CheckUsernameAsync(string username)
        {
            try
            {
                var instaUri = UriCreator.GetCheckUsernameUri();
                var data = new JObject
                {
                    {"username", username}
                };
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaAccountCheck>(json);
                _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);
                if (!obj.IsSucceed || response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountCheck>(response, json);
                else
                {
                    _instaApi._user.PublicKey = _user.PublicKey = string.Join("", response.Headers.GetValues(InstaApiConstants.RESPONSE_HEADER_IG_PASSWORD_ENC_PUB_KEY));
                    _instaApi._user.PublicKeyId = _user.PublicKeyId = string.Join("", response.Headers.GetValues(InstaApiConstants.RESPONSE_HEADER_IG_PASSWORD_ENC_KEY_ID));
                    return Result.Success(obj);
                }
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

        /// <summary>
        ///     Check email availablity
        /// </summary>
        /// <param name="email">Email</param>
        public async Task<IResult<InstaCheckEmailRegistration>> CheckEmailAsync(string email)
        {
            try
            {
                if (RegistrationWaterfallId == null)
                    RegistrationWaterfallId = Guid.NewGuid().ToString();

                var data = new Dictionary<string, string>
                {
                    {"android_device_id",   _deviceInfo.DeviceId},
                    {"login_nonce_map",     "{}"},
                    {"login_nonces",        "[]"},
                    {"email",               email},
                    {"qe_id",               Guid.NewGuid().ToString()},
                    {"waterfall_id",        RegistrationWaterfallId},
                };
                var instaUri = UriCreator.GetCheckEmailUri();
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var obj = JsonConvert.DeserializeObject<InstaCheckEmailRegistration>(json);
                    if (obj.ErrorType == "fail")
                        return Result.UnExpectedResponse<InstaCheckEmailRegistration>(response, json);
                    if (obj.ErrorType == "email_is_taken")
                        return Result.Fail("Email is taken.", (InstaCheckEmailRegistration)null);
                    if (obj.ErrorType == "invalid_email")
                        return Result.Fail("Please enter a valid email address.", (InstaCheckEmailRegistration)null);

                    return Result.UnExpectedResponse<InstaCheckEmailRegistration>(response, json);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<InstaCheckEmailRegistration>(json);
                    if (obj.ErrorType == "fail")
                        return Result.UnExpectedResponse<InstaCheckEmailRegistration>(response, json);
                    if (obj.ErrorType == "email_is_taken")
                        return Result.Fail("Email is taken.", (InstaCheckEmailRegistration)null);
                    if (obj.ErrorType == "invalid_email")
                        return Result.Fail("Please enter a valid email address.", (InstaCheckEmailRegistration)null);
                    InstaCheckEmailRegistration = obj;
                    return Result.Success(obj);
                }
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaCheckEmailRegistration), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCheckEmailRegistration>(exception);
            }
        }


        /// <summary>
        ///     Get signup consent config
        /// </summary>
        /// <param name="isMainAccount">Is this main account ? always set to to false</param>
        /// <param name="loggedInUserId">Logged in user id (pk) if available</param>
        public async Task<IResult<InstaSignupConsentConfig>> GetSignupConsentConfigAsync(bool isMainAccount = false, long? loggedInUserId = null)
        {
            try
            {
                var instaUri = UriCreator.GetSignupConsentConfigUri(_deviceInfo.DeviceGuid.ToString(), isMainAccount, loggedInUserId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);
                var obj = JsonConvert.DeserializeObject<InstaSignupConsentConfig>(json);

                return obj.IsSucceed ? Result.Success(obj) : Result.UnExpectedResponse<InstaSignupConsentConfig>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaSignupConsentConfig), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaSignupConsentConfig>(exception);
            }
        }

        /// <summary>
        ///     Send registration verify email
        /// </summary>
        /// <param name="email">Email</param>
        public async Task<IResult<bool>> SendRegistrationVerifyEmailAsync(string email)
        {
            var data = new Dictionary<string, string>
            {
                {"phone_id",            _deviceInfo.PhoneGuid.ToString()},
                {"guid",                _deviceInfo.DeviceGuid.ToString()},
                {"device_id",           _deviceInfo.DeviceId},
                {"email",               email},
                {"waterfall_id",        RegistrationWaterfallId},
                {"auto_confirm_only",   "false"},
            };
            return await GetResultAsync(UriCreator.GetSendRegistrationVerifyEmailUri(), data, true, true).ConfigureAwait(false);
        }

        /// <summary>
        ///     Check registration confirmation code from email
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="verificationCode">Verification code from email</param>
        public async Task<IResult<InstaRegistrationConfirmationCode>> CheckRegistrationConfirmationCodeAsync(string email, string verificationCode)
        {
            try
            {
                var data = new Dictionary<string, string>
                {
                    {"code",                verificationCode},
                    {"device_id",           _deviceInfo.DeviceId},
                    {"email",               email},
                    {"waterfall_id",        RegistrationWaterfallId},
                };
                var instaUri = UriCreator.GetCheckRegistrationConfirmationCodeUri();
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);
                var obj = JsonConvert.DeserializeObject<InstaRegistrationConfirmationCode>(json);
                ForceSignupCode = obj.SignupCode;
                return obj.IsSucceed ? Result.Success(obj) : Result.UnExpectedResponse<InstaRegistrationConfirmationCode>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaRegistrationConfirmationCode), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaRegistrationConfirmationCode>(exception);
            }
        }

        /// <summary>
        ///     Get si-fetch headers
        /// </summary>
        public async Task<IResult<bool>> GetSiFetchHeadersAsync() =>
            await GetResultAsync(UriCreator.GetSiFetchHeadersUri(_deviceInfo.DeviceGuid.ToString().Replace("-", "")),
                  null, false, true).ConfigureAwait(false);

        /// <summary>
        ///     Get username suggestions
        /// </summary>
        /// <param name="name">Name => will respond with containing provided name</param>
        /// <param name="email">Email => 
        ///         <para>Required for email registration!</para>
        ///         Optional for phone registration!
        /// </param>
        public async Task<IResult<InstaRegistrationSuggestionResponse>> GetUsernameSuggestionsAsync(string name, string email = null)
        {
            try
            {
                var data = new Dictionary<string, string>
                {
                    {"phone_id",        _deviceInfo.PhoneGuid.ToString()},
                    {"guid",            _deviceInfo.DeviceGuid.ToString()},
                    {"name",            name},
                    {"device_id",       _deviceInfo.DeviceId},
                    {"email",           email ?? ""},
                    {"waterfall_id",    RegistrationWaterfallId},
                };
                var instaUri = UriCreator.GetUsernameSuggestionsUri();
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var o = JsonConvert.DeserializeObject<InstaAccountRegistrationPhoneNumber>(json);

                    return Result.Fail(o.Message?.Errors?[0], (InstaRegistrationSuggestionResponse)null);
                }

                var obj = JsonConvert.DeserializeObject<InstaRegistrationSuggestionResponse>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaRegistrationSuggestionResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaRegistrationSuggestionResponse>(exception);
            }
        }

        /// <summary>
        ///     Check age eligibility
        /// </summary>
        /// <param name="birthday">Birthday => Passing null, will generate randomly and save it to <see cref="IRegistrationService.Birthday"/></param>
        public async Task<IResult<InstaCheckAgeEligibility>> CheckAgeEligibilityAsync(DateTime? birthday = null)
        {
            try
            {
                Birthday = birthday ?? GenerateRandomBirthday();
                var data = new Dictionary<string, string>
                {
                    {"day",             Birthday.Day.ToString()},
                    {"year",            Birthday.Year.ToString()},
                    {"month",           Birthday.Month.ToString()},
                };
                var instaUri = UriCreator.GetCheckAgeEligibilityUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                var obj = JsonConvert.DeserializeObject<InstaCheckAgeEligibility>(json);
                return obj.IsSucceed ? Result.Success(obj) : Result.UnExpectedResponse<InstaCheckAgeEligibility>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaCheckAgeEligibility), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCheckAgeEligibility>(exception);
            }
        }

        /// <summary>
        ///     Onboarding steps of registration
        /// </summary>
        /// <param name="progressState">Progress state</param>
        /// <param name="registrationMethod">Registration method</param>
        public async Task<IResult<bool>> GetOnboardingStepsAsync(InstaOnboardingProgressState progressState, InstaRegistrationMethod registrationMethod = InstaRegistrationMethod.Email)
        {
            try
            {
                RegistrationWaterfallId = Guid.NewGuid().ToString();

                var data = new Dictionary<string, string>
                {
                    {"is_secondary_account_creation",       progressState == InstaOnboardingProgressState.Prefetch ? "false" : "true"},
                    {"fb_connected",                        "false"},
                    {"progress_state",                      progressState.ToString().ToLower()},
                    {"phone_id",                            _deviceInfo.PhoneGuid.ToString()},
                    {"fb_installed",                        "false"},
                    {"locale",                              InstaApiConstants.ACCEPT_LANGUAGE.Replace("-","_")},
                    {"timezone_offset",                     InstaApiConstants.TIMEZONE_OFFSET.ToString()},
                    {"network_type",                        "WIFI-UNKNOWN"},
                    {"guid",                                _deviceInfo.DeviceGuid.ToString()},
                    {"is_ci",                               "false"},
                    {"android_id",                          _deviceInfo.DeviceId},
                    {"waterfall_id",                        RegistrationWaterfallId},
                    {"tos_accepted",                        progressState == InstaOnboardingProgressState.Prefetch ? "false" : "true"}
                };

                if (registrationMethod == InstaRegistrationMethod.Email)
                {
                    switch (progressState)
                    {
                        case InstaOnboardingProgressState.Start:
                            data.Add("_uid", _user.LoggedInUser.Pk.ToString());
                            data.Add("reg_flow_taken", "email");
                            break;

                        case InstaOnboardingProgressState.Finish:
                            data.Add("seen_steps", "[{\"step_name\":\"CHECK_FOR_PHONE\",\"value\":1},{\"step_name\":\"FB_CONNECT\",\"value\":0},{\"step_name\":\"FB_FOLLOW\",\"value\":-1},{\"step_name\":\"UNKNOWN\",\"value\":-1},{\"step_name\":\"CONTACT_INVITE\",\"value\":-1},{\"step_name\":\"ACCOUNT_PRIVACY\",\"value\":-1},{\"step_name\":\"TAKE_PROFILE_PHOTO\",\"value\":0},{\"step_name\":\"ADD_PHONE\",\"value\":-1},{\"step_name\":\"TURN_ON_ONETAP\",\"value\":-1}]");
                            break;
                    }
                }
                else
                {
                    data.Add("reg_flow_taken", "phone");
                    switch (progressState)
                    {
                        case InstaOnboardingProgressState.Start:
                            data.Add("_uid", _user.LoggedInUser.Pk.ToString());
                            break;

                        case InstaOnboardingProgressState.Finish:
                            data.Add("seen_steps", "[{\"step_name\":\"CHECK_FOR_PHONE\",\"value\":1},{\"step_name\":\"CREATE_PASSWORD\",\"value\":-1},{\"step_name\":\"FB_CONNECT\",\"value\":0},{\"step_name\":\"FB_FOLLOW\",\"value\":-1},{\"step_name\":\"UNKNOWN\",\"value\":-1},{\"step_name\":\"CONTACT_INVITE\",\"value\":-1},{\"step_name\":\"ACCOUNT_PRIVACY\",\"value\":-1},{\"step_name\":\"TAKE_PROFILE_PHOTO\",\"value\":0},{\"step_name\":\"ADD_PHONE\",\"value\":-1},{\"step_name\":\"TURN_ON_ONETAP\",\"value\":-1},{\"step_name\":\"DISCOVER_PEOPLE\",\"value\":1},{\"step_name\":\"INTEREST_ACCOUNT_SUGGESTIONS\",\"value\":-1}]");
                            break;
                    }
                }
                if (progressState != InstaOnboardingProgressState.Finish)
                    data.Add("seen_steps", "[]");

                var instaUri = UriCreator.GetOnboardingStepsUri(progressState == InstaOnboardingProgressState.Start);
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (progressState == InstaOnboardingProgressState.Prefetch)
                    _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor);
                IResult<bool> FailResponse()
                {
                    var oa = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                    return Result.Fail<bool>(oa.Message);
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    try
                    {
                        var o = JsonConvert.DeserializeObject<InstaAccountRegistrationPhoneNumber>(json);

                        return Result.Fail<bool>(o.Message?.Errors?[0]);
                    }
                    catch { return FailResponse(); }
                }

                var obj = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                return obj.IsSucceed ? Result.Success(true) : Result.UnExpectedResponse<bool>(response, json);
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
        ///     New user flow begins consent
        /// </summary>
        public async Task<IResult<bool>> NewUserFlowBeginsConsentAsync()
        {
            var data = new JObject
            {
                {"phone_id",        _deviceInfo.PhoneGuid.ToString()},
            };
            return await GetResultAsync(UriCreator.GetConsentNewUserFlowBeginsUri(), data, true).ConfigureAwait(false);
        }

        /// <summary>
        ///     Create new account via email
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name => Optional</param>
        /// <param name="signUpCode">ForceSignUpCode from <see cref="IRegistrationService.CheckRegistrationConfirmationCodeAsync"/> => Optional</param>
        /// <param name="birthday">Birthday => Optional</param>
        public async Task<IResult<InstaAccountCreation>> CreateNewAccountWithEmailAsync(string email, string username,
            string password, string firstName = "", string signUpCode = null, DateTime? birthday = null)
        {
            try
            {
                Birthday = birthday ?? GenerateRandomBirthday();

                var encryptedPassword = _instaApi.GetEncryptedPassword(password);
                var data = new Dictionary<string, string>
                {
                    {"is_secondary_account_creation",           "true"},
                    {"jazoest",                                 ExtensionHelper.GenerateJazoest(_deviceInfo.PhoneGuid.ToString())},
                    {"suggestedUsername",                       ""},
                    {"do_not_auto_login_if_credentials_match",  "true"},
                    {"phone_id",                                _deviceInfo.PhoneGuid.ToString()},
                    {"enc_password",                            encryptedPassword},
                    {"username",                                username},
                    {"first_name",                              firstName.Replace(" ", "+")},
                    {"adid",                                    _deviceInfo.AdId.ToString()},
                    {"guid",                                    _deviceInfo.DeviceGuid.ToString()},
                    {"device_id",                               _deviceInfo.DeviceId},
                    {"_uuid",                                   _deviceInfo.DeviceGuid.ToString()},
                    {"email",                                   email},
                    {"force_sign_up_code",                      signUpCode ?? ForceSignupCode},
                    {"waterfall_id",                            RegistrationWaterfallId},
                    {"sn_result",                               "GOOGLE_PLAY_UNAVAILABLE:SERVICE_INVALID"},
                    {"day",                                     Birthday.Day.ToString()},
                    {"year",                                    Birthday.Year.ToString()},
                    {"month",                                   Birthday.Month.ToString()},
                    {"sn_nonce",                                ExtensionHelper.GenerateSnNonce(email)},
                    {"qs_stamp",                                ""},
                    {"one_tap_opt_in",                          "true"}
                };
                if (InstaCheckEmailRegistration?.TosVersion == "row")
                    data.Add("tos_version", "row");

                var instaUri = UriCreator.GetCreateAccountUri();
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                _user.SetCsrfTokenIfAvailable(response, _httpRequestProcessor, true);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var loginFailReason = JsonConvert.DeserializeObject<InstaLoginBaseResponse>(json);

                    if (loginFailReason.ErrorType == "checkpoint_challenge_required"
                        || loginFailReason.Message == "challenge_required")
                    {
                        _instaApi.ChallengeLoginInfo = loginFailReason.Challenge;

                        return Result.Fail("Challenge is required", ResponseType.ChallengeRequired, default(InstaAccountCreation));
                    }
                    return Result.UnExpectedResponse<InstaAccountCreation>(response, json);
                }
                var obj = JsonConvert.DeserializeObject<InstaAccountCreation>(json);

                if (obj.AccountCreated && obj.CreatedUser != null)
                {
                    _instaApi.ValidateUserAsync(obj.CreatedUser, _user.CsrfToken, true, password);
                    ValidateUser(obj.CreatedUser);
                }
                return obj.IsSucceed ? Result.Success(obj) : Result.UnExpectedResponse<InstaAccountCreation>(response, json);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountCreation), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountCreation>(exception);
            }
        }

        /// <summary>
        ///     Get multiple accounts family
        /// </summary>
        public async Task<IResult<bool>> GetMultipleAccountsFamilyAsync() =>
            await GetResultAsync(UriCreator.GetMultipleAccountsFamilyUri(true), null, false, false).ConfigureAwait(false);

        /// <summary>
        ///     Get zr token result
        /// </summary>
        public async Task<IResult<bool>> GetZrTokenResultAsync() =>
            await GetResultAsync(UriCreator.GetZrTokenResultUri(_deviceInfo.DeviceGuid.ToString(), _deviceInfo.DeviceId, true), null, false, false).ConfigureAwait(false);

        /// <summary>
        ///     Launcher sync [ after registration is done ]
        /// </summary>
        public async Task<IResult<bool>> LauncherSyncAsync()
        {
            var data = new JObject
            {
            };
            if (_instaApi.IsUserAuthenticated && _user?.LoggedInUser != null)
            {
                data.Add("id", _user.LoggedInUser.Pk.ToString());
                data.Add("_uid", _user.LoggedInUser.Pk.ToString());
            }
            else
                data.Add("id", _deviceInfo.DeviceGuid.ToString());

            data.Add("_uuid", _deviceInfo.DeviceGuid.ToString());
            data.Add("server_config_retrieval", "1");
            return await GetResultAsync(UriCreator.GetLauncherSyncUri(true), data, false).ConfigureAwait(false);
        }

        /// <summary>
        ///     Qe sync [ after registration is done ]
        /// </summary>
        public async Task<IResult<bool>> QeSyncAsync()
        {
            var data = new JObject
            {
            };
            if (_instaApi.IsUserAuthenticated && _user?.LoggedInUser != null)
            {
                data.Add("id", _user.LoggedInUser.Pk.ToString());
                data.Add("_uid", _user.LoggedInUser.Pk.ToString());
            }
            else
                data.Add("id", _deviceInfo.DeviceGuid.ToString());

            data.Add("_uuid", _deviceInfo.DeviceGuid.ToString());
            data.Add("server_config_retrieval", "1");
            data.Add("experiments", InstaApiConstants.SIGNUP_EXPERIMENTS_CONFIGS);

            return await GetResultAsync(UriCreator.GetQeSyncUri(true), data, false).ConfigureAwait(false);
        }

        /// <summary>
        ///     Nux new account seen
        /// </summary>
        public async Task<IResult<bool>> NuxNewAccountSeenAsync()
        {
            var data = new JObject
            {
                {"is_fb4a_installed",   "false"},
                {"phone_id",            _deviceInfo.PhoneGuid.ToString()},
                {"_uid",                _user.LoggedInUser.Pk.ToString()},
                {"guid",                _deviceInfo.DeviceGuid.ToString()},
                {"device_id",           _deviceInfo.DeviceId},
                {"_uuid",               _deviceInfo.DeviceGuid.ToString()},
                {"waterfall_id",        Guid.NewGuid().ToString()},
            };
            return await GetResultAsync(UriCreator.GetNuxNewAccountSeenUri(true), data, false).ConfigureAwait(false);
        }

        /// <summary>
        ///     Get contact point prefill [ after registration is done ]
        /// </summary>
        public async Task<IResult<bool>> GetContactPointPrefillAsync()
        {
            var data = new JObject
            {
                {"_uid",                _user.LoggedInUser.Pk.ToString()},
                {"device_id",           _deviceInfo.DeviceId},
                {"_uuid",               _deviceInfo.DeviceGuid.ToString()},
                {"usage",               "auto_confirmation"},
            };
            return await GetResultAsync(UriCreator.GetContactPointPrefillUri(true), data, false).ConfigureAwait(false);
        }



#region Phone registration

        /// <summary>
        ///     Check phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<bool>> CheckPhoneNumberAsync(string phoneNumber)
        {
            var data = new Dictionary<string, string>
            {
                {"phone_id",            _deviceInfo.PhoneGuid.ToString()},
                {"login_nonce_map",     "{}"},
                {"phone_number",        phoneNumber},
                {"guid",                _deviceInfo.DeviceGuid.ToString()},
                {"device_id",           _deviceInfo.DeviceId},
                {"prefill_shown",       "False"},
            };
            return await GetResultAsync(UriCreator.GetCheckPhoneNumberUri(), data, true);
        }

        /// <summary>
        ///     Send singup sms code
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<bool>> SendSignUpSmsCodeAsync(string phoneNumber)
        {
            var data = new Dictionary<string, string>
            {
                {"phone_id",            _deviceInfo.PhoneGuid.ToString()},
                {"phone_number",        phoneNumber},
                {"guid",                _deviceInfo.DeviceGuid.ToString()},
                {"device_id",           _deviceInfo.DeviceId},
                {"android_build_type",  "release"},
                {"waterfall_id",        RegistrationWaterfallId},
            };
            return await GetResultAsync(UriCreator.GetSignUpSMSCodeUri(), data, true);
        }

        /// <summary>
        ///     Verify signup sms code
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        public async Task<IResult<InstaPhoneNumberRegistration>> VerifySignUpSmsCodeAsync(string phoneNumber, string verificationCode)
        {
            try
            {
                SmsVerificationCode = verificationCode ?? SmsVerificationCode;
                var data = new Dictionary<string, string>
                {
                    {"verification_code",         verificationCode},
                    {"phone_number",              phoneNumber},
                    {"guid",                      _deviceInfo.DeviceGuid.ToString()},
                    {"device_id",                 _deviceInfo.DeviceId},
                    {"waterfall_id",              RegistrationWaterfallId},
                };
                var instaUri = UriCreator.GetValidateSignUpSMSCodeUri();
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                IResult<InstaPhoneNumberRegistration> FailResponse()
                {
                    var oa = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                    return Result.Fail<InstaPhoneNumberRegistration>(oa.Message);
                }
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    try
                    {
                        var o = JsonConvert.DeserializeObject<InstaAccountRegistrationPhoneNumberVerifySms>(json);

                        return Result.Fail(o.Errors?.Nonce?[0], (InstaPhoneNumberRegistration)null);
                    }
                    catch
                    {
                        return FailResponse();
                    }
                }
                try
                {
                    var r = JsonConvert.DeserializeObject<InstaAccountRegistrationPhoneNumberVerifySms>(json);
                    if (r.ErrorType == "invalid_nonce")
                        return Result.Fail(r.Errors?.Nonce?[0], (InstaPhoneNumberRegistration)null);
                }
                catch
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        return FailResponse();
                }
                var obj = JsonConvert.DeserializeObject<InstaPhoneNumberRegistration>(json);
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaPhoneNumberRegistration), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaPhoneNumberRegistration>(exception);
            }
        }


        /// <summary>
        ///     Create new account via phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name</param>
        /// <param name="verificationCode">Verification code from sms</param>
        /// <param name="birthday">Birthday => Optional</param>
        public async Task<IResult<InstaAccountCreation>> CreateNewAccountWithPhoneNumberAsync(string phoneNumber,
            string username, string password, string firstName, string verificationCode, DateTime? birthday = null)
        {
            try
            {
                Birthday = birthday ?? GenerateRandomBirthday();
                var encryptedPassword = _instaApi.GetEncryptedPassword(password);
                var postData = new Dictionary<string, string>
                {
                    {"is_secondary_account_creation",           "false"},
                    {"jazoest",                                 ExtensionHelper.GenerateJazoest(_deviceInfo.PhoneGuid.ToString())},
                    {"tos_version",                             "row"},
                    {"suggestedUsername",                       ""},
                    {"verification_code",                       verificationCode ?? SmsVerificationCode},
                    {"do_not_auto_login_if_credentials_match",  "true"},
                    {"phone_id",                                _deviceInfo.PhoneGuid.ToString()},
                    {"enc_password",                            encryptedPassword},
                    {"phone_number",                            phoneNumber},
                    {"username",                                username},
                    {"first_name",                              firstName},
                    {"adid",                                    _deviceInfo.AdId.ToString()},
                    {"guid",                                    _deviceInfo.DeviceGuid.ToString()},
                    {"device_id",                               _deviceInfo.DeviceId},
                    {"_uuid",                                   _deviceInfo.DeviceGuid.ToString()},
                    {"force_sign_up_code",                      ""},
                    {"waterfall_id",                            RegistrationWaterfallId},
                    {"has_sms_consent",                         "true"},
                    {"sn_result",                               "GOOGLE_PLAY_UNAVAILABLE:SERVICE_INVALID"},
                    {"day",                                     Birthday.Day.ToString()},
                    {"year",                                    Birthday.Year.ToString()},
                    {"month",                                   Birthday.Month.ToString()},
                    {"sn_nonce",                                ExtensionHelper.GenerateSnNonce(phoneNumber)},
                    {"qs_stamp",                                ""},
                    {"one_tap_opt_in",                          "true"}
                };
                var instaUri = UriCreator.GetCreateValidatedUri();
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                IResult<InstaAccountCreation> FailResponse()
                {
                    var oa = JsonConvert.DeserializeObject<InstaDefaultResponse>(json);
                    return Result.Fail<InstaAccountCreation>(oa.Message);
                }
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    try
                    {
                        var o = JsonConvert.DeserializeObject<InstaAccountCreationResponse>(json);

                        return Result.Fail(o.Errors?.Username?[0], (InstaAccountCreation)null);
                    }
                    catch
                    {
                        return FailResponse();
                    }
                }
                try
                {
                    var r = JsonConvert.DeserializeObject<InstaAccountCreationResponse>(json);
                    if (r.ErrorType == "username_is_taken")
                        return Result.Fail(r.Errors?.Username?[0], (InstaAccountCreation)null);
                }
                catch
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        return FailResponse();
                }
                var obj = JsonConvert.DeserializeObject<InstaAccountCreation>(json);
                if (obj.AccountCreated && obj.CreatedUser != null)
                {
                    _instaApi.ValidateUserAsync(obj.CreatedUser, _user.CsrfToken, true, password);
                    ValidateUser(obj.CreatedUser);
                }
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountCreation), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountCreation>(exception);
            }
        }

#endregion Phone registration

#endregion Public Async Functions
    }
}