/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryPollStickerItem
    {
        public string Id { get; set; }

        public long PollId { get; set; }

        public string Question { get; set; }

        public List<InstaStoryTalliesItem> Tallies { get; set; } = new List<InstaStoryTalliesItem>();

        public bool ViewerCanVote { get; set; }

        public bool IsSharedResult { get; set; }

        public bool Finished { get; set; }

        public long ViewerVote { get; set; } = 0;
    }
}
