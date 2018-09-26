/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class InstaDiscoverRecentSearchesResponse
    {
        [JsonProperty("recent")]
        public List<InstaRecentSearches> Recent { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaRecentSearches
    {
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("user")]
        public InstaBroadcastUser User { get; set; }
        [JsonProperty("client_time")]
        public int ClientTime { get; set; }
    }
}
