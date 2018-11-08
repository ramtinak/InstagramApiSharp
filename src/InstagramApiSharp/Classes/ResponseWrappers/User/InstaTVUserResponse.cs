/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTVUserResponse
    {
        [JsonProperty("show_insights_terms")]
        public bool ShowInsightsTerms { get; set; }
        [JsonProperty("can_boost_post")]
        public bool CanBoostPost { get; set; }
        [JsonProperty("following_tag_count")]
        public int FollowingTagCount { get; set; }
        [JsonProperty("follower_count")]
        public int FollowerCount { get; set; }
        [JsonProperty("media_count")]
        public int MediaCount { get; set; }
        [JsonProperty("can_see_organic_insights")]
        public bool CanSeeOrganicInsights { get; set; }
        [JsonProperty("geo_media_count")]
        public int GeoMediaCount { get; set; }
        [JsonProperty("has_biography_translation")]
        public bool HasBiographyTranslation { get; set; }
        [JsonProperty("can_link_entities_in_bio")]
        public bool CanLinkEntitiesInBio { get; set; }
        [JsonProperty("has_placed_orders")]
        public bool HasPlacedOrders { get; set; }
        [JsonProperty("following_count")]
        public int FollowingCount { get; set; }
        [JsonProperty("biography_with_entities")]
        public InstaBiographyEntities BiographyWithEntities { get; set; }
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("external_lynx_url")]
        public string ExternalLynxUrl { get; set; }
        [JsonProperty("reel_auto_archive")]
        public string ReelAutoArchive { get; set; }
        [JsonProperty("has_anonymous_profile_picture")]
        public bool HasAnonymousProfilePicture { get; set; }
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("allowed_commenter_type")]
        public string AllowedCommenterType { get; set; }
        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("profile_pic_id")]
        public string ProfilePicId { get; set; }
        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }
        [JsonProperty("total_igtv_videos")]
        public int TotalIGTVVideosCount { get; set; }
        [JsonProperty("friendship_status")]
        public InstaFriendshipStatusResponse FriendshipStatus { get; set; }
    }
}
