using System;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaCaption
    {
        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public DateTime CreatedAt { get; set; }

        public InstaUserShort User { get; set; }

        public string Text { get; set; }

        public string MediaId { get; set; }

        public string Pk { get; set; }

        public int Type { get; set; }

        public int BitFlags { get; set; }

        public bool DidReportAsSpam { get; set; }

        public bool ShareEnabled { get; set; }

        public bool HasTranslation { get; set; }
    }
}