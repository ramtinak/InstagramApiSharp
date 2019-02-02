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
    public class InstaBroadcastLiveHeartBeatViewerCountResponse
    {
        [JsonProperty("viewer_count")]
        public float ViewerCount { get; set; }
        [JsonProperty("broadcast_status")]
        public string BroadcastStatus { get; set; }
        [JsonProperty("cobroadcaster_ids")]
        public object[] CobroadcasterIds { get; set; }
        [JsonProperty("offset_to_video_start")]
        public int OffsetToVideoStart { get; set; }
        [JsonProperty("total_unique_viewer_count")]
        public int TotalUniqueViewerCount { get; set; }
        [JsonProperty("is_top_live_eligible")]
        public int IsTopLiveEligible { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
