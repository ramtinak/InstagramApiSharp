namespace InstagramApiSharp.Classes.Models
{
    public class InstaInlineFollow
    {
        public bool IsOutgoingRequest { get; set; }
        public bool IsFollowing { get; set; }
        public InstaUserShort User { get; set; }
    }
}