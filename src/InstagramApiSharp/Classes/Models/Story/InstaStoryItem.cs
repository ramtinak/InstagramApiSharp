using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryItem
    {
        public bool ShowOneTapTooltip { get; set; }

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

        public string StoryStickerIds { get; set; }

        public double TimezoneOffset { get; set; }

        public bool StoryIsSavedToArchive { get; set; }

        public double ViewerCount { get; set; }

        public double TotalViewerCount { get; set; }

        public string ViewerCursor { get; set; }

        public double HasSharedToFb { get; set; }

        public List<InstaReelMention> ReelMentions { get; set; } = new List<InstaReelMention>();

        public List<InstaReelMention> StoryHashtags { get; set; } = new List<InstaReelMention>();

        public List<InstaStoryLocation> StoryLocations { get; set; } = new List<InstaStoryLocation>();

        public DateTime TakenAt { get; set; }

        public DateTime ImportedTakenAt { get; set; }

        public string VideoDashManifest { get; set; }

        public bool SupportsReelReactions { get; set; }

        public List<InstaStoryCTA> StoryCTA { get; set; } = new List<InstaStoryCTA>();

        public List<InstaStoryFeedMedia> StoryFeedMedia { get; set; } = new List<InstaStoryFeedMedia>();

        public InstaUserShort User { get; set; }

        public double VideoDuration { get; set; }

        public List<InstaVideo> VideoList { get; set; } = new List<InstaVideo>();

        public List<InstaStoryPollItem> StoryPolls { get; set; } = new List<InstaStoryPollItem>();

        public List<InstaStorySliderItem> StorySliders { get; set; } = new List<InstaStorySliderItem>();

        public List<InstaStoryQuestionItem> StoryQuestions { get; set; } = new List<InstaStoryQuestionItem>();

        public List<InstaStoryQuestionInfo> StoryQuestionsResponderInfos { get; set; } = new List<InstaStoryQuestionInfo>();

        public List<InstaStoryPollVoterInfoItem> StoryPollVoters { get; set; } = new List<InstaStoryPollVoterInfoItem>();

        public List<InstaStorySliderVoterInfoItem> StorySliderVoters { get; set; } = new List<InstaStorySliderVoterInfoItem>();

        public List<InstaUserShort> Viewers { get; set; } = new List<InstaUserShort>();

        public List<InstaUserShort> Likers { get; set; } = new List<InstaUserShort>();

        public List<InstaComment> PreviewComments { get; set; } = new List<InstaComment>();

        public List<InstaStoryCountdownItem> Countdowns { get; set; } = new List<InstaStoryCountdownItem>();
    }
}