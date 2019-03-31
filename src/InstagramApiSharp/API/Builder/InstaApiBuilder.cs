using System;
using System.Net.Http;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Logger;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Classes.SessionHandlers;

namespace InstagramApiSharp.API.Builder
{
    public class InstaApiBuilder : IInstaApiBuilder
    {
        private IRequestDelay _delay = RequestDelay.Empty();
        private AndroidDevice _device;
        private HttpClient _httpClient;
        private HttpClientHandler _httpHandler = new HttpClientHandler();
        private IHttpRequestProcessor _httpRequestProcessor;
        private IInstaLogger _logger;
        private ApiRequestMessage _requestMessage;
        private UserSessionData _user;
        private InstaApiVersionType? _apiVersionType;
        private ISessionHandler _sessionHandler;

        private InstaApiBuilder()
        {
        }

        /// <summary>
        ///     Create new API instance
        /// </summary>
        /// <returns>
        ///     API instance
        /// </returns>
        /// <exception cref="ArgumentNullException">User auth data must be specified</exception>
        public IInstaApi Build()
        {
            if (_user == null)
                _user = UserSessionData.Empty;

            if (_httpHandler == null) _httpHandler = new HttpClientHandler();

            if (_httpClient == null)
                _httpClient = new HttpClient(_httpHandler) { BaseAddress = new Uri(InstaApiConstants.INSTAGRAM_URL) };

            if (_requestMessage == null)
            {
                if (_device == null)
                    _device = AndroidDeviceGenerator.GetRandomAndroidDevice();
                _requestMessage = new ApiRequestMessage
                {
                    PhoneId = _device.PhoneGuid.ToString(),
                    Guid = _device.DeviceGuid,
                    Password = _user?.Password,
                    Username = _user?.UserName,
                    DeviceId = ApiRequestMessage.GenerateDeviceId(),
                    AdId = _device.AdId.ToString()
                };
            }

            if (string.IsNullOrEmpty(_requestMessage.Password)) _requestMessage.Password = _user?.Password;
            if (string.IsNullOrEmpty(_requestMessage.Username)) _requestMessage.Username = _user?.UserName;

            try
            {
                InstaApiConstants.TIMEZONE_OFFSET = int.Parse(DateTimeOffset.Now.Offset.TotalSeconds.ToString());
            }
            catch { }

            if (_httpRequestProcessor == null)
                _httpRequestProcessor =
                    new HttpRequestProcessor(_delay, _httpClient, _httpHandler, _requestMessage, _logger);

            if (_apiVersionType == null)
                _apiVersionType = InstaApiVersionType.Version86;

            var instaApi = new InstaApi(_user, _logger, _device, _httpRequestProcessor, _apiVersionType.Value);
            if (_sessionHandler != null)
            {
                _sessionHandler.InstaApi = instaApi;
                instaApi.SessionHandler = _sessionHandler;
            }
            return instaApi;
        }

        /// <summary>
        ///     Use custom logger
        /// </summary>
        /// <param name="logger">IInstaLogger implementation</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder UseLogger(IInstaLogger logger)
        {
            _logger = logger;
            return this;
        }

        /// <summary>
        ///     Set specific HttpClient
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder UseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            return this;
        }

        /// <summary>
        ///     Set custom HttpClientHandler to be able to use certain features, e.g Proxy and so on
        /// </summary>
        /// <param name="handler">HttpClientHandler</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler)
        {
            _httpHandler = handler;
            return this;
        }

        /// <summary>
        ///     Specify user login, password from here
        /// </summary>
        /// <param name="user">User auth data</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder SetUser(UserSessionData user)
        {
            _user = user;
            return this;
        }

        /// <summary>
        ///     Set custom request message. Used to be able to customize device info.
        /// </summary>
        /// <param name="requestMessage">Custom request message object</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        /// <remarks>
        ///     Please, do not use if you don't know what you are doing
        /// </remarks>
        public IInstaApiBuilder SetApiRequestMessage(ApiRequestMessage requestMessage)
        {
            _requestMessage = requestMessage;
            return this;
        }

        /// <summary>
        ///     Set delay between requests. Useful when API supposed to be used for mass-bombing.
        /// </summary>
        /// <param name="delay">Timespan delay</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder SetRequestDelay(IRequestDelay delay)
        {
            if (delay == null)
                delay = RequestDelay.Empty();
            _delay = delay;
            return this;
        }

        /// <summary>
        ///     Set custom android device.
        ///     <para>Note: this is optional, if you didn't set this, InstagramApiSharp will choose random device.</para>
        /// </summary>
        /// <param name="androidDevice">Android device</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder SetDevice(AndroidDevice androidDevice)
        {
            _device = androidDevice;
            return this;
        }
        /// <summary>
        ///     Set instagram api version (for user agent version)
        /// </summary>
        /// <param name="apiVersion">Api version</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder SetApiVersion(InstaApiVersionType apiVersion)
        {
            _apiVersionType = apiVersion;
            return this;
        }

        /// <summary>
        ///     Set session handler
        /// </summary>
        /// <param name="sessionHandler">Session handler</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder SetSessionHandler(ISessionHandler sessionHandler)
        {
            _sessionHandler = sessionHandler;
            return this;
        }

        /// <summary>
        ///     Set Http request processor
        /// </summary>
        /// <param name="httpRequestProcessor">HttpRequestProcessor</param>
        /// <returns>
        ///     API Builder
        /// </returns>
        public IInstaApiBuilder SetHttpRequestProcessor(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
            return this;
        }

        /// <summary>
        ///     Creates the builder.
        /// </summary>
        /// <returns>
        ///     API Builder
        /// </returns>
        public static IInstaApiBuilder CreateBuilder()
        {
            return new InstaApiBuilder();
        }
    }
}