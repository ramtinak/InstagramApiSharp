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
    public class InstaBroadcastStatusItem
    {
        public long Id { get; set; }

        public string BroadcastStatus { get; set; }

        public float ViewerCount { get; set; }

        public bool HasReducedVisibility { get; set; }

        public string CoverFrameUrl { get; set; }
    }
}
