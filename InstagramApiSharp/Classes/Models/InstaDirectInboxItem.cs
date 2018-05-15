using System;

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

        public Guid ClientContext { get; set; }
    }
}