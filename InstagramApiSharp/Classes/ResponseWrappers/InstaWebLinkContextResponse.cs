using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaWebLinkContextResponse
    {
        [JsonProperty("link_url")] public string LinkUrl { get; set; }

        [JsonProperty("link_title")] public string LinkTitle { get; set; }

        [JsonProperty("link_summary")] public string LinkSummary { get; set; }

        [JsonProperty("link_image_url")] public string LinkImageUrl { get; set; }
    }
}