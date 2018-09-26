/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class InstaDiscoverSuggestionResponse : InstaDiscoverRecentSearchesResponse
    {
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
        [JsonProperty("suggested")]
        public List<InstaRecentSearches> Suggested { get; set; }
    }
}
