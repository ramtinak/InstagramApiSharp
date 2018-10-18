using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaWebLinkResponse
    {
        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("link_context")] public InstaWebLinkContextResponse LinkContext { get; set; }
    }
}