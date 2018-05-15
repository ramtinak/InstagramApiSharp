using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class BroadcastViewerListResponse
    {
        [JsonProperty("users")]
        public List<BroadcastUser> Users { get; set; }
        [JsonProperty("next_max_id")]
        public object NextMaxId { get; set; }
        [JsonProperty("total_viewer_count")]
        public int TotalViewerCount { get; set; }
        [JsonProperty("total_unique_viewer_count")]
        public int TotalUniqueViewerCount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class BroadcastFinalViewerListResponse
    {
        [JsonProperty("users")]
        public List<BroadcastUser> Users { get; set; }
        [JsonProperty("total_unique_viewer_count")]
        public int TotalUniqueViewerCount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
