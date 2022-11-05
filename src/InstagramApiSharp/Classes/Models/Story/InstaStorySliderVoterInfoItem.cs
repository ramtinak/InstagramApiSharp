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

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStorySliderVoterInfoItem
    {
        public long SliderId { get; set; }

        public List<InstaStoryVoterItem> Voters { get; set; } = new List<InstaStoryVoterItem>();

        public string MaxId { get; set; }

        public bool MoreAvailable { get; set; }

        public DateTime LatestSliderVoteTime { get; set; }
    }
}
