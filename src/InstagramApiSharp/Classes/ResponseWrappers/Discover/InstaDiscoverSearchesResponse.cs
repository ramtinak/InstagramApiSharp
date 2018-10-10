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

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDiscoverSearchesResponse
    {
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
        [JsonProperty("client_time")]
        public int? ClientTime { get; set; }
    }
}
