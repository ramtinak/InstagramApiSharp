/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaPlaceShortResponse
    {
        [JsonProperty("pk")] public long Pk { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("short_name")] public string ShortName { get; set; }

        [JsonProperty("lng")] public double Lng { get; set; }

        [JsonProperty("lat")] public double Lat { get; set; }

        [JsonProperty("external_source")] public string ExternalSource { get; set; }

        [JsonProperty("facebook_places_id")] public long FacebookPlacesId { get; set; }
    }
}
