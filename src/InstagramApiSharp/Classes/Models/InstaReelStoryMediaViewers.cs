using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaReelStoryMediaViewers
    {
        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }
        [JsonProperty("screenshotter_user_ids")]
        public object[] ScreenshotterUserIds { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("total_screenshot_count")]
        public int TotalScreenshotCount { get; set; }
        [JsonProperty("total_viewer_count")]
        public int TotalViewerCount { get; set; }
        [JsonProperty("updated_media")]
        public InstaUpdatedMedia UpdatedMedia { get; set; }
        [JsonProperty("user_count")]
        public int UserCount { get; set; }
        [JsonProperty("users")]
        public InstaUserShortResponse[] Users { get; set; }
    }

    public class InstaUpdatedMedia
    {
        [JsonProperty("viewers")]
        public InstaUserShortResponse[] Viewers { get; set; }
    }

    //public class User
    //{
    //    public string allowed_commenter_type { get; set; }
    //    public bool can_boost_post { get; set; }
    //    public bool can_see_organic_insights { get; set; }
    //    public string full_name { get; set; }
    //    public bool has_anonymous_profile_picture { get; set; }
    //    public bool is_private { get; set; }
    //    public bool is_unpublished { get; set; }
    //    public bool is_verified { get; set; }
    //    public int pk { get; set; }
    //    public string profile_pic_id { get; set; }
    //    public string profile_pic_url { get; set; }
    //    public string reel_auto_archive { get; set; }
    //    public bool show_insights_terms { get; set; }
    //    public string username { get; set; }
    //}

    //public class Viewer
    //{
    //    public string full_name { get; set; }
    //    public bool is_private { get; set; }
    //    public bool is_verified { get; set; }
    //    public int pk { get; set; }
    //    public string profile_pic_id { get; set; }
    //    public string profile_pic_url { get; set; }
    //    public string username { get; set; }
    //}
    

}
