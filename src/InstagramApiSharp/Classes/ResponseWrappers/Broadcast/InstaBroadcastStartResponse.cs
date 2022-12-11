/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    internal class InstaBroadcastStartResponse
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
