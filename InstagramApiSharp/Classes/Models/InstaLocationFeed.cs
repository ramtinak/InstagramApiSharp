namespace InstagramApiSharp.Classes.Models
{
    public class InstaLocationFeed : InstaBaseFeed
    {
        public InstaMediaList RankedMedias { get; set; } = new InstaMediaList();
        public InstaStory Story { get; set; }
        public InstaLocation Location { get; set; }

        public long MediaCount { get; set; }
    }
}