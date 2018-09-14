using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaInlineCommentList
    {
        public int ChildCommentCount { get; set; }
        
        public bool HasMoreTailChildComments { get; set; }
        
        public bool HasMoreHeadChildComments { get; set; }
        
        public string NextMaxId { get; set; }
        
        public string NextMinId { get; set; }
        
        public int NumTailChildComments { get; set; }

        public InstaComment ParentComment { get; set; }

        public List<InstaComment> ChildComments { get; set; } = new List<InstaComment>();
        
        internal string Status { get; set; }
    }
}
