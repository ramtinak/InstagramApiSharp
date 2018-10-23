namespace InstagramApiSharp.Classes.Models
{
    public class InstaExploreFeed : InstaBaseFeed
    {
        public InstaStoryTray StoryTray { get; set; } = new InstaStoryTray();
        public InstaChannel Channel { get; set; } = new InstaChannel();
        public string MaxId { get; set; }
        public string RankToken { get; set; }
        public bool MoreAvailable { get; set; }
        public int ResultsCount { get; set; }
        public bool AutoLoadMoreEnabled { get; set; }
    }
}