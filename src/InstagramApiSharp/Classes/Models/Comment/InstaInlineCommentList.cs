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
