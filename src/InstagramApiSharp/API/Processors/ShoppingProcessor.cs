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

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Shopping and commerce api functions.
    /// </summary>
    internal class ShoppingProcessor : IShoppingProcessor
    {
        #region Properties and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        private readonly HttpHelper _httpHelper;
        public ShoppingProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        ///     Get all user shoppable media by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        public async Task<IResult<InstaMediaList>> GetUserShoppableMediaAsync(string username,
             PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            var user = await _instaApi.UserProcessor.GetUserAsync(username);
            if (!user.Succeeded)
                return Result.Fail<InstaMediaList>("Unable to get user to load shoppable media");
            return await GetUserShoppableMedia(user.Value.Pk, paginationParameters);
        }





        public async Task<IResult<InstaProductInfo>> GetProductInfoAsync(long productId, string mediaPk, int deviceWidth = 720)
        {
            try
            {
                var instaUri = UriCreator.GetProductInfoUri(productId, mediaPk, deviceWidth);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaProductInfo>(response, json);

                var productInfoResponse = JsonConvert.DeserializeObject<InstaProductInfoResponse>(json);
                var converted = ConvertersFabric.Instance.GetProductInfoConverter(productInfoResponse).Convert();

                return Result.Success(converted);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaProductInfo>(exception);
            }
        }








        private async Task<IResult<InstaMediaList>> GetUserShoppableMedia(long userId,
                                            PaginationParameters paginationParameters)
        {
            var mediaList = new InstaMediaList();
            try
            {
                var instaUri = UriCreator.GetUserShoppableMediaListUri(userId, paginationParameters.NextMaxId);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaList>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());

                mediaList = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse).Convert();
                mediaList.NextMaxId = paginationParameters.NextMaxId = mediaResponse.NextMaxId;

                while (mediaResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextMaxId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    paginationParameters.PagesLoaded++;
                    var nextMedia = await GetUserShoppableMedia(userId, paginationParameters);
                    if (!nextMedia.Succeeded)
                        return Result.Fail(nextMedia.Info, mediaList);
                    mediaList.NextMaxId = paginationParameters.NextMaxId = nextMedia.Value.NextMaxId;
                    mediaList.AddRange(nextMedia.Value);
                }

                mediaList.Pages = paginationParameters.PagesLoaded;
                mediaList.PageSize = mediaResponse.ResultsCount;
                return Result.Success(mediaList);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, mediaList);
            }
        }
    }
}
