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
