namespace InstagramApiSharp.Classes.Models
{
    public class InstaWebLink
    {
        public string Text { get; set; }

        public InstaWebLinkContext LinkContext { get; set; }
    }
    public class InstaWebLinkContext
    {
        public string LinkUrl { get; set; }

        public string LinkTitle { get; set; }

        public string LinkSummary { get; set; }

        public string LinkImageUrl { get; set; }
    }
}
