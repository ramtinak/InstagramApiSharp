using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaInlineFollowResponse
    {
        [JsonProperty("outgoing_request")] public bool IsOutgoingRequest { get; set; }

        [JsonProperty("following")] public bool IsFollowing { get; set; }

        [JsonProperty("user_info")] public InstaUserShortResponse UserInfo { get; set; }
    }
}