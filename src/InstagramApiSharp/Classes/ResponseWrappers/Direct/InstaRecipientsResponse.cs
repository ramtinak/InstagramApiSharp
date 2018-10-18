using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaRecipientsResponse : BaseStatusResponse
    {
        [JsonProperty("expires")] public long Expires { get; set; }

        [JsonProperty("filtered")] public bool Filtered { get; set; }

        [JsonProperty("rank_token")] public string RankToken { get; set; }

        [JsonProperty("request_id")] public string RequestId { get; set; }
    }
}