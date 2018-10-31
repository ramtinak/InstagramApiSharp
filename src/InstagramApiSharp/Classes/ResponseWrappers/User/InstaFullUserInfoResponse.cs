/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaFullUserInfoResponse
    {
        [JsonProperty("user_detail")]
        public InstaUserInfoContainerResponse UserDetail { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("feed")]
        public InstaFullUserInfoUserFeedResponse Feed { get; set; }

        [JsonProperty("reel_feed")]
        public InstaFullUserInfoUserStoryReelResponse ReelFeed { get; set; }

        [JsonProperty("user_story")]
        public InstaFullUserInfoUserStoryResponse UserStory { get; set; }
    }


    public class InstaFullUserInfoUserStoryResponse
    {
        [JsonProperty("reel")]
        public InstaFullUserInfoUserStoryReelResponse Reel { get; set; }
        [JsonProperty("broadcast")]
        public InstaBroadcastSuggestedResponse Broadcast { get; set; }
    }

    public class InstaFullUserInfoUserFeedResponse
    {
        [JsonProperty("num_results")]
        public int NumResults { get; set; }
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }
        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }
        [JsonProperty("next_min_id")]
        public string NextMinId { get; set; }
        [JsonProperty("auto_load_more_enabled")]
        public bool AutoLoadMoreEnabled { get; set; }
        [JsonProperty("items")]
        public List<InstaMediaItemResponse> Items { get; set; } = new List<InstaMediaItemResponse>();
    }

    public class InstaFullUserInfoUserStoryReelResponse
    {
        [JsonProperty("user")]
        public InstaUserShortResponse User { get; set; }
        [JsonProperty("items")]
        public List<InstaStoryItemResponse> Items { get; set; } = new List<InstaStoryItemResponse>();
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("latest_reel_media")]
        public int? LatestReelMedia { get; set; }
        [JsonProperty("expiring_at")]
        public long ExpiringAt { get; set; }
        [JsonProperty("seen")]
        public long? Seen { get; set; }
        [JsonProperty("can_reply")]
        public bool CanReply { get; set; }
        [JsonProperty("can_reshare")]
        public bool CanReshare { get; set; }
        [JsonProperty("reel_type")]
        public string ReelType { get; set; }
        [JsonProperty("prefetch_count")]
        public int PrefetchCount { get; set; }
        [JsonProperty("has_besties_media")]
        public bool HasBestiesMedia { get; set; }
    }

}
