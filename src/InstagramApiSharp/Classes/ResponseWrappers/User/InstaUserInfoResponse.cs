using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaUserInfoResponse
    {
        [JsonProperty("pk")] public long Pk { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("full_name")] public string FullName { get; set; }

        [JsonProperty("is_private")] public bool IsPrivate { get; set; }

        [JsonProperty("profile_pic_url")] public string ProfilePicUrl { get; set; }

        [JsonProperty("profile_pic_id")] public string ProfilePicId { get; set; }

        [JsonProperty("is_verified")] public bool IsVerified { get; set; }

        [JsonProperty("has_anonymous_profile_picture")] public bool HasAnonymousProfilePicture { get; set; }

        [JsonProperty("media_count")] public long MediaCount { get; set; }

        [JsonProperty("geo_media_count")] public long GeoMediaCount { get; set; }

        [JsonProperty("follower_count")] public long FollowerCount { get; set; }

        [JsonProperty("following_count")] public long FollowingCount { get; set; }

        [JsonProperty("biography")] public string Biography { get; set; }

        [JsonProperty("can_link_entities_in_bio")] public bool CanLinkEntitiesInBio { get; set; }

        [JsonProperty("external_url")] public string ExternalUrl { get; set; }

        [JsonProperty("external_lynx_url")] public string ExternalLynxUrl { get; set; }

        [JsonProperty("has_biography_translation")] public bool HasBiographyTranslation { get; set; }

        [JsonProperty("can_boost_post")] public bool CanBoostPost { get; set; }

        [JsonProperty("can_see_organic_insights")] public bool CanSeeOrganicInsights { get; set; }

        [JsonProperty("show_insights_terms")] public bool ShowInsightsTerms { get; set; }

        [JsonProperty("can_convert_to_business")] public bool CanConvertToBusiness { get; set; }

        [JsonProperty("can_create_sponsor_tags")] public bool CanCreateSponsorTags { get; set; }

        [JsonProperty("can_be_tagged_as_sponsor")] public bool CanBeTaggedAsSponsor { get; set; }

        [JsonProperty("total_igtv_videos")] public int TotalIGTVVideos { get; set; }

        [JsonProperty("total_ar_effects")] public int TotalArEffects { get; set; }

        [JsonProperty("reel_auto_archive")] public string ReelAutoArchive { get; set; }

        [JsonProperty("is_profile_action_needed")] public bool IsProfileActionNeeded { get; set; }

        [JsonProperty("usertags_count")] public long UsertagsCount { get; set; }

        [JsonProperty("usertag_review_enabled")] public bool UsertagReviewEnabled { get; set; }

        [JsonProperty("is_needy")] public bool IsNeedy { get; set; }

        [JsonProperty("has_recommend_accounts")] public bool HasRecommendAccounts { get; set; }

        [JsonProperty("has_placed_orders")] public bool HasPlacedOrders { get; set; }

        [JsonProperty("can_tag_products_from_merchants")] public bool CanTagProductsFromMerchants { get; set; }

        [JsonProperty("show_business_conversion_icon")] public bool ShowBusinessConversionIcon { get; set; }

        [JsonProperty("show_conversion_edit_entry")] public bool ShowConversionEditEntry { get; set; }

        [JsonProperty("aggregate_promote_engagement")] public bool AggregatePromoteEngagement { get; set; }

        [JsonProperty("allowed_commenter_type")] public string AllowedCommenterType { get; set; }

        [JsonProperty("is_video_creator")] public bool IsVideoCreator { get; set; }

        [JsonProperty("has_profile_video_feed")] public bool HasProfileVideoFeed { get; set; }

        [JsonProperty("is_eligible_to_show_fb_cross_sharing_nux")] public bool IsEligibleToShowFBCrossSharingNux { get; set; }

        [JsonProperty("page_id_for_new_suma_biz_account")] public object PageIdForNewSumaBizAccount { get; set; }

        [JsonProperty("account_type")] public int AccountType { get; set; }

        [JsonProperty("is_favorite")] public bool IsFavorite { get; set; }

        [JsonProperty("has_chaining")] public bool HasChaining { get; set; }

        [JsonProperty("profile_context")] public string ProfileContext { get; set; }

        [JsonProperty("profile_context_mutual_follow_ids")] public List<long> ProfileContextMutualFollowIds { get; set; }

        [JsonProperty("is_business")] public bool IsBusiness { get; set; }

        [JsonProperty("include_direct_blacklist_status")] public bool IncludeDirectBlacklistStatus { get; set; }

        [JsonProperty("has_unseen_besties_media")] public bool HasUnseenBestiesMedia { get; set; }

        [JsonProperty("auto_expand_chaining")] public bool AutoExpandChaining { get; set; }

        
        [JsonProperty("biography_with_entities")] public InstaBiographyEntities BiographyWithEntities { get; set; }

        [JsonProperty("is_eligible_for_school")] public bool IsEligibleForSchool { get; set; }

        [JsonProperty("following_tag_count")] public int FollowingTagCount { get; set; }

        [JsonProperty("is_favorite_for_stories")] public bool IsFavoriteForStories { get; set; }

        [JsonProperty("profile_context_links_with_user_ids")] public List<InstaUserContextResponse> ProfileContextIds { get; set; }

        [JsonProperty("friendship_status")] public InstaStoryFriendshipStatusResponse FriendshipStatus { get; set; }

        // Business accounts

        [JsonProperty("hd_profile_pic_versions")] public List<ImageResponse> HdProfilePicVersions { get; set; }

        [JsonProperty("hd_profile_pic_url_info")] public ImageResponse HdProfilePicUrlInfo { get; set; }

        [JsonProperty("public_phone_number")] public string PublicPhoneNumber { get; set; }

        [JsonProperty("contact_phone_number")] public string ContactPhoneNumber { get; set; }

        [JsonProperty("public_phone_country_code")] public string PublicPhoneCountryCode { get; set; }

        [JsonProperty("shoppable_posts_count")] public int? ShoppablePostsCount { get; set; }

        [JsonProperty("city_id")] public long? CityId { get; set; }

        [JsonProperty("can_be_reported_as_fraud")] public bool? CanBeReportedAsFraud { get; set; }

        [JsonProperty("category")] public string Category { get; set; }

        [JsonProperty("is_favorite_for_highlights")] public bool? IsFavoriteForHighlights { get; set; }

        [JsonProperty("is_call_to_action_enabled")] public bool? IsCallToActionEnabled { get; set; }

        [JsonProperty("direct_messaging")] public string DirectMessaging { get; set; }

        [JsonProperty("latitude")] public double? Latitude { get; set; }

        [JsonProperty("fb_page_call_to_action_id")] public string FbPageCallToActionId { get; set; }

        [JsonProperty("business_contact_method")] public string BusinessContactMethod { get; set; }

        [JsonProperty("zip")] public string Zip { get; set; }

        [JsonProperty("is_interest_account")] public bool? IsInterestAccount { get; set; }

        [JsonProperty("longitude")] public double? Longitude { get; set; }

        [JsonProperty("city_name")] public string CityName { get; set; }

        [JsonProperty("address_street")] public string AddressStreet { get; set; }

        [JsonProperty("has_highlight_reels")] public bool? HasHighlightReels { get; set; }

        [JsonProperty("public_email")] public string PublicEmail { get; set; }

        [JsonProperty("show_shoppable_feed")] public bool? ShowShoppableFeed { get; set; }

        [JsonProperty("is_potential_business")] public bool? IsPotentialBusiness { get; set; }

        [JsonProperty("is_bestie")] public bool? IsBestie { get; set; }

        [JsonProperty("show_account_transparency_details")] public bool? ShowAccountTransparencyDetails { get; set; }

        [JsonProperty("highlight_reshare_disabled")] public bool? HighlightReshareDisabled { get; set; }

        [JsonProperty("page_name")] public string PageName { get; set; }

        [JsonProperty("page_id")] public long? PageId { get; set; }

    }
}