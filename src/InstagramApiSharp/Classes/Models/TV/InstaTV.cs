/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaTV
    {
        public List<InstaTVChannel> Channels { get; set; } = new List<InstaTVChannel>();

        public InstaTVSelfChannel MyChannel { get; set; }

        internal string Status { get; set; }
    }
}
