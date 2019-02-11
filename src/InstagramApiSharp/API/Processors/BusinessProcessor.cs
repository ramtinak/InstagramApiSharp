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
using System.Linq;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Business api functions
    ///     <para>Note: All functions of this interface only works with business accounts!</para>
    /// </summary>
    internal class BusinessProcessor : IBusinessProcessor
    {
        #region Properties and constructor
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpHelper _httpHelper;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly InstaApi _instaApi;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;
        private readonly UserAuthValidate _userAuthValidate;
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
        ///     Add button to your business account
        /// </summary>
        /// <param name="businessPartner">Desire partner button (Use <see cref="IBusinessProcessor.GetBusinessPartnersButtonsAsync"/> to get business buttons(instagram partner) list!)</param>
        /// <param name="uri">Uri (related to Business partner button)</param>
        public async Task<IResult<InstaBusinessUser>> AddOrChangeBusinessButtonAsync(InstaBusinessPartner businessPartner, Uri uri)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (businessPartner == null)
                    return Result.Fail<InstaBusinessUser>("Business partner cannot be null");
                if (uri == null)
                    return Result.Fail<InstaBusinessUser>("Uri related to business partner cannot be null");

                var validateUri = await ValidateUrlAsync(businessPartner, uri);
                if (!validateUri.Succeeded)
                    return Result.Fail<InstaBusinessUser>(validateUri.Info.Message);

                var instaUri = UriCreator.GetUpdateBusinessInfoUri();

                var data = new JObject
                {
                    {"ix_url", uri.ToString()},
                    {"ix_app_id", businessPartner.AppId},
                    {"is_call_to_action_enabled","1"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };

                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessUser>(exception);
            }
        }

        /// <summary>
        ///     Add users to approval branded whitelist
        /// </summary>
        /// <param name="userIdsToAdd">User ids (pk) to add</param>
        public async Task<IResult<InstaBrandedContent>> AddUserToBrandedWhiteListAsync(params long[] userIdsToAdd)
        {
            if (userIdsToAdd == null || userIdsToAdd != null && !userIdsToAdd.Any())
                return Result.Fail<InstaBrandedContent>("At least one user id is require.");

            return await UpdateBrandedContent(null, userIdsToAdd);
        }

        /// <summary>
        ///     Disable branded content approval
        /// </summary>
        public async Task<IResult<InstaBrandedContent>> DisbaleBrandedContentApprovalAsync()
        {
            return await UpdateBrandedContent(0);
        }

        /// <summary>
        ///     Enable branded content approval
        /// </summary>
        public async Task<IResult<InstaBrandedContent>> EnableBrandedContentApprovalAsync()
        {
            return await UpdateBrandedContent(1);
        }

        /// <summary>
        ///     Change business category
        ///     <para>Note: Get it from <see cref="IBusinessProcessor.GetSubCategoriesAsync(string)"/></para>
        /// </summary>
        /// <param name="subCategoryId">Sub category id (Get it from <see cref="IBusinessProcessor.GetSubCategoriesAsync(string)"/>)
        /// </param>
        public async Task<IResult<InstaBusinessUser>> ChangeBusinessCategoryAsync(string subCategoryId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (string.IsNullOrEmpty(subCategoryId))
                    return Result.Fail<InstaBusinessUser>("Sub category id cannot be null or empty");

                var instaUri = UriCreator.GetSetBusinessCategoryUri();
                var data = new JObject
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"category_id", subCategoryId},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessUser>(exception);
            }
        }

        /// <summary>
        ///     Get account details for an business account ( like it's joined date )
        ///     <param name="userId">User id (pk)</param>
        /// </summary>
        public async Task<IResult<InstaAccountDetails>> GetAccountDetailsAsync(long userId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetAccountDetailsUri(userId);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaAccountDetails>(response, "Can't find account details for this user pk", json);

                var obj = JsonConvert.DeserializeObject<InstaAccountDetailsResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetAccountDetailsConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaAccountDetails), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaAccountDetails>(exception);
            }
        }

        /// <summary>
        ///     Get logged in business account information
        /// </summary>
        public async Task<IResult<InstaUserInfo>> GetBusinessAccountInformationAsync()
        {
            return await _instaApi.UserProcessor.GetUserInfoByIdAsync(_user.LoggedInUser.Pk);
        }

        /// <summary>
        ///     Get business get buttons (partners)
        /// </summary>
        public async Task<IResult<InstaBusinessPartnersList>> GetBusinessPartnersButtonsAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var data = new JObject();
                var dataStr = _httpHelper.GetSignature(data);
                var instaUri = UriCreator.GetBusinessInstantExperienceUri(dataStr);

                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBusinessPartnersList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBusinessPartnerContainer>(json);
                var partners = new InstaBusinessPartnersList();

                foreach (var p in obj.Partners)
                    partners.Add(p);

                return Result.Success(partners);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBusinessPartnersList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessPartnersList>(exception);
            }
        }

        /// <summary>
        ///     Get all categories 
        /// </summary>
        public async Task<IResult<InstaBusinessCategoryList>> GetCategoriesAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetBusinessGraphQLUri();

                var queryParams = new JObject
                {
                    {"0", "-1"}
                };
                var data = new Dictionary<string, string>
                {
                    {"query_id", "425892567746558"},
                    {"locale", InstaApiConstants.ACCEPT_LANGUAGE.Replace("-", "_")},
                    {"vc_policy", "ads_viewer_context_policy"},
                    {"signed_body", $"{_httpHelper._apiVersion.SignatureKey}."},
                    {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION},
                    {"strip_nulls", "true"},
                    {"strip_defaults", "true"},
                    {"query_params", queryParams.ToString(Formatting.None)},
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBusinessCategoryList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBusinessCategoryList>(json, new InstaBusinessCategoryDataConverter());
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBusinessCategoryList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessCategoryList>(exception);
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
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaFullMediaInsights), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFullMediaInsights>(exception);
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
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMediaInsights), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaMediaInsights>(exception);
            }
        }

        /// <summary>
        ///     Get promotable media feeds
        /// </summary>
        public async Task<IResult<InstaMediaList>> GetPromotableMediaFeedsAsync()
        {
            var mediaList = new InstaMediaList();
            try
            {
                var instaUri = UriCreator.GetPromotableMediaFeedsUri();
                var request = _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaList>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());

                mediaList = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse).Convert();
                mediaList.PageSize = mediaResponse.ResultsCount;
                return Result.Success(mediaList);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaMediaList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, mediaList);
            }
        }

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
                    {"timezone", InstaApiConstants.TIMEZONE},
                    {"activityTab", true},
                    {"audienceTab", true},
                    {"contentTab", true}
                };
                var data = new Dictionary<string, string>
                {
                    {"access_token", "undefined"},
                    {"fb_api_caller_class", "RelayModern"},
                    {"variables", variables.ToString(Formatting.None)},
                    {"doc_id", "1926322010754880"}
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
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaStatistics), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaStatistics>(exception);
            }
        }
        
        #region Direct threads
        /// <summary>
        ///     Star direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> StarDirectThreadAsync(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetStarThreadUri(threadId);

                var data = new Dictionary<string, string>
                {
                    {"thread_label", "1"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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

        /// <summary>
        ///     Unstar direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        public async Task<IResult<bool>> UnStarDirectThreadAsync(string threadId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUnStarThreadUri(threadId);

                var data = new Dictionary<string, string>
                {
                    {"_csrftoken", _user.CsrfToken},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
        #endregion Direct threads

        /// <summary>
        ///     Get sub categories of an category
        /// </summary>
        /// <param name="categoryId">Category id (Use <see cref="IBusinessProcessor.GetCategoriesAsync"/> to get category id)</param>
        public async Task<IResult<InstaBusinessCategoryList>> GetSubCategoriesAsync(string categoryId)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (string.IsNullOrEmpty(categoryId))
                    return Result.Fail<InstaBusinessCategoryList>("Category id cannot be null or empty");

                var instaUri = UriCreator.GetBusinessGraphQLUri();

                var queryParams = new JObject
                {
                    {"0", categoryId}
                };
                var data = new Dictionary<string, string>
                {
                    {"query_id", "425892567746558"},
                    {"locale", InstaApiConstants.ACCEPT_LANGUAGE.Replace("-", "_")},
                    {"vc_policy", "ads_viewer_context_policy"},
                    {"signed_body", $"{_httpHelper._apiVersion.SignatureKey}."},
                    {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION},
                    {"strip_nulls", "true"},
                    {"strip_defaults", "true"},
                    {"query_params", queryParams.ToString(Formatting.None)},
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBusinessCategoryList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBusinessCategoryList>(json, new InstaBusinessCategoryDataConverter());
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBusinessCategoryList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessCategoryList>(exception);
            }
        }

        /// <summary>
        ///     Get suggested categories 
        /// </summary>
        public async Task<IResult<InstaBusinessSuggestedCategoryList>> GetSuggestedCategoriesAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetBusinessGraphQLUri();

                var zero = new JObject
                {
                    {"page_name", _user.UserName.ToLower()},
                    {"num_result", "5"}
                };
                var queryParams = new JObject
                {
                    {"0", zero}
                };
                var data = new Dictionary<string, string>
                {
                    {"query_id", "706774002864790"},
                    {"locale", InstaApiConstants.ACCEPT_LANGUAGE.Replace("-", "_")},
                    {"vc_policy", "ads_viewer_context_policy"},
                    {"signed_body", $"{_httpHelper._apiVersion.SignatureKey}."},
                    {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION},
                    {"strip_nulls", "true"},
                    {"strip_defaults", "true"},
                    {"query_params", queryParams.ToString(Formatting.None)},
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBusinessSuggestedCategoryList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBusinessSuggestedCategoryList>(json, new InstaBusinessSuggestedCategoryDataConverter());
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBusinessSuggestedCategoryList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessSuggestedCategoryList>(exception);
            }
        }

        /// <summary>
        ///     Get branded content approval settings
        ///     <para>Note: Only approved partners can tag you in branded content when you require approvals.</para>
        /// </summary>
        public async Task<IResult<InstaBrandedContent>> GetBrandedContentApprovalAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetBusinessBrandedSettingsUri();

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);

                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBrandedContent>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBrandedContentResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetBrandedContentConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBrandedContent), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBrandedContent>(exception);
            }
        }

        /// <summary>
        ///     Remove button from your business account
        /// </summary>
        public async Task<IResult<InstaBusinessUser>> RemoveBusinessButtonAsync()
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetUpdateBusinessInfoUri();

                var data = new JObject
                {
                    {"is_call_to_action_enabled","0"},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };

                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessUser>(exception);
            }
        }

        /// <summary>
        ///     Remove business location
        /// </summary>
        public async Task<IResult<InstaBusinessUser>> RemoveBusinessLocationAsync()
        {
            return await UpdateBusinessInfoAsync(null, null, null, null, null);
        }

        /// <summary>
        ///     Remove users from approval branded whitelist
        /// </summary>
        /// <param name="userIdsToRemove">User ids (pk) to remove</param>
        public async Task<IResult<InstaBrandedContent>> RemoveUserFromBrandedWhiteListAsync(params long[] userIdsToRemove)
        {
            if (userIdsToRemove == null || userIdsToRemove != null && !userIdsToRemove.Any())
                return Result.Fail<InstaBrandedContent>("At least one user id is require.");

            return await UpdateBrandedContent(null, null, userIdsToRemove);
        }

        /// <summary>
        ///     Search city location for business account
        /// </summary>
        /// <param name="cityOrTown">City/town name</param>
        public async Task<IResult<InstaBusinessCityLocationList>> SearchCityLocationAsync(string cityOrTown)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (string.IsNullOrEmpty(cityOrTown))
                    return Result.Fail<InstaBusinessCityLocationList>("CityOrTown cannot be null or empty");

                var instaUri = UriCreator.GetBusinessGraphQLUri();

                var queryParams = new JObject
                {
                    {"0", cityOrTown}
                };
                var data = new Dictionary<string, string>
                {
                    {"query_id", "1860980127555904"},
                    {"locale", InstaApiConstants.ACCEPT_LANGUAGE.Replace("-", "_")},
                    {"vc_policy", "ads_viewer_context_policy"},
                    {"signed_body", $"{_httpHelper._apiVersion.SignatureKey}."},
                    {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION},
                    {"strip_nulls", "true"},
                    {"strip_defaults", "true"},
                    {"query_params", queryParams.ToString(Formatting.None)},
                };
                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBusinessCityLocationList>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBusinessCityLocationList>(json, new InstaBusinessCityLocationDataConverter());
                return Result.Success(obj);
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBusinessCityLocationList), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessCityLocationList>(exception);
            }
        }
        
        /// <summary>
        ///     Search branded users for adding to your branded whitelist
        /// </summary>
        /// <param name="query">Query(name, username or...) to search</param>
        /// <param name="count">Count</param>
        public async Task<IResult<InstaDiscoverSearchResult>> SearchBrandedUsersAsync(string query, int count = 85)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if (count < 10)
                    count = 10;

                var instaUri = UriCreator.GetBusinessBrandedSearchUserUri(query, count);

                var request =
                    _httpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);

                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaDiscoverSearchResult>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaDiscoverSearchResultResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetDiscoverSearchResultConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaDiscoverSearchResult), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaDiscoverSearchResult>(exception);
            }
        }

        /// <summary>
        ///     Update business information
        /// </summary>
        /// <param name="phoneNumberWithCountryCode">Phone number with country code [set null if you don't want to change it]</param>
        /// <param name="cityLocation">City Location (get it from <see cref="IBusinessProcessor.SearchCityLocationAsync(string)"/>)</param>
        /// <param name="streetAddress">Street address</param>
        /// <param name="zipCode">Zip code</param>
        /// <param name="businessContactType">Phone contact type (<see cref="InstaUserInfo.BusinessContactMethod"/>) [set null if you don't want to change it]</param>
        public async Task<IResult<InstaBusinessUser>> UpdateBusinessInfoAsync(string phoneNumberWithCountryCode,
            InstaBusinessCityLocation cityLocation,
            string streetAddress, string zipCode,
            InstaBusinessContactType? businessContactType)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var info = await GetBusinessAccountInformationAsync();
                if (!info.Succeeded)
                    return Result.Fail<InstaBusinessUser>("Cannot get current business information");

                var user = info.Value;
                if (!user.IsBusiness)
                    return Result.Fail<InstaBusinessUser>($"'{user.Username}' is not a business account");

                var instaUri = UriCreator.GetUpdateBusinessInfoUri();


                if (phoneNumberWithCountryCode == null)
                    phoneNumberWithCountryCode = $"{user.PublicPhoneCountryCode}{user.PublicPhoneNumber}";

                if (businessContactType == null)
                    businessContactType = user.BusinessContactMethod;

                var publicPhoneContact = new JObject
                {
                    {"public_phone_number", phoneNumberWithCountryCode},
                    {"business_contact_method", businessContactType.ToString().ToUpper()},
                };


                var cityId = "0";
                if (cityLocation != null)
                    cityId = cityLocation.Id;

                var businessAddress = new JObject
                {
                    {"address_street", streetAddress ?? string.Empty},
                    {"city_id", cityId},
                    {"zip", zipCode ?? string.Empty},
                };


                var data = new JObject
                {
                    {"page_id", user.PageId.Value.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"public_phone_contact", publicPhoneContact.ToString(Formatting.None)},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"public_email", user.PublicEmail ?? string.Empty},
                    {"business_address", businessAddress.ToString(Formatting.None)},
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
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
                _logger?.LogException(exception);
                return Result.Fail<InstaBusinessUser>(exception);
            }
        }

        /// <summary>
        ///     Validate an uri for an button(instagram partner)
        ///     <para>Note: Use <see cref="IBusinessProcessor.GetBusinessPartnersButtonsAsync"/> to get business buttons(instagram partner) list!</para>
        /// </summary>
        /// <param name="desirePartner">Desire partner (Use <see cref="IBusinessProcessor.GetBusinessPartnersButtonsAsync"/> to get business buttons(instagram partner) list!)</param>
        /// <param name="uri">Uri to check (Must be related to desire partner!)</param>
        public async Task<IResult<bool>> ValidateUrlAsync(InstaBusinessPartner desirePartner, Uri uri)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                if(desirePartner?.AppId == null)
                    return Result.Fail<bool>("Desire partner cannot be null");
                if (uri == null)
                    return Result.Fail<bool>("Uri cannot be null");

                var instaUri = UriCreator.GetBusinessValidateUrlUri();

                var data = new JObject
                {
                    {"app_id", desirePartner.AppId},
                    {"_csrftoken", _user.CsrfToken},
                    {"url", uri.ToString()},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<InstaBusinessValidateUrl>(json);
                return obj.IsValid ? Result.Success(true) : Result.Fail<bool>(obj.ErrorMessage);
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


        private async Task<IResult<InstaBrandedContent>> UpdateBrandedContent(int? approval = null,
            long[] usersToAdd = null,
            long[] usersToRemove = null)
        {
            UserAuthValidator.Validate(_userAuthValidate);
            try
            {
                var instaUri = UriCreator.GetBusinessBrandedUpdateSettingsUri();
                var data = new JObject
                {
                    {"require_approval", (approval ?? 1).ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"_uid", _user.LoggedInUser.Pk.ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()}
                };
                var addArray = new JArray();
                var removeArray = new JArray();

                if (usersToAdd != null && usersToAdd.Any())
                    foreach (var item in usersToAdd)
                        addArray.Add($"{item}");

                if (usersToRemove != null && usersToRemove.Any())
                    foreach (var item in usersToRemove)
                        removeArray.Add($"{item}");

                data.Add("added_user_ids", addArray);
                data.Add("removed_user_ids", removeArray);

                var request =
                    _httpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);

                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaBrandedContent>(response, json);

                var obj = JsonConvert.DeserializeObject<InstaBrandedContentResponse>(json);
                return Result.Success(ConvertersFabric.Instance.GetBrandedContentConverter(obj).Convert());
            }
            catch (HttpRequestException httpException)
            {
                _logger?.LogException(httpException);
                return Result.Fail(httpException, default(InstaBrandedContent), ResponseType.NetworkProblem);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaBrandedContent>(exception);
            }
        }

    }
}
