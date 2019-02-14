/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Enums;
using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaVisualMediaContainer
    {
        public DateTime UrlExpireAt { get; set; }

        public InstaVisualMedia Media { get; set; }

        public int? SeenCount { get; set; }

        public DateTime ReplayExpiringAtUs { get; set; }

        public InstaViewMode ViewMode { get; set; }

        public List<long> SeenUserIds { get; set; } = new List<long>();

        public bool IsExpired
        {
            get
            {
                if (Media != null)
                    return Media.IsExpired;

                return false;
            }
        }
    }
}
