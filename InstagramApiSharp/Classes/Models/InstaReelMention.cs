namespace InstagramApiSharp.Classes.Models
{
    public class InstaReelMention
    {
        public double Rotation { get; set; }

        public double Height { get; set; }

        public InstaHashtag Hashtag { get; set; }

        public bool IsPinned { get; set; }

        public double Width { get; set; }

        public InstaUserShort User { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
    }
}