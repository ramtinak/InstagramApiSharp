using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaReelFeedResponse
    {
        [JsonProperty("has_besties_media")] public long HasBestiesMedia { get; set; }

        [JsonProperty("prefetch_count")] public long PrefetchCount { get; set; }

        [JsonProperty("can_reshare")] public string CanReshare { get; set; }

        [JsonProperty("can_reply")] public bool CanReply { get; set; }

        [JsonProperty("expiring_at")] public long ExpiringAt { get; set; }

        [JsonProperty("items")] public List<InstaStoryItemResponse> Items { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("latest_reel_media")] public long? LatestReelMedia { get; set; }

        [JsonProperty("seen")] public long? Seen { get; set; }

        [JsonProperty("user")] public InstaUserShortFriendshipFullResponse User { get; set; }
    }
}