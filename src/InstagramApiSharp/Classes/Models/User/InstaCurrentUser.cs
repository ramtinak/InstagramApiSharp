using InstagramApiSharp.Enums;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaCurrentUser : InstaUserShort
    {
        public InstaCurrentUser(InstaUserShort instaUserShort)
        {
            Pk = instaUserShort.Pk;
            UserName = instaUserShort.UserName;
            FullName = instaUserShort.FullName;
            IsPrivate = instaUserShort.IsPrivate;
            ProfilePicture = instaUserShort.ProfilePicture;
            ProfilePictureId = instaUserShort.ProfilePictureId;
            IsVerified = instaUserShort.IsVerified;
            HasAnonymousProfilePicture = instaUserShort.HasAnonymousProfilePicture;
            CanBoostPost = instaUserShort.CanBoostPost;
            CanSeeOrganicInsights = instaUserShort.CanSeeOrganicInsights;
            ShowInsightsTerms = instaUserShort.ShowInsightsTerms;
            ReelAutoArchive = instaUserShort.ReelAutoArchive;
            IsUnpublished = instaUserShort.IsUnpublished;
            AllowedCommenterType = instaUserShort.AllowedCommenterType;
            LatestReelMedia = instaUserShort.LatestReelMedia;
            IsFavorite = instaUserShort.IsFavorite;
        }

        public string Biography { get; set; }
        public string ExternalUrl { get; set; }
        public List<InstaImage> HdProfileImages { get; set; } = new List<InstaImage>();
        public InstaImage HdProfilePicture { get; set; }
        public bool ShowConversionEditEntry { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryCode { get; set; }
        public long NationalNumber { get; set; }
        public InstaGenderType Gender { get; set; }
        public string Email { get; set; }
    }
}