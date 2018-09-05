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
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class DiscoverSuggestionResponse : DiscoverRecentSearchsResponse
    {
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
        [JsonProperty("suggested")]
        public List<RecentSearchs> Suggested { get; set; }
    }
}
