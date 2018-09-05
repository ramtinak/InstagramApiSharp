using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaCommentList : IInstaBaseList
    {
        public bool LikesEnabled { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool MoreHeadLoadAvailable { get; set; }

        public InstaCaption Caption { get; set; }

        public bool MoreComentsAvailable { get; set; }

        public List<InstaComment> Comments { get; set; } = new List<InstaComment>();

        public string NextId { get; set; }
    }
}