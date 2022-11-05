/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDiscoverRecentSearchesResponse
    {
        [JsonProperty("recent")]
        public List<InstaDiscoverSearchesResponse> Recent { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
