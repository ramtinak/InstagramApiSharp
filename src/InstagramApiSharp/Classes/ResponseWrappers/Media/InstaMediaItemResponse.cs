using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaMediaAlbumResponse
    {
        [JsonProperty("media")] public InstaMediaItemResponse Media { get; set; }

        [JsonProperty("client_sidecar_id")] public string ClientSidecarId { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
    public class InstaMediaItemResponse
    {
        [JsonProperty("comment_count")] public long CommentsCount { get; set; }
        [JsonProperty("inline_composer_display_condition")] public string InlineComposerDisplayCondition { get; set; }
        [JsonProperty("inline_composer_imp_trigger_time")] public int InlineComposerImpTriggerTime { get; set; }
        [JsonProperty("main_feed_carousel_starting_media_id")] public string MainFeedCarouselStartingMediaId { get; set; }
        [JsonProperty("main_feed_carousel_has_unseen_cover_media")] public bool? MainFeedCarouselHasUnseenCoverMedia { get; set; }
        [JsonProperty("force_overlay")] public bool? ForceOverlay { get; set; }
        [JsonProperty("hide_nux_text")] public bool? HideNuxText { get; set; }
        [JsonProperty("overlay_text")] public string OverlayText { get; set; }
        [JsonProperty("overlay_title")] public string OverlayTitle { get; set; }
        [JsonProperty("overlay_subtitle")] public string OverlaySubtitle { get; set; }
        [JsonProperty("dominant_color")] public string DominantColor { get; set; }
        [JsonProperty("follower_count")] public int? FollowerCount { get; set; }
        [JsonProperty("post_count")] public int? PostCount { get; set; }
        [JsonProperty("fb_page_url")] public string FBPageUrl { get; set; }
        [JsonProperty("expiring_at")] public int? ExpiringAt { get; set; }
        [JsonProperty("carousel_media_count")] public int? CarouselMediaCount { get; set; }
        [JsonProperty("link")] public string Link { get; set; }
        [JsonProperty("link_text")] public string LinkText { get; set; }
        [JsonProperty("ad_action")] public string AdAction { get; set; }
        [JsonProperty("link_hint_text")] public string LinkHintText { get; set; }
        [JsonProperty("iTunesItem")] public object iTunesItem { get; set; }
        [JsonProperty("ad_link_type")] public int? AdLinkType { get; set; }
        [JsonProperty("ad_header_style")] public int? AdHeaderStyle { get; set; }
        [JsonProperty("dr_ad_type")] public int? DrAdType { get; set; }
        [JsonProperty("preview")] public string Preview { get; set; }
        [JsonProperty("inventory_source")] public string InventorySource { get; set; }
        [JsonProperty("is_seen")] public bool IsSeen { get; set; }
        [JsonProperty("is_eof")] public bool IsEof { get; set; }
        [JsonProperty("collapse_comments")] public bool? CollapseComments { get; set; }



        [JsonProperty("taken_at")] public string TakenAtUnixLike { get; set; }

        [JsonProperty("pk")] public string Pk { get; set; }

        [JsonProperty("id")] public string InstaIdentifier { get; set; }

        [JsonProperty("device_timestamp")] public string DeviceTimeStampUnixLike { get; set; }

        [JsonProperty("media_type")] public InstaMediaType MediaType { get; set; }

        [JsonProperty("code")] public string Code { get; set; }

        [JsonProperty("client_cache_key")] public string ClientCacheKey { get; set; }

        [JsonProperty("filter_type")] public string FilterType { get; set; }

        [JsonProperty("image_versions2")] public InstaImageCandidatesResponse Images { get; set; }

        [JsonProperty("video_versions")] public List<InstaVideoResponse> Videos { get; set; }

        [JsonProperty("original_width")] public int Width { get; set; }

        [JsonProperty("original_height")] public string Height { get; set; }

        [JsonProperty("user")] public InstaUserResponse User { get; set; }

        [JsonProperty("organic_tracking_token")] public string TrackingToken { get; set; }

        [JsonProperty("like_count")] public int LikesCount { get; set; }

        [JsonProperty("next_max_id")] public string NextMaxId { get; set; }

        [JsonProperty("caption")] public InstaCaptionResponse Caption { get; set; }

      //  [JsonProperty("comment_count")] public string CommentsCount { get; set; }

        [JsonProperty("comments_disabled")] public bool IsCommentsDisabled { get; set; }

        [JsonProperty("photo_of_you")] public bool PhotoOfYou { get; set; }

        [JsonProperty("has_liked")] public bool HasLiked { get; set; }

        [JsonProperty("type")] public int Type { get; set; }

        [JsonProperty("view_count")] public double ViewCount { get; set; }

        [JsonProperty("has_audio")] public bool HasAudio { get; set; }

        [JsonProperty("usertags")] public InstaUserTagListResponse UserTagList { get; set; }

        [JsonProperty("likers")] public List<InstaUserShortResponse> Likers { get; set; }

        [JsonProperty("carousel_media")] public InstaCarouselResponse CarouselMedia { get; set; }

        [JsonProperty("location")] public InstaLocationResponse Location { get; set; }

        [JsonProperty("preview_comments")] public List<InstaCommentResponse> PreviewComments { get; set; }



        [JsonProperty("comment_likes_enabled")] public bool CommentLikesEnabled { get; set; }

        [JsonProperty("comment_threading_enabled")] public bool CommentThreadingEnabled { get; set; }

        [JsonProperty("has_more_comments")] public bool HasMoreComments { get; set; }

        [JsonProperty("max_num_visible_preview_comments")] public int MaxNumVisiblePreviewComments { get; set; }

        [JsonProperty("can_view_more_preview_comments")] public bool CanViewMorePreviewComments { get; set; }

        [JsonProperty("can_viewer_reshare")] public bool CanViewerReshare { get; set; }

        [JsonProperty("caption_is_edited")] public bool CaptionIsEdited { get; set; }

        [JsonProperty("can_viewer_save")] public bool CanViewerSave { get; set; }

        [JsonProperty("has_viewer_saved")] public bool HasViewerSaved { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("product_type")] public string ProductType { get; set; }

        [JsonProperty("nearly_complete_copyright_match")] public bool? NearlyCompleteCopyrightMatch { get; set; }

        [JsonProperty("number_of_qualities")] public int? NumberOfQualities { get; set; }

        [JsonProperty("video_duration")] public double? VideoDuration { get; set; }

        [JsonProperty("product_tags")] public InstaProductTagsContainerResponse ProductTags { get; set; }


    }
}