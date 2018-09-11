using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaDirectInboxThread
    {
        public bool Muted { get; set; }

        public List<InstaUserShort> Users { get; set; }

        public string Title { get; set; }

        public string OldestCursor { get; set; }

        public DateTime LastActivity { get; set; }

        public string VieweId { get; set; }
        public string ThreadId { get; set; }
        public bool HasOlder { get; set; }

        public InstaUserShort Inviter { get; set; }
        public bool Named { get; set; }
        public bool Pending { get; set; }

        public bool Canonical { get; set; }

        public bool HasNewer { get; set; }


        public bool IsSpam { get; set; }


        public InstaDirectThreadType ThreadType { get; set; }


        public List<InstaDirectInboxItem> Items { get; set; }



        public bool IsPin { get; set; }

        public bool ValuedRequest { get; set; }

        public long PendingScore { get; set; }

        public bool VCMuted { get; set; }

        public bool IsGroup { get; set; }

        public int ReshareSendCount { get; set; }

        public int ReshareReceiveCount { get; set; }

        public int ExpiringMediaSendCount { get; set; }

        public int ExpiringMediaReceiveCount { get; set; }

        public List<InstaLastSeen> LastSeenAt { get; set; }
    }
}