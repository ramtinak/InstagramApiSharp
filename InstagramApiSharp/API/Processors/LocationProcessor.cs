using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.API.UriCreators;
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
    internal class LocationProcessor : ILocationProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IUriCreatorNextId _getFeedUriCreator = new GetLocationFeedUriCreator();
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly IUriCreator _searchLocationUriCreator = new SearchLocationUriCreator();
        private readonly UserSessionData _user;

        public LocationProcessor(AndroidDevice deviceInfo, UserSessionData user,
            IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }

        public async Task<IResult<InstaLocationShortList>> Search(double latitude, double longitude, string query)
        {
            try
            {
                var uri = _searchLocationUriCreator.GetUri();

                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk.ToString()},
                    {"_csrftoken", _user.CsrfToken},
                    {"latitude", latitude.ToString(CultureInfo.InvariantCulture)},
                    {"longitude", longitude.ToString(CultureInfo.InvariantCulture)},
                    {"rank_token", _user.RankToken}
                };

                if (!string.IsNullOrEmpty(query))
                    fields.Add("search_query", query);
                else
                    fields.Add("timestamp", DateTimeHelper.GetUnixTimestampSeconds().ToString());
                if (!Uri.TryCreate(uri, fields.AsQueryString(), out var newuri))
                    return Result.Fail<InstaLocationShortList>("Unable to create uri for location search");

                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, newuri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaLocationShortList>(response, json);
                var locations = JsonConvert.DeserializeObject<InstaLocationSearchResponse>(json);
                var converter = ConvertersFabric.Instance.GetLocationsSearchConverter(locations);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaLocationShortList>(exception);
            }
        }

        public async Task<IResult<InstaLocationFeed>> GetFeed(long locationId,
            PaginationParameters paginationParameters)
        {
            try
            {
                var uri = _getFeedUriCreator.GetUri(locationId, paginationParameters.NextId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaLocationFeed>(response, json);

                var feedResponse = JsonConvert.DeserializeObject<InstaLocationFeedResponse>(json);
                var feed = ConvertersFabric.Instance.GetLocationFeedConverter(feedResponse).Convert();
                paginationParameters.PagesLoaded++;
                paginationParameters.NextId = feed.NextId;

                while (feedResponse.MoreAvailable
                       && !string.IsNullOrEmpty(paginationParameters.NextId)
                       && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFeed = await GetFeed(locationId, paginationParameters);
                    if (!nextFeed.Succeeded)
                        return nextFeed;
                    paginationParameters.StartFromId(nextFeed.Value.NextId);
                    paginationParameters.PagesLoaded++;
                    feed.NextId = nextFeed.Value.NextId;
                    feed.Medias.AddRange(nextFeed.Value.Medias);
                    feed.RankedMedias.AddRange(nextFeed.Value.RankedMedias);
                }

                return Result.Success(feed);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaLocationFeed>(exception);
            }
        }
    }
}