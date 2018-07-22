/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class DiscoverTopLiveResponse
    {
        [JsonProperty("broadcasts")]
        public List<InstaBroadcast> Broadcasts { get; set; }
        [JsonProperty("post_live_broadcasts")]
        public List<PostLiveBroadcasts> PostLiveBroadcasts { get; set; }
        [JsonProperty("score_map")]
        public ScoreMap ScoreMap { get; set; }
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }
        [JsonProperty("auto_load_more_enabled")]
        public bool AutoLoadMoreEnabled { get; set; }
        [JsonProperty("next_max_id")]
        public int NextMaxId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ScoreMap
    {
        public int _20615294 { get; set; }
        public int _344585014 { get; set; }
        public int _1565192808 { get; set; }
        public int _1440116683 { get; set; }
        public int _1698875260 { get; set; }
        public int _6609348201 { get; set; }
        public int _6006855109 { get; set; }
        public int _3005773868 { get; set; }
        public int _2236153363 { get; set; }
        public int _2727428151 { get; set; }
        public int _4307328564 { get; set; }
        public int _1667438788 { get; set; }
        public int _1466222421 { get; set; }
        public int _1547718867 { get; set; }
        public int _6101192911 { get; set; }
        public int _1724420191 { get; set; }
        public int _1402390711 { get; set; }
        public int _1634147529 { get; set; }
        public int _5490016106 { get; set; }
        public int _1473821207 { get; set; }
        public int _5577500658 { get; set; }
        public int _1476975762 { get; set; }
        public int _1128462520 { get; set; }
    }

    public class PostLiveBroadcasts
    {
        [JsonProperty("pk")]
        public string Pk { get; set; }
        [JsonProperty("user")]
        public BroadcastUser User { get; set; }
        [JsonProperty("broadcasts")]
        public List<BroadcastInfo> Broadcasts { get; set; }
        [JsonProperty("peak_viewer_count")]
        public int PeakViewerCount { get; set; }
    }

    public class BroadcastInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("broadcast_status")]
        public string BroadcastStatus { get; set; }
        [JsonProperty("dash_manifest")]
        public string DashManifest { get; set; }
        [JsonProperty("expire_at")]
        public int ExpireAt { get; set; }
        [JsonProperty("encoding_tag")]
        public string EncodingTag { get; set; }
        [JsonProperty("internal_only")]
        public bool InternalOnly { get; set; }
        [JsonProperty("number_of_qualities")]
        public int NumberOfQualities { get; set; }
        [JsonProperty("cover_frame_url")]
        public string CoverFrameUrl { get; set; }
        [JsonProperty("broadcast_owner")]
        public BroadcastUser BroadcastOwner { get; set; }
        [JsonProperty("published_time")]
        public int PublishedTime { get; set; }
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
