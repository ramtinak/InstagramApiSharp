/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models.Business;
using InstagramApiSharp.Classes.ResponseWrappers.Business;
namespace InstagramApiSharp.Converters.Business
{
    internal class InstaStatisticsConverter : IObjectConverter<InstaStatistics, InstaStatisticsRootResponse>
    {
        public InstaStatisticsRootResponse SourceObject { get; set; }

        public InstaStatistics Convert()
        {
            if (SourceObject?.Data?.User == null)
                return null;
            var user = SourceObject.Data.User;
            var statisfics = new InstaStatistics
            {
                BusinessProfileId = user.BusinessProfile.Id,
                FollowersCount = user.FollowersCount ?? 0,
                Id = user.Id,
                UserId = user.InstagramUserId,
                Username = user.Username
            };
            if (user.BusinessProfile != null && user.BusinessProfile.Id != null)
                statisfics.BusinessProfileId = user.BusinessProfile.Id;

            if (user.ProfilePicture != null && user.ProfilePicture.Uri != null)
                statisfics.ProfilePicture = user.ProfilePicture.Uri;
            statisfics.BusinessManager = new InstaStatisticsBusinessManager();

            var businessManager = user.BusinessManager;

            if (businessManager.PromotionsUnit != null && businessManager.PromotionsUnit.SummaryPromotions != null)
            {
                try
                {
                    statisfics.BusinessManager.PromotionsUnit = new InstaStatisticsSummaryPromotions
                    {
                        Edges = businessManager.PromotionsUnit.SummaryPromotions.Edges
                    };
                }
                catch { }
            }
            if (businessManager.AccountSummaryUnit != null && businessManager.AccountSummaryUnit != null)
            {
                try
                {
                    statisfics.BusinessManager.AccountSummaryUnit = new InstaStatisticsAccountSummaryUnit
                    {
                        FollowersCount = businessManager.AccountSummaryUnit.FollowersCount ?? 0,
                        FollowersDeltaFromLastWeek = businessManager.AccountSummaryUnit.FollowersDeltaFromLastWeek ?? 0,
                        PostsCount = businessManager.AccountSummaryUnit.PostsCount ?? 0,
                        PostsDeltaFromLastWeek = businessManager.AccountSummaryUnit.PostsDeltaFromLastWeek ?? 0
                    };
                }
                catch { }
            }
            if (businessManager.StoriesUnit != null)
            {
                try
                {
                    var storyUnit = new InstaStatisticsStoriesUnit
                    {
                        LastWeekStoriesCount = businessManager.StoriesUnit.LastWeekStoriesCount ?? 0,
                        State = businessManager.StoriesUnit.State,
                        WeekOverWeekStoriesDelta = businessManager.StoriesUnit.WeekOverWeekStoriesDelta ?? 0
                    };
                    if (businessManager.StoriesUnit.SummaryStories != null)
                    {
                        storyUnit.SummaryStories = new InstaStatisticsSummaryStories
                        {
                            Count = businessManager.StoriesUnit.SummaryStories.Count ?? 0,
                            Edges = businessManager.StoriesUnit.SummaryStories.Edges
                        };
                    }
                    statisfics.BusinessManager.StoriesUnit = storyUnit;
                }
                catch { }
            }
            if (businessManager.TopPostsUnit != null)
            {
                try
                {
                    statisfics.BusinessManager.TopPostsUnit = new InstaStatisticsTopPostsUnit
                    {
                        LastWeekPostsCount = businessManager.TopPostsUnit.LastWeekPostsCount ?? 0,
                        WeekOverWeekPostsDelta = businessManager.TopPostsUnit.WeekOverWeekPostsDelta ?? 0
                    };
                    if (businessManager.TopPostsUnit.SummaryPosts != null)
                        foreach (var media in businessManager.TopPostsUnit.SummaryPosts.Edges)
                        {
                            try
                            {
                                var convertedMedia = ConvertersFabric.Instance.GetMediaShortConverter(media.Node).Convert();
                                statisfics.BusinessManager.TopPostsUnit.SummaryPosts.Add(convertedMedia);
                            }
                            catch { }
                        }
                    if (businessManager.TopPostsUnit.TopPosts != null)
                        foreach (var media in businessManager.TopPostsUnit.TopPosts.Edges)
                        {
                            try
                            {
                                var convertedMedia = ConvertersFabric.Instance.GetMediaShortConverter(media.Node).Convert();
                                statisfics.BusinessManager.TopPostsUnit.TopPosts.Add(convertedMedia);
                            }
                            catch { }
                        }
                }
                catch { }
            }
            if (businessManager.FollowersUnit != null)
            {
                try
                {
                    statisfics.BusinessManager.FollowersUnit = new InstaStatisticsFollowersUnit
                    {
                        FollowersUnitState = businessManager.FollowersUnit.FollowersUnitState,
                        FollowersDeltaFromLastWeek = businessManager.FollowersUnit.FollowersDeltaFromLastWeek ?? default(int)
                    };
                    foreach (var dataPoint in businessManager.FollowersUnit.AllFollowersAgeGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.AllFollowersAgeGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }
                    foreach (var graph in businessManager.FollowersUnit.DaysHourlyFollowersGraphs)
                    {
                        foreach (var dataPoint in graph.DataPoints)
                        {
                            try
                            {
                                var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                                statisfics.BusinessManager.FollowersUnit.DaysHourlyFollowersGraphs.Add(convertedDataPoint);
                            }
                            catch { }
                        }
                    }

                    foreach (var dataPoint in businessManager.FollowersUnit.FollowersTopCitiesGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.FollowersTopCitiesGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }

                    foreach (var dataPoint in businessManager.FollowersUnit.FollowersTopCountriesGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.FollowersTopCountriesGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }
                    foreach (var dataPoint in businessManager.FollowersUnit.GenderGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.GenderGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }
                    foreach (var dataPoint in businessManager.FollowersUnit.MenFollowersAgeGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.MenFollowersAgeGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }
                    foreach (var dataPoint in businessManager.FollowersUnit.TodayHourlyGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.TodayHourlyGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }
                    foreach (var dataPoint in businessManager.FollowersUnit.WeekDailyFollowersGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.WeekDailyFollowersGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }
                    foreach (var dataPoint in businessManager.FollowersUnit.WomenFollowersAgeGraph.DataPoints)
                    {
                        try
                        {
                            var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                            statisfics.BusinessManager.FollowersUnit.WomenFollowersAgeGraph.Add(convertedDataPoint);
                        }
                        catch { }
                    }
                }
                catch { }
            }
            if (businessManager.AccountInsightsUnit != null)
            {
                try
                {
                    statisfics.BusinessManager.AccountInsightsUnit = new InstaStatisticsAccountInsightsUnit
                    {
                        LastWeekCall = businessManager.AccountInsightsUnit.LastWeekCall ?? 0,
                        LastWeekGetDirection = businessManager.AccountInsightsUnit.LastWeekGetDirection ?? 0,
                        LastWeekImpressions = businessManager.AccountInsightsUnit.LastWeekImpressions ?? 0,
                        LastWeekProfileVisits = businessManager.AccountInsightsUnit.LastWeekProfileVisits ?? 0,
                        LastWeekReach = businessManager.AccountInsightsUnit.LastWeekReach ?? 0,
                        LastWeekText = businessManager.AccountInsightsUnit.LastWeekText ?? 0,
                        LastWeekWebsiteVisits = businessManager.AccountInsightsUnit.LastWeekWebsiteVisits ?? 0,
                        LastWeekEmail = businessManager.AccountInsightsUnit.LastWeekWebsiteVisits ?? 0,
                        WeekOverWeekCall = businessManager.AccountInsightsUnit.WeekOverWeekEmail ?? 0,
                        WeekOverWeekEmail = businessManager.AccountInsightsUnit.WeekOverWeekEmail ?? 0,
                        WeekOverWeekGetDirection = businessManager.AccountInsightsUnit.WeekOverWeekGetDirection ?? 0,
                        WeekOverWeekImpressions = businessManager.AccountInsightsUnit.WeekOverWeekImpressions ?? 0,
                        WeekOverWeekProfileVisits = businessManager.AccountInsightsUnit.WeekOverWeekReach ?? 0,
                        WeekOverWeekReach = businessManager.AccountInsightsUnit.WeekOverWeekReach ?? 0,
                        WeekOverWeekText = businessManager.AccountInsightsUnit.WeekOverWeekText ?? 0,
                        WeekOverWeekWebsiteVisits = businessManager.AccountInsightsUnit.WeekOverWeekWebsiteVisits ?? 0
                    };

                    if (businessManager.AccountInsightsUnit.InstagramAccountInsightsChannel != null)
                    {
                        try
                        {
                            statisfics.BusinessManager.AccountInsightsUnit.InstagramAccountInsightsChannel = new InstaStatisticsInsightsChannel
                            {
                                ChannelId = businessManager.AccountInsightsUnit.InstagramAccountInsightsChannel.ChannelId,
                                Id = businessManager.AccountInsightsUnit.InstagramAccountInsightsChannel.Id,
                                Tips = businessManager.AccountInsightsUnit.InstagramAccountInsightsChannel.Tips,
                                UnseenCount = businessManager.AccountInsightsUnit.InstagramAccountInsightsChannel.UnseenCount ?? 0
                            };
                        }
                        catch { }
                    }

                    if (businessManager.AccountInsightsUnit.AccountActionsLastWeekDailyGraph != null &&
                        businessManager.AccountInsightsUnit.AccountActionsLastWeekDailyGraph.TotalCountGraph != null &&
                        businessManager.AccountInsightsUnit.AccountActionsLastWeekDailyGraph.TotalCountGraph.DataPoints!= null)
                    {
                        foreach (var dataPoint in businessManager.AccountInsightsUnit.AccountActionsLastWeekDailyGraph.TotalCountGraph.DataPoints)
                        {
                            try
                            {
                                var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                                statisfics.BusinessManager.AccountInsightsUnit.AccountActionsLastWeekDailyGraph.Add(convertedDataPoint);
                            }
                            catch { }
                        }
                    }

                    if (businessManager.AccountInsightsUnit.AccountDiscoveryLastWeekDailyGraph != null &&
                        businessManager.AccountInsightsUnit.AccountDiscoveryLastWeekDailyGraph.Nodes != null)
                    {
                        foreach (var node in businessManager.AccountInsightsUnit.AccountDiscoveryLastWeekDailyGraph.Nodes)
                        {
                            foreach (var dataPoint in node.DataPoints)
                            {
                                try
                                {
                                    var convertedDataPoint = ConvertersFabric.Instance.GetStatisticsDataPointConverter(dataPoint).Convert();
                                    statisfics.BusinessManager.AccountInsightsUnit.AccountDiscoveryLastWeekDailyGraph.Add(convertedDataPoint);
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch { }
            }
            return statisfics;
        }
    }
}
