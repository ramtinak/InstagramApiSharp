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

        [JsonProperty("has_anonymous_profile_picture")]
        public bool HasAnonymousProfilePicture { get; set; }

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

        [JsonProperty("profile_context_mutual_follow_ids")]
        public List<long> ProfileContextMutualFollowIds { get; set; }

        [JsonProperty("is_business")] public bool IsBusiness { get; set; }

        [JsonProperty("include_direct_blacklist_status")]
        public bool IncludeDirectBlacklistStatus { get; set; }

        [JsonProperty("has_unseen_besties_media")]
        public bool HasUnseenBestiesMedia { get; set; }

        [JsonProperty("auto_expand_chaining")] public bool AutoExpandChaining { get; set; }

        [JsonProperty("public_phone_number")] public string PublicPhoneNumber { get; set; }
        [JsonProperty("contact_phone_number")] public string ContactPhoneNumber { get; set; }
        [JsonProperty("public_phone_country_code")] public string PublicPhoneCountryCode { get; set; }
        [JsonProperty("biography_with_entities")] public InstaBiographyEntities BiographyWithEntities { get; set; }

        [JsonProperty("is_eligible_for_school")] public bool IsEligibleForSchool { get; set; }

        [JsonProperty("following_tag_count")] public int FollowingTagCount { get; set; }

        [JsonProperty("is_favorite_for_stories")] public bool IsFavoriteForStories { get; set; }

    }
}