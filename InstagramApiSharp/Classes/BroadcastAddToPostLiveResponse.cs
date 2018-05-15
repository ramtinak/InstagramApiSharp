using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class BroadcastAddToPostLiveResponse
    {
        [JsonProperty("pk")]
        public string Pk { get; set; }
        [JsonProperty("user")]
        public BroadcastUser User { get; set; }
        [JsonProperty("broadcasts")]
        public List<InstaBroadcast> Broadcasts { get; set; }
        [JsonProperty("last_seen_broadcast_ts")]
        public int LastSeenBroadcastTs { get; set; }
        [JsonProperty("can_reply")]
        public bool CanReply { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
