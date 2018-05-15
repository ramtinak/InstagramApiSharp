using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaTagFeed : InstaFeed
    {
        public List<InstaMedia> RankedMedias { get; set; } = new List<InstaMedia>();
    }
}