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
    public class InstaBroadcastPostLiveResponse
    {
        [JsonProperty("pk")]
        public string Pk { get; set; }
        [JsonProperty("user")]
        public InstaUserShortFriendshipFullResponse User { get; set; }
        [JsonProperty("broadcasts")]
        public List<InstaBroadcastInfoResponse> Broadcasts { get; set; }
        [JsonProperty("peak_viewer_count")]
        public int PeakViewerCount { get; set; }
    }
}
