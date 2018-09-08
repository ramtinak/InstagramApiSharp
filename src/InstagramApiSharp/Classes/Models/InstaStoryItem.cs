using InstagramApiSharp.Classes.ResponseWrappers;
using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryItem
    {
        public bool HasLiked { get; set; }

        public string Code { get; set; }

        public InstaCaption Caption { get; set; }

        public bool CanReshare { get; set; }

        public string AdAction { get; set; }

        public bool CanViewerSave { get; set; }

        public long CaptionPosition { get; set; }

        public bool CaptionIsEdited { get; set; }

        public DateTime DeviceTimestamp { get; set; }

        public bool CommentLikesEnabled { get; set; }

        public long CommentCount { get; set; }

        public bool CommentThreadingEnabled { get; set; }

        public long FilterType { get; set; }

        public DateTime ExpiringAt { get; set; }

        public bool HasAudio { get; set; }

        public string LinkText { get; set; }

        public long Pk { get; set; }

        public string Id { get; set; }

        public bool HasMoreComments { get; set; }

        public List<InstaImage> ImageList { get; set; } = new List<InstaImage>();

        public long LikeCount { get; set; }

        public bool IsReelMedia { get; set; }

        public string OrganicTrackingToken { get; set; }

        public long MediaType { get; set; }

        public long MaxNumVisiblePreviewComments { get; set; }

        public long NumberOfQualities { get; set; }

        public long OriginalWidth { get; set; }

        public long OriginalHeight { get; set; }

        public bool PhotoOfYou { get; set; }

        public List<InstaReelMention> ReelMentions { get; set; } = new List<InstaReelMention>();

        public List<InstaReelMention> StoryHashtags { get; set; } = new List<InstaReelMention>();

        public List<InstaLocation> StoryLocations { get; set; } = new List<InstaLocation>();

        public DateTime TakenAt { get; set; }

        public string VideoDashManifest { get; set; }

        public bool SupportsReelReactions { get; set; }

        public List<StoryCTA> StoryCTA { get; set; }

        public InstaUserShort User { get; set; }

        public double VideoDuration { get; set; }

        public List<InstaVideo> VideoList { get; set; } = new List<InstaVideo>();
    }
}