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
using Newtonsoft.Json.Linq;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaUserPresenceContainerResponse
    {
        [JsonIgnore]
        public List<InstaUserPresenceResponse> Items { get; set; } = new List<InstaUserPresenceResponse>();
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class InstaUserPresenceResponse
    {
        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }
        [JsonProperty("last_activity_at_ms")]
        public long? LastActivityAtMs { get; set; }
        [JsonIgnore]
        public long Pk { get; set; }
    }
}
