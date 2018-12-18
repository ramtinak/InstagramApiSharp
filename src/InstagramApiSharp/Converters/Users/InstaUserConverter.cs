using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaUserConverter : IObjectConverter<InstaUser, InstaUserResponse>
    {
        public InstaUserResponse SourceObject { get; set; }

        public InstaUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var shortConverter = ConvertersFabric.Instance.GetUserShortConverter(SourceObject);
            var user = new InstaUser(shortConverter.Convert())
            {
                HasAnonymousProfilePicture = SourceObject.HasAnonymousProfilePicture,
                FollowersCount = SourceObject.FollowersCount,
                FollowersCountByLine = SourceObject.FollowersCountByLine,
                SearchSocialContext = SourceObject.SearchSocialContext,
                SocialContext = SourceObject.SocialContext
            };

            if (double.TryParse(SourceObject.MulualFollowersCount, out var mutualFollowers))
                user.MutualFollowers = System.Convert.ToInt16(mutualFollowers);

            if (SourceObject.FriendshipStatus != null)
            {
                var freindShipStatusConverter =
                    ConvertersFabric.Instance.GetSingleFriendshipShortStatusConverter(SourceObject.FriendshipStatus);
                user.FriendshipStatus = freindShipStatusConverter.Convert();
            }

            return user;
        }
    }
}