using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaFriendshipShortStatusList : List<InstaFriendshipShortStatus> { }

    public class InstaFriendshipShortStatus
    {
        public long Pk { get; set; }

        public bool Following { get; set; }

        public bool IsPrivate { get; set; }

        public bool IncomingRequest { get; set; }

        public bool OutgoingRequest { get; set; }

        public bool IsBestie { get; set; }
    }
}
