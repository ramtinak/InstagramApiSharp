using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
using System.Linq;
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
                ContactPhoneNumber = SourceObject.User.ContactPhoneNumber ?? string.Empty,
                PublicPhoneNumber = SourceObject.User.PublicPhoneNumber ?? string.Empty,
                PublicPhoneCountryCode = SourceObject.User.PublicPhoneCountryCode ?? string.Empty,
                IsEligibleForSchool = SourceObject.User.IsEligibleForSchool,
                IsFavoriteForStories = SourceObject.User.IsFavoriteForStories,
                FollowingTagCount = SourceObject.User.FollowingTagCount,
                
                // business account
                AddressStreet = SourceObject.User.AddressStreet,
                CanBeReportedAsFraud = SourceObject.User.CanBeReportedAsFraud ?? false,
                Category = SourceObject.User.Category,
                CityId = SourceObject.User.CityId ?? 0,
                CityName = SourceObject.User.CityName,
                DirectMessaging = SourceObject.User.DirectMessaging,
                FbPageCallToActionId = SourceObject.User.FbPageCallToActionId,
                HasHighlightReels = SourceObject.User.HasHighlightReels ?? false,
                HighlightReshareDisabled = SourceObject.User.HighlightReshareDisabled ?? false,
                IsBestie = SourceObject.User.IsBestie ?? false,
                IsCallToActionEnabled = SourceObject.User.IsCallToActionEnabled ?? false,
                IsFavoriteForHighlights = SourceObject.User.IsFavoriteForHighlights ?? false,
                IsInterestAccount = SourceObject.User.IsInterestAccount ?? false,
                IsPotentialBusiness = SourceObject.User.IsPotentialBusiness ?? false,
                Latitude = SourceObject.User.Latitude ?? 0,
                Longitude = SourceObject.User.Longitude ?? 0,
                PublicEmail = SourceObject.User.PublicEmail,
                ShoppablePostsCount = SourceObject.User.ShoppablePostsCount ?? 0,
                ShowAccountTransparencyDetails = SourceObject.User.ShowAccountTransparencyDetails ?? false,
                ShowShoppableFeed = SourceObject.User.ShowShoppableFeed ?? false,
                ZipCode = SourceObject.User.Zip,
                PageId = SourceObject.User.PageId ?? 0,
                PageName = SourceObject.User.PageName
            };
            if (SourceObject.User.BiographyWithEntities != null && SourceObject.User.BiographyWithEntities.Entities != null)
                userInfo.BiographyWithEntities = SourceObject.User.BiographyWithEntities;
            if (!string.IsNullOrEmpty(SourceObject.User.BusinessContactMethod))
            {
                try
                {
                    var t = SourceObject.User.BusinessContactMethod.Replace("_", "");
                    if (Enum.TryParse(t, true, out InstaBusinessContactType type))
                        userInfo.BusinessContactMethod = type;
                }
                catch { }
            }
            if (SourceObject.User.HdProfilePicUrlInfo != null)
            {
                try
                {
                    userInfo.HdProfilePicUrlInfo = ConvertersFabric.Instance.GetImageConverter(SourceObject.User.HdProfilePicUrlInfo).Convert();
                }
                catch { }
            }
            if (SourceObject.User.HdProfilePicVersions != null && SourceObject.User.HdProfilePicVersions.Any())
            {
                try
                {
                    foreach (var img in SourceObject.User.HdProfilePicVersions)
                        userInfo.HdProfilePicVersions.Add(ConvertersFabric.Instance.GetImageConverter(img).Convert());
                }
                catch { }
            }
            return userInfo;
        }
    }
}