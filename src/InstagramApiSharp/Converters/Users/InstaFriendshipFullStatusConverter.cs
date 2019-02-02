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
                Following = SourceObject.Following,
                Blocking = SourceObject.Blocking,
                FollowedBy = SourceObject.FollowedBy,
                OutgoingRequest = SourceObject.OutgoingRequest,
                IsBestie = SourceObject.IsBestie,
                Muting = SourceObject.Muting
            };
            friendShip.IncomingRequest = SourceObject.IncomingRequest;
            friendShip.IsPrivate = SourceObject.IsPrivate;
            return friendShip;
        }
    }
}