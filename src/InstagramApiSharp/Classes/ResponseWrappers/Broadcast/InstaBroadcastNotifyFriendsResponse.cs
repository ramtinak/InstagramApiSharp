/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBroadcastNotifyFriendsResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("friends")]
        public List<InstaUserShortFriendshipFullResponse> Friends { get; set; }
        [JsonProperty("online_friends_count")]
        public int? OnlineFriendsCount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
