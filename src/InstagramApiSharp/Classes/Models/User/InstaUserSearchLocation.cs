/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.ResponseWrappers;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserSearchLocation
    {
        [JsonProperty("list")]
        public List<InstaUserSearchLocationList> Items { get; set; }
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
        [JsonProperty("clear_client_cache")]
        public bool ClearClientCache { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
    }
    public class InstaUserSearchLocationList
    {
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("user")]
        public InstaUserInfoResponse User { get; set; }
    }
}
