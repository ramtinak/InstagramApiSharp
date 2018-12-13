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
    public class InstaAnimatedImageMediaContainerResponse
    {
        [JsonProperty("fixed_height")]
        public InstaAnimatedImageMediaResponse Media { get; set; }
    }

    public class InstaAnimatedImageMediaResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("width")]
        public string Width { get; set; }
        [JsonProperty("height")]
        public string Height { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
        [JsonProperty("mp4")]
        public string Mp4 { get; set; }
        [JsonProperty("mp4_size")]
        public string Mp4Size { get; set; }
        [JsonProperty("webp")]
        public string Webp { get; set; }
        [JsonProperty("webp_size")]
        public string WebpSize { get; set; }
    }
}
