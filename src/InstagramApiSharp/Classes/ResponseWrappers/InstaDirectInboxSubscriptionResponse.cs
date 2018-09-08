using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDirectInboxSubscriptionResponse
    {
        [JsonProperty("topic")] public string Topic { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("auth")] public string Auth { get; set; }

        [JsonProperty("sequence")] public string Sequence { get; set; }
    }
}