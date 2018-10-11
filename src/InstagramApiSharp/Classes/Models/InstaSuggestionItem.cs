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
    public class InstaSuggestionItem
    {
        public InstaUserShort User { get; set; }

        public string Algorithm { get; set; }

        public string SocialContext { get; set; }

        public string Icon { get; set; }

        public string Caption { get; set; }

        public List<string> MediaIds { get; set; } = new List<string>();

        public List<string> ThumbnailUrls { get; set; } = new List<string>(); 

        public List<string> LargeUrls { get; set; } = new List<string>();

        public List<InstaMedia> MediaInfos { get; set; } = new List<InstaMedia>();

        public float Value { get; set; }

        public bool IsNewSuggestion { get; set; }

        public string Uuid { get; set; }
    }
    public class InstaSuggestionItemList : List<InstaSuggestionItem> { }
}
