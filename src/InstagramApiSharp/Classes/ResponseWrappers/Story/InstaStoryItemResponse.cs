using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryItemResponse
    {
        [JsonProperty("show_one_tap_fb_share_tooltip")] public bool ShowOneTapTooltip { get; set; }

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

        [JsonProperty("comment_likes_enabled")] public bool CommentLikesEnabled { get; set; }

        [JsonProperty("comment_count")] public long CommentCount { get; set; }

        [JsonProperty("comment_threading_enabled")] public bool CommentThreadingEnabled { get; set; }

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

        [JsonProperty("likers")] public List<InstaUserShortResponse> Likers { get; set; }

        [JsonProperty("organic_tracking_token")] public string OrganicTrackingToken { get; set; }

        [JsonProperty("media_type")] public long MediaType { get; set; }

        [JsonProperty("max_num_visible_preview_comments")] public long MaxNumVisiblePreviewComments { get; set; }

        [JsonProperty("number_of_qualities")] public long? NumberOfQualities { get; set; }

        [JsonProperty("original_width")] public long OriginalWidth { get; set; }

        [JsonProperty("original_height")] public long OriginalHeight { get; set; }

        [JsonProperty("photo_of_you")] public bool PhotoOfYou { get; set; }

        [JsonProperty("story_sticker_ids")] public string StoryStickerIds { get; set; }

        [JsonProperty("timezone_offset")] public double? TimezoneOffset { get; set; }

        [JsonProperty("story_is_saved_to_archive")] public bool? StoryIsSavedToArchive { get; set; }

        [JsonProperty("viewer_count")] public double? ViewerCount { get; set; }

        [JsonProperty("total_viewer_count")] public double? TotalViewerCount { get; set; }

        [JsonProperty("viewer_cursor")] public string ViewerCursor { get; set; }

        [JsonProperty("has_shared_to_fb")] public double? HasSharedToFb { get; set; }

        [JsonProperty("story_events")] public List<object> StoryEvents { get; set; }

        [JsonProperty("story_polls")] public List<InstaStoryPollItemResponse> StoryPolls { get; set; }

        [JsonProperty("story_sliders")] public List<InstaStorySliderItemResponse> StorySliders { get; set; }

        [JsonProperty("story_questions")] public List<InstaStoryQuestionItemResponse> StoryQuestions { get; set; }

        [JsonProperty("story_question_responder_infos")] public List<InstaStoryQuestionInfoResponse> StoryQuestionsResponderInfos { get; set; }

        [JsonProperty("reel_mentions")] public List<InstaReelMentionResponse> ReelMentions { get; set; }

        [JsonProperty("preview_comments")] public List<InstaCommentResponse> PreviewComments { get; set; }

        [JsonProperty("story_hashtags")] public List<InstaReelMentionResponse> StoryHashtags { get; set; }

        [JsonProperty("story_feed_media")] public List<InstaStoryFeedMediaResponse> StoryFeedMedia { get; set; }

        [JsonProperty("story_locations")] public List<InstaStoryLocationResponse> StoryLocations { get; set; }

        [JsonProperty("taken_at")] public long TakenAt { get; set; }

        [JsonProperty("imported_taken_at")] public long? ImportedTakenAt { get; set; }

        [JsonProperty("video_dash_manifest")] public string VideoDashManifest { get; set; }

        [JsonProperty("supports_reel_reactions")] public bool SupportsReelReactions { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("video_duration")] public double? VideoDuration { get; set; }

        [JsonProperty("video_versions")] public List<InstaVideoResponse> VideoVersions { get; set; }

        [JsonProperty("story_cta")] public List<InstaStoryCTAContainerResponse> StoryCTA { get; set; }

        [JsonProperty("story_poll_voter_infos")] public List<InstaStoryPollVoterInfoItemResponse> StoryPollVoters { get; set; }

        [JsonProperty("story_slider_voter_infos")] public List<InstaStorySliderVoterInfoItemResponse> StorySliderVoters { get; set; }

        [JsonProperty("viewers")] public List<InstaUserShortResponse> Viewers { get; set; }

        [JsonProperty("story_countdowns")] public List<InstaStoryCountdownItemResponse> Countdowns { get; set; }
    }
}