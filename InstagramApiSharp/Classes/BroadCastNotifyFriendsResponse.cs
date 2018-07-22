/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class BroadCastNotifyFriendsResponse
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
