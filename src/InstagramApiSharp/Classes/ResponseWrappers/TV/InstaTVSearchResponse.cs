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
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTVSearchResponse
    {
        [JsonProperty("results")]
        public List<InstaTVSearchResultResponse> Results { get; set; }
        [JsonProperty("num_results")]
        public int? NumResults { get; set; }
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
    }

    public class InstaTVSearchResultResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("user")]
        public InstaUserShortFriendshipResponse User { get; set; }
        [JsonProperty("channel")]
        public InstaTVChannelResponse Channel { get; set; }
    }
}
