/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBroadcastAddToPostLive
    {
        public string Pk { get; set; }

        public InstaUserShortFriendshipFull User { get; set; }

        public InstaBroadcastList Broadcasts { get; set; } = new InstaBroadcastList();

        public double LastSeenBroadcastTs { get; set; }

        public bool CanReply { get; set; }
    }
}
