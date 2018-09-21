using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaDirectInbox
    {
        public bool HasOlder { get; set; }

        public long UnseenCountTs { get; set; }

        public long UnseenCount { get; set; }

        public string OldestCursor { get; set; }

        public bool BlendedInboxEnabled { get; set; }

        public List<InstaDirectInboxThread> Threads { get; set; }
        
    }
}