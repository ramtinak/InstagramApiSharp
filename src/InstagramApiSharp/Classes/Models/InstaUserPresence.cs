using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserPresence
    {
        public bool IsActive { get; set; }

        public DateTime LastActivity { get; set; }

        public long Pk { get; set; }
    }
    public class InstaUserPresenceList : List<InstaUserPresence> { }
}
