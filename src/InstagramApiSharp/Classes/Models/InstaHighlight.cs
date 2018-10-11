/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaHighlightFeeds
    {
        public bool ShowEmptyState { get; set; }

        internal string Status { get; set; }

        public List<InstaHighlightFeed> Items { get; set; } = new List<InstaHighlightFeed>();
    }
    public class InstaHighlightSingleFeed : InstaHighlightFeed
    {
        public List<InstaStoryItem> Items { get; set; } = new List<InstaStoryItem>();
    }
    public class InstaHighlightFeed
    {
        public string HighlightId { get; set; }

        public int LatestReelMedia { get; set; }

        public object Seen { get; set; }
        
        public bool CanReply { get; set; }

        public object CanReshare { get; set; }

        public string ReelType { get; set; }

        public InstaHighlightCoverMedia CoverMedia { get; set; }

        public InstaUserShort User { get; set; }

        public int RankedPosition { get; set; }

        public string Title { get; set; }

        public int SeenRankedPosition { get; set; }

        public int PrefetchCount { get; set; }

        public int MediaCount { get; set; }
    }

    public class InstaHighlightCoverMedia
    {
        public InstaImage CroppedImage { get; set; }

        public float[] CropRect { get; set; }

        public string MediaId { get; set; }

        public InstaImage Image { get; set; }
    }
}