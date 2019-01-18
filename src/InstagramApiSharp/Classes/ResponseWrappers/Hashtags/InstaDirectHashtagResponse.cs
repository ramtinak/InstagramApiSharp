/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDirectHashtagResponse
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("media_count")] public long MediaCount { get; set; }

        [JsonProperty("media")] public InstaMediaItemResponse Media { get; set; }
    }
}
