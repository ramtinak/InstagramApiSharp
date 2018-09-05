using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaCommentListResponse : BaseStatusResponse
    {
        [JsonProperty("comment_count")] public int CommentsCount { get; set; }

        [JsonProperty("next_max_id")] public string NextMaxId { get; set; }

        [JsonProperty("comment_likes_enabled")]
        public bool LikesEnabled { get; set; }

        [JsonProperty("caption_is_edited")] public bool CaptionIsEdited { get; set; }

        [JsonProperty("has_more_headload_comments")]
        public bool MoreHeadLoadAvailable { get; set; }

        [JsonProperty("caption")] public InstaCaptionResponse Caption { get; set; }

        [JsonProperty("has_more_comments")] public bool MoreComentsAvailable { get; set; }

        [JsonProperty("comments")] public List<InstaCommentResponse> Comments { get; set; }
    }
}