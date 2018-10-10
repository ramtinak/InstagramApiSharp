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

namespace InstagramApiSharp.Classes.ResponseWrappers.Business
{
    public class InstaFullMediaInsightsRootResponse
    {
        [JsonProperty("data")]
        public InstaFullMediaInsightsDataResponse Data { get; set; }
    }

    public class InstaFullMediaInsightsDataResponse
    {
        [JsonProperty("media")]
        public InstaFullMediaInsightsResponse Media { get; set; }
    }

    public class InstaFullMediaInsightsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("inline_insights_node")]
        public InstaFullMediaInsightsInlineNodeResponse InlineInsightsNode { get; set; }
        [JsonProperty("creation_time")]
        public long? CreationTime { get; set; }
        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }
        [JsonProperty("instagram_media_type")]
        public string InstagramMediaType { get; set; }
        [JsonProperty("comment_count")]
        public int? CommentCount { get; set; }
        [JsonProperty("like_count")]
        public int? LikeCount { get; set; }
        [JsonProperty("save_count")]
        public int? SaveCount { get; set; }
    }











    public class InstaFullMediaInsightsInlineNodeResponse
    {
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("metrics")]
        public InstaFullMediaInsightsMetricsResponse Metrics { get; set; }
    }


    public class InstaFullMediaInsightsMetricsResponse
    {
        [JsonProperty("owner_profile_views_count")]
        public int? OwnerProfileViewsCount { get; set; }
        [JsonProperty("reach_count")]
        public int? ReachCount { get; set; }
        [JsonProperty("profile_actions")]
        public InstaFullMediaInsightsProfileActionsResponse ProfileActions { get; set; }
        [JsonProperty("impression_count")]
        public int? ImpressionCount { get; set; }
        [JsonProperty("impressions")]
        public InstaFullMediaInsightsImpressionsResponse Impressions { get; set; }
        [JsonProperty("owner_account_follows_count")]
        public int? OwnerAccountFollowsCount { get; set; }
        [JsonProperty("reach")]
        public InstaFullMediaInsightsReachResponse Reach { get; set; }
    }






    public class InstaFullMediaInsightsReachResponse
    {
        [JsonProperty("value")]
        public int? Value { get; set; }
        [JsonProperty("follow_status")]
        public InstaFullMediaInsightsNodeResponse FollowStatus { get; set; }
    }

    public class InstaFullMediaInsightsNodeResponse
    {
        [JsonProperty("nodes")]
        public InstaInsightsDataNodeResponse[] Nodes { get; set; }
    }





    public class InstaInsightsDataNodeResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public int? Value { get; set; }
    }







    public class InstaFullMediaInsightsImpressionsResponse
    {
        [JsonProperty("value")]
        public int? Value { get; set; }
        [JsonProperty("surfaces")]
        public InstaFullMediaInsightsNodeResponse Surfaces { get; set; }
    }











    public class InstaFullMediaInsightsProfileActionsResponse
    {
        [JsonProperty("actions")]
        public InstaFullMediaInsightsActionsResponse Actions { get; set; }
    }


    public class InstaFullMediaInsightsActionsResponse : InstaFullMediaInsightsNodeResponse
    {
        [JsonProperty("value")]
        public int? Value { get; set; }
    }

    



}
