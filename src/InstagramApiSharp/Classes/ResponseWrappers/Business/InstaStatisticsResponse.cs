/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers.Business
{

    public class InstaStatisticsRootResponse
    {
        [JsonProperty("data")]
        public InstaStatisticsDataResponse Data { get; set; }
    }

    public class InstaStatisticsDataResponse
    {
        [JsonProperty("user")]
        public InstaStatisticsUserDataResponse User { get; set; }
    }

    public class InstaStatisticsUserDataResponse
    {
        [JsonProperty("business_profile")]
        public InstaStatisticsBusinessProfileResponse BusinessProfile { get; set; }
        [JsonProperty("business_manager")]
        public InstaStatisticsBusinessManagerResponse BusinessManager { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("profile_picture")]
        public InstaStatisticsImageResponse ProfilePicture { get; set; }
        [JsonProperty("instagram_user_id")]
        public string InstagramUserId { get; set; }
        [JsonProperty("followers_count")]
        public int? FollowersCount { get; set; } = 0;
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class InstaStatisticsBusinessProfileResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public class InstaStatisticsImageResponse
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

























    public class InstaStatisticsBusinessManagerResponse
    {
        [JsonProperty("account_insights_unit")]
        public InstaStatisticsAccountInsightsUnitResponse AccountInsightsUnit { get; set; }
        [JsonProperty("followers_unit")]
        public InstaStatisticsFollowersUnitResponse FollowersUnit { get; set; }
        [JsonProperty("top_posts_unit")]
        public InstaStatisticsTopPostsUnitResponse TopPostsUnit { get; set; }
        [JsonProperty("stories_unit")]
        public InstaStatisticsStoriesUnitResponse StoriesUnit { get; set; }
        [JsonProperty("account_summary_unit")]
        public InstaStatisticsAccountSummaryUnitResponse AccountSummaryUnit { get; set; }
        [JsonProperty("promotions_unit")]
        public InstaStatisticsPromotionsUnitResponse PromotionsUnit { get; set; }
    }

    public class InstaStatisticsPromotionsUnitResponse
    {
        [JsonProperty("summary_promotions")]
        public InstaStatisticsSummaryPromotionsResponse SummaryPromotions { get; set; }
    }

    public class InstaStatisticsSummaryPromotionsResponse
    {
        [JsonProperty("edges")]
        public object[] Edges { get; set; }
    }





    public class InstaStatisticsAccountSummaryUnitResponse
    {
        [JsonProperty("posts_count")]
        public long? PostsCount { get; set; } = 0;
        [JsonProperty("followers_count")]
        public long? FollowersCount { get; set; } = 0;
        [JsonProperty("followers_delta_from_last_week")]
        public long? FollowersDeltaFromLastWeek { get; set; } = 0;
        [JsonProperty("posts_delta_from_last_week")]
        public long? PostsDeltaFromLastWeek { get; set; } = 0;
    }







    public class InstaStatisticsStoriesUnitResponse
    {
        [JsonProperty("last_week_stories_count")]
        public long? LastWeekStoriesCount { get; set; } = 0;
        [JsonProperty("week_over_week_stories_delta")]
        public long? WeekOverWeekStoriesDelta { get; set; } = 0;
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("summary_stories")]
        public InstaStatisticsSummaryStoriesResponse SummaryStories { get; set; }
    }
    public class InstaStatisticsSummaryStoriesResponse : InstaStatisticsSummaryPromotionsResponse
    {
        [JsonProperty("count")]
        public long? Count { get; set; } = 0;
    }















    public class InstaStatisticsTopPostsUnitResponse
    {
        [JsonProperty("last_week_posts_count")]
        public long? LastWeekPostsCount { get; set; } = 0;
        [JsonProperty("week_over_week_posts_delta")]
        public long? WeekOverWeekPostsDelta { get; set; } = 0;
        [JsonProperty("summary_posts")]
        public InstaStatisticsTopPostsResponse SummaryPosts { get; set; }
        [JsonProperty("top_posts")]
        public InstaStatisticsTopPostsResponse TopPosts { get; set; }
    }

    public class InstaStatisticsTopPostsResponse
    {
        [JsonProperty("edges")]
        public InstaStatisticsEdgeResponse[] Edges { get; set; }
    }

    public class InstaStatisticsEdgeResponse
    {
        [JsonProperty("node")]
        public InstaMediaShortResponse Node { get; set; }
    }

    public class InstaMediaShortResponse
    {
        [JsonProperty("image")]
        public InstaStatisticsImageResponse Image { get; set; }
        [JsonProperty("instagram_media_id")]
        public string MediaIdentifier { get; set; }
        [JsonProperty("instagram_media_type")]
        public string InstagramMediaType { get; set; }
        [JsonProperty("inline_insights_node")]
        public InstaStatisticsInlineInsightsNodeResponse InlineInsightsNode { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class InstaStatisticsInlineInsightsNodeResponse
    {
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("metrics")]
        public InstaStatisticsMetricsResponse Metrics { get; set; }
    }

    public class InstaStatisticsMetricsResponse
    {
        [JsonProperty("impressions")]
        public InstaStatisticsImpressionsResponse Impressions { get; set; }
    }

    public class InstaStatisticsImpressionsResponse
    {
        [JsonProperty("organic")]
        public InstaStatisticsOrganicResponse Organic { get; set; }
    }

    public class InstaStatisticsOrganicResponse
    {
        [JsonProperty("value")]
        public long? Value { get; set; } = 0;
    }


















    public class InstaStatisticsDataPointsResponse
    {
        [JsonProperty("data_points")]
        public InstaStatisticsDataPointItemResponse[] DataPoints { get; set; }
    }
    public class InstaStatisticsDataPointItemResponse
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("value")]
        public int? Value { get; set; } = 0;
    }





























    public class InstaStatisticsFollowersUnitResponse
    {
        [JsonProperty("followers_unit_state")]
        public string FollowersUnitState { get; set; }
        [JsonProperty("followers_delta_from_last_week")]
        public int? FollowersDeltaFromLastWeek { get; set; } = 0;
        [JsonProperty("gender_graph")]
        public InstaStatisticsDataPointsResponse GenderGraph { get; set; }
        [JsonProperty("all_followers_age_graph")]
        public InstaStatisticsDataPointsResponse AllFollowersAgeGraph { get; set; }
        [JsonProperty("men_followers_age_graph")]
        public InstaStatisticsDataPointsResponse MenFollowersAgeGraph { get; set; }
        [JsonProperty("women_followers_age_graph")]
        public InstaStatisticsDataPointsResponse WomenFollowersAgeGraph { get; set; }
        [JsonProperty("followers_top_cities_graph")]
        public InstaStatisticsDataPointsResponse FollowersTopCitiesGraph { get; set; }
        [JsonProperty("followers_top_countries_graph")]
        public InstaStatisticsDataPointsResponse FollowersTopCountriesGraph { get; set; }
        [JsonProperty("week_daily_followers_graph")]
        public InstaStatisticsDataPointsResponse WeekDailyFollowersGraph { get; set; }
        [JsonProperty("days_hourly_followers_graphs")]
        public InstaStatisticsDaysHourlyFollowersGraphsResponse[] DaysHourlyFollowersGraphs { get; set; }
        [JsonProperty("today_hourly_graph")]
        public InstaStatisticsDataPointsResponse TodayHourlyGraph { get; set; }
    }

    public class InstaStatisticsDaysHourlyFollowersGraphsResponse : InstaStatisticsDataPointsResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }





























    public class InstaStatisticsAccountInsightsUnitResponse
    {
        [JsonProperty("last_week_website_visits")]
        public int? LastWeekWebsiteVisits { get; set; } = 0;
        [JsonProperty("week_over_week_website_visits")]
        public int? WeekOverWeekWebsiteVisits { get; set; } = 0;
        [JsonProperty("last_week_call")]
        public int? LastWeekCall { get; set; } = 0;
        [JsonProperty("week_over_week_call")]
        public int? WeekOverWeekCall { get; set; } = 0;
        [JsonProperty("last_week_text")]
        public int? LastWeekText { get; set; } = 0;
        [JsonProperty("week_over_week_text")]
        public int? WeekOverWeekText { get; set; } = 0;
        [JsonProperty("last_week_email")]
        public int? LastWeekEmail { get; set; } = 0;
        [JsonProperty("week_over_week_email")]
        public int? WeekOverWeekEmail { get; set; } = 0;
        [JsonProperty("last_week_get_direction")]
        public int? LastWeekGetDirection { get; set; } = 0;
        [JsonProperty("week_over_week_get_direction")]
        public int? WeekOverWeekGetDirection { get; set; } = 0;
        [JsonProperty("account_actions_last_week_daily_graph")]
        public InstaStatisticsDataPointsDailyNodesResponse AccountActionsLastWeekDailyGraph { get; set; }
        [JsonProperty("last_week_profile_visits")]
        public int? LastWeekProfileVisits { get; set; } = 0;
        [JsonProperty("week_over_week_profile_visits")]
        public int? WeekOverWeekProfileVisits { get; set; } = 0;
        [JsonProperty("account_discovery_last_week_daily_graph")]
        public InstaStatisticsDataPointsDicoveryDailyNodesResponse AccountDiscoveryLastWeekDailyGraph { get; set; }
        [JsonProperty("last_week_impressions")]
        public int? LastWeekImpressions { get; set; } = 0;
        [JsonProperty("week_over_week_impressions")]
        public int? WeekOverWeekImpressions { get; set; } = 0;
        [JsonProperty("last_week_reach")]
        public int? LastWeekReach { get; set; } = 0;
        [JsonProperty("week_over_week_reach")]
        public int? WeekOverWeekReach { get; set; } = 0;
        [JsonProperty("aymt_instagram_account_insights_channel")]
        public InstaStatisticsInsightsChannelResponse InstagramAccountInsightsChannel { get; set; }
        //[JsonProperty("last_week_impressions_day_graph")]
        //public object LastWeekImpressionsDayGraph { get; set; }
        //[JsonProperty("last_week_reach_day_graph")]
        //public object LastWeekTeachDayGraph { get; set; }
        //[JsonProperty("last_week_profile_visits_day_graph")]
        //public object LastWeekProfileVisitsDayGraph { get; set; }
    }
    public class InstaStatisticsDataPointsDicoveryDailyNodesResponse
    {
        [JsonProperty("nodes")]
        public InstaStatisticsDataPointsNodeResponse[] Nodes { get; set; }
    }
    public class InstaStatisticsDataPointsDailyNodesResponse
    {
        [JsonProperty("total_count_graph")]
        public InstaStatisticsDataPointsNodeResponse TotalCountGraph { get; set; }
    }

    public class InstaStatisticsDataPointsNodeResponse : InstaStatisticsDataPointsResponse
    {
        [JsonProperty("graph_name")]
        public string GraphName { get; set; }
    }
    
    

    public class InstaStatisticsInsightsChannelResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("tips")]
        public object[] Tips { get; set; }
        [JsonProperty("unseen_count")]
        public int? UnseenCount { get; set; } = 0;
    }



}
