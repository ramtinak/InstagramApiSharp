using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserInfo
    {
        public long Pk { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public bool IsPrivate { get; set; }

        public string ProfilePicUrl { get; set; }

        public bool IsVerified { get; set; }

        public bool HasAnonymousProfilePicture { get; set; }

        public long MediaCount { get; set; }

        public long GeoMediaCount { get; set; }

        public long FollowerCount { get; set; }

        public long FollowingCount { get; set; }

        public string Biography { get; set; }

        public string ExternalUrl { get; set; }

        public string ExternalLynxUrl { get; set; }

        public string ReelAutoArchive { get; set; }

        public long UsertagsCount { get; set; }

        public bool IsFavorite { get; set; }

        public bool HasChaining { get; set; }

        public string ProfileContext { get; set; }

        public List<long> ProfileContextMutualFollowIds { get; set; }

        public bool IsBusiness { get; set; }
        public bool IncludeDirectBlacklistStatus { get; set; }
        public bool HasUnseenBestiesMedia { get; set; }
        public bool AutoExpandChaining { get; set; }

        /// <summary>
        ///     Only for current user
        /// </summary>
        public string PublicPhoneNumber { get; set; }
        /// <summary>
        ///     Only for current user
        /// </summary>
        public string ContactPhoneNumber { get; set; }
        /// <summary>
        ///     Only for current user
        /// </summary>
        public string PublicPhoneCountryCode { get; set; }

        public InstaBiographyEntities BiographyWithEntities { get; set; }

        public bool IsEligibleForSchool { get; set; }

        public int FollowingTagCount { get; set; }

        public bool IsFavoriteForStories { get; set; }
    }
}