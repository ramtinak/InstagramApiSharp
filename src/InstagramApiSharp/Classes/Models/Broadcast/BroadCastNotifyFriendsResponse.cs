/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class InstaBroadCastNotifyFriendsResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("friends")]
        public object[] Friends { get; set; }
        [JsonProperty("online_friends_count")]
        public int OnlineFriendsCount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
