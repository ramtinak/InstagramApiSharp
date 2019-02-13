using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaUserShortConverter : IObjectConverter<InstaUserShort, InstaUserShortResponse>
    {
        public InstaUserShortResponse SourceObject { get; set; }

        public InstaUserShort Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var user = new InstaUserShort
            {
                Pk = SourceObject.Pk,
                UserName = SourceObject.UserName,
                FullName = SourceObject.FullName,
                IsPrivate = SourceObject.IsPrivate,
                ProfilePicture = SourceObject.ProfilePicture,
                ProfilePictureId = SourceObject.ProfilePictureId,
                IsVerified = SourceObject.IsVerified,
                ProfilePicUrl = SourceObject.ProfilePicture,
                HasAnonymousProfilePicture = SourceObject.HasAnonymousProfilePicture,
                CanBoostPost = SourceObject.CanBoostPost,
                CanSeeOrganicInsights = SourceObject.CanSeeOrganicInsights,
                ShowInsightsTerms = SourceObject.ShowInsightsTerms,
                ReelAutoArchive = SourceObject.ReelAutoArchive,
                IsUnpublished = SourceObject.IsUnpublished,
                AllowedCommenterType = SourceObject.AllowedCommenterType,
                LatestReelMedia = SourceObject.LatestReelMedia,
                IsFavorite = SourceObject.IsFavorite
            };
            return user;
        }
    }
}