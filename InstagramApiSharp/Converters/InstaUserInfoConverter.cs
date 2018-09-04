using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaUserInfoConverter : IObjectConverter<InstaUserInfo, InstaUserInfoContainerResponse>
    {
        public InstaUserInfoContainerResponse SourceObject { get; set; }

        public InstaUserInfo Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException("SourceObject");

            var userInfo = new InstaUserInfo
            {
                Pk = SourceObject.User.Pk,
                Username = SourceObject.User.Username,
                FullName = SourceObject.User.FullName,
                IsPrivate = SourceObject.User.IsPrivate,
                ProfilePicUrl = SourceObject.User.ProfilePicUrl,
                IsVerified = SourceObject.User.IsVerified,
                HasAnonymousProfilePicture = SourceObject.User.HasAnonymousProfilePicture,
                MediaCount = SourceObject.User.MediaCount,
                GeoMediaCount = SourceObject.User.GeoMediaCount,
                FollowerCount = SourceObject.User.FollowerCount,
                FollowingCount = SourceObject.User.FollowingCount,
                Biography = SourceObject.User.Biography,
                ExternalUrl = SourceObject.User.ExternalUrl,
                ExternalLynxUrl = SourceObject.User.ExternalLynxUrl,
                ReelAutoArchive = SourceObject.User.ReelAutoArchive,
                UsertagsCount = SourceObject.User.UsertagsCount,
                IsFavorite = SourceObject.User.IsFavorite,
                HasChaining = SourceObject.User.HasChaining,
                ProfileContext = SourceObject.User.ProfileContext,
                ProfileContextMutualFollowIds = SourceObject.User.ProfileContextMutualFollowIds,
                IsBusiness = SourceObject.User.IsBusiness,
                IncludeDirectBlacklistStatus = SourceObject.User.IncludeDirectBlacklistStatus,
                HasUnseenBestiesMedia = SourceObject.User.HasUnseenBestiesMedia,
                AutoExpandChaining = SourceObject.User.AutoExpandChaining,
                ContactPhoneNumber = SourceObject.User.ContactPhoneNumber,
                PublicPhoneNumber = SourceObject.User.PublicPhoneNumber,
                PublicPhoneCountryCode = SourceObject.User.PublicPhoneCountryCode
            };
            return userInfo;
        }
    }
}