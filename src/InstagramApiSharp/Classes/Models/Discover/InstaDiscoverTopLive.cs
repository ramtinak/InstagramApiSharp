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
    public class InstaDiscoverTopLive
    {
        public InstaBroadcastList Broadcasts { get; set; } = new InstaBroadcastList();

        public List<InstaBroadcastPostLive> PostLiveBroadcasts { get; set; } = new List<InstaBroadcastPostLive>();

        public bool MoreAvailable { get; set; }

        public bool AutoLoadMoreEnabled { get; set; }

        public string NextMaxId { get; set; }
    }
}
