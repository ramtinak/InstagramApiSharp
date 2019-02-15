using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaInboxMedia
    {
        public List<InstaImage> Images { get; set; } = new List<InstaImage>();
        public long OriginalWidth { get; set; }
        public long OriginalHeight { get; set; }
        public InstaMediaType MediaType { get; set; }
        public List<InstaVideo> Videos { get; set; } = new List<InstaVideo>();
    }
}