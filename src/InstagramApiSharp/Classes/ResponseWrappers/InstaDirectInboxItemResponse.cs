using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDirectInboxItemResponse : BaseStatusResponse
    { 
        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("like")] public string Like { get; set; }

        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("timestamp")] public string TimeStamp { get; set; }

        [JsonProperty("item_id")] public string ItemId { get; set; }

        [JsonProperty("item_type")] public string ItemType { get; set; }

        [JsonProperty("media_share")] public InstaMediaItemResponse MediaShare { get; set; }

        [JsonProperty("media")] public InstaInboxMediaResponse Media { get; set; }

        [JsonProperty("link")] public InstaWebLinkResponse Link { get; set; }

        [JsonProperty("client_context")] public string ClientContext { get; set; }

        [JsonProperty("story_share")] public InstaStoryShareResponse StoryShare { get; set; }

        [JsonProperty("raven_media")] public InstaMediaItemResponse/*InstaRavenMediaResponse*/ RavenMedia { get; set; }
        // raven media properties
        [JsonProperty("view_mode")] public string RavenViewMode { get; set; }

        [JsonProperty("seen_user_ids")] public List<long> RavenSeenUserIds { get; set; }

        [JsonProperty("reply_chain_count")] public int RavenReplayChainCount { get; set; }

        [JsonProperty("seen_count")] public int RavenSeenCount { get; set; }

        [JsonProperty("expiring_media_action_summary")] public InstaRavenMediaActionSummaryResponse RavenExpiringMediaActionSummary { get; set; }
        // end
        [JsonProperty("action_log")] public InstaActionLogResponse ActionLogMedia { get; set; }

        [JsonProperty("profile")] public InstaUserShortResponse ProfileMedia { get; set; }

        [JsonProperty("preview_medias")] public List<InstaMediaItemResponse> ProfileMediasPreview { get; set; }

        [JsonProperty("placeholder")] public InstaPlaceholderResponse Placeholder { get; set; }

        [JsonProperty("location")] public InstaLocationResponse LocationMedia { get; set; }

        [JsonProperty("felix_share")] public InstaFelixShareResponse FelixShareMedia { get; set; }

    }
}