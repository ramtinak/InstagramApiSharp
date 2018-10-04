using System.Collections.Generic;
using InstagramApiSharp.Enums;
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
        
        public InstaBiographyEntities BiographyWithEntities { get; set; }

        public bool IsEligibleForSchool { get; set; }

        public int FollowingTagCount { get; set; }

        public bool IsFavoriteForStories { get; set; }


        // Business accounts

        /// <summary>
        ///     Only for business account
        /// </summary>
        public List<InstaImage> HdProfilePicVersions { get; set; } = new List<InstaImage>();
        /// <summary>
        ///     Only for business account
        /// </summary>
        public InstaImage HdProfilePicUrlInfo { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string PublicPhoneNumber { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string ContactPhoneNumber { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string PublicPhoneCountryCode { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public int ShoppablePostsCount { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public long CityId { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool CanBeReportedAsFraud { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool IsFavoriteForHighlights { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool IsCallToActionEnabled { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string DirectMessaging { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string FbPageCallToActionId { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public InstaBusinessContactType BusinessContactMethod { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool IsInterestAccount { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string AddressStreet { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool HasHighlightReels { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public string PublicEmail { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool ShowShoppableFeed { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool IsPotentialBusiness { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool IsBestie { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool ShowAccountTransparencyDetails { get; set; }
        /// <summary>
        ///     Only for business account
        /// </summary>
        public bool HighlightReshareDisabled { get; set; }
    }
}