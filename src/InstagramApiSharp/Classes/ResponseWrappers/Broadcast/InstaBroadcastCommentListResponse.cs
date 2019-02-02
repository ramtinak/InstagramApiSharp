/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using Newtonsoft.Json;
using System.Collections.Generic;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBroadcastCommentListResponse
    {
        [JsonProperty("comment_likes_enabled")]
        public bool? CommentLikesEnabled { get; set; }
        [JsonProperty("comments")]
        public List<InstaBroadcastCommentResponse> Comments { get; set; }
        [JsonProperty("comment_count")]
        public int? CommentCount { get; set; }
        [JsonProperty("caption")]
        public InstaCaptionResponse Caption { get; set; }
        [JsonProperty("caption_is_edited")]
        public bool? CaptionIsEdited { get; set; }
        [JsonProperty("has_more_comments")]
        public bool? HasMoreComments { get; set; }
        [JsonProperty("has_more_headload_comments")]
        public bool? HasMoreHeadloadComments { get; set; }
        [JsonProperty("media_header_display")]
        public string MediaHeaderDisplay { get; set; }
        [JsonProperty("live_seconds_per_comment")]
        public int? LiveSecondsPerComment { get; set; }
        [JsonProperty("is_first_fetch")]
        public string IsFirstFetch { get; set; }
        [JsonProperty("pinned_comment")]
        public InstaBroadcastCommentResponse PinnedComment { get; set; }
        [JsonProperty("system_comments")]
        public object SystemComments { get; set; }
        [JsonProperty("comment_muted")]
        public int? CommentMuted { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
