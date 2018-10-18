using System;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaRavenMedia
    {
        public InstaMediaType MediaType { get; set; }
    }
    public class InstaRavenMediaActionSummary
    {
        public InstaRavenType Type { get; set; }

        public DateTime ExpireTime { get; set; }

        public int Count { get; set; }
    }
    public enum InstaRavenType
    {
        Delivered,
        Opened
    }
}
