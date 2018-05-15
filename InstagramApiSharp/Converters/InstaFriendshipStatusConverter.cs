using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaFriendshipStatusConverter :
        IObjectConverter<InstaFriendshipStatus, InstaFriendshipStatusResponse>
    {
        public InstaFriendshipStatusResponse SourceObject { get; set; }

        public InstaFriendshipStatus Convert()
        {
            var friendShip = new InstaFriendshipStatus
            {
                Following = SourceObject.Following,
                Blocking = SourceObject.Blocking,
                FollowedBy = SourceObject.FollowedBy,
                IncomingRequest = SourceObject.IncomingRequest,
                OutgoingRequest = SourceObject.OutgoingRequest
            };
            friendShip.IncomingRequest = SourceObject.IncomingRequest;
            friendShip.IsPrivate = SourceObject.IsPrivate;
            return friendShip;
        }
    }
}