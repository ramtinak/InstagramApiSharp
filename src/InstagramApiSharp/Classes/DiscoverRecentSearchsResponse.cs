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
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class DiscoverRecentSearchsResponse
    {
        [JsonProperty("recent")]
        public List<RecentSearchs> Recent { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class RecentSearchs
    {
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("user")]
        public BroadcastUser User { get; set; }
        [JsonProperty("client_time")]
        public int ClientTime { get; set; }
    }
}
