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
    public class InstaDiscoverSearchResult
    {
        public int NumResults { get; set; }

        public List<InstaUser> Users { get; set; } = new List<InstaUser>();

        public bool HasMoreAvailable { get; set; }

        public string RankToken { get; set; }
    }
}
