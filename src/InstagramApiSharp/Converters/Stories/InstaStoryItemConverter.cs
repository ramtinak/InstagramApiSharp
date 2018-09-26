using System;
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
                StoryCTA = SourceObject.StoryCTA
            };

            if (SourceObject.User != null)
                instaStory.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert();

            if (SourceObject.Caption != null)
                instaStory.Caption = ConvertersFabric.Instance.GetCaptionConverter(SourceObject.Caption).Convert();

            if (SourceObject.Images?.Candidates != null)
                foreach (var image in SourceObject.Images.Candidates)
                    instaStory.ImageList.Add(new InstaImage(image.Url, int.Parse(image.Width),
                        int.Parse(image.Height)));

            if (SourceObject.VideoVersions != null)
                foreach (var video in SourceObject.VideoVersions)
                    instaStory.VideoList.Add(new InstaVideo(video.Url, int.Parse(video.Width), int.Parse(video.Height),
                        video.Type));

            if (SourceObject.ReelMentions != null)
                foreach (var mention in SourceObject.ReelMentions)
                    instaStory.ReelMentions.Add(ConvertersFabric.Instance.GetMentionConverter(mention).Convert());
            if (SourceObject.StoryHashtags != null)
                foreach (var hashtag in SourceObject.StoryHashtags)
                    instaStory.StoryHashtags.Add(ConvertersFabric.Instance.GetMentionConverter(hashtag).Convert());

            if (SourceObject.StoryLocations != null)
                foreach (var location in SourceObject.StoryLocations)
                    instaStory.StoryLocations.Add(ConvertersFabric.Instance.GetLocationConverter(location).Convert());

            return instaStory;
        }
    }
}