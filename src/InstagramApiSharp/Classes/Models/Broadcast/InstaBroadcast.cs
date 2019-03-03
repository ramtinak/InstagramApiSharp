/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaBroadcast
    {
        public string Id { get; set; }

        public string RtmpPlaybackUrl { get; set; }

        public string DashPlaybackUrl { get; set; }

        public string DashAbrPlaybackUrl { get; set; }

        public string BroadcastStatus { get; set; }

        public long ViewerCount { get; set; }

        public bool InternalOnly { get; set; }

        public string CoverFrameUrl { get; set; }

        public InstaUserShortFriendshipFull BroadcastOwner { get; set; }

        public DateTime PublishedTime { get; set; }

        public string MediaId { get; set; }

        public string BroadcastMessage { get; set; }

        public string OrganicTrackingToken { get; set; }

        public string DashManifest { get; set; }
    }
}
