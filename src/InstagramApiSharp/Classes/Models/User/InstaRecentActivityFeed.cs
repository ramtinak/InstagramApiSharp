using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaRecentActivityFeed
    {
        public long ProfileId { get; set; }

        public string ProfileImage { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }

        public string RichText { get; set; }

        public List<InstaLink> Links { get; set; } = new List<InstaLink>();
        public InstaInlineFollow InlineFollow { get; set; }
        public int Type { get; set; }

        public string Pk { get; set; }

        public List<InstaActivityMedia> Medias { get; set; } = new List<InstaActivityMedia>();
    }
}