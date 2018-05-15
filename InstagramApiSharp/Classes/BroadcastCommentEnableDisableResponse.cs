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
