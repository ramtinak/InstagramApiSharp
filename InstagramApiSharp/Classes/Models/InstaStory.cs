using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStory
    {
        public long TakenAtUnix { get; set; }

        public bool CanReply { get; set; }

        public DateTime ExpiringAt { get; set; }

        public InstaUserShort User { get; set; }

        public InstaUserShort Owner { get; set; }

        public string SourceToken { get; set; }

        public DateTime Seen { get; set; }

        public string LatestReelMedia { get; set; }

        public string Id { get; set; }

        public int RankedPosition { get; set; }

        public bool Muted { get; set; }

        public int SeenRankedPosition { get; set; }

        public List<InstaMedia> Items { get; set; } = new InstaMediaList();

        public int PrefetchCount { get; set; }

        public string SocialContext { get; set; }
    }
}