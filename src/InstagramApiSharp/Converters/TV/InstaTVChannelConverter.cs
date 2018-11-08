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

namespace InstagramApiSharp.Converters
{
    internal class InstaTVUserConverter : IObjectConverter<InstaTVUser, InstaTVUserResponse>
    {
        public InstaTVUserResponse SourceObject { get; set; }

        public InstaTVUser Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException("SourceObject");

            var user = new InstaTVUser
            {
                AllowedCommenterType = SourceObject.AllowedCommenterType,
                Biography = SourceObject.Biography,
                BiographyWithEntities = SourceObject.BiographyWithEntities,
                CanBoostPost = SourceObject.CanBoostPost,
                CanLinkEntitiesInBio = SourceObject.CanLinkEntitiesInBio,
                CanSeeOrganicInsights = SourceObject.CanSeeOrganicInsights,
                ExternalLynxUrl = SourceObject.ExternalLynxUrl,
                ExternalUrl = SourceObject.ExternalUrl,
                FollowerCount = SourceObject.FollowerCount,
                FollowingCount = SourceObject.FollowingCount,
                FollowingTagCount = SourceObject.FollowingTagCount,
                FullName = SourceObject.FullName,
                GeoMediaCount = SourceObject.GeoMediaCount,
                HasAnonymousProfilePicture = SourceObject.HasAnonymousProfilePicture,
                HasBiographyTranslation = SourceObject.HasBiographyTranslation,
                HasPlacedOrders = SourceObject.HasPlacedOrders,
                IsPrivate = SourceObject.IsPrivate,
                IsVerified = SourceObject.IsVerified,
                MediaCount = SourceObject.MediaCount,
                Pk = SourceObject.Pk,
                ProfilePicId = SourceObject.ProfilePicId,
                ProfilePicUrl = SourceObject.ProfilePicUrl,
                ReelAutoArchive = SourceObject.ReelAutoArchive,
                ShowInsightsTerms = SourceObject.ShowInsightsTerms,
                TotalIGTVVideosCount = SourceObject.TotalIGTVVideosCount,
                Username = SourceObject.Username
            };
            try
            {
                user.FriendshipStatus = ConvertersFabric.Instance.GetFriendShipStatusConverter(SourceObject.FriendshipStatus).Convert();
            }
            catch { }
            return user;
        }
    }
}
