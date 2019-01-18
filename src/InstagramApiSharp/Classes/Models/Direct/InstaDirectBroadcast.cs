/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaDirectBroadcast
    {
        public InstaBroadcast Broadcast { get; set; }

        public string Text { get; set; }

        public bool IsLinked { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}
