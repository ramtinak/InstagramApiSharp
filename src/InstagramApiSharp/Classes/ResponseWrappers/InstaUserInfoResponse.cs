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

        [JsonProperty("is_verified")] public bool IsVerified { get; set; }

        [JsonProperty("has_anonymous_profile_picture")] public bool HasAnonymousProfilePicture { get; set; }

        [JsonProperty("media_count")] public long MediaCount { get; set; }

        [JsonProperty("geo_media_count")] public long GeoMediaCount { get; set; }

        [JsonProperty("follower_count")] public long FollowerCount { get; set; }

        [JsonProperty("following_count")] public long FollowingCount { get; set; }

        [JsonProperty("biography")] public string Biography { get; set; }

        [JsonProperty("external_url")] public string ExternalUrl { get; set; }

        [JsonProperty("external_lynx_url")] public string ExternalLynxUrl { get; set; }

        [JsonProperty("reel_auto_archive")] public string ReelAutoArchive { get; set; }

        [JsonProperty("usertags_count")] public long UsertagsCount { get; set; }

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