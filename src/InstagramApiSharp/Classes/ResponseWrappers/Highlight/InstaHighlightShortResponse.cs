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
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaHighlightShortListResponse : InstaDefault
    {
        public List<InstaHighlightShortResponse> Items { get; set; }

        [JsonProperty("num_results")]
        public int ResultsCount { get; set; }
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }
        [JsonProperty("max_id")]
        public string MaxId { get; set; }
    }
    public class InstaHighlightShortResponse
    {
        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }
        [JsonProperty("media_count")]
        public int MediaCount { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("reel_type")]
        public string ReelType { get; set; }
        [JsonProperty("latest_reel_media")]
        public int LatestReelMedia { get; set; }
    }
}
