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

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryCountdownList
    {
        public List<InstaStoryCountdownStickerItem> Items { get; set; } = new List<InstaStoryCountdownStickerItem>();

        public bool MoreAvailable { get; set; }

        public string MaxId { get; set; }
    }
}
