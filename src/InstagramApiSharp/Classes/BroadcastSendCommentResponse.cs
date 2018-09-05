/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{

    public class BroadcastSendCommentResponse
    {
        [JsonProperty("comment")]
        public BroadcastSendComment Comment { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class BroadcastSendComment
    {
        [JsonProperty("content_type")]
        public string ContentType { get; set; }
        [JsonProperty("user")]
        public BroadcastUser User { get; set; }
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("created_at")]
        public float CreatedAt { get; set; }
        [JsonProperty("created_at_utc")]
        public int CreatedAtUtc { get; set; }
        [JsonProperty("media_id")]
        public long MediaId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }



}
