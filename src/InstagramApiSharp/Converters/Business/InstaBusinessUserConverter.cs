/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
using System.Linq;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.ResponseWrappers.Business;

namespace InstagramApiSharp.Converters.Business
{
    internal class InstaBusinessUserConverter : IObjectConverter<InstaBusinessUser, InstaBusinessUserContainerResponse>
    {
        public InstaBusinessUserContainerResponse SourceObject { get; set; }

        public InstaBusinessUser Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException("SourceObject");

            var userInfo = new InstaBusinessUser
            {
                Pk = SourceObject.User.Pk ?? 0,
                Username = SourceObject.User.Username,
                FullName = SourceObject.User.FullName,
                IsPrivate = SourceObject.User.IsPrivate ?? false,
                IsVerified = SourceObject.User.IsVerified ?? false,
                HasAnonymousProfilePicture = SourceObject.User.HasAnonymousProfilePicture ?? false,
                Biography = SourceObject.User.Biography,
                ExternalUrl = SourceObject.User.ExternalUrl,
                ReelAutoArchive = SourceObject.User.ReelAutoArchive,
                IsBusiness = SourceObject.User.IsBusiness ?? false,
                ContactPhoneNumber = SourceObject.User.ContactPhoneNumber ?? string.Empty,
                PublicPhoneNumber = SourceObject.User.PublicPhoneNumber ?? string.Empty,
                PublicPhoneCountryCode = SourceObject.User.PublicPhoneCountryCode ?? string.Empty,
                ShowConversionEditEntry = SourceObject.User.ShowConversionEditEntry ?? false,
                AllowedCommenterType = SourceObject.User.AllowedCommenterType,
                AddressStreet = SourceObject.User.AddressStreet,
                Category = SourceObject.User.Category,
                CityId = SourceObject.User.CityId ?? 0,
                CityName = SourceObject.User.CityName,
                DirectMessaging = SourceObject.User.DirectMessaging,
                FbPageCallToActionId = SourceObject.User.FbPageCallToActionId,
                IsCallToActionEnabled = SourceObject.User.IsCallToActionEnabled ?? false,
                Latitude = SourceObject.User.Latitude ?? 0,
                Longitude = SourceObject.User.Longitude ?? 0,
                PublicEmail = SourceObject.User.PublicEmail,
                ZipCode = SourceObject.User.ZipCode,
                CanBoostPost = SourceObject.User.CanBoostPost ?? false,
                CanClaimPage = SourceObject.User.CanClaimPage ?? false,
                CanConvertToBusiness = SourceObject.User.CanConvertToBusiness ?? false,
                CanCrosspostWithoutFbToken = SourceObject.User.CanCrosspostWithoutFbToken ?? false,
                CanLinkEntitiesInBio = SourceObject.User.CanLinkEntitiesInBio ?? false,
                CanSeeOrganicInsights = SourceObject.User.CanSeeOrganicInsights ?? false,
                HasPlacedOrders = SourceObject.User.HasPlacedOrders ?? false,
                ShowBusinessConversionIcon = SourceObject.User.ShowBusinessConversionIcon ?? false,
                ShowInsightsTerms = SourceObject.User.ShowInsightsTerms ?? false,
                FbPageCallToActionIxAppId = SourceObject.User.FbPageCallToActionIxAppId ?? 0,
                FbPageCallToActionIxPartner = SourceObject.User.FbPageCallToActionIxPartner,
                FbPageCallToActionIxUrl = SourceObject.User.FbPageCallToActionIxUrl,
                InstagramLocationId = SourceObject.User.InstagramLocationId,
                PageId = SourceObject.User.PageId ?? 0,
                PageName = SourceObject.User.PageName,
                ProfilePicId = SourceObject.User.ProfilePicId,
                ProfileVisitsCount = SourceObject.User.ProfileVisitsCount ?? 0,
                ProfileVisitsNumDays = SourceObject.User.ProfileVisitsNumDays ?? 0,
                ProfilPicUrl = SourceObject.User.ProfilPicUrl,
                 
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
            if (SourceObject.User.FbPageCallToActionIxLabelBundle != null)
            {
                try
                {
                    userInfo.FbPageCallToActionIxLabelBundle = new InstaBusinessUserFbBundle
                    {
                        ContactBar = SourceObject.User.FbPageCallToActionIxLabelBundle.ContactBar,
                        SettingToggle = SourceObject.User.FbPageCallToActionIxLabelBundle.SettingToggle,
                        SettingToggleDescription = SourceObject.User.FbPageCallToActionIxLabelBundle.SettingToggleDescription
                    };
                }
                catch { }
            }
            return userInfo;
        }
    }
}
