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

namespace InstagramApiSharp.Converters
{
    internal class InstaUserShortFriendshipConverter : IObjectConverter<InstaUserShortFriendship, InstaUserShortFriendshipResponse>
    {
        public InstaUserShortFriendshipResponse SourceObject { get; set; }

        public InstaUserShortFriendship Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var user = new InstaUserShortFriendship
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
            {
                var item = SourceObject.FriendshipStatus;
                var friend = new InstaFriendshipShortStatus
                {
                    Following = item.Following,
                    IncomingRequest = item.IncomingRequest,
                    IsBestie = item.IsBestie,
                    IsPrivate = item.IsPrivate,
                    OutgoingRequest = item.OutgoingRequest,
                    Pk = 0
                };
                user.FriendshipStatus = friend;
            }
            return user;
        }
    }
}
