using System;

namespace InstagramApiSharp.Classes.Models
{
    [Serializable]
    public class InstaUserShort
    {
        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }
        public long Pk { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePicUrl { get; set; }
        public string ProfilePictureId { get; set; } = "unknown";
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool HasAnonymousProfilePicture { get; set; }
        public bool CanBoostPost { get; set; }
        public bool CanSeeOrganicInsights { get; set; }
        public bool ShowInsightsTerms { get; set; }
        public string ReelAutoArchive { get; set; }
        public bool IsUnpublished { get; set; }
        public string AllowedCommenterType { get; set; }
        public int LatestReelMedia { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsBusiness { get; set; }
        public object IsCallToActionEnabled { get; set; }
        public int AccountType { get; set; }
        public bool HasPlacedOrders { get; set; }
        public bool AllowContactsSync { get; set; }
        public string PhoneNumber { get; set; }

        public static InstaUserShort Empty => new InstaUserShort { FullName = string.Empty, UserName = string.Empty };

        public bool Equals(InstaUserShort user)
        {
            return Pk == user?.Pk;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InstaUserShort);
        }

        public override int GetHashCode()
        {
            return Pk.GetHashCode();
        }
    }
}