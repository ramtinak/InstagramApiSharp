using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.API.Processors
{
    internal class CollectionProcessor : ICollectionProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
        private readonly InstaApi _instaApi;
        public CollectionProcessor(AndroidDevice deviceInfo, UserSessionData user,
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
        /// <summary>
        ///     Get your collection for given collection id
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollectionItem" />
        /// </returns>
        public async Task<IResult<InstaCollectionItem>> GetCollectionAsync(long collectionId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var collectionUri = UriCreator.GetCollectionUri(collectionId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, collectionUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCollectionItem>(response, json);

                var collectionsListResponse =
                    JsonConvert.DeserializeObject<InstaCollectionItemResponse>(json,
                        new InstaCollectionDataConverter());
                var converter = ConvertersFabric.Instance.GetCollectionConverter(collectionsListResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCollectionItem>(exception.Message);
            }
        }
        /// <summary>
        ///     Get your collections
        /// </summary>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollections" />
        /// </returns>
        public async Task<IResult<InstaCollections>> GetCollectionsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var collectionUri = UriCreator.GetCollectionsUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, collectionUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCollections>(response, json);

                var collectionsResponse = JsonConvert.DeserializeObject<InstaCollectionsResponse>(json);
                var converter = ConvertersFabric.Instance.GetCollectionsConverter(collectionsResponse);

                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCollections>(exception.Message);
            }
        }
        /// <summary>
        ///     Create a new collection
        /// </summary>
        /// <param name="collectionName">The name of the new collection</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollectionItem" />
        /// </returns>
        public async Task<IResult<InstaCollectionItem>> CreateCollectionAsync(string collectionName)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var createCollectionUri = UriCreator.GetCreateCollectionUri();

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"name", collectionName},
                    {"module_name", InstaApiConstants.COLLECTION_CREATE_MODULE}
                };

                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Get, createCollectionUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                var newCollectionResponse = JsonConvert.DeserializeObject<InstaCollectionItemResponse>(json);
                var converter = ConvertersFabric.Instance.GetCollectionConverter(newCollectionResponse);

                return response.StatusCode != HttpStatusCode.OK
                    ? Result.UnExpectedResponse<InstaCollectionItem>(response, json)
                    : Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCollectionItem>(exception.Message);
            }
        }
        /// <summary>
        ///     Delete your collection for given collection id
        /// </summary>
        /// <param name="collectionId">Collection ID to delete</param>
        /// <returns>true if succeed</returns>
        public async Task<IResult<bool>> DeleteCollectionAsync(long collectionId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var createCollectionUri = UriCreator.GetDeleteCollectionUri(collectionId);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"module_name", "collection_editor"}
                };

                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Get, createCollectionUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true);

                var error = JsonConvert.DeserializeObject<BadStatusResponse>(json);
                return Result.Fail(error.Message, false);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, false);
            }
        }
        /// <summary>
        ///     Adds items to collection asynchronous.
        /// </summary>
        /// <param name="collectionId">Collection identifier.</param>
        /// <param name="mediaIds">Media id list.</param>
        public async Task<IResult<InstaCollectionItem>> AddItemsToCollectionAsync(long collectionId,
            params string[] mediaIds)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (mediaIds?.Length < 1)
                    return Result.Fail<InstaCollectionItem>("Provide at least one media id to add to collection");
                var editCollectionUri = UriCreator.GetEditCollectionUri(collectionId);

                var data = new JObject
                {
                    {"module_name", InstaApiConstants.FEED_SAVED_ADD_TO_COLLECTION_MODULE},
                    {"added_media_ids", JsonConvert.SerializeObject(mediaIds)},
                    {"radio_type", "wifi-none"},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUser.Pk},
                    {"_csrftoken", _user.CsrfToken}
                };

                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Get, editCollectionUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCollectionItem>(response, json);
                var newCollectionResponse = JsonConvert.DeserializeObject<InstaCollectionItemResponse>(json);
                var converter = ConvertersFabric.Instance.GetCollectionConverter(newCollectionResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCollectionItem>(exception.Message);
            }
        }
    }
}