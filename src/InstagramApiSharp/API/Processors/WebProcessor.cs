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
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using InstagramApiSharp.Helpers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.Web;
using System.Collections.Generic;

namespace InstagramApiSharp.API.Processors
{
    internal class WebProcessor : IWebProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;

        public WebProcessor(AndroidDevice deviceInfo, UserSessionData user, IHttpRequestProcessor httpRequestProcessor,
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
        ///     Get self account information like joined date or switched to business account date.
        /// </summary>
        public async Task<IResult<InstaWebAccountInfo>> GetAccountInfoAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var mediaUri = WebUriCreator.GetAccountsDataUri();
                var request = _httpHelper.GetWebRequest(HttpMethod.Get, mediaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var html = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail($"Error! Status code: {response.StatusCode}", default(InstaWebAccountInfo));

                var json = html.GetJson();
                if (json == null)
                    return Result.Fail($"Json response isn't available.", default(InstaWebAccountInfo));

                var obj = JsonConvert.DeserializeObject<InstaWebContainer>(json);

                if (obj.Entry?.SettingsPages != null)
                {
                    var first = obj.Entry.SettingsPages.FirstOrDefault();
                    if (first != null)
                        return Result.Success(ConvertersFabric.Instance.GetWebAccountInfoConverter(first).Convert());
                }
                return Result.Fail($"Date joined isn't available.", default(InstaWebAccountInfo));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaWebAccountInfo), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, default(InstaWebAccountInfo));
            }
        }



    }
}
