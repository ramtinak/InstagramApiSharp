/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryPollVoterInfoItem
    {
        public long PollId { get; set; }

        public List<InstaStoryVoterItem> Voters { get; set; } = new List<InstaStoryVoterItem>();

        public string MaxId { get; set; }

        public bool MoreAvailable { get; set; }

        public DateTime LatestPollVoteTime { get; set; }
    }
}
