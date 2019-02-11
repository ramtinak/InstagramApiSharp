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
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaSectionMediaListResponse
    {
        [JsonProperty("sections")]
        public List<InstaSectionMediaResponse> Sections { get; set; } = new List<InstaSectionMediaResponse>();
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }
        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }
        [JsonProperty("next_page")]
        public int? NextPage { get; set; }
        [JsonProperty("next_media_ids")]
        public List<long> NextMediaIds { get; set; }
        [JsonProperty("auto_load_more_enabled")]
        public bool? AutoLoadMoreEnabled { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaSectionMediaResponse
    {
        [JsonProperty("layout_type")]
        public string LayoutType { get; set; }
        [JsonProperty("layout_content")]
        public InstaSectionMediaLayoutContentResponse LayoutContent { get; set; }
        [JsonProperty("feed_type")]
        public string FeedType { get; set; }
        [JsonProperty("explore_item_info")]
        public InstaSectionMediaExploreItemInfoResponse ExploreItemInfo { get; set; }
    }
    public class InstaSectionMediaExploreItemInfoResponse
    {
        [JsonProperty("num_columns")]
        public int NumBolumns { get; set; }
        [JsonProperty("total_num_columns")]
        public int TotalNumBolumns { get; set; }
        [JsonProperty("aspect_ratio")]
        public float AspectRatio { get; set; }
        [JsonProperty("autoplay")]
        public bool Autoplay { get; set; }
    }

    public class InstaSectionMediaLayoutContentResponse
    {
        [JsonProperty("medias")]
        public List<InstaMediaAlbumResponse> Medias { get; set; }
    }

    

}
