using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaCommentList
    {
        public bool LikesEnabled { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool MoreHeadLoadAvailable { get; set; }

        public InstaCaption Caption { get; set; }

        public bool MoreCommentsAvailable { get; set; }

        public List<InstaComment> Comments { get; set; } = new List<InstaComment>();

        public string NextMaxId { get; set; }

        public int CommentsCount { get; set; }
                
        public bool ThreadingEnabled { get; set; }
        
        public string MediaHeaderDisplay { get; set; }
        
        public bool InitiateAtTop { get; set; }
        
        public bool InsertNewCommentToTop { get; set; }
        
        public List<InstaComment> PreviewComments { get; set; }
        
        public bool CanViewMorePreviewComments { get; set; }
        
        public string NextMinId { get; set; }




    }
}