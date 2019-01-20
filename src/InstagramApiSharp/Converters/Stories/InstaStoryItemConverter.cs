using System;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryItemConverter : IObjectConverter<InstaStoryItem, InstaStoryItemResponse>
    {
        public InstaStoryItemResponse SourceObject { get; set; }

        public InstaStoryItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var instaStory = new InstaStoryItem
            {
                CanViewerSave = SourceObject.CanViewerSave,
                CaptionIsEdited = SourceObject.CaptionIsEdited,
                CaptionPosition = SourceObject.CaptionPosition,
                Code = SourceObject.Code,
                CommentCount = SourceObject.CommentCount,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.ExpiringAt),
                FilterType = SourceObject.FilterType,
                HasAudio = SourceObject.HasAudio ?? false,
                HasLiked = SourceObject.HasLiked,
                HasMoreComments = SourceObject.HasMoreComments,
                Id = SourceObject.Id,
                IsReelMedia = SourceObject.IsReelMedia,
                LikeCount = SourceObject.LikeCount,
                MaxNumVisiblePreviewComments = SourceObject.MaxNumVisiblePreviewComments,
                MediaType = SourceObject.MediaType,
                OriginalHeight = SourceObject.OriginalHeight,
                OriginalWidth = SourceObject.OriginalWidth,
                PhotoOfYou = SourceObject.PhotoOfYou,
                Pk = SourceObject.Pk,
                TakenAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.TakenAt),
                VideoDuration = SourceObject.VideoDuration ?? 0,
                AdAction = SourceObject.AdAction,
                SupportsReelReactions = SourceObject.SupportsReelReactions,
                ShowOneTapTooltip = SourceObject.ShowOneTapTooltip
            };

            if (SourceObject.User != null)
                instaStory.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert();

            if (SourceObject.Caption != null)
                instaStory.Caption = ConvertersFabric.Instance.GetCaptionConverter(SourceObject.Caption).Convert();

            if (SourceObject.Images?.Candidates != null)
                foreach (var image in SourceObject.Images.Candidates)
                    instaStory.ImageList.Add(new InstaImage(image.Url, int.Parse(image.Width),
                        int.Parse(image.Height)));

            if (SourceObject.VideoVersions != null && SourceObject.VideoVersions.Any())
                foreach (var video in SourceObject.VideoVersions)
                    instaStory.VideoList.Add(new InstaVideo(video.Url, int.Parse(video.Width), int.Parse(video.Height),
                        video.Type));

            if (SourceObject.ReelMentions != null && SourceObject.ReelMentions.Any())
                foreach (var mention in SourceObject.ReelMentions)
                    instaStory.ReelMentions.Add(ConvertersFabric.Instance.GetMentionConverter(mention).Convert());
            if (SourceObject.StoryHashtags != null && SourceObject.StoryHashtags.Any())
                foreach (var hashtag in SourceObject.StoryHashtags)
                    instaStory.StoryHashtags.Add(ConvertersFabric.Instance.GetMentionConverter(hashtag).Convert());

            if (SourceObject.StoryLocations != null && SourceObject.StoryLocations.Any())
                foreach (var location in SourceObject.StoryLocations)
                    instaStory.StoryLocations.Add(ConvertersFabric.Instance.GetLocationConverter(location).Convert());

            if (SourceObject.StoryFeedMedia != null && SourceObject.StoryFeedMedia.Any())
                foreach (var storyFeed in SourceObject.StoryFeedMedia)
                    instaStory.StoryFeedMedia.Add(ConvertersFabric.Instance.GetStoryFeedMediaConverter(storyFeed).Convert());

            if (SourceObject.StoryCTA != null && SourceObject.StoryCTA.Any())
                foreach (var cta in SourceObject.StoryCTA)
                    if (cta.Links != null && cta.Links.Any())
                        foreach (var link in cta.Links)
                            instaStory.StoryCTA.Add(ConvertersFabric.Instance.GetStoryCtaConverter(link).Convert());

            if (SourceObject.StoryPolls?.Count > 0)
                foreach (var poll in SourceObject.StoryPolls)
                    instaStory.StoryPolls.Add(ConvertersFabric.Instance.GetStoryPollItemConverter(poll).Convert());

            if (SourceObject.StoryPollVoters?.Count > 0)
                foreach (var voter in SourceObject.StoryPollVoters)
                    instaStory.StoryPollVoters.Add(ConvertersFabric.Instance.GetStoryPollVoterInfoItemConverter(voter).Convert());

            if (SourceObject.Viewers?.Count > 0)
                foreach (var viewer in SourceObject.Viewers)
                    instaStory.Viewers.Add(ConvertersFabric.Instance.GetUserShortConverter(viewer).Convert());

            return instaStory;
        }
    }
}