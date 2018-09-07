using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaLocationResponse : InstaLocationShortResponse
    { 
        [JsonProperty("facebook_places_id")] public long FacebookPlacesId { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("pk")] public long Pk { get; set; }

        [JsonProperty("short_name")] public string ShortName { get; set; }
    }
}