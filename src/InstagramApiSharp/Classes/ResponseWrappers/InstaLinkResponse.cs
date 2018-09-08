using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaLinkResponse
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("start")] public string Start { get; set; }

        [JsonProperty("end")] public string End { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }
}