/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBroadcastInfoResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("broadcast_status")]
        public string BroadcastStatus { get; set; }
        [JsonProperty("dash_manifest")]
        public string DashManifest { get; set; }
        [JsonProperty("expire_at")]
        public long? ExpireAt { get; set; }
        [JsonProperty("encoding_tag")]
        public string EncodingTag { get; set; }
        [JsonProperty("internal_only")]
        public bool InternalOnly { get; set; }
        [JsonProperty("number_of_qualities")]
        public int NumberOfQualities { get; set; }
        [JsonProperty("cover_frame_url")]
        public string CoverFrameUrl { get; set; }
        [JsonProperty("broadcast_owner")]
        public InstaUserShortFriendshipFullResponse BroadcastOwner { get; set; }
        [JsonProperty("published_time")]
        public long? PublishedTime { get; set; }
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
        [JsonProperty("broadcast_message")]
        public string BroadcastMessage { get; set; }
        [JsonProperty("organic_tracking_token")]
        public string OrganicTrackingToken { get; set; }
        [JsonProperty("total_unique_viewer_count")]
        public int TotalUniqueViewerCount { get; set; }
    }
}
