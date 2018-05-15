using System;
using System.Collections.Generic;
using System.Linq;
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

namespace InstagramApiSharp.API.Processors
{
    public class HashtagProcessor : IHashtagProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;

        public HashtagProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }

        public async Task<IResult<InstaHashtagSearch>> Search(string query, IEnumerable<long> excludeList, string rankToken)
        {
            var RequestHeaderFieldsTooLarge = (HttpStatusCode)431;
            var count = 50;
            var tags = new InstaHashtagSearch();

            try
            {
                var userUri = UriCreator.GetSearchTagUri(query, count, excludeList, rankToken);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == RequestHeaderFieldsTooLarge)
                    return Result.Success(tags);
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaHashtagSearch>(response, json);

                var tagsResponse = JsonConvert.DeserializeObject<InstaHashtagSearchResponse>(json);
                tags = ConvertersFabric.Instance.GetHashTagsSearchConverter(tagsResponse).Convert();

                if (tags.Any() && excludeList != null && excludeList.Contains(tags.First().Id))
                    tags.RemoveAt(0);
                if (!tags.Any())
                    tags = new InstaHashtagSearch();

                return Result.Success(tags);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, tags);
            }
        }

        public async Task<IResult<InstaHashtag>> GetHashtagInfo(string tagname)
        {
            try
            {
                var userUri = UriCreator.GetTagInfoUri(tagname);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaHashtag>(response, json);

                var tagInfoResponse = JsonConvert.DeserializeObject<InstaHashtagResponse>(json);
                var tagInfo = ConvertersFabric.Instance.GetHashTagConverter(tagInfoResponse).Convert();

                return Result.Success(tagInfo);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaHashtag>(exception.Message);
            }
        }
    }
}