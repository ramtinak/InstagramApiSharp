/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class InstaBroadcastViewerListResponse
    {
        [JsonProperty("users")]
        public List<InstaBroadcastUser> Users { get; set; }
        [JsonProperty("next_max_id")]
        public object NextMaxId { get; set; }
        [JsonProperty("total_viewer_count")]
        public int TotalViewerCount { get; set; }
        [JsonProperty("total_unique_viewer_count")]
        public int TotalUniqueViewerCount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class InstaBroadcastFinalViewerListResponse
    {
        [JsonProperty("users")]
        public List<InstaBroadcastUser> Users { get; set; }
        [JsonProperty("total_unique_viewer_count")]
        public int TotalUniqueViewerCount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
