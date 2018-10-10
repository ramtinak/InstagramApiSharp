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
using InstagramApiSharp.Classes.Models.Business;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.Business;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Enums;
namespace InstagramApiSharp.Converters.Business
{
    internal class InstaFullMediaInsightsConverter : IObjectConverter<InstaFullMediaInsights, InstaFullMediaInsightsResponse>
    {
        public InstaFullMediaInsightsResponse SourceObject { get; set; }

        public InstaFullMediaInsights Convert()
        {
            var fullMediaInsights = new InstaFullMediaInsights
            {
                CommentCount = SourceObject.CommentCount ?? 0,
                DisplayUrl = SourceObject.DisplayUrl,
                Id = SourceObject.Id,
                LikeCount = SourceObject.LikeCount ?? 0,
                SaveCount = SourceObject.SaveCount ?? 0,
            };
            if (SourceObject.CreationTime != null)
                fullMediaInsights.CreationTime = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreationTime.ToString());

            if (SourceObject.InstagramMediaType != null)
            {
                try
                {
                    fullMediaInsights.MediaType = (InstaMediaType)Enum.Parse(typeof(InstaMediaType), SourceObject.InstagramMediaType, true);
                }
                catch { }
            }

            var inlineInsights = SourceObject.InlineInsightsNode;

            if (SourceObject.InlineInsightsNode != null)
            {
                var node = new InstaFullMediaInsightsMetrics
                {
                    State = inlineInsights.State
                };
                if (inlineInsights.Metrics != null)
                {
                    node.ImpressionCount = inlineInsights.Metrics.ImpressionCount ?? 0;
                    node.OwnerAccountFollowsCount = inlineInsights.Metrics.OwnerAccountFollowsCount ?? 0;
                    node.OwnerProfileViewsCount = inlineInsights.Metrics.OwnerProfileViewsCount ?? 0;
                    node.ReachCount = inlineInsights.Metrics.ReachCount ?? 0;


                    if (inlineInsights.Metrics.Reach != null)
                    {
                        try
                        {
                            var reach = new InstaFullMediaInsightsNodeItem
                            {
                                Value = inlineInsights.Metrics.Reach.Value ?? 0
                            };
                            foreach (var item in inlineInsights.Metrics.Reach.FollowStatus.Nodes)
                            {
                                var convertedItem = ConvertersFabric.Instance.GetInsightsDataNodeConverter(item).Convert();
                                reach.Items.Add(convertedItem);
                            }
                            node.Reach = reach;
                        }
                        catch { }
                    }

                    if (inlineInsights.Metrics.Impressions != null)
                    {
                        try
                        {
                            var impressions = new InstaFullMediaInsightsNodeItem
                            {
                                Value = inlineInsights.Metrics.Impressions.Value ?? 0
                            };
                            foreach (var item in inlineInsights.Metrics.Impressions.Surfaces.Nodes)
                            {
                                var convertedItem = ConvertersFabric.Instance.GetInsightsDataNodeConverter(item).Convert();
                                impressions.Items.Add(convertedItem);
                            }
                            node.Impressions = impressions;
                        }
                        catch { }
                    }
                    if (inlineInsights.Metrics.ProfileActions != null)
                    {
                        try
                        {
                            var profileActions = new InstaFullMediaInsightsNodeItem
                            {
                                Value = inlineInsights.Metrics.ProfileActions.Actions.Value ?? 0
                            };
                            foreach (var item in inlineInsights.Metrics.ProfileActions.Actions.Nodes)
                            {
                                var convertedItem = ConvertersFabric.Instance.GetInsightsDataNodeConverter(item).Convert();
                                profileActions.Items.Add(convertedItem);
                            }
                            node.ProfileActions = profileActions;
                        }
                        catch { }
                    }
                }
                fullMediaInsights.InlineInsightsNode = node;
            }

            return fullMediaInsights;
        }
    }
}
