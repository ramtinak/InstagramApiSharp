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
    public class InstaReelShareResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("reel_owner_id")]
        public long ReelOwnerId { get; set; }
        [JsonProperty("is_reel_persisted")]
        public bool? IsReelPersisted { get; set; }
        [JsonProperty("reel_type")]
        public string ReelType { get; set; }
        [JsonProperty("media")]
        public InstaStoryItemResponse Media { get; set; }
    }
}
