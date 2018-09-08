using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryItemResponse
    {
        [JsonProperty("has_liked")] public bool HasLiked { get; set; }

        [JsonProperty("code")] public string Code { get; set; }

        [JsonProperty("caption")] public InstaCaptionResponse Caption { get; set; }

        [JsonProperty("can_reshare")] public bool CanReshare { get; set; }

        [JsonProperty("ad_action")] public string AdAction { get; set; }

        [JsonProperty("can_viewer_save")] public bool CanViewerSave { get; set; }

        [JsonProperty("caption_position")] public long CaptionPosition { get; set; }

        [JsonProperty("caption_is_edited")] public bool CaptionIsEdited { get; set; }

        [JsonProperty("client_cache_key")] public string ClientCacheKey { get; set; }

        [JsonProperty("device_timestamp")] public long DeviceTimestamp { get; set; }

        [JsonProperty("comment_likes_enabled")]
        public bool CommentLikesEnabled { get; set; }

        [JsonProperty("comment_count")] public long CommentCount { get; set; }

        [JsonProperty("comment_threading_enabled")]
        public bool CommentThreadingEnabled { get; set; }

        [JsonProperty("filter_type")] public long FilterType { get; set; }

        [JsonProperty("expiring_at")] public long ExpiringAt { get; set; }

        [JsonProperty("has_audio")] public bool? HasAudio { get; set; }

        [JsonProperty("link_text")] public string LinkText { get; set; }

        [JsonProperty("pk")] public long Pk { get; set; }

        [JsonProperty("is_dash_eligible")] public long? IsDashEligible { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("has_more_comments")] public bool HasMoreComments { get; set; }

        [JsonProperty("image_versions2")] public InstaImageCandidatesResponse Images { get; set; }

        [JsonProperty("like_count")] public long LikeCount { get; set; }

        [JsonProperty("is_reel_media")] public bool IsReelMedia { get; set; }

        [JsonProperty("likers")] public List<object> Likers { get; set; }

        [JsonProperty("organic_tracking_token")]
        public string OrganicTrackingToken { get; set; }

        [JsonProperty("media_type")] public long MediaType { get; set; }

        [JsonProperty("max_num_visible_preview_comments")]
        public long MaxNumVisiblePreviewComments { get; set; }

        [JsonProperty("number_of_qualities")] public long? NumberOfQualities { get; set; }

        [JsonProperty("original_width")] public long OriginalWidth { get; set; }

        [JsonProperty("original_height")] public long OriginalHeight { get; set; }

        [JsonProperty("photo_of_you")] public bool PhotoOfYou { get; set; }

        [JsonProperty("story_events")] public List<object> StoryEvents { get; set; }

        [JsonProperty("story_polls")] public List<object> StoryPolls { get; set; }

        [JsonProperty("reel_mentions")] public List<InstaReelMentionResponse> ReelMentions { get; set; }

        [JsonProperty("preview_comments")] public List<object> PreviewComments { get; set; }

        [JsonProperty("story_hashtags")] public List<InstaReelMentionResponse> StoryHashtags { get; set; }

        [JsonProperty("story_feed_media")] public List<object> StoryFeedMedia { get; set; }

        [JsonProperty("story_locations")] public List<InstaLocationResponse> StoryLocations { get; set; }

        [JsonProperty("taken_at")] public long TakenAt { get; set; }

        [JsonProperty("video_dash_manifest")] public string VideoDashManifest { get; set; }

        [JsonProperty("supports_reel_reactions")]
        public bool SupportsReelReactions { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("video_duration")] public double? VideoDuration { get; set; }

        [JsonProperty("video_versions")] public List<InstaVideoResponse> VideoVersions { get; set; }

        [JsonProperty("story_cta")] public List<StoryCTA> StoryCTA { get; set; }
    }

    public class StoryCTA
    {
        [JsonProperty("links")] public Link[] Links { get; set; }
    }

    public class Link
    {
        [JsonProperty("linkType")] public int LinkType { get; set; }
        [JsonProperty("webUri")] public string WebUri { get; set; }
        [JsonProperty("androidClass")] public string AndroidClass { get; set; }
        [JsonProperty("package")] public string Package { get; set; }
        [JsonProperty("deeplinkUri")] public string DeeplinkUri { get; set; }
        [JsonProperty("callToActionTitle")] public string CallToActionTitle { get; set; }
        [JsonProperty("redirectUri")] public object RedirectUri { get; set; }
        [JsonProperty("leadGenFormId")] public string LeadGenFormId { get; set; }
        [JsonProperty("igUserId")] public string IgUserId { get; set; }
    }
}