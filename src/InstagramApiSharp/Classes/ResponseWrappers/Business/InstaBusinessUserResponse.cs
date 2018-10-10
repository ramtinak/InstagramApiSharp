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
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers.Business
{
    public class InstaBusinessUserContainerResponse
    {
        [JsonProperty("user")]
        public InstaBusinessUserResponse User { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaBusinessUserResponse
    {
        [JsonProperty("profile_pic_url")]
        public string ProfilPicUrl { get; set; }
        [JsonProperty("can_boost_post")]
        public bool? CanBoostPost { get; set; }
        [JsonProperty("biography_with_entities")]
        public InstaBiographyEntities BiographyWithEntities { get; set; }
        [JsonProperty("can_see_organic_insights")]
        public bool? CanSeeOrganicInsights { get; set; }
        [JsonProperty("has_placed_orders")]
        public bool? HasPlacedOrders { get; set; }
        [JsonProperty("can_link_entities_in_bio")]
        public bool? CanLinkEntitiesInBio { get; set; }
        [JsonProperty("show_insights_terms")]
        public bool? ShowInsightsTerms { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("is_verified")]
        public bool? IsVerified { get; set; }
        [JsonProperty("show_conversion_edit_entry")]
        public bool? ShowConversionEditEntry { get; set; }
        [JsonProperty("allowed_commenter_type")]
        public string AllowedCommenterType { get; set; }
        [JsonProperty("is_private")]
        public bool? IsPrivate { get; set; }
        [JsonProperty("reel_auto_archive")]
        public string ReelAutoArchive { get; set; }
        [JsonProperty("pk")]
        public long? Pk { get; set; }
        [JsonProperty("profile_pic_id")]
        public string ProfilePicId { get; set; }
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }
        [JsonProperty("has_anonymous_profile_picture")]
        public bool? HasAnonymousProfilePicture { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        

        [JsonProperty("is_business")]
        public bool? IsBusiness { get; set; }
        [JsonProperty("profile_visits_count")]
        public int? ProfileVisitsCount { get; set; }
        [JsonProperty("can_crosspost_without_fb_token")]
        public bool? CanCrosspostWithoutFbToken { get; set; }
        [JsonProperty("show_business_conversion_icon")]
        public bool? ShowBusinessConversionIcon { get; set; }
        [JsonProperty("page_name")]
        public string PageName { get; set; }
        [JsonProperty("can_claim_page")]
        public bool? CanClaimPage { get; set; }
        [JsonProperty("fb_page_call_to_action_ix_app_id")]
        public long? FbPageCallToActionIxAppId { get; set; }
        [JsonProperty("fb_page_call_to_action_ix_partner")]
        public string FbPageCallToActionIxPartner { get; set; }
        [JsonProperty("is_call_to_action_enabled")]
        public bool? IsCallToActionEnabled { get; set; }
        [JsonProperty("instagram_location_id")]
        public string InstagramLocationId { get; set; }
        [JsonProperty("fb_page_call_to_action_ix_url")]
        public string FbPageCallToActionIxUrl { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("page_id")]
        public long? PageId { get; set; }
        [JsonProperty("zip")]
        public string ZipCode { get; set; }
        [JsonProperty("contact_phone_number")]
        public string ContactPhoneNumber { get; set; }
        [JsonProperty("business_contact_method")]
        public string BusinessContactMethod { get; set; }
        [JsonProperty("city_name")]
        public string CityName { get; set; }
        [JsonProperty("direct_messaging")]
        public string DirectMessaging { get; set; }
        [JsonProperty("longitude")]
        public float? Longitude { get; set; }
        [JsonProperty("fb_page_call_to_action_id")]
        public string FbPageCallToActionId { get; set; }
        [JsonProperty("city_id")]
        public long? CityId { get; set; }
        [JsonProperty("profile_visits_num_days")]
        public int? ProfileVisitsNumDays { get; set; }
        [JsonProperty("can_convert_to_business")]
        public bool? CanConvertToBusiness { get; set; }
        [JsonProperty("public_phone_number")]
        public string PublicPhoneNumber { get; set; }
        [JsonProperty("latitude")]
        public float? Latitude { get; set; }
        [JsonProperty("public_email")]
        public string PublicEmail { get; set; }
        [JsonProperty("public_phone_country_code")]
        public string PublicPhoneCountryCode { get; set; }
        [JsonProperty("address_street")]
        public string AddressStreet { get; set; }
        [JsonProperty("fb_page_call_to_action_ix_label_bundle")]
        public InstaUserEditFbBundleResponse FbPageCallToActionIxLabelBundle { get; set; }
    }

    public class InstaUserEditFbBundleResponse
    {
        [JsonProperty("contact_bar")]
        public string ContactBar { get; set; }
        [JsonProperty("setting_toggle")]
        public string SettingToggle { get; set; }
        [JsonProperty("setting_toggle_description")]
        public string SettingToggleDescription { get; set; }
    }
}
