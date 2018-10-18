/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

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
        public string NextMaxId { get; set; }

        [JsonProperty("next_in_child_cursor")]
        public string NextMinId { get; set; }

        [JsonProperty("num_tail_child_comments")]
        public int NumTailChildComments { get; set; }

        [JsonProperty("parent_comment")] public InstaCommentResponse ParentComment { get; set; }

        [JsonProperty("child_comments")] public List<InstaCommentResponse> ChildComments { get; set; }

    }


    class InstaInlineCommentListResponseABC
    {
        [JsonProperty("comment_likes_enabled")]
        public bool CommentLikesEnabled { get; set; }
        [JsonProperty("comments")]
        public List<InstaCommentResponse> Comments { get; set; }
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }
        [JsonProperty("caption")]
        public InstaCaptionResponse Caption { get; set; }
        [JsonProperty("caption_is_edited")]
        public bool CaptionIsEdited { get; set; }
        [JsonProperty("has_more_comments")]
        public bool HasMoreComments { get; set; }
        [JsonProperty("has_more_headload_comments")]
        public bool HasMoreHeadloadComments { get; set; }
        [JsonProperty("threading_enabled")]
        public bool ThreadingEnabled { get; set; }
        [JsonProperty("media_header_display")]
        public string MediaHeaderDisplay { get; set; }
        [JsonProperty("initiate_at_top")]
        public bool InitiateAtTop { get; set; }
        [JsonProperty("insert_new_comment_to_top")]
        public bool InsertNewCommentToTop { get; set; }
        //[JsonProperty("quick_response_emojis")]
        //public object[] quick_response_emojis { get; set; }
        [JsonProperty("preview_comments")]
        public List<InstaCommentResponse> PreviewComments { get; set; }
        [JsonProperty("can_view_more_preview_comments")]
        public bool CanViewMorePreviewComments { get; set; }
        [JsonProperty("next_min_id")]
        public string NextMinId { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }
    }

    internal class InstaInlineCommentNextIdResponse
    {
        [JsonProperty("cached_comments_cursor")]
        public string CachedCommentsCursor { get; set; }
        [JsonProperty("bifilter_token")]
        public string BifilterToken { get; set; }
        [JsonProperty("server_cursor")]
        public string ServerCursor { get; set; }
    }
    
}
