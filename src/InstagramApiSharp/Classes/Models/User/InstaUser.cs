namespace InstagramApiSharp.Classes.Models
{
    public class InstaUser : InstaUserShort
    {
        public InstaUser(InstaUserShort instaUserShort)
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
       
        public int FollowersCount { get; set; }
        public string FollowersCountByLine { get; set; }
        public string SocialContext { get; set; }
        public string SearchSocialContext { get; set; }
        public int MutualFollowers { get; set; }
        public int UnseenCount { get; set; }
        public InstaFriendshipShortStatus FriendshipStatus { get; set; }
    }
}