namespace InstagramApiSharp.Classes.Models
{
    public class InstaChannel
    {
        public string Title { get; set; }
        public string ChannelId { get; set; }
        public string ChannelType { get; set; }
        public string Header { get; set; }
        public string Context { get; set; }
        public InstaMedia Media { get; set; }
    }
}