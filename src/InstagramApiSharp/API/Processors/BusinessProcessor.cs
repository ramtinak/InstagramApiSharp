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
using InstagramApiSharp.Classes.Models.Business;

namespace InstagramApiSharp.API.Processors
{
    internal class BusinessProcessor : IBusinessProcessor
    {
        #region Properties and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        private readonly HttpHelper _httpHelper;
        public BusinessProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        /// <summary>
        ///     Get statistics of current account
        /// </summary>
        public async Task<IResult<InstaStatistics>> GetStatisticsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetGraphStatisticsUri(InstaApiConstants.ACCEPT_LANGUAGE);
                var queryParamsData = new JObject
                {
                    {"access_token", ""},
                    {"id", _user.LoggedInUser.Pk.ToString()}
                };
                var variables = new JObject
                {
                    {"query_params", queryParamsData},
                    {"timezone", InstaApiConstants.TIMEZONE}
                };
                var data = new Dictionary<string, string>
                {
                    {"access_token", "undefined"},
                    {"fb_api_caller_class", "RelayModern"},
                    {"variables", variables.ToString(Formatting.None)},
                    {"doc_id", "1618080801573402"}
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaStatistics>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaStatisticsRootResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetStatisticsConverter(obj).Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStatistics>(exception);
            }
        }
        /// <summary>
        ///     Get media insights
        /// </summary>
        /// <param name="mediaPk">Media PK (<see cref="InstaMedia.Pk"/>)</param>
        public async Task<IResult<InstaMediaInsights>> GetMediaInsightsAsync(string mediaPk)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetMediaSingleInsightsUri(mediaPk);
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaInsights>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaMediaInsightsContainer>(json);
                return Result.Success(obj.MediaOrganicInsights);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMediaInsights>(exception);
            }
        }
        /// <summary>
        ///     Get full media insights
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        public async Task<IResult<InstaFullMediaInsights>> GetFullMediaInsightsAsync(string mediaId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetGraphStatisticsUri(InstaApiConstants.ACCEPT_LANGUAGE, InstaInsightSurfaceType.Post);

                var queryParamsData = new JObject
                {
                    {"access_token", ""},
                    {"id", mediaId}
                };
                var variables = new JObject
                {
                    {"query_params", queryParamsData}
                };
                var data = new Dictionary<string, string>
                {
                    {"access_token", "undefined"},
                    {"fb_api_caller_class", "RelayModern"},
                    {"variables", variables.ToString(Formatting.None)},
                    {"doc_id", "1527362987318283"}
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFullMediaInsights>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaFullMediaInsightsRootResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetFullMediaInsightsConverter(obj.Data.Media).Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFullMediaInsights>(exception);
            }
        }

    }
}
