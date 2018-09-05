using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryResponse
    {
        [JsonProperty("taken_at")] public long TakenAtUnixLike { get; set; }

        [JsonProperty("can_reply")] public bool CanReply { get; set; }

        [JsonProperty("expiring_at")] public long ExpiringAt { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("owner")] public InstaUserShortResponse Owner { get; set; }

        [JsonProperty("source_token")] public string SourceToken { get; set; }

        [JsonProperty("seen")] public long? Seen { get; set; }

        [JsonProperty("latest_reel_media")] public string LatestReelMedia { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("ranked_position")] public int RankedPosition { get; set; }

        [JsonProperty("muted")] public bool Muted { get; set; }

        [JsonProperty("seen_ranked_position")] public int SeenRankedPosition { get; set; }

        [JsonProperty("items")] public List<InstaMediaItemResponse> Items { get; set; }

        [JsonProperty("prefetch_count")] public int PrefetchCount { get; set; }

        [JsonProperty("social_context")] public string SocialContext { get; set; }
    }
}