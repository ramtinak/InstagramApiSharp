using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;

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
}
