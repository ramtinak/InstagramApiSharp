using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBlockedUserInfo
    {
        public string UserName { get; set; }

        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public bool IsPrivate { get; set; }

        public long Pk { get; set; }

        public long BlockedAt { get; set; }
    }
}
