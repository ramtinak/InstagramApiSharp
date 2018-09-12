using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaInlineCommentList
    {
        public bool CommentLikesEnabled { get; set; }

        public List<InstaComment> Comments { get; set; } = new List<InstaComment>();

        public int CommentCount { get; set; }

        public InstaCaption Caption { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool HasMoreComments { get; set; }

        public bool HasMoreHeadloadComments { get; set; }

        public bool ThreadingEnabled { get; set; }

        public string MediaHeaderDisplay { get; set; }

        public bool InitiateAtTop { get; set; }

        public bool InsertNewCommentToTop { get; set; }
        
        public List<InstaComment> PreviewComments { get; set; } = new List<InstaComment>();

        public bool CanViewMorePreviewComments { get; set; }

        public string NextMinId { get; set; }

        internal string Status { get; set; }

        public string NextMaxId { get; set; }
    }
}
