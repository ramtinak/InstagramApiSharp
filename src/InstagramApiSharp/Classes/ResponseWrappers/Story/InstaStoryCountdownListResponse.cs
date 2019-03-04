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
    public class InstaStoryCountdownListResponse
    {
        [JsonProperty("countdowns")] public List<InstaStoryCountdownStickerItemResponse> Items { get; set; }

        [JsonProperty("more_available")] public bool? MoreAvailable { get; set; }

        [JsonProperty("max_id")] public string MaxId { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}
