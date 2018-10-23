using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers.BaseResponse
{
    public class BaseLoadableResponse : BaseStatusResponse
    {
        [JsonProperty("more_available")] public bool MoreAvailable { get; set; }

        [JsonProperty("num_results")] public int ResultsCount { get; set; }

        [JsonProperty("total_count")] public int TotalCount { get; set; }

        [JsonProperty("auto_load_more_enabled")] public bool AutoLoadMoreEnabled { get; set; }

        [JsonProperty("next_max_id")] public string NextMaxId { get; set; }

        [JsonProperty("rank_token")] public string RankToken { get; set; } = "unknown";
    }
}