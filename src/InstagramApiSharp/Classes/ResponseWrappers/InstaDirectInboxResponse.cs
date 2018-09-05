using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDirectInboxResponse
    {
        [JsonProperty("has_older")] public bool HasOlder { get; set; }

        [JsonProperty("unseen_count_ts")] public long UnseenCountTs { get; set; }

        [JsonProperty("unseen_count")] public long UnseenCount { get; set; }

        [JsonProperty("threads")] public List<InstaDirectInboxThreadResponse> Threads { get; set; }

        [JsonProperty("oldest_cursor")] public string OldestCursor { get; set; }

        [JsonProperty("blended_inbox_enabled")] public bool BlendedInboxEnabled { get; set; }
    }
}