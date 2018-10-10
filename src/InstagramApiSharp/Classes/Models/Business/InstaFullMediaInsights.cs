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
using InstagramApiSharp.Classes.ResponseWrappers.Business;
using InstagramApiSharp.Enums;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models.Business
{
    public class InstaFullMediaInsights
    {
        public string Id { get; set; }

        public InstaFullMediaInsightsMetrics InlineInsightsNode { get; set; }

        public DateTime CreationTime { get; set; }

        public string DisplayUrl { get; set; }

        public InstaMediaType MediaType { get; set; }

        public int CommentCount { get; set; }

        public int LikeCount { get; set; }

        public int SaveCount { get; set; }
    }

    public class InstaInsightsDataNode
    {
        public InstaInsightsNameType NameType { get; set; }

        public int Value { get; set; }
    }


    public class InstaFullMediaInsightsMetrics
    {
        public string State { get; set; }

        public int OwnerProfileViewsCount { get; set; }

        public int ReachCount { get; set; }

        public InstaFullMediaInsightsNodeItem ProfileActions { get; set; }

        public int ImpressionCount { get; set; }

        public InstaFullMediaInsightsNodeItem Impressions { get; set; }

        public int OwnerAccountFollowsCount { get; set; }

        public InstaFullMediaInsightsNodeItem Reach { get; set; }
    }


    public class InstaFullMediaInsightsNodeItem
    {
        public int Value { get; set; }

        public List<InstaInsightsDataNode> Items { get; set; } = new List<InstaInsightsDataNode>();
    }





}
