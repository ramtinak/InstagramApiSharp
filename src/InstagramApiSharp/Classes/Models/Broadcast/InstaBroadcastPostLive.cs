/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBroadcastPostLive
    {
        public string Pk { get; set; }

        public InstaUserShortFriendshipFull User { get; set; }

        public List<InstaBroadcastInfo> Broadcasts { get; set; } = new List<InstaBroadcastInfo>();

        public int PeakViewerCount { get; set; }
    }
}
