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
    public class InstaBroadcastInfo
    {
        public long Id { get; set; }

        public string BroadcastStatus { get; set; }

        public string DashManifest { get; set; }

        public DateTime ExpireAt { get; set; }

        public string EncodingTag { get; set; }

        public bool InternalOnly { get; set; }

        public int NumberOfQualities { get; set; }

        public string CoverFrameUrl { get; set; }

        public InstaUserShortFriendshipFull BroadcastOwner { get; set; }

        public DateTime PublishedTime { get; set; }

        public string MediaId { get; set; }

        public string BroadcastMessage { get; set; }

        public string OrganicTrackingToken { get; set; }

        public int TotalUniqueViewerCount { get; set; }
    }
}
