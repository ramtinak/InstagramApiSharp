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
    public class InstaPlaceResponse
    {
        [JsonProperty("location")] public InstaPlaceShortResponse Location { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("subtitle")] public string Subtitle { get; set; }

        // always empty
        //[JsonProperty("media_bundles")] public object MediaBundles { get; set; }
        // always empty
        //[JsonProperty("header_media")] public object HeaderMedia { get; set; }
    }
}
