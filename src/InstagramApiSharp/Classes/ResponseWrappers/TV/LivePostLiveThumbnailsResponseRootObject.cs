/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class LivePostLiveThumbnailsResponseRootObject : BaseStatusResponse
    {
        [JsonProperty("thumbnails")] public List<string> Thumbnails { get; set; }
        [JsonProperty("title_prefill")] public string Title { get; set; }
    }
}
