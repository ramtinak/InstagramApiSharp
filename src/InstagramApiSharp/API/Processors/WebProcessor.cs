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
using InstagramApiSharp.Helpers;
using System.Net.Http;
using System.Net;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers.Web;
using InstagramApiSharp.Enums;

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

        #region public part

        /// <summary>
        ///     Get self account information like joined date or switched to business account date
        /// </summary>
        public async Task<IResult<InstaWebAccountInfo>> GetAccountInfoAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = WebUriCreator.GetAccountsDataUri();
                var request = _httpHelper.GetWebRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var html = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail($"Error! Status code: {response.StatusCode}", default(InstaWebAccountInfo));

                var json = html.GetJson();
                if (json.IsEmpty())
                    return Result.Fail($"Json response isn't available.", default(InstaWebAccountInfo));

                var obj = JsonConvert.DeserializeObject<InstaWebContainerResponse>(json);

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

        /// <summary>
        ///     Get self account follow requests
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaWebTextData>> GetFollowRequestsAsync(PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var textDataList = new InstaWebTextData();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaWebTextData Convert(InstaWebSettingsPageResponse settingsPageResponse)
                {
                    return ConvertersFabric.Instance.GetWebTextDataListConverter(settingsPageResponse).Convert();
                }
                Uri CreateUri(string cursor = null)
                {
                    return WebUriCreator.GetCurrentFollowRequestsUri(cursor);
                }
                var request = await GetRequest(CreateUri(paginationParameters?.NextMaxId));
                if (!request.Succeeded)
                {
                    if (request.Value != null)
                        return Result.Fail(request.Info, Convert(request.Value));
                    else
                        return Result.Fail(request.Info, textDataList);
                }

                var response = request.Value;

                paginationParameters.NextMaxId = response.Data.Cursor;

                while (!string.IsNullOrEmpty(paginationParameters.NextMaxId)
                     && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextRequest = await GetRequest(CreateUri(paginationParameters?.NextMaxId));
                    if (!nextRequest.Succeeded)
                        return Result.Fail(nextRequest.Info, Convert(response));
                    var nextResponse = nextRequest.Value;

                    if (nextResponse.Data != null)
                        response.Data.Data.AddRange(nextResponse.Data.Data);

                    response.Data.Cursor = paginationParameters.NextMaxId = nextResponse.Data?.Cursor;
                    paginationParameters.PagesLoaded++;
                }
                return Result.Success(Convert(response));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, textDataList, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, textDataList);
            }
        }

        /// <summary>
        ///     Get former biography texts
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaWebData>> GetFormerBiographyLinksAsync(PaginationParameters paginationParameters)
        {
            return await GetFormerAsync(InstaWebType.FormerLinksInBio, paginationParameters);
        }

        /// <summary>
        ///     Get former biography texts
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaWebData>> GetFormerBiographyTextsAsync(PaginationParameters paginationParameters)
        {
            return await GetFormerAsync(InstaWebType.FormerBioTexts, paginationParameters);
        }
        /// <summary>
        ///     Get former emails
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaWebData>> GetFormerEmailsAsync(PaginationParameters paginationParameters)
        {
            return await GetFormerAsync(InstaWebType.FormerEmails, paginationParameters);
        }

        /// <summary>
        ///     Get former full names
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaWebData>> GetFormerFullNamesAsync(PaginationParameters paginationParameters)
        {
            return await GetFormerAsync(InstaWebType.FormerFullNames, paginationParameters);
        }

        /// <summary>
        ///     Get former phone numbers
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaWebData>> GetFormerPhoneNumbersAsync(PaginationParameters paginationParameters)
        {
            return await GetFormerAsync(InstaWebType.FormerPhones, paginationParameters);
        }

        /// <summary>
        ///     Get former usernames
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaWebData>> GetFormerUsernamesAsync(PaginationParameters paginationParameters)
        {
            return await GetFormerAsync(InstaWebType.FormerUsernames, paginationParameters);
        }
        
        #endregion public part

        #region private part

        private async Task<IResult<InstaWebData>> GetFormerAsync(InstaWebType type, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var webData = new InstaWebData();
            try
            {
                if (paginationParameters == null)
                    paginationParameters = PaginationParameters.MaxPagesToLoad(1);

                InstaWebData Convert(InstaWebSettingsPageResponse settingsPageResponse)
                {
                    return ConvertersFabric.Instance.GetWebDataConverter(settingsPageResponse).Convert();
                }
                Uri CreateUri(string cursor = null)
                {
                    switch(type)
                    {
                        case InstaWebType.FormerBioTexts:
                            return WebUriCreator.GetFormerBiographyTextsUri(cursor);
                        case InstaWebType.FormerLinksInBio:
                            return WebUriCreator.GetFormerBiographyLinksUri(cursor);
                        case InstaWebType.FormerUsernames:
                            return WebUriCreator.GetFormerUsernamesUri(cursor);
                        case InstaWebType.FormerFullNames:
                            return WebUriCreator.GetFormerFullNamesUri(cursor);
                        case InstaWebType.FormerPhones:
                            return WebUriCreator.GetFormerPhoneNumbersUri(cursor);
                        case InstaWebType.FormerEmails:
                            return WebUriCreator.GetFormerEmailsUri(cursor);
                        default:
                            return WebUriCreator.GetFormerBiographyLinksUri(cursor);
                    }
                }
                var request = await GetRequest(CreateUri(paginationParameters?.NextMaxId));
                if (!request.Succeeded)
                {
                    if (request.Value != null)
                        return Result.Fail(request.Info, Convert(request.Value));
                    else
                        return Result.Fail(request.Info, webData);
                }

                var response = request.Value;

                paginationParameters.NextMaxId = response.Data.Cursor;

                while (!string.IsNullOrEmpty(paginationParameters.NextMaxId)
                     && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextRequest = await GetRequest(CreateUri(paginationParameters?.NextMaxId));
                    if (!nextRequest.Succeeded)
                        return Result.Fail(nextRequest.Info, Convert(response));
                    var nextResponse = nextRequest.Value;

                    if (nextResponse.Data != null)
                        response.Data.Data.AddRange(nextResponse.Data.Data);

                    response.Data.Cursor = paginationParameters.NextMaxId = nextResponse.Data?.Cursor;
                    paginationParameters.PagesLoaded++;
                }
                return Result.Success(Convert(response));
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, webData, ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, webData);
            }
        }

        private async Task<IResult<InstaWebSettingsPageResponse>> GetRequest(Uri instaUri)
        {
            try
            {
                var request = _httpHelper.GetWebRequest(HttpMethod.Get, instaUri, _deviceInfo);

                request.Headers.Add("upgrade-insecure-requests", "1");
                request.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                var response = await _httpRequestProcessor.SendAsync(request);
                var html = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail($"Error! Status code: {response.StatusCode}", default(InstaWebSettingsPageResponse));

                if (instaUri.ToString().ToLower().Contains("a=1&cursor="))
                    return Result.Success(JsonConvert.DeserializeObject<InstaWebSettingsPageResponse>(html));
                else
                {
                    var json = html.GetJson();
                    if (json.IsEmpty())
                        return Result.Fail($"Json response isn't available.", default(InstaWebSettingsPageResponse));

                    var obj = JsonConvert.DeserializeObject<InstaWebContainerResponse>(json);

                    if (obj.Entry?.SettingsPages != null)
                    {
                        var first = obj.Entry.SettingsPages.FirstOrDefault();
                        if (first != null)
                            return Result.Success(first);
                    }
                    return Result.Fail($"Data isn't available.", default(InstaWebSettingsPageResponse));
                }
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaWebSettingsPageResponse), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, default(InstaWebSettingsPageResponse));
            }
        }
        
        #endregion private part
    }
}
