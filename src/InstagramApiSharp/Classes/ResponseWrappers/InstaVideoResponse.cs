using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaVideoResponse
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("height")] public string Height { get; set; }

        [JsonProperty("type")] public int Type { get; set; }

        [JsonProperty("width")] public string Width { get; set; }
    }
}