/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryUploadOptions
    {
        public List<InstaStoryLocationUpload> Locations { get; set; } = new List<InstaStoryLocationUpload>();

        public List<InstaStoryHashtagUpload> Hashtags { get; set; } = new List<InstaStoryHashtagUpload>();

        public List<InstaStoryPollUpload> Polls { get; set; } = new List<InstaStoryPollUpload>();

        public InstaStorySliderUpload Slider { get; set; }

        public InstaStoryCountdownUpload Countdown { get; set; }

        internal InstaMediaStoryUpload MediaStory { get; set; }

        public List<InstaStoryMentionUpload> Mentions { get; set; } = new List<InstaStoryMentionUpload>();

        public List<InstaStoryQuestionUpload> Questions { get; set; } = new List<InstaStoryQuestionUpload>();
    }
}
