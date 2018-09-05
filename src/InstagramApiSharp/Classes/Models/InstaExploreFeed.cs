namespace InstagramApiSharp.Classes.Models
{
    public class InstaExploreFeed : InstaBaseFeed
    {
        public InstaStoryTray StoryTray { get; set; } = new InstaStoryTray();
        public InstaChannel Channel { get; set; } = new InstaChannel();
    }
}