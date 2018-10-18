using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryShareResponse
    {
        [JsonProperty("media")] public InstaMediaItemResponse Media { get; set; }
        [JsonProperty("reel_type")] public string ReelType { get; set; }
        [JsonProperty("is_reel_persisted")] public bool IsReelPersisted { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
        [JsonProperty("is_linked")] public bool IsLinked { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
    }
}
