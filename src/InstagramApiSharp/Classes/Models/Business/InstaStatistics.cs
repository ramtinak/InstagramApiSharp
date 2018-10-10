/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models.Business
{
    public class InstaStatistics
    {
        public string BusinessProfileId { get; set; }

        public InstaStatisticsBusinessManager BusinessManager { get; set; }

        public string Username { get; set; }

        public string ProfilePicture { get; set; }

        public string UserId { get; set; }

        public int FollowersCount { get; set; } = 0;

        public string Id { get; set; }
    }


    public class InstaStatisticsBusinessManager
    {
        public InstaStatisticsAccountInsightsUnit AccountInsightsUnit { get; set; }

        public InstaStatisticsFollowersUnit FollowersUnit { get; set; }

        public InstaStatisticsTopPostsUnit TopPostsUnit { get; set; }

        public InstaStatisticsStoriesUnit StoriesUnit { get; set; }

        public InstaStatisticsAccountSummaryUnit AccountSummaryUnit { get; set; }

        public InstaStatisticsSummaryPromotions PromotionsUnit { get; set; }
    }

    public class InstaStatisticsSummaryPromotions
    {
        public object[] Edges { get; set; }
    }







    public class InstaStatisticsAccountSummaryUnit
    {
        public long PostsCount { get; set; } = 0;

        public long FollowersCount { get; set; } = 0;

        public long FollowersDeltaFromLastWeek { get; set; } = 0;

        public long PostsDeltaFromLastWeek { get; set; } = 0;
    }




    public class InstaStatisticsStoriesUnit
    {
        public long LastWeekStoriesCount { get; set; } = 0;

        public long WeekOverWeekStoriesDelta { get; set; } = 0;

        public string State { get; set; }

        public InstaStatisticsSummaryStories SummaryStories { get; set; }
    }
    public class InstaStatisticsSummaryStories : InstaStatisticsSummaryPromotions
    {
        public long Count { get; set; } = 0;
    }



















    public class InstaStatisticsTopPostsUnit
    {
        public long LastWeekPostsCount { get; set; } = 0;

        public long WeekOverWeekPostsDelta { get; set; } = 0;

        public List<InstaMediaShort> SummaryPosts { get; set; } = new List<InstaMediaShort>();

        public List<InstaMediaShort> TopPosts { get; set; } = new List<InstaMediaShort>();
    }
    public class InstaMediaShort
    {
        public string Image { get; set; }

        public string MediaIdentifier { get; set; }

        public string InsightsState { get; set; }

        public long MetricsImpressionsOrganicValue { get; set; }

        public string Id { get; set; }

        public InstaMediaType MediaType { get; set; }
    }





















    
    public class InstaStatisticsDataPointItem
    {
        public string Label { get; set; }

        public int Value { get; set; } = 0;
    }
    
    public class InstaStatisticsFollowersUnit
    {
        public string FollowersUnitState { get; set; }

        public int FollowersDeltaFromLastWeek { get; set; }

        public List<InstaStatisticsDataPointItem> GenderGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> AllFollowersAgeGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> MenFollowersAgeGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> WomenFollowersAgeGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> FollowersTopCitiesGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> FollowersTopCountriesGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> WeekDailyFollowersGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> DaysHourlyFollowersGraphs { get; set; } = new List<InstaStatisticsDataPointItem>();

        public List<InstaStatisticsDataPointItem> TodayHourlyGraph { get; set; } = new List<InstaStatisticsDataPointItem>();
    }





















    public class InstaStatisticsAccountInsightsUnit
    {
        public int LastWeekWebsiteVisits { get; set; } = 0;

        public int WeekOverWeekWebsiteVisits { get; set; } = 0;

        public int LastWeekCall { get; set; } = 0;

        public int WeekOverWeekCall { get; set; } = 0;

        public int LastWeekText { get; set; } = 0;

        public int WeekOverWeekText { get; set; } = 0;

        public int LastWeekEmail { get; set; } = 0;

        public int WeekOverWeekEmail { get; set; } = 0;
        
        public int LastWeekGetDirection { get; set; } = 0;

        public int WeekOverWeekGetDirection { get; set; } = 0;

        public List<InstaStatisticsDataPointItem> AccountActionsLastWeekDailyGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public int LastWeekProfileVisits { get; set; } = 0;

        public int WeekOverWeekProfileVisits { get; set; } = 0;

        public List<InstaStatisticsDataPointItem> AccountDiscoveryLastWeekDailyGraph { get; set; } = new List<InstaStatisticsDataPointItem>();

        public int LastWeekImpressions { get; set; } = 0;

        public int WeekOverWeekImpressions { get; set; } = 0;

        public int LastWeekReach { get; set; } = 0;

        public int WeekOverWeekReach { get; set; } = 0;

        public InstaStatisticsInsightsChannel InstagramAccountInsightsChannel { get; set; }
    }
    



    public class InstaStatisticsInsightsChannel
    {
        public string Id { get; set; }

        public string ChannelId { get; set; }

        public object[] Tips { get; set; }

        public int UnseenCount { get; set; } = 0;
    }












}
