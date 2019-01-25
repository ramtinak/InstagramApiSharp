using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{

    public class InstaBlockedUsers : InstaDefault
    {
        public List<InstaBlockedUserInfo> BlockedList { get; set; } = new List<InstaBlockedUserInfo>();
        
        public int? PageSize { get; set; }

        public string MaxId { get; set; }
    }
}
