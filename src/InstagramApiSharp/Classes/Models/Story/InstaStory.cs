using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStory
    {
        public long TakenAtUnix { get; set; }

        public bool CanReply { get; set; }

        public DateTime ExpiringAt { get; set; }

        public InstaUserShortFriendshipFull User { get; set; }

        public InstaUserShort Owner { get; set; }

        public string SourceToken { get; set; }

        public DateTime Seen { get; set; }

        public string LatestReelMedia { get; set; }

        public string Id { get; set; }

        public int RankedPosition { get; set; }

        public bool Muted { get; set; }

        public int SeenRankedPosition { get; set; }

        public List<InstaStoryItem> Items { get; set; } = new List<InstaStoryItem>();

        public int PrefetchCount { get; set; }

        public string SocialContext { get; set; }




        public string ClientCacheKey { get; set; }

        public double? CaptionPosition { get; set; }

        public bool IsReelMedia { get; set; }

        public double VideoDuration { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool PhotoOfYou { get; set; }

        public bool CanViewerSave { get; set; }

        public DateTime ImportedTakenAt { get; set; }

        public bool CanReshare { get; set; }

        public bool SupportsReelReactions { get; set; }
        
        public bool HasSharedToFb { get; set; }

        public List<InstaReelMention> StoryHashtags { get; set; } = new List<InstaReelMention>();

        public List<InstaStoryLocation> StoryLocation { get; set; } = new List<InstaStoryLocation>();
    }
}