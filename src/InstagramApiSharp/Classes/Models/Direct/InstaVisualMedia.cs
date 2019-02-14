/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace InstagramApiSharp.Classes.Models
{
    public class InstaVisualMedia
    {
        public long MediaId { get; set; }

        public string InstaIdentifier { get; set; }

        public InstaMediaType MediaType { get; set; }

        public List<InstaImage> Images { get; set; } = new List<InstaImage>();

        public List<InstaVideo> Videos { get; set; } = new List<InstaVideo>();

        public string TrackingToken { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public DateTime UrlExpireAt { get; set; }

        public bool IsExpired => string.IsNullOrEmpty(InstaIdentifier); 
    }
}
