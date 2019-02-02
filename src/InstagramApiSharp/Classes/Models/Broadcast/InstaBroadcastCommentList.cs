/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBroadcastCommentList
    {
        public bool CommentLikesEnabled { get; set; }

        public List<InstaBroadcastComment> Comments { get; set; } = new List<InstaBroadcastComment>();

        public int CommentCount { get; set; }

        public InstaCaption Caption { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool HasMoreComments { get; set; }

        public bool HasMoreHeadloadComments { get; set; }

        public string MediaHeaderDisplay { get; set; }

        public int LiveSecondsPerComment { get; set; }

        public string IsFirstFetch { get; set; }

        public InstaBroadcastComment PinnedComment { get; set; }

        public object SystemComments { get; set; }

        public int CommentMuted { get; set; }
    }
}
