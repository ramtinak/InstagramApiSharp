/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaReelStoryMediaViewersResponse
    {
        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }
        [JsonProperty("screenshotter_user_ids")]
        public object[] ScreenshotterUserIds { get; set; }
        [JsonProperty("total_screenshot_count")]
        public double? TotalScreenshotCount { get; set; }
        [JsonProperty("total_viewer_count")]
        public double? TotalViewerCount { get; set; }
        [JsonProperty("updated_media")]
        public InstaStoryItemResponse UpdatedMedia { get; set; }
        [JsonProperty("user_count")]
        public double? UserCount { get; set; }
        [JsonProperty("users")]
        public List<InstaUserShortResponse> Users { get; set; }
    }
}
