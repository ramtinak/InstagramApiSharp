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
    public class InstaVoiceMediaResponse
    {
        [JsonProperty("media")]
        public InstaVoiceResponse Media { get; set; }
        [JsonProperty("seen_user_ids")]
        public long[] SeenUserIds { get; set; }
        [JsonProperty("view_mode")]
        public string ViewMode { get; set; }
        [JsonProperty("seen_count")]
        public int? SeenCount { get; set; }
        [JsonProperty("replay_expiring_at_us")]
        public string ReplayExpiringAtUs { get; set; }
    }
}
