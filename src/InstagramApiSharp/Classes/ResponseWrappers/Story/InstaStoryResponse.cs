using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryResponse
    {
        [JsonProperty("taken_at")] public long TakenAtUnixLike { get; set; }

        [JsonProperty("can_reply")] public bool CanReply { get; set; }

        [JsonProperty("expiring_at")] public long ExpiringAt { get; set; }

        [JsonProperty("user")] public InstaUserShortFriendshipFullResponse User { get; set; }

        [JsonProperty("owner")] public InstaUserShortResponse Owner { get; set; }

        [JsonProperty("source_token")] public string SourceToken { get; set; }

        [JsonProperty("seen")] public long? Seen { get; set; }

        [JsonProperty("latest_reel_media")] public string LatestReelMedia { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("ranked_position")] public int RankedPosition { get; set; }

        [JsonProperty("muted")] public bool Muted { get; set; }

        [JsonProperty("seen_ranked_position")] public int SeenRankedPosition { get; set; }

        [JsonProperty("items")] public List<InstaStoryItemResponse> Items { get; set; }

        [JsonProperty("prefetch_count")] public int PrefetchCount { get; set; }

        [JsonProperty("social_context")] public string SocialContext { get; set; }



        [JsonProperty("client_cache_key")] public string ClientCacheKey { get; set; }
        [JsonProperty("caption_position")] public double? CaptionPosition { get; set; }
        [JsonProperty("is_reel_media")] public bool IsReelMedia { get; set; }
        [JsonProperty("video_duration")] public double? VideoDuration { get; set; }
        [JsonProperty("caption_is_edited")] public bool CaptionIsEdited { get; set; }
        [JsonProperty("photo_of_you")] public bool PhotoOfYou { get; set; }
        [JsonProperty("can_viewer_save")] public bool CanViewerSave { get; set; }
        [JsonProperty("imported_taken_at")] public long ImportedTakenAt { get; set; }
        [JsonProperty("can_reshare")] public bool CanReshare { get; set; }
        [JsonProperty("supports_reel_reactions")] public bool SupportsReelReactions { get; set; }
        [JsonProperty("has_shared_to_fb")] public bool HasSharedToFb { get; set; }
        [JsonProperty("story_hashtags")] public List<InstaReelMentionResponse> StoryHashtags { get; set; }
        [JsonProperty("story_locations")] public List<InstaStoryLocation> StoryLocation { get; set; }


        [JsonProperty("show_one_tap_fb_share_tooltip")] public bool ShowOneTapFbShareTooltip { get; set; }





    }
}