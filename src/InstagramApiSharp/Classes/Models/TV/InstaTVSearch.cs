/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaTVSearch
    {
        public List<InstaTVSearchResult> Results { get; set; } = new List<InstaTVSearchResult>();

        public int NumResults { get; set; }

        public string RankToken { get; set; }

        internal string Status { get; set; }
    }

    public class InstaTVSearchResult
    {
        public string Type { get; set; }

        public InstaUserShortFriendship User { get; set; }

        public InstaTVChannel Channel { get; set; }
    }

}
