using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaCarouselItem
    {
        public string InstaIdentifier { get; set; }

        public InstaMediaType MediaType { get; set; }

        public List<InstaImage> Images { get; set; } = new List<InstaImage>();

        public List<InstaVideo> Videos { get; set; } = new List<InstaVideo>();

        public int Width { get; set; }

        public int Height { get; set; }

        public string Pk { get; set; }

        public string CarouselParentId { get; set; }

        public List<InstaUserTag> UserTags { get; set; } = new List<InstaUserTag>();
    }
}