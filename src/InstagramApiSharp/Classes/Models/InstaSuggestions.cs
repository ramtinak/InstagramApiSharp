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
    public class InstaSuggestions
    {
        public bool MoreAvailable { get; set; }

        public string NextMaxId { get; set; }

        public List<InstaSuggestionItem> SuggestedUsers { get; set; } = new List<InstaSuggestionItem>();

        public List<InstaSuggestionItem> NewSuggestedUsers { get; set; } = new List<InstaSuggestionItem>();
    }
}
