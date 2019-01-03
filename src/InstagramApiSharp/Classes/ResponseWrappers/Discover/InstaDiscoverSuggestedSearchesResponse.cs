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
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDiscoverSuggestedSearchesResponse
    {
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
        [JsonProperty("suggested")]
        public List<InstaDiscoverSearchesResponse> Suggested { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    
}
