/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDirectBroadcastResponse
    {
        [JsonProperty("broadcast")] public InstaBroadcast Broadcast { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("is_linked")] public bool? IsLinked { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("message")] public string Message { get; set; }
    }
}
