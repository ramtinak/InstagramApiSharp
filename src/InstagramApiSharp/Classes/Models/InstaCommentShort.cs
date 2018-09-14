using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaCommentShort
    {
        public InstaContentType ContentType { get; set; } 

        public InstaUserShort User { get; set; }

        public long Pk { get; set; }

        public string Text { get; set; }

        public int Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public long MediaId { get; set; }

        public string Status { get; set; }

        public long ParentCommentId { get; set; }

        public bool HasLikedComment { get; set; }

        public int CommentLikeCount { get; set; }
    }
}
