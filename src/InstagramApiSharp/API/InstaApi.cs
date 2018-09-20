using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.API.Processors;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.API
{
    internal class InstaApi : IInstaApi
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private ICollectionProcessor _collectionProcessor;
        private ICommentProcessor _commentProcessor;
        private AndroidDevice _deviceInfo;
        private IFeedProcessor _feedProcessor;

        private IHashtagProcessor _hashtagProcessor;
        private ILocationProcessor _locationProcessor;
        private IMediaProcessor _mediaProcessor;
        private IMessagingProcessor _messagingProcessor;
        private IStoryProcessor _storyProcessor;
        private TwoFactorLoginInfo _twoFactorInfo;
        private InstaChallengeLoginInfo _challengeinfo;
        private UserSessionData _userSession;
        private UserSessionData _user
        {
            get { return _userSession; }
            set { _userSession = value; _userAuthValidate.User = value; }
        }
        private UserAuthValidate _userAuthValidate;
        private IUserProcessor _userProcessor;

        private ILiveProcessor _liveProcessor;
        /// <summary>
        ///     Live api functions.
        /// </summary>
        public ILiveProcessor LiveProcessor => _liveProcessor;

        private IDiscoverProcessor _discoverProcessor;
        /// <summary>
        ///     Discover api functions.
        /// </summary>
        public IDiscoverProcessor DiscoverProcessor => _discoverProcessor;

        private IAccountProcessor _accountProcessor;
        /// <summary>
        ///     Account api functions.
        /// </summary>
        public IAccountProcessor AccountProcessor => _accountProcessor;
        /// <summary>
        ///     Comments api functions.
        /// </summary>
        public ICommentProcessor CommentProcessor => _commentProcessor;
        /// <summary>
        ///     Story api functions.
        /// </summary>
        public IStoryProcessor StoryProcessor => _storyProcessor;
        /// <summary>
        ///     Media api functions.
        /// </summary>
        public IMediaProcessor MediaProcessor => _mediaProcessor;
        /// <summary>
        ///     Messaging (direct) api functions.
        /// </summary>
        public IMessagingProcessor MessagingProcessor => _messagingProcessor;
        /// <summary>
        ///     Feed api functions.
        /// </summary>
        public IFeedProcessor FeedProcessor => _feedProcessor;
        /// <summary>
        ///     Collection api functions.
        /// </summary>
        public ICollectionProcessor CollectionProcessor => _collectionProcessor;
        /// <summary>
        /// Location api functions.
        /// </summary>
        public ILocationProcessor LocationProcessor => _locationProcessor;
        /// <summary>
        ///     Hashtag api functions.
        /// </summary>
        public IHashtagProcessor HashtagProcessor => _hashtagProcessor;
        /// <summary>
        ///     User api functions.
        /// </summary>
        public IUserProcessor UserProcessor => _userProcessor;

        HelperProcessor _helperProcessor;
        /// <summary>
        ///     Helper processor for other processors
        /// </summary>
        internal HelperProcessor HelperProcessor => _helperProcessor;

        ITVProcessor _tvProcessor;
        /// <summary>
        ///     Instagram TV api functions
        /// </summary>
        public ITVProcessor TVProcessor => _tvProcessor;

        public InstaApi(UserSessionData user, IInstaLogger logger, AndroidDevice deviceInfo,
            IHttpRequestProcessor httpRequestProcessor)
        {
            _userAuthValidate = new UserAuthValidate();
            _user = user;
            _logger = logger;
            _deviceInfo = deviceInfo;
            _httpRequestProcessor = httpRequestProcessor;
        }

        bool IsCustomDeviceSet = false;
        /// <summary>
        ///     Set custom android device.
        ///     <para>Note 1: If you want to use this method, you should call it before you calling <seealso cref="IInstaApi.LoadStateDataFromStream(Stream)"/> or <seealso cref="IInstaApi.LoadStateDataFromString(string)"/></para>
        ///     <para>Note 2: this is optional, if you didn't set this, InstagramApiSharp will choose random device.</para>
        /// </summary>
        /// <param name="androidDevice">Android device</param>
        public void SetDevice(AndroidDevice device)
        {
            IsCustomDeviceSet = false;
            if (device == null)
                return;
            _deviceInfo = device;
            IsCustomDeviceSet = true;
        }
        /// <summary>
        ///     Gets current device
        /// </summary>
        public AndroidDevice GetCurrentDevice()
        {
            return _deviceInfo;
        }
        /// <summary>
        ///     Gets logged in user
        /// </summary>
        public UserSessionData GetLoggedUser()
        {
            return _user;
        }
        /// <summary>
        ///     Get currently logged in user info asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCurrentUser" />
        /// </returns>
        public async Task<IResult<InstaCurrentUser>> GetCurrentUserAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            return await _userProcessor.GetCurrentUserAsync();
        }
        #region Authentication/State data
        private bool _isUserAuthenticated;
        /// <summary>
        ///     Indicates whether user authenticated or not
        /// </summary>
        public bool IsUserAuthenticated
        {
            get { return _isUserAuthenticated; }
            internal set { _isUserAuthenticated = value; _userAuthValidate.IsUserAuthenticated = value; }
        }
        #region Register new account with Phone number and email
        string _waterfallIdReg = "", _deviceIdReg = "", _phoneIdReg = "", _guidReg = "";
        /// <summary>
        ///     Check email availability
        /// </summary>
        /// <param name="email">Email to check</param>
        public async Task<IResult<CheckEmailRegistration>> CheckEmailAsync(string email)
        {
            try
            {
                _waterfallIdReg = Guid.NewGuid().ToString();
                var firstResponse = await _httpRequestProcessor.GetAsync(_httpRequestProcessor.Client.BaseAddress);
                var cookies = 
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                
                var postData = new Dictionary<string, string>
                {
                    {"_csrftoken",      csrftoken},
                    {"login_nonces",    "[]"},
                    {"email",           email},
                    {"qe_id",           Guid.NewGuid().ToString()},
                    {"waterfall_id",    _waterfallIdReg},
                };
                var instaUri = UriCreator.GetCheckEmailUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var obj = JsonConvert.DeserializeObject<CheckEmailRegistration>(json);
                    if (obj.ErrorType == "fail")
                        return Result.UnExpectedResponse<CheckEmailRegistration>(response, json);
                    else if (obj.ErrorType == "email_is_taken")
                        return Result.Fail("Email is taken.", (CheckEmailRegistration)null);
                    else if (obj.ErrorType == "invalid_email")
                        return Result.Fail("Please enter a valid email address.", (CheckEmailRegistration)null);
                    return Result.UnExpectedResponse<CheckEmailRegistration>(response, json);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<CheckEmailRegistration>(json);
                    if(obj.ErrorType == "fail")
                        return Result.UnExpectedResponse<CheckEmailRegistration>(response, json);
                    else if (obj.ErrorType == "email_is_taken")
                        return Result.Fail("Email is taken.", (CheckEmailRegistration)null);
                    else if (obj.ErrorType == "invalid_email")
                        return Result.Fail("Please enter a valid email address.", (CheckEmailRegistration)null);
                    return Result.Success(obj);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<CheckEmailRegistration>(exception);
            }
        }
        /// <summary>
        ///     Check phone number availability
        /// </summary>
        /// <param name="phoneNumber">Phone number to check</param>
        public async Task<IResult<bool>> CheckPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                _deviceIdReg = ApiRequestMessage.GenerateDeviceId();
                var firstResponse = await _httpRequestProcessor.GetAsync(_httpRequestProcessor.Client.BaseAddress);
                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                
                var postData = new Dictionary<string, string>
                {
                    {"_csrftoken",      csrftoken},
                    {"login_nonces",    "[]"},
                    {"phone_number",    phoneNumber},
                    {"device_id",    _deviceIdReg},
                };
                var instaUri = UriCreator.GetCheckPhoneNumberUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return Result.UnExpectedResponse<bool>(response, json);
                }
                else
                {              
                    return Result.Success(true);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        ///     Check username availablity. 
        /// </summary>
        /// <param name="username">Username</param>
        public async Task<IResult<AccountCheckResponse>> CheckUsernameAsync(string username)
        {
            try
            {
                var instaUri = UriCreator.GetCheckUsernameUri();
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"username", username}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<AccountCheckResponse>(json);
                return Result.Success(obj);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountCheckResponse>(exception);
            }
        }
        /// <summary>
        ///     Send sign up sms code
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        public async Task<IResult<bool>> SendSignUpSmsCodeAsync(string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(_deviceIdReg))
                    throw new ArgumentException("You should call CheckPhoneNumberAsync function first.");
                _phoneIdReg = Guid.NewGuid().ToString();
                _waterfallIdReg = Guid.NewGuid().ToString();
                _guidReg = Guid.NewGuid().ToString();
                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                var postData = new Dictionary<string, string>
                {
                    {"phone_id",        _phoneIdReg},
                    {"phone_number",    phoneNumber},
                    {"_csrftoken",      csrftoken},
                    {"guid",            _guidReg},
                    {"device_id",       _deviceIdReg},
                    {"waterfall_id",    _waterfallIdReg},
                };
                var instaUri = UriCreator.GetSignUpSMSCodeUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var o = JsonConvert.DeserializeObject<AccountRegistrationPhoneNumber>(json);

                    return Result.UnExpectedResponse<bool>(response, o.Message?.Errors?[0], json);
                }
                else
                {
                    return Result.Success(true);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<bool>(exception);
            }
        }
        /// <summary>
        ///     Verify sign up sms code
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        public async Task<IResult<PhoneNumberRegistration>> VerifySignUpSmsCodeAsync(string phoneNumber, string verificationCode)
        {
            try
            {
                if (string.IsNullOrEmpty(_deviceIdReg))
                    throw new ArgumentException("You should call CheckPhoneNumberAsync function first.");

                if (string.IsNullOrEmpty(_guidReg) || string.IsNullOrEmpty(_waterfallIdReg))
                    throw new ArgumentException("You should call SendSignUpSmsCodeAsync function first.");

                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                var postData = new Dictionary<string, string>
                {
                    {"verification_code",         verificationCode},
                    {"phone_number",              phoneNumber},
                    {"_csrftoken",                csrftoken},
                    {"guid",                      _guidReg},
                    {"device_id",                 _deviceIdReg},
                    {"waterfall_id",              _waterfallIdReg},
                };
                var instaUri = UriCreator.GetValidateSignUpSMSCodeUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var o = JsonConvert.DeserializeObject<AccountRegistrationPhoneNumberVerifySms>(json);

                    return Result.Fail(o.Errors?.Nonce?[0], (PhoneNumberRegistration)null);
                }
                else
                {
                    var r = JsonConvert.DeserializeObject<AccountRegistrationPhoneNumberVerifySms>(json);
                    if(r.ErrorType == "invalid_nonce")
                        return Result.Fail(r.Errors?.Nonce?[0], (PhoneNumberRegistration)null);

                    await GetRegistrationStepsAsync();
                    var obj = JsonConvert.DeserializeObject<PhoneNumberRegistration>(json);
                    return Result.Success(obj);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<PhoneNumberRegistration>(exception);
            }
        }
        /// <summary>
        ///     Get username suggestions
        /// </summary>
        /// <param name="name">Name</param>
        public async Task<IResult<RegistrationSuggestionResponse>> GetUsernameSuggestionsAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(_deviceIdReg))
                    _deviceIdReg = ApiRequestMessage.GenerateDeviceId();
                _phoneIdReg = Guid.NewGuid().ToString();
                _waterfallIdReg = Guid.NewGuid().ToString();
                _guidReg = Guid.NewGuid().ToString();
                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                var postData = new Dictionary<string, string>
                {
                    {"phone_id",        _phoneIdReg},
                    {"name",            name},
                    {"_csrftoken",      csrftoken},
                    {"guid",            _guidReg},
                    {"device_id",       _deviceIdReg},
                    {"email",           ""},
                    {"waterfall_id",    _waterfallIdReg},
                };
                var instaUri = UriCreator.GetUsernameSuggestionsUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var o = JsonConvert.DeserializeObject<AccountRegistrationPhoneNumber>(json);

                    return Result.Fail(o.Message?.Errors?[0], (RegistrationSuggestionResponse)null);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<RegistrationSuggestionResponse>(json);
                    return Result.Success(obj);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<RegistrationSuggestionResponse>(exception);
            }
        }
        /// <summary>
        ///     Validate new account creation with phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        /// <param name="username">Username to set</param>
        /// <param name="password">Password to set</param>
        /// <param name="firstName">First name to set</param>
        public async Task<IResult<AccountCreation>> ValidateNewAccountWithPhoneNumberAsync(string phoneNumber, string verificationCode, string username, string password, string firstName)
        {
            try
            {
                if (string.IsNullOrEmpty(_deviceIdReg))
                    throw new ArgumentException("You should call CheckPhoneNumberAsync function first.");

                if (string.IsNullOrEmpty(_guidReg) || string.IsNullOrEmpty(_waterfallIdReg))
                    throw new ArgumentException("You should call SendSignUpSmsCodeAsync function first.");

                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                //sn_nonce:Kzk4OTE3NDMxNDAwNnwxNTM0MTg0MjYzfAhfpJ9rzGNAlQLWQe+kor/nDAntXA0i8Q==
                //+989174314006|1534184263|_��k�c@��A濫��	�\
                //"�
                var postData = new Dictionary<string, string>
                {
                    {"allow_contacts_sync",       "true"},
                    {"verification_code",         verificationCode},
                    {"sn_result",                 "API_ERROR:+null"},
                    {"phone_id",                  _phoneIdReg},
                    {"phone_number",              phoneNumber},
                    {"_csrftoken",                csrftoken},
                    {"username",                  username},
                    {"first_name",                firstName},
                    {"adid",                      Guid.NewGuid().ToString()},
                    {"guid",                      _guidReg},
                    {"device_id",                 _deviceIdReg},
                    {"sn_nonce",                  ""},
                    {"force_sign_up_code",        ""},
                    {"waterfall_id",              _waterfallIdReg},
                    {"qs_stamp",                  ""},
                    {"password",                  password},
                    {"has_sms_consent",           "true"},
                };
                var instaUri = UriCreator.GetCreateValidatedUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var o = JsonConvert.DeserializeObject<AccountCreationResponse>(json);

                    return Result.Fail(o.Errors?.Username?[0], (AccountCreation)null);
                }
                else
                {
                    var r = JsonConvert.DeserializeObject<AccountCreationResponse>(json);
                    if (r.ErrorType == "username_is_taken")
                        return Result.Fail(r.Errors?.Username?[0], (AccountCreation)null);

                    var obj = JsonConvert.DeserializeObject<AccountCreation>(json);
                    if (obj.AccountCreated && obj.CreatedUser != null)
                        ValidateUserAsync(obj.CreatedUser, csrftoken, true);
                    return Result.Success(obj);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountCreation>(exception);
            }
        }


        private async Task<IResult<object>> GetRegistrationStepsAsync()
        {
            try
            {
                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                var postData = new Dictionary<string, string>
                {
                    {"fb_connected",            "false"},
                    {"seen_steps",            "[]"},
                    {"phone_id",        _phoneIdReg},
                    {"fb_installed",            "false"},
                    {"locale",            "en_US"},
                    {"timezone_offset",            "16200"},
                    {"network_type",            "WIFI-UNKNOWN"},
                    {"_csrftoken",      csrftoken},
                    {"guid",            _guidReg},
                    {"is_ci",            "false"},
                    {"android_id",       _deviceIdReg},
                    {"reg_flow_taken",           "phone"},
                    {"tos_accepted",    "false"},
                };
                var instaUri = UriCreator.GetOnboardingStepsUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var o = JsonConvert.DeserializeObject<AccountRegistrationPhoneNumber>(json);

                    return Result.Fail(o.Message?.Errors?[0], (RegistrationSuggestionResponse)null);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<RegistrationSuggestionResponse>(json);
                    return Result.Success(obj);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<RegistrationSuggestionResponse>(exception);
            }
        }

        /// <summary>
        ///     Create a new instagram account
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="firstName">First name (optional)</param>
        /// <returns></returns>
        public async Task<IResult<AccountCreation>> CreateNewAccountAsync(string username, string password, string email, string firstName)
        {
            AccountCreation createResponse = new AccountCreation();
            try
            {
                var _deviceIdReg = ApiRequestMessage.GenerateDeviceId();
                var _phoneIdReg = Guid.NewGuid().ToString();
                var _waterfallIdReg = Guid.NewGuid().ToString();
                var _guidReg = Guid.NewGuid().ToString();

                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                    .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                var postData = new Dictionary<string, string>
                {
                    {"allow_contacts_sync",       "true"},
                    {"sn_result",                 "API_ERROR:+null"},
                    {"phone_id",                  _phoneIdReg},
                    {"_csrftoken",                csrftoken},
                    {"username",                  username},
                    {"first_name",                firstName},
                    {"adid",                      Guid.NewGuid().ToString()},
                    {"guid",                      _guidReg},
                    {"device_id",                 _deviceIdReg},
                    {"email",                     email},
                    {"sn_nonce",                  ""},
                    {"force_sign_up_code",        ""},
                    {"waterfall_id",              _waterfallIdReg},
                    {"qs_stamp",                  ""},
                    {"password",                  password},
                };
                var instaUri = UriCreator.GetCreateAccountUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<AccountCreation>(response, json);
                var o = JsonConvert.DeserializeObject<AccountCreation>(json);
                //{"account_created": false, "errors": {"email": ["Another account is using iranramtin73jokar@live.com."], "username": ["This username isn't available. Please try another."]}, "allow_contacts_sync": true, "status": "ok", "error_type": "email_is_taken, username_is_taken"}
                //{"message": "feedback_required", "spam": true, "feedback_title": "Signup Error", "feedback_message": "Sorry! There\u2019s a problem signing you up right now. Please try again later. We restrict certain content and actions to protect our community. Tell us if you think we made a mistake.", "feedback_url": "repute/report_problem/instagram_signup/", "feedback_appeal_label": "Report problem", "feedback_ignore_label": "OK", "feedback_action": "report_problem", "status": "fail", "error_type": "signup_block"}

                
                return Result.Success(o);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<AccountCreation>(exception);
            }
        }
        #endregion
        /// <summary>
        ///     Login using given credentials asynchronously
        /// </summary>
        /// <param name="isNewLogin"></param>
        /// <returns>
        ///     Success --> is succeed
        ///     TwoFactorRequired --> requires 2FA login.
        ///     BadPassword --> Password is wrong
        ///     InvalidUser --> User/phone number is wrong
        ///     Exception --> Something wrong happened
        /// </returns>
        public async Task<IResult<InstaLoginResult>> LoginAsync(bool isNewLogin = true)
        {
            ValidateUser();
            ValidateRequestMessage();
            try
            {
                if (isNewLogin)
                {
                    var firstResponse = await _httpRequestProcessor.GetAsync(_httpRequestProcessor.Client.BaseAddress);
                    var html = await firstResponse.Content.ReadAsStringAsync();
                    _logger?.LogResponse(firstResponse);
                }
                var cookies =
                    _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                        .BaseAddress);
              
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                var instaUri = UriCreator.GetLoginUri();
                var signature = string.Empty;
                string devid = string.Empty;
                if (isNewLogin)
                    signature = $"{_httpRequestProcessor.RequestMessage.GenerateSignature(InstaApiConstants.IG_SIGNATURE_KEY, out devid)}.{_httpRequestProcessor.RequestMessage.GetMessageString()}";
                else
                    signature = $"{_httpRequestProcessor.RequestMessage.GenerateChallengeSignature(InstaApiConstants.IG_SIGNATURE_KEY,csrftoken, out devid)}.{_httpRequestProcessor.RequestMessage.GetChallengeMessageString(csrftoken)}";
                _deviceInfo.DeviceId = devid;
                var fields = new Dictionary<string, string>
                {
                    {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                    {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION}
                };
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Headers.Add("Host", "i.instagram.com");
                request.Content = new FormUrlEncodedContent(fields);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) //If the password is correct BUT 2-Factor Authentication is enabled, it will still get a 400 error (bad request)
                {
                    //Then check it
                    var loginFailReason = JsonConvert.DeserializeObject<InstaLoginBaseResponse>(json);

                    if (loginFailReason.InvalidCredentials)
                        return Result.Fail("Invalid Credentials",
                            loginFailReason.ErrorType == "bad_password"
                                ? InstaLoginResult.BadPassword
                                : InstaLoginResult.InvalidUser);
                    if (loginFailReason.TwoFactorRequired)
                    {
                        _twoFactorInfo = loginFailReason.TwoFactorLoginInfo;
                        //2FA is required!
                        return Result.Fail("Two Factor Authentication is required", InstaLoginResult.TwoFactorRequired);
                    }
                    if (loginFailReason.ErrorType == "checkpoint_challenge_required")
                    {
                        _challengeinfo = loginFailReason.Challenge;

                        return Result.Fail("Challenge is required", InstaLoginResult.ChallengeRequired);
                    }
                    if (loginFailReason.ErrorType == "rate_limit_error")
                    {
                        return Result.Fail("Please wait a few minutes before you try again.", InstaLoginResult.LimitError);
                    }
                    if (loginFailReason.ErrorType == "inactive user" || loginFailReason.ErrorType == "inactive_user")
                    {
                        return Result.Fail($"{loginFailReason.Message}\r\nHelp url: {loginFailReason.HelpUrl}", InstaLoginResult.InactiveUser);
                    }
                    return Result.UnExpectedResponse<InstaLoginResult>(response, json);
                }
                var loginInfo = JsonConvert.DeserializeObject<InstaLoginResponse>(json);
                IsUserAuthenticated = loginInfo.User?.UserName.ToLower() == _user.UserName.ToLower();
                var converter = ConvertersFabric.Instance.GetUserShortConverter(loginInfo.User);
                _user.LoggedInUser = converter.Convert();
                _user.RankToken = $"{_user.LoggedInUser.Pk}_{_httpRequestProcessor.RequestMessage.PhoneId}";
                if(string.IsNullOrEmpty(_user.CsrfToken))
                {
                    cookies =
                      _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                          .BaseAddress);
                    _user.CsrfToken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                }
                return Result.Success(InstaLoginResult.Success);
            }
            catch (Exception exception)
            {
                LogException(exception);
                return Result.Fail(exception, InstaLoginResult.Exception);
            }
            finally
            {
                InvalidateProcessors();
            }
        }

        /// <summary>
        ///     2-Factor Authentication Login using a verification code
        ///     Before call this method, please run LoginAsync first.
        /// </summary>
        /// <param name="verificationCode">Verification Code sent to your phone number</param>
        /// <returns>
        ///     Success --> is succeed
        ///     InvalidCode --> The code is invalid
        ///     CodeExpired --> The code is expired, please request a new one.
        ///     Exception --> Something wrong happened
        /// </returns>
        public async Task<IResult<InstaLoginTwoFactorResult>> TwoFactorLoginAsync(string verificationCode)
        {
            if (_twoFactorInfo == null)
                return Result.Fail<InstaLoginTwoFactorResult>("Run LoginAsync first");

            try
            {
                var twoFactorRequestMessage = new ApiTwoFactorRequestMessage(verificationCode,
                    _httpRequestProcessor.RequestMessage.Username,
                    _httpRequestProcessor.RequestMessage.DeviceId,
                    _twoFactorInfo.TwoFactorIdentifier);

                var instaUri = UriCreator.GetTwoFactorLoginUri();
                var signature =
                    $"{twoFactorRequestMessage.GenerateSignature(InstaApiConstants.IG_SIGNATURE_KEY)}.{twoFactorRequestMessage.GetMessageString()}";
                var fields = new Dictionary<string, string>
                {
                    {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                    {
                        InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                        InstaApiConstants.IG_SIGNATURE_KEY_VERSION
                    }
                };
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var loginInfo =
                        JsonConvert.DeserializeObject<InstaLoginResponse>(json);
                    IsUserAuthenticated = IsUserAuthenticated =
                        loginInfo.User != null && loginInfo.User.UserName.ToLower() == _user.UserName.ToLower();
                    var converter = ConvertersFabric.Instance.GetUserShortConverter(loginInfo.User);
                    _user.LoggedInUser = converter.Convert();
                    _user.RankToken = $"{_user.LoggedInUser.Pk}_{_httpRequestProcessor.RequestMessage.PhoneId}";

                    return Result.Success(InstaLoginTwoFactorResult.Success);
                }

                var loginFailReason = JsonConvert.DeserializeObject<InstaLoginTwoFactorBaseResponse>(json);

                if (loginFailReason.ErrorType == "sms_code_validation_code_invalid")
                    return Result.Fail("Please check the security code.", InstaLoginTwoFactorResult.InvalidCode);
                return Result.Fail("This code is no longer valid, please, call LoginAsync again to request a new one",
                    InstaLoginTwoFactorResult.CodeExpired);
            }
            catch (Exception exception)
            {
                LogException(exception);
                return Result.Fail(exception, InstaLoginTwoFactorResult.Exception);
            }
        }

        /// <summary>
        ///     Get Two Factor Authentication details
        /// </summary>
        /// <returns>
        ///     An instance of TwoFactorInfo if success.
        ///     A null reference if not success; in this case, do LoginAsync first and check if Two Factor Authentication is
        ///     required, if not, don't run this method
        /// </returns>
        public async Task<IResult<TwoFactorLoginInfo>> GetTwoFactorInfoAsync()
        {
            return await Task.Run(() =>
                _twoFactorInfo != null
                    ? Result.Success(_twoFactorInfo)
                    : Result.Fail<TwoFactorLoginInfo>("No Two Factor info available."));
        }


        /// <summary>
        ///     Send recovery code by Username
        /// </summary>
        /// <param name="username">Username</param>
        public async Task<IResult<InstaRecovery>> SendRecoveryByUsernameAsync(string username)
        {
            return await SendRecoveryByEmailAsync(username);
        }

        /// <summary>
        ///     Send recovery code by Email
        /// </summary>
        /// <param name="email">Email Address</param>
        public async Task<IResult<InstaRecovery>> SendRecoveryByEmailAsync(string email)
        {
            try
            {
                string token = "";
                if (!string.IsNullOrEmpty(_user.CsrfToken))
                    token = _user.CsrfToken;
                else
                {
                    var firstResponse = await _httpRequestProcessor.GetAsync(_httpRequestProcessor.Client.BaseAddress);
                    var cookies =
                        _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                            .BaseAddress);
                    _logger?.LogResponse(firstResponse);
                    token = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                }

                var postData = new JObject
                {
                    {"query", email },
                    {"adid", _deviceInfo.GoogleAdId },
                    {"device_id",  ApiRequestMessage.GenerateDeviceId()},
                    {"guid",  _deviceInfo.DeviceGuid.ToString()},
                    {"_csrftoken", token },
                };

                var instaUri = UriCreator.GetAccountRecoveryEmailUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);

                var response = await _httpRequestProcessor.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = JsonConvert.DeserializeObject<MessageErrorsResponseRecoveryEmail>(result);
                    return Result.Fail<InstaRecovery>(error.Message);
                }

                return Result.Success(JsonConvert.DeserializeObject<InstaRecovery>(result));
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaRecovery>(exception);
            }
        }

        /// <summary>
        ///     Send recovery code by Phone
        /// </summary>
        /// <param name="phone">Phone Number</param>
        public async Task<IResult<InstaRecovery>> SendRecoveryByPhoneAsync(string phone)
        {
            try
            {
                string token = "";
                if (!string.IsNullOrEmpty(_user.CsrfToken))
                    token = _user.CsrfToken;
                else
                {
                    var firstResponse = await _httpRequestProcessor.GetAsync(_httpRequestProcessor.Client.BaseAddress);
                    var cookies =
                        _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                            .BaseAddress);
                    _logger?.LogResponse(firstResponse);
                    token = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                }

                var postData = new JObject
                {
                    {"query",  phone},
                    {"_csrftoken",  _user.CsrfToken},
                };

                var instaUri = UriCreator.GetAccountRecoverPhoneUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);

                var response = await _httpRequestProcessor.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = JsonConvert.DeserializeObject<BadStatusErrorsResponse>(result);
                    var errors = "";
                    error.Message.Errors.ForEach(errorContent => errors += errorContent + "\n");
                    return Result.Fail<InstaRecovery>(errors);
                }
                else if (result.Contains("errors"))
                {
                    var error = JsonConvert.DeserializeObject<BadStatusErrorsResponseRecovery>(result);
                    var errors = "";
                    error.Phone_number.Errors.ForEach(errorContent => errors += errorContent + "\n");

                    return Result.Fail<InstaRecovery>(errors);
                }
                return Result.Success(JsonConvert.DeserializeObject<InstaRecovery>(result));
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaRecovery>(exception);
            }
        }


        /// <summary>
        ///    Send Two Factor Login SMS Again
        /// </summary>
        public async Task<IResult<TwoFactorLoginSMSResponse>> SendTwoFactorLoginSMSAsync()
        {
            try
            {
                if (_twoFactorInfo == null)
                    return Result.Fail<TwoFactorLoginSMSResponse>("Run LoginAsync first");

                var postData = new Dictionary<string, string>
                {
                    { "two_factor_identifier",  _twoFactorInfo.TwoFactorIdentifier },
                    { "username",    _httpRequestProcessor.RequestMessage.Username},
                    { "device_id",   _httpRequestProcessor.RequestMessage.DeviceId},
                    { "guid",        _deviceInfo.DeviceGuid.ToString()},
                    { "_csrftoken",    _user.CsrfToken }
                };

                var instaUri = UriCreator.GetAccount2FALoginAgainUri();
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, postData);
                var response = await _httpRequestProcessor.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();

                var T = JsonConvert.DeserializeObject<TwoFactorLoginSMSResponse>(result);
                if (!string.IsNullOrEmpty(T.TwoFactorInfo.TwoFactorIdentifier))
                    _twoFactorInfo.TwoFactorIdentifier = T.TwoFactorInfo.TwoFactorIdentifier;
                return Result.Success(T);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<TwoFactorLoginSMSResponse>(exception);
            }
        }


        /// <summary>
        ///     Logout from instagram asynchronously
        /// </summary>
        /// <returns>
        ///     True if logged out without errors
        /// </returns>
        public async Task<IResult<bool>> LogoutAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetLogoutUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.UnExpectedResponse<bool>(response, json);
                var logoutInfo = JsonConvert.DeserializeObject<BaseStatusResponse>(json);
                if (logoutInfo.Status == "ok")
                    IsUserAuthenticated = false;
                return Result.Success(!IsUserAuthenticated);
            }
            catch (Exception exception)
            {
                LogException(exception);
                return Result.Fail(exception, false);
            }
        }
        string _challengeGuid, _challengeDeviceId;
        public async Task<IResult<ChallengeRequireVerifyMethod>> GetChallengeRequireVerifyMethodAsync()
        {
            if (_challengeinfo == null)
                return Result.Fail("challenge require info is empty.\r\ntry to call LoginAsync function first.", (ChallengeRequireVerifyMethod)null);

            try
            {
                _challengeGuid = Guid.NewGuid().ToString();
                _challengeDeviceId = ApiRequestMessage.GenerateDeviceId();
                var instaUri = UriCreator.GetChallengeRequireFirstUri(_challengeinfo.ApiPath, _challengeGuid, _challengeDeviceId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var msg = "";
                    try
                    {
                        var j = JsonConvert.DeserializeObject<ChallengeRequireVerifyMethod>(json);
                        msg = j.Message;
                    }
                    catch { }
                    return Result.UnExpectedResponse<ChallengeRequireVerifyMethod>(response, json);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<ChallengeRequireVerifyMethod>(json);
                    return Result.Success(obj);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex, (ChallengeRequireVerifyMethod)null);
            }
        }

        public async Task<IResult<ChallengeRequireVerifyMethod>> ResetChallengeRequireVerifyMethodAsync()
        {
            if (_challengeinfo == null)
                return Result.Fail("challenge require info is empty.\r\ntry to call LoginAsync function first.", (ChallengeRequireVerifyMethod)null);

            try
            {
                _challengeGuid = Guid.NewGuid().ToString();
                _challengeDeviceId = ApiRequestMessage.GenerateDeviceId();
                var instaUri = UriCreator.GetResetChallengeRequireUri(_challengeinfo.ApiPath);
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"guid", _challengeGuid},
                    {"device_id", _challengeDeviceId},
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var msg = "";
                    try
                    {
                        var j = JsonConvert.DeserializeObject<ChallengeRequireVerifyMethod>(json);
                        msg = j.Message;
                    }
                    catch { }
                    return Result.UnExpectedResponse<ChallengeRequireVerifyMethod>(response, json);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<ChallengeRequireVerifyMethod>(json);
                    return Result.Success(obj);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex, (ChallengeRequireVerifyMethod)null);
            }
        }

        public async Task<IResult<ChallengeRequireSMSVerify>> RequestVerifyCodeToSMSForChallengeRequireAsync()
        {
            if (_challengeinfo == null)
                return Result.Fail("challenge require info is empty.\r\ntry to call LoginAsync function first.", (ChallengeRequireSMSVerify)null);

            try
            {
                var instaUri = UriCreator.GetChallengeRequireUri(_challengeinfo.ApiPath);
                if (string.IsNullOrEmpty(_challengeGuid))
                    _challengeGuid = Guid.NewGuid().ToString();
                if (string.IsNullOrEmpty(_challengeDeviceId))
                    _challengeDeviceId = ApiRequestMessage.GenerateDeviceId();
                var data = new JObject
                {
                    {"choice", "0"},
                    {"_csrftoken", _user.CsrfToken},
                    {"guid", _challengeGuid},
                    {"device_id", _challengeDeviceId},
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var msg = "";
                    try
                    {
                        var j = JsonConvert.DeserializeObject<ChallengeRequireSMSVerify>(json);
                        msg = j.Message;
                    }
                    catch { }
                    return Result.Fail(msg, (ChallengeRequireSMSVerify)null);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<ChallengeRequireSMSVerify>(json);
                    return Result.Success(obj);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex, (ChallengeRequireSMSVerify)null);
            }
        }

        public async Task<IResult<ChallengeRequireEmailVerify>> RequestVerifyCodeToEmailForChallengeRequireAsync()
        {
            if (_challengeinfo == null)
                return Result.Fail("challenge require info is empty.\r\ntry to call LoginAsync function first.", (ChallengeRequireEmailVerify)null);

            try
            {
                var instaUri = UriCreator.GetChallengeRequireUri(_challengeinfo.ApiPath);
                if (string.IsNullOrEmpty(_challengeGuid))
                    _challengeGuid = Guid.NewGuid().ToString();
                if (string.IsNullOrEmpty(_challengeDeviceId))
                    _challengeDeviceId = ApiRequestMessage.GenerateDeviceId();
                var data = new JObject
                {
                    {"choice", "1"},
                    {"_csrftoken", _user.CsrfToken},
                    {"guid", _challengeGuid},
                    {"device_id", _challengeDeviceId},
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var msg = "";
                    try
                    {
                        var j = JsonConvert.DeserializeObject<ChallengeRequireEmailVerify>(json);
                        msg = j.Message;
                    }
                    catch { }
                    return Result.Fail(msg, (ChallengeRequireEmailVerify)null);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<ChallengeRequireEmailVerify>(json);
                    return Result.Success(obj);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex, (ChallengeRequireEmailVerify)null);
            }
        }

        public async Task<IResult<InstaLoginResult>> VerifyCodeForChallengeRequireAsync(string verifyCode)
        {
            if(verifyCode.Length != 6)
                return Result.Fail("Verify code must be an 6 digit number.", InstaLoginResult.Exception);

            if (_challengeinfo == null)
                return Result.Fail("challenge require info is empty.\r\ntry to call LoginAsync function first.", InstaLoginResult.Exception);

            try
            {
                var cookies =
            _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(_httpRequestProcessor.Client
                .BaseAddress);
                var csrftoken = cookies[InstaApiConstants.CSRFTOKEN]?.Value ?? String.Empty;
                _user.CsrfToken = csrftoken;
                var instaUri = UriCreator.GetChallengeRequireUri(_challengeinfo.ApiPath);
                if (string.IsNullOrEmpty(_challengeGuid))
                    _challengeGuid = Guid.NewGuid().ToString();
                if (string.IsNullOrEmpty(_challengeDeviceId))
                    _challengeDeviceId = ApiRequestMessage.GenerateDeviceId();
                var data = new JObject
                {
                    {"security_code", verifyCode},
                    {"_csrftoken", _user.CsrfToken},
                    {"guid", _challengeGuid},
                    {"device_id", _challengeDeviceId},
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                request.Headers.Add("Host", "i.instagram.com");
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var msg = "";
                    try
                    {
                        var j = JsonConvert.DeserializeObject<ChallengeRequireVerifyCode>(json);
                        msg = j.Message;
                    }
                    catch { }
                    return Result.UnExpectedResponse<InstaLoginResult>(response, msg + "\t"+ json);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<ChallengeRequireVerifyCode>(json);
                    if (obj != null)
                    {
                        if (obj.LoggedInUser != null)
                        {
                            ValidateUserAsync(obj.LoggedInUser, csrftoken);
                            await Task.Delay(3000);
                            await _messagingProcessor.GetDirectInboxAsync();
                            await _feedProcessor.GetRecentActivityFeedAsync(PaginationParameters.MaxPagesToLoad(1));

                            return Result.Success(InstaLoginResult.Success);
                        }
                        else if (!string.IsNullOrEmpty(obj.Action))
                        {
                            // we should wait at least 15 seconds and then trying to login again
                            await Task.Delay(15000);
                            return await LoginAsync(false);
                        }
                    }
                    return Result.UnExpectedResponse<InstaLoginResult>(response, json);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Result.Fail(ex, InstaLoginResult.Exception);
            }
        }


        private void ValidateUserAsync(InstaUserShortResponse user, string csrfToken, bool validateExtra = true)
        {
            try
            {
                var converter = ConvertersFabric.Instance.GetUserShortConverter(user);
                _user.LoggedInUser = converter.Convert();
                if (validateExtra)
                {
                    _user.RankToken = $"{_user.LoggedInUser.Pk}_{_httpRequestProcessor.RequestMessage.PhoneId}";
                    _user.CsrfToken = csrfToken;
                    IsUserAuthenticated = true;
                    InvalidateProcessors();
                }
            }
            catch { }
        }
        /// <summary>
        ///     Set cookie and html document to verify login information.
        /// </summary>
        /// <param name="htmlDocument">Html document source</param>
        /// <param name="cookies">Cookies from webview or webbrowser control</param>
        /// <returns>True if logged in, False if not</returns>
        public async Task<IResult<bool>> SetCookiesAndHtmlForFacebookLoginAsync(string htmlDocument, string cookie, bool facebookLogin = false)
        {
            if (!string.IsNullOrEmpty(cookie) && !string.IsNullOrEmpty(htmlDocument))
            {
                try
                {
                    var start = "<script type=\"text/javascript\">window._sharedData";
                    var end = ";</script>";

                    var str = htmlDocument.Substring(htmlDocument.IndexOf(start) + start.Length);
                    str = str.Substring(0, str.IndexOf(end));
                    str = str.Substring(str.IndexOf("=") + 2);
                    var o = JsonConvert.DeserializeObject<WebBrowserResponse>(str);
                    return await SetCookiesAndHtmlForFacebookLogin(o, cookie, facebookLogin);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, false);
                }
            }
            return Result.Fail("", false);
        }
        /// <summary>
        ///     Set cookie and web browser response object to verify login information.
        /// </summary>
        /// <param name="webBrowserResponse">Web browser response object</param>
        /// <param name="cookies">Cookies from webview or webbrowser control</param>
        /// <returns>True if logged in, False if not</returns>
        public async Task<IResult<bool>> SetCookiesAndHtmlForFacebookLogin(WebBrowserResponse webBrowserResponse, string cookie, bool facebookLogin = false)
        {
            if(webBrowserResponse == null)
                return Result.Fail("", false);
            if(webBrowserResponse.Config == null)
                return Result.Fail("", false);
            if(webBrowserResponse.Config.Viewer == null)
                return Result.Fail("", false);

            if (!string.IsNullOrEmpty(cookie))
            {
                try
                {
                    var uri = new Uri(InstaApiConstants.INSTAGRAM_URL);
                    //if (cookie.Contains("urlgen"))
                    //{
                    //    var removeStart = "urlgen=";
                    //    var removeEnd = ";";
                    //    var t = cookie.Substring(cookie.IndexOf(removeStart) + 0);
                    //    t = t.Substring(0, t.IndexOf(removeEnd) + 2);
                    //    cookie = cookie.Replace(t, "");
                    //}
                    cookie = cookie.Replace(';', ',');
                    _httpRequestProcessor.HttpHandler.CookieContainer.SetCookies(uri, cookie);

                    InstaUserShort user = new InstaUserShort
                    {
                        Pk = long.Parse(webBrowserResponse.Config.Viewer.Id),
                        UserName = _user.UserName,
                        ProfilePictureId = "unknown",
                        FullName = webBrowserResponse.Config.Viewer.FullName,
                        ProfilePicture = webBrowserResponse.Config.Viewer.ProfilePicUrl
                    };
                    _user.LoggedInUser = user;
                    _user.CsrfToken = webBrowserResponse.Config.CsrfToken;
                    _user.RankToken = $"{webBrowserResponse.Config.Viewer.Id}_{_httpRequestProcessor.RequestMessage.PhoneId}";
                    IsUserAuthenticated = true;
                    if (facebookLogin)
                    {
                        try
                        {
                            var instaUri = UriCreator.GetFacebookSignUpUri();
                            var data = new JObject
                            {
                                {"dryrun", "true"},
                                {"phone_id", _deviceInfo.DeviceGuid.ToString()},
                                {"_csrftoken", _user.CsrfToken},
                                {"adid", Guid.NewGuid().ToString()},
                                {"guid", Guid.NewGuid().ToString()},
                                {"device_id", ApiRequestMessage.GenerateDeviceId()},
                                {"waterfall_id", Guid.NewGuid().ToString()},
                                {"fb_access_token", InstaApiConstants.FB_ACCESS_TOKEN},
                            };
                            var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                            request.Headers.Add("Host", "i.instagram.com");
                            var response = await _httpRequestProcessor.SendAsync(request);
                            var json = await response.Content.ReadAsStringAsync();
                            var obj = JsonConvert.DeserializeObject<FacebookLoginResponse>(json);
                            _user.FacebookUserId = obj.FbUserId;
                        }
                        catch(Exception)
                        {
                        }
                        InvalidateProcessors();
                    }
                    return Result.Success(true);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, false);
                }
            }
            return Result.Fail("", false);
        }
        /// <summary>
        ///     Get current state info as Memory stream
        /// </summary>
        /// <returns>
        ///     State data
        /// </returns>
        public Stream GetStateDataAsStream()
        {

            var Cookies = _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(new Uri(InstaApiConstants.INSTAGRAM_URL));
            var RawCookiesList = new List<Cookie>();
            foreach (Cookie cookie in Cookies)
            {
                RawCookiesList.Add(cookie);
            }


            var state = new StateData
            {
                DeviceInfo = _deviceInfo,
                IsAuthenticated = IsUserAuthenticated,
                UserSession = _user,
                Cookies = _httpRequestProcessor.HttpHandler.CookieContainer,
                RawCookies = RawCookiesList
            };
            return SerializationHelper.SerializeToStream(state);
        }
        /// <summary>
        ///     Get current state info as Json string
        /// </summary>
        /// <returns>
        ///     State data
        /// </returns>
        public string GetStateDataAsString()
        {

            var Cookies = _httpRequestProcessor.HttpHandler.CookieContainer.GetCookies(new Uri(InstaApiConstants.INSTAGRAM_URL));
            var RawCookiesList = new List<Cookie>();
            foreach (Cookie cookie in Cookies)
            {
                RawCookiesList.Add(cookie);
            }

            var state = new StateData
            {
                DeviceInfo = _deviceInfo,
                IsAuthenticated = IsUserAuthenticated,
                UserSession = _user,
                Cookies = _httpRequestProcessor.HttpHandler.CookieContainer,
                RawCookies = RawCookiesList
            };
            return SerializationHelper.SerializeToString(state);
        }
        /// <summary>
        ///     Get current state info as Memory stream asynchronously
        /// </summary>
        /// <returns>
        ///     State data
        /// </returns>
        public async Task<Stream> GetStateDataAsStreamAsync()
        {
            return await Task<Stream>.Factory.StartNew(() =>
            {
                var state = GetStateDataAsStream();
                Task.Delay(1000);
                return state;
            });
        }
        /// <summary>
        ///     Get current state info as Json string asynchronously
        /// </summary>
        /// <returns>
        ///     State data
        /// </returns>
        public async Task<string> GetStateDataAsStringAsync()
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                var state = GetStateDataAsString();
                Task.Delay(1000);
                return state;
            });
        }
        /// <summary>
        ///     Loads the state data from stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void LoadStateDataFromStream(Stream stream)
        {
            var data = SerializationHelper.DeserializeFromStream<StateData>(stream);
            if (!IsCustomDeviceSet)
                _deviceInfo = data.DeviceInfo;
            _user = data.UserSession;
            
            _httpRequestProcessor.RequestMessage.Username = data.UserSession.UserName;
            _httpRequestProcessor.RequestMessage.Password = data.UserSession.Password;
            
            _httpRequestProcessor.RequestMessage.DeviceId = data.DeviceInfo.DeviceId;
            _httpRequestProcessor.RequestMessage.PhoneId = data.DeviceInfo.PhoneGuid.ToString();
            _httpRequestProcessor.RequestMessage.Guid = data.DeviceInfo.DeviceGuid;
            _httpRequestProcessor.RequestMessage.AdId = data.DeviceInfo.AdId.ToString();

            foreach (Cookie cookie in data.RawCookies)
            {
                _httpRequestProcessor.HttpHandler.CookieContainer.Add(new Uri(InstaApiConstants.INSTAGRAM_URL), cookie);
            }
            

            IsUserAuthenticated = data.IsAuthenticated;
            InvalidateProcessors();
        }
        /// <summary>
        ///     Set state data from provided json string
        /// </summary>
        public void LoadStateDataFromString(string json)
        {
            var data = SerializationHelper.DeserializeFromString<StateData>(json);
            if (!IsCustomDeviceSet)
                _deviceInfo = data.DeviceInfo;
            _user = data.UserSession;
            
            //Load Stream Edit 
            _httpRequestProcessor.RequestMessage.Username = data.UserSession.UserName;
            _httpRequestProcessor.RequestMessage.Password = data.UserSession.Password;
            
            _httpRequestProcessor.RequestMessage.DeviceId = data.DeviceInfo.DeviceId;
            _httpRequestProcessor.RequestMessage.PhoneId = data.DeviceInfo.PhoneGuid.ToString();
            _httpRequestProcessor.RequestMessage.Guid = data.DeviceInfo.DeviceGuid;
            _httpRequestProcessor.RequestMessage.AdId = data.DeviceInfo.AdId.ToString();

            foreach (Cookie cookie in data.RawCookies)
            {
                _httpRequestProcessor.HttpHandler.CookieContainer.Add(new Uri(InstaApiConstants.INSTAGRAM_URL), cookie);
            }
            

            IsUserAuthenticated = data.IsAuthenticated;
            InvalidateProcessors();
        }
        /// <summary>
        ///     Set state data from provided stream asynchronously
        /// </summary>
        public async Task LoadStateDataFromStreamAsync(Stream stream)
        {
            await Task.Factory.StartNew(() =>
            {
                LoadStateDataFromStream(stream);
                Task.Delay(1000);
            });
        }
        /// <summary>
        ///     Set state data from provided json string asynchronously
        /// </summary>
        public async Task LoadStateDataFromStringAsync(string json)
        {
            await Task.Factory.StartNew(() =>
            {
                LoadStateDataFromString(json);
                Task.Delay(1000);
            });
        }
        #endregion


        #region private part

        private void InvalidateProcessors()
        {
            _hashtagProcessor = new HashtagProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _locationProcessor = new LocationProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _collectionProcessor = new CollectionProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _mediaProcessor = new MediaProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _userProcessor = new UserProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _storyProcessor = new StoryProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _commentProcessor = new CommentProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _messagingProcessor = new MessagingProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _feedProcessor = new FeedProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);

            _liveProcessor = new LiveProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _discoverProcessor = new DiscoverProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _accountProcessor = new AccountProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _helperProcessor = new HelperProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);
            _tvProcessor = new TVProcessor(_deviceInfo, _user, _httpRequestProcessor, _logger, _userAuthValidate, this);


        }
        private void ValidateUser()
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
        }

        private void ValidateLoggedIn()
        {
            if (!IsUserAuthenticated)
                throw new ArgumentException("user must be authenticated");
        }

        private void ValidateRequestMessage()
        {
            if (_httpRequestProcessor.RequestMessage == null || _httpRequestProcessor.RequestMessage.IsEmpty())
                throw new ArgumentException("API request message null or empty");
        }

        private void LogException(Exception exception)
        {
            _logger?.LogException(exception);
        }

#endregion
    }
}