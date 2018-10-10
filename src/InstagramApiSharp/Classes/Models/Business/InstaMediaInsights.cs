/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Models.Business
{
    public class InstaMediaInsights
    {
        [JsonProperty("like_count")]
        public int LikeCount { get; set; }
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }
        [JsonProperty("save_count")]
        public int SaveCount { get; set; }
        [JsonProperty("impression_count")]
        public int ImpressionCount { get; set; }
        [JsonProperty("reach_count")]
        public int ReachCount { get; set; }
        [JsonProperty("engagement_count")]
        public int EngagementCount { get; set; }
        [JsonProperty("avg_engagement_count")]
        public int AverageEngagementCount { get; set; }
    }

    internal class InstaMediaInsightsContainer : InstaDefaultResponse
    {
        [JsonProperty("media_organic_insights")]
        public InstaMediaInsights MediaOrganicInsights { get; set; }
    }
    
}
