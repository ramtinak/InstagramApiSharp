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
    public class InstaProductMediaList
    {
        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();

        public bool MoreAvailable { get; set; }

        public int ResultsCount { get; set; }

        public int TotalCount { get; set; }

        public bool AutoLoadMoreEnabled { get; set; }

        public string NextMaxId { get; set; }
    }
}
