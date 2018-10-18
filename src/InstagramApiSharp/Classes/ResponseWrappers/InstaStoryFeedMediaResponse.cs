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
    public class InstaStoryFeedMediaResponse
    {
        [JsonProperty("rotation")] public long Rotation { get; set; }

        [JsonProperty("is_pinned")] public long IsPinned { get; set; }

        [JsonProperty("height")] public double Height { get; set; }

        [JsonProperty("media_id")] public long MediaId { get; set; }

        [JsonProperty("product_type")] public string ProductType { get; set; }

        [JsonProperty("x")] public double X { get; set; }

        [JsonProperty("width")] public double Width { get; set; }

        [JsonProperty("y")] public double Y { get; set; }

        [JsonProperty("z")] public double Z { get; set; }
    }
}
