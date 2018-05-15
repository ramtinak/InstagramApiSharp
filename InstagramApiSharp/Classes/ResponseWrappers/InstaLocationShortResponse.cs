using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaLocationShortResponse
    {
        [JsonProperty("lat")] public double Lat { get; set; }

        [JsonProperty("lng")] public double Lng { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("external_id")] public string ExternalId { get; set; }

        [JsonProperty("external_id_source")] public string ExternalIdSource { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}