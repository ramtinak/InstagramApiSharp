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
    public class InstaBroadcastAddToPostLiveContainerResponse
    {
        [JsonProperty("post_live_items")]
        public List<InstaBroadcastAddToPostLiveResponse> PostLiveItems { get; set; }
    }
    public class InstaBroadcastAddToPostLiveResponse
    {
        [JsonProperty("pk")]
        public string Pk { get; set; }
        [JsonProperty("user")]
        public InstaUserShortFriendshipFullResponse User { get; set; }
        [JsonProperty("broadcasts")]
        public List<InstaBroadcastResponse> Broadcasts { get; set; } = new List<InstaBroadcastResponse>();
        [JsonProperty("last_seen_broadcast_ts")]
        public double? LastSeenBroadcastTs { get; set; }
        [JsonProperty("can_reply")]
        public bool CanReply { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("dash_manifest")]
        public string DashManifest { get; set; }
    }
}
