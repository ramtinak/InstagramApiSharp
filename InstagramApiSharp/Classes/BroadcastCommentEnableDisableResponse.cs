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

    public class BroadcastCommentEnableDisableResponse
    {
        [JsonProperty("comment_muted")]
        public int CommentMuted { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
