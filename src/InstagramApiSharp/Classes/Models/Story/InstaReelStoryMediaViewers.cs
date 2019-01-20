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
    public class InstaReelStoryMediaViewers
    {
        public string NextMaxId { get; set; }
        
        public int TotalScreenshotCount { get; set; }

        public int TotalViewerCount { get; set; }

        public InstaStoryItem UpdatedMedia { get; set; }

        public int UserCount { get; set; }

        public List<InstaUserShort> Users { get; set; } = new List<InstaUserShort>();
    }
}
