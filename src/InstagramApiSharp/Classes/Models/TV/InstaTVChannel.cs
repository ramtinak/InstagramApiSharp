/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Enums;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaTVChannel
    {
        public InstaTVChannelType Type { get; set; }

        public string Title { get; set; }

        public string Id { get; set; }

        public List<InstaMedia> Items { get; set; } = new List<InstaMedia>();

        public bool HasMoreAvailable { get; set; }

        public string MaxId { get; set; }
        //public Seen_State1 seen_state { get; set; }

        public InstaTVUser UserDetail { get; set; }
    }
    public class InstaTVSelfChannel : InstaTVChannel
    {
        public InstaTVUser User { get; set; }
    }
}
