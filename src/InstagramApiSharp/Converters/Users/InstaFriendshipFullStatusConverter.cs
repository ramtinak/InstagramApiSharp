/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaFriendshipFullStatusConverter : IObjectConverter<InstaFriendshipFullStatus, InstaFriendshipFullStatusResponse>
    {
        public InstaFriendshipFullStatusResponse SourceObject { get; set; }

        public InstaFriendshipFullStatus Convert()
        {
            var friendShip = new InstaFriendshipFullStatus
            {
                Following = SourceObject.Following ?? false,
                Blocking = SourceObject.Blocking ?? false,
                FollowedBy = SourceObject.FollowedBy ?? false,
                OutgoingRequest = SourceObject.OutgoingRequest ?? false,
                IsBestie = SourceObject.IsBestie ?? false,
                Muting = SourceObject.Muting ?? false
            };
            friendShip.IncomingRequest = SourceObject.IncomingRequest ?? false;
            friendShip.IsPrivate = SourceObject.IsPrivate ?? false;
            return friendShip;
        }
    }
}