/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaHighlightShortList
    {
        public List<InstaHighlightShort> Items { get; set; } = new List<InstaHighlightShort>();
        
        public int ResultsCount { get; set; }

        public bool MoreAvailable { get; set; }

        public string MaxId { get; set; }
    }
    public class InstaHighlightShort
    {
        public DateTime Time { get; set; }

        public int MediaCount { get; set; }

        public string Id { get; set; }

        public string ReelType { get; set; }

        public int LatestReelMedia { get; set; }
    }
}
