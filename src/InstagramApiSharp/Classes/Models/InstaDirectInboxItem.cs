using InstagramApiSharp.Classes.ResponseWrappers;
using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaDirectInboxItem
    {
        public string Text { get; set; }

        public long UserId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string ItemId { get; set; }

        public InstaDirectThreadItemType ItemType { get; set; } = InstaDirectThreadItemType.Text;

        public InstaInboxMedia Media { get; set; }

        public InstaMedia MediaShare { get; set; }

        public string ClientContext { get; set; }

        public InstaStoryShare StoryShare { get; set; }

        public InstaMedia/*InstaRavenMedia*/ RavenMedia { get; set; }

        // raven media properties
        public string RavenViewMode { get; set; }

        public List<long> RavenSeenUserIds { get; set; }

        public int RavenReplayChainCount { get; set; }

        public int RavenSeenCount { get; set; }

        public InstaRavenMediaActionSummary RavenExpiringMediaActionSummary { get; set; }

        public InstaActionLog ActionLog { get; set; }

        public InstaUserShort ProfileMedia { get; set; }

        public List<InstaMedia> ProfileMediasPreview { get; set; }

        public InstaPlaceholder Placeholder { get; set; }

        public InstaWebLink LinkMedia { get; set; }

        public InstaLocation LocationMedia { get; set; }

        public InstaMedia FelixShareMedia { get; set; }

    }
}