using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaCaptionResponse : BaseStatusResponse
    {
        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("created_at_utc")] public string CreatedAtUtcUnixLike { get; set; }

        [JsonProperty("created_at")] public string CreatedAtUnixLike { get; set; }

        [JsonProperty("content_type")] public string ContentType { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("media_id")] public string MediaId { get; set; }

        [JsonProperty("pk")] public string Pk { get; set; }
    }
}