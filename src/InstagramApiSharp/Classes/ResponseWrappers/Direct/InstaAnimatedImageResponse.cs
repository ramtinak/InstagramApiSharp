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
    public class InstaAnimatedImageResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("images")]
        public InstaAnimatedImageMediaContainerResponse Images { get; set; }
        [JsonProperty("is_random")]
        public bool? IsRandom { get; set; }
        [JsonProperty("is_sticker")]
        public bool? IsSticker { get; set; }
        [JsonProperty("user")]
        public InstaAnimatedImageUserResponse User { get; set; }
    }
}
