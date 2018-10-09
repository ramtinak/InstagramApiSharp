using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaHashtagMediaListResponse
    {
        [JsonProperty("sections")]
        public List<InstaHashtagMediaResponse> Sections { get; set; }
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

    public class InstaHashtagMediaResponse
    {
        [JsonProperty("layout_type")]
        public string LayoutType { get; set; }
        [JsonProperty("layout_content")]
        public InstaHashtagMediaLayoutContentResponse LayoutContent { get; set; }
        [JsonProperty("feed_type")]
        public string FeedType { get; set; }
        [JsonProperty("explore_item_info")]
        public InstaHashtagMediaExploreItemInfoResponse ExploreItemInfo { get; set; }
    }
    public class InstaHashtagMediaExploreItemInfoResponse
    {
        [JsonProperty("num_columns")]
        public int NumBolumns { get; set; }
        [JsonProperty("total_num_columns")]
        public int TotalNumBolumns { get; set; }
        [JsonProperty("aspect_ratio")]
        public int AspectYatio { get; set; }
        [JsonProperty("autoplay")]
        public bool Autoplay { get; set; }
    }

    public class InstaHashtagMediaLayoutContentResponse
    {
        [JsonProperty("medias")]
        public List<InstaMediaAlbumResponse> Medias { get; set; }
    }

    

}
