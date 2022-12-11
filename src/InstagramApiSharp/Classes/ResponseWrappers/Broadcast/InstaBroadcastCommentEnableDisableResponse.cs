/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBroadcastCommentEnableDisableResponse
    {
        [JsonProperty("comment_muted")]
        public int CommentMuted { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
