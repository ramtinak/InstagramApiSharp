/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaPlaceListResponse
    {
        [JsonProperty("items")] public List<InstaPlaceResponse> Items { get; set; }

        [JsonProperty("has_more")] public bool? HasMore { get; set; }

        [JsonProperty("rank_token")] public string RankToken { get; set; }
        
        [JsonProperty("status")] internal string Status { get; set; }

        [JsonProperty("message")] internal string Message { get; set; }

        [JsonIgnore] public List<long> ExcludeList = new List<long>();
    }
}
