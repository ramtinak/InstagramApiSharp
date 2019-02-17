/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models.Web
{
    public class InstaWebTextDataList
    {
        public string CursorId { get; set; }

        public List<string> Items { get; set; } = new List<string>();
    }
}
