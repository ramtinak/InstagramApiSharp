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
    public class InstaBroadcastComment : InstaBroadcastSendComment
    {
        public long UserId { get; set; }

        public int BitFlags { get; set; }

        public bool DidReportAsSpam { get; set; }

        public string InlineComposerDisplayCondition { get; set; }
    }
}
