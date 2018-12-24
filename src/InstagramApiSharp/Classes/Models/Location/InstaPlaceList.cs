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
    public class InstaPlaceList
    {
        public List<InstaPlace> Items { get; set; } = new List<InstaPlace>();

        public bool HasMore { get; set; }

        public string RankToken { get; set; }

        internal string Status { get; set; }

        public List<long> ExcludeList { get; set; } = new List<long>();
    }
}
