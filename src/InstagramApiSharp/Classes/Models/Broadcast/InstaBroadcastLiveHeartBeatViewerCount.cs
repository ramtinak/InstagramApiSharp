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
    public class InstaBroadcastLiveHeartBeatViewerCount
    {
        public float ViewerCount { get; set; }

        public string BroadcastStatus { get; set; }

        public object[] CobroadcasterIds { get; set; }

        public int OffsetToVideoStart { get; set; }

        public int TotalUniqueViewerCount { get; set; }

        public int IsTopLiveEligible { get; set; }
    }
}
