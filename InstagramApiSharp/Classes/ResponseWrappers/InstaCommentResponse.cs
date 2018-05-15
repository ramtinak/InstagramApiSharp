using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaCommentResponse
    {
        [JsonProperty("type")] public int Type { get; set; }

        [JsonProperty("bit_flags")] public int BitFlags { get; set; }

        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("created_at_utc")] public string CreatedAtUtc { get; set; }

        [JsonProperty("comment_like_count")] public int LikesCount { get; set; }

        [JsonProperty("created_at")] public string CreatedAt { get; set; }

        [JsonProperty("content_type")] public string ContentType { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("pk")] public long Pk { get; set; }

        [JsonProperty("text")] public string Text { get; set; }
    }
}