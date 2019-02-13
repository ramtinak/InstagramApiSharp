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
    public class InstaStoryCountdownStickerItemResponse
    {
        [JsonProperty("countdown_id")]
        public long CountdownId { get; set; }
        [JsonProperty("end_ts")]
        public long? EndTime { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("text_color")]
        public string TextColor { get; set; }
        [JsonProperty("start_background_color")]
        public string StartBackgroundColor { get; set; }
        [JsonProperty("end_background_color")]
        public string EndBackgroundColor { get; set; }
        [JsonProperty("digit_color")]
        public string DigitColor { get; set; }
        [JsonProperty("digit_card_color")]
        public string DigitCardColor { get; set; }
        [JsonProperty("following_enabled")]
        public bool? FollowingEnabled { get; set; }
        [JsonProperty("is_owner")]
        public bool? IsOwner { get; set; }
        //public object Attribution { get; set; } // Naa moshakhas
        [JsonProperty("viewer_is_following")]
        public bool? ViewerIsFollowing { get; set; }
    }
}
