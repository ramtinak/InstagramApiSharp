using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaLocationResponse : InstaLocationShortResponse
    {
        [JsonProperty("x")] public float X { get; set; }
        [JsonProperty("y")] public float Y { get; set; }
        [JsonProperty("z")] public int Z { get; set; }
        [JsonProperty("width")] public float Width { get; set; }
        [JsonProperty("height")] public float Height { get; set; }
        [JsonProperty("rotation")] public float Rotation { get; set; }
        [JsonProperty("facebook_places_id")] public long FacebookPlacesId { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("pk")] public long Pk { get; set; }

        [JsonProperty("short_name")] public string ShortName { get; set; }
    }
}