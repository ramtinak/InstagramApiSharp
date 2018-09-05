using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryFeed
    {
        public int FaceFilterNuxVersion { get; set; }

        public bool HasNewNuxStory { get; set; }

        public string StoryRankingToken { get; set; }

        public int StickerVersion { get; set; }

        public List<InstaReelFeed> Items { get; set; } = new List<InstaReelFeed>();
    }
}