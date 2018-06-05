using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaInlineCommentListResponse : BaseStatusResponse
    {
        [JsonProperty("child_comment_count")]
        public int ChildCommentCount { get; set; }

        [JsonProperty("has_more_tail_child_comments")]
        public bool HasMoreTailChildComments { get; set; }

        [JsonProperty("has_more_head_child_comments")]
        public bool HasMoreHeadChildComments { get; set; }

        [JsonProperty("next_max_child_cursor")]
        public string NextId { get; set; }

        [JsonProperty("num_tail_child_comments")]
        public int NumTailChildComments { get; set; }

        [JsonProperty("parent_comment")] public InstaCommentResponse ParentComment { get; set; }

        [JsonProperty("child_comments")] public List<InstaCommentResponse> ChildComments { get; set; }
    }
}
