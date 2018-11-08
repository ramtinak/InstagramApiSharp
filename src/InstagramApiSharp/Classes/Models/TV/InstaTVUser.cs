/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaTVUser
    {
        public bool ShowInsightsTerms { get; set; }

        public bool CanBoostPost { get; set; }

        public int FollowingTagCount { get; set; }

        public int FollowerCount { get; set; }

        public int MediaCount { get; set; }

        public bool CanSeeOrganicInsights { get; set; }

        public int GeoMediaCount { get; set; }

        public bool HasBiographyTranslation { get; set; }

        public bool CanLinkEntitiesInBio { get; set; }

        public bool HasPlacedOrders { get; set; }

        public int FollowingCount { get; set; }

        public InstaBiographyEntities BiographyWithEntities { get; set; }

        public string Biography { get; set; }

        public bool IsVerified { get; set; }

        public string ExternalLynxUrl { get; set; }

        public string ReelAutoArchive { get; set; }

        public bool HasAnonymousProfilePicture { get; set; }

        public long Pk { get; set; }

        public string AllowedCommenterType { get; set; }

        public bool IsPrivate { get; set; }

        public string FullName { get; set; }

        public string ProfilePicUrl { get; set; }

        public string Username { get; set; }

        public string ProfilePicId { get; set; }

        public string ExternalUrl { get; set; }

        public int TotalIGTVVideosCount { get; set; }

        public InstaFriendshipStatus FriendshipStatus { get; set; }
    }
}
