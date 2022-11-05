/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

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
