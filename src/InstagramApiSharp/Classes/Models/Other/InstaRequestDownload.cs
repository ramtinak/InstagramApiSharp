/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaRequestDownloadData
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = false;
        [JsonProperty("status")]
        internal string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
