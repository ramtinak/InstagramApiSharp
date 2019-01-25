using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using InstagramApiSharp.Enums;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Instagram TV api functions.
    /// </summary>
    internal class TVProcessor : ITVProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        public TVProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        /// <summary>
        ///     Get channel by user id (pk) => channel owner
        /// </summary>
        /// <param name="userId">User id (pk) => channel owner</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaTVChannel>> GetChannelByIdAsync(long userId, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await GetChannel(null, userId, paginationParameters);
        }

        /// <summary>
        ///     Get channel by <seealso cref="InstaTVChannelType"/>
        /// </summary>
        /// <param name="channelType">Channel type</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        public async Task<IResult<InstaTVChannel>> GetChannelByTypeAsync(InstaTVChannelType channelType, PaginationParameters paginationParameters)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await GetChannel(channelType, null, paginationParameters);
        }

        /// <summary>
        ///     Get suggested searches
        /// </summary>
        public async Task<IResult<InstaTVSearch>> GetSuggestedSearchesAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetIGTVSuggestedSearchesUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTVSearch>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaTVSearchResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetTVSearchConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTVSearch), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaTVSearch>(exception);
            }
        }

        /// <summary>
        ///     Get TV Guide (gets popular and suggested channels)
        /// </summary>
        public async Task<IResult<InstaTV>> GetTVGuideAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetIGTVGuideUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTV>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaTVResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetTVConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTV), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaTV>(exception);
            }
        }
        /// <summary>
        ///     Search channels
        /// </summary>
        /// <param name="query">Channel or username</param>
        public async Task<IResult<InstaTVSearch>> SearchAsync(string query)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetIGTVSearchUri(query);
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTVSearch>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaTVSearchResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetTVSearchConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTVSearch), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaTVSearch>(exception);
            }
        }

        /// <summary>
        ///     Upload video to Instagram TV
        /// </summary>
        /// <param name="video">Video to upload (aspect ratio is very important for thumbnail and video | range 0.5 - 1.0 | Width = 480, Height = 852)</param>
        /// <param name="title">Title</param>
        /// <param name="caption">Caption</param>
        public async Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideoUpload video, string title, string caption)
        {
            return await UploadVideoAsync(null, video, title, caption);
        }

        /// <summary>
        ///     Upload video to Instagram TV with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload (aspect ratio is very important for thumbnail and video | range 0.5 - 1.0 | Width = 480, Height = 852)</param>
        /// <param name="title">Title</param>
        /// <param name="caption">Caption</param>
        public async Task<IResult<InstaMedia>> UploadVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, string title, string caption)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            return await _instaApi.HelperProcessor.SendIGTVVideoAsync(progress, video, title, caption);
        }

        private async Task<IResult<InstaTVChannel>> GetChannel(InstaTVChannelType? channelType, long? userId, PaginationParameters paginationParameters)
        {
            try
            {
                var instaUri = UriCreator.GetIGTVChannelUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken}
                };
                if (channelType != null)
                    data.Add("id", channelType.Value.GetRealChannelType());
                else
                    data.Add("id", $"user_{userId}");
                if (paginationParameters != null && !string.IsNullOrEmpty(paginationParameters.NextMaxId))
                    data.Add("max_id", paginationParameters.NextMaxId);
                var request = _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTVChannel>(response, json);
                var obj = JsonConvert.DeserializeObject<InstaTVChannelResponse>(json);

                return Result.Success(ConvertersFabric.Instance.GetTVChannelConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaTVChannel), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaTVChannel>(exception);
            }
        }
    }
}
