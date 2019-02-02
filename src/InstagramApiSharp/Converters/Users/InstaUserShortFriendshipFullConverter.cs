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

namespace InstagramApiSharp.Converters.Users
{
    internal class InstaUserShortFriendshipFullConverter : IObjectConverter<InstaUserShortFriendshipFull, InstaUserShortFriendshipFullResponse>
    {
        public InstaUserShortFriendshipFullResponse SourceObject { get; set; }

        public InstaUserShortFriendshipFull Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var user = new InstaUserShortFriendshipFull
            {
                Pk = SourceObject.Pk,
                UserName = SourceObject.UserName,
                FullName = SourceObject.FullName,
                IsPrivate = SourceObject.IsPrivate,
                ProfilePicture = SourceObject.ProfilePicture,
                ProfilePictureId = SourceObject.ProfilePictureId,
                IsVerified = SourceObject.IsVerified,
                ProfilePicUrl = SourceObject.ProfilePicture
            };
            if (SourceObject.FriendshipStatus != null)
                user.FriendshipStatus = ConvertersFabric.Instance.GetFriendshipFullStatusConverter(SourceObject.FriendshipStatus).Convert();
            return user;
        }
    }
}
