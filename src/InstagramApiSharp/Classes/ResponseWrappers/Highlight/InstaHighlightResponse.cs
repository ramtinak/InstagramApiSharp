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
using InstagramApiSharp.Classes.Models;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaHighlightFeedsResponse
    {
        [JsonProperty("show_empty_state")]
        public bool? ShowEmptyState { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("tray")]
        public List<InstaHighlightFeedResponse> Items { get; set; } = new List<InstaHighlightFeedResponse>();
    }
    public class InstaHighlightReelResponse : InstaDefault
    {
        [JsonIgnore]
        public InstaHighlightSingleFeedResponse Reel { get; set; }
    }

    public class InstaHighlightSingleFeedResponse : InstaHighlightFeedResponse
    {
        [JsonProperty("items")]
        public List<InstaStoryItemResponse> Items { get; set; } = new List<InstaStoryItemResponse>();
    }
    public class InstaHighlightFeedResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("latest_reel_media")]
        public int LatestReelMedia { get; set; }
        [JsonProperty("seen")]
        public object Seen { get; set; }
        [JsonProperty("can_reply")]
        public bool CanReply { get; set; }
        [JsonProperty("can_reshare")]
        public object CanReshare { get; set; }
        [JsonProperty("reel_type")]
        public string ReelType { get; set; }
        [JsonProperty("cover_media")]
        public InstaHighlightCoverMediaResponse CoverMedia { get; set; }
        [JsonProperty("user")]
        public InstaUserShortResponse User { get; set; }
        [JsonProperty("ranked_position")]
        public int RankedPosition { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("seen_ranked_position")]
        public int SeenRankedPosition { get; set; }
        [JsonProperty("prefetch_count")]
        public int PrefetchCount { get; set; }
        [JsonProperty("media_count")]
        public int MediaCount { get; set; }
    }

    public class InstaHighlightCoverMediaResponse
    {
        [JsonProperty("cropped_image_version")]
        public ImageResponse CroppedImageVersion { get; set; }
        [JsonProperty("crop_rect")]
        public float[] CropRect { get; set; }
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
        [JsonProperty("full_image_version")]
        public ImageResponse FullImageVersion { get; set; }
    }
}
