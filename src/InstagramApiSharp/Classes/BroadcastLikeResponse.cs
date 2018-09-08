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
    public class BroadcastLikeResponse
    {
        [JsonProperty("likes")]
        public int Likes { get; set; }
        [JsonProperty("burst_likes")]
        public int BurstLikes { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("like_ts")]
        public string LikeTs { get; set; }
    }

}
