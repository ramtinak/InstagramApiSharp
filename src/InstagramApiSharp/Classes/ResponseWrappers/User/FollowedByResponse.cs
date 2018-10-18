using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class FollowedByResponse
    {
        [JsonProperty("count")] public int Count { get; set; }
    }
}