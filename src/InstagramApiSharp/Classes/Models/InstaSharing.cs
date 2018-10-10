/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Models
{

    public class InstaSharing
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("status_code")]
        public string StatusCode { get; set; }
        [JsonProperty("payload")]
        public InstaSharingPayload[] Payload { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
    }

    public class InstaSharingPayload
    {
        [JsonProperty("thread_id")]
        public string ThreadId { get; set; }
        [JsonProperty("item_id")]
        public string ItemId { get; set; }
        [JsonProperty("participant_ids")]
        public long[] ParticipantIds { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("client_context")]
        public object ClientContext { get; set; }
        [JsonProperty("canonical")]
        public bool Cnonical { get; set; }
    }

}
