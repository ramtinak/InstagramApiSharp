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
    public class InstaProductInfo
    {
        public InstaProduct Product { get; set; }

        public List<InstaProduct> OtherProducts { get; set; } = new List<InstaProduct>();

        public InstaUserShort User { get; set; }

        public InstaProductMediaList MoreFromBusiness { get; set; }
    }
}
