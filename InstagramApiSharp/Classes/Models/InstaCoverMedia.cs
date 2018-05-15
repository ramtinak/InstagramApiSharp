using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaCoverMedia
    {
        public long Id { get; set; }

        public List<InstaImage> ImageVersions { get; set; }

        public int MediaType { get; set; }

        public int OriginalHeight { get; set; }

        public int OriginalWidth { get; set; }
    }
}