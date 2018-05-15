using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDirectInboxThreadResponse : BaseStatusResponse
    {
        [JsonProperty("muted")] public bool Muted { get; set; }

        [JsonProperty("users")] public List<InstaUserShortResponse> Users { get; set; }

        [JsonProperty("thread_title")] public string Title { get; set; }

        [JsonProperty("oldest_cursor")] public string OldestCursor { get; set; }

        [JsonProperty("last_activity_at")] public string LastActivity { get; set; }

        [JsonProperty("viewer_id")] public string VieweId { get; set; }

        [JsonProperty("thread_id")] public string ThreadId { get; set; }

        [JsonProperty("has_older")] public bool HasOlder { get; set; }

        [JsonProperty("inviter")] public InstaUserShortResponse Inviter { get; set; }

        [JsonProperty("named")] public bool Named { get; set; }

        [JsonProperty("pending")] public bool Pending { get; set; }

        [JsonProperty("canonical")] public bool Canonical { get; set; }

        [JsonProperty("has_newer")] public bool HasNewer { get; set; }

        [JsonProperty("is_spam")] public bool IsSpam { get; set; }

        [JsonProperty("thread_type")] public InstaDirectThreadType ThreadType { get; set; }

        [JsonProperty("items")] public List<InstaDirectInboxItemResponse> Items { get; set; }
    }
}