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

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTVResponse
    {
        [JsonProperty("channels")]
        public List<InstaTVChannelResponse> Channels { get; set; }
        [JsonProperty("my_channel")]
        public InstaTVSelfChannelResponse MyChannel { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
        //public Badging badging { get; set; }
        //public Composer composer { get; set; }
    }
}
