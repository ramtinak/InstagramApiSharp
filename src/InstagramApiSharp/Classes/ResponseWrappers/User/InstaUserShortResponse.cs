using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaUserShortResponse : BaseStatusResponse
    {
        [JsonProperty("username")] public string UserName { get; set; }

        [JsonProperty("profile_pic_url")] public string ProfilePicture { get; set; }

        [JsonProperty("profile_pic_id")] public string ProfilePictureId { get; set; } = "unknown";

        [JsonProperty("full_name")] public string FullName { get; set; }

        [JsonProperty("is_verified")] public bool IsVerified { get; set; }

        [JsonProperty("is_private")] public bool IsPrivate { get; set; }

        [JsonProperty("pk")] public long Pk { get; set; }
        [JsonProperty("has_anonymous_profile_picture")] public bool HasAnonymousProfilePicture { get; set; }

        [JsonProperty("can_boost_post")] public bool CanBoostPost { get; set; }

        [JsonProperty("can_see_organic_insights")] public bool CanSeeOrganicInsights { get; set; }

        [JsonProperty("show_insights_terms")] public bool ShowInsightsTerms { get; set; }

        [JsonProperty("reel_auto_archive")] public string ReelAutoArchive { get; set; }

        [JsonProperty("is_unpublished")] public bool IsUnpublished { get; set; }

        [JsonProperty("allowed_commenter_type")] public string AllowedCommenterType { get; set; }

        [JsonProperty("latest_reel_media")] public int LatestReelMedia { get; set; }

        [JsonProperty("is_favorite")] public bool IsFavorite { get; set; }

        [JsonProperty("is_business")] public bool IsBusiness { get; set; }

        [JsonProperty("is_call_to_action_enabled")] public object IsCallToActionEnabled { get; set; }

        [JsonProperty("account_type")] public int AccountType { get; set; }

        [JsonProperty("has_placed_orders")] public bool HasPlacedOrders { get; set; }

        [JsonProperty("allow_contacts_sync")] public bool AllowContactsSync { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }
    }
}