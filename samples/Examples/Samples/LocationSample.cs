using System;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class LocationSample : IDemoSample
    {
        private readonly IInstaApi InstaApi;

        public LocationSample(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        public async Task DoShow()
        {
            // search for related locations near location with latitude = 55.753923, logitude = 37.620940
            // additionaly you can specify search query or just empty string
            var result = await InstaApi.LocationProcessor.SearchLocationAsync(55.753923, 37.620940, "square");
            Console.WriteLine($"Loaded {result.Value.Count} locations");
            var firstLocation = result.Value?.FirstOrDefault();
            if(firstLocation == null)
                return;
            Console.WriteLine($"Loading feed for location: name={firstLocation.Name}; id={firstLocation.ExternalId}.");

            var locationFeed =
                await InstaApi.LocationProcessor.GetLocationFeedAsync(long.Parse(firstLocation.ExternalId), PaginationParameters.MaxPagesToLoad(5));

            Console.WriteLine(locationFeed.Succeeded
                ? $"Loaded {locationFeed.Value.Medias?.Count} medias for location, total location medias: {locationFeed.Value.MediaCount}"
                : $"Unable to load location '{firstLocation.Name}' feed");
        }
    }
}