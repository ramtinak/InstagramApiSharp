using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaChannelResponse
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("channel_id")] public string ChannelId { get; set; }

        [JsonProperty("channel_type")] public string ChannelType { get; set; }

        [JsonProperty("header")] public string Header { get; set; }

        [JsonProperty("context")] public string Context { get; set; }

        [JsonProperty("media")] public InstaMediaItemResponse Media { get; set; }
    }
}