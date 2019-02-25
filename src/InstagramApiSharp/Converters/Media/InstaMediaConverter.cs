using System;
using System.Globalization;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaMediaConverter : IObjectConverter<InstaMedia, InstaMediaItemResponse>
    {
        public InstaMediaItemResponse SourceObject { get; set; }

        public InstaMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var media = new InstaMedia
            {
                InstaIdentifier = SourceObject.InstaIdentifier,
                Code = SourceObject.Code,
                Pk = SourceObject.Pk,
                ClientCacheKey = SourceObject.ClientCacheKey,
                CommentsCount = SourceObject.CommentsCount,
                HasLiked = SourceObject.HasLiked,
                PhotoOfYou = SourceObject.PhotoOfYou,
                TrackingToken = SourceObject.TrackingToken,
                TakenAtUnix = long.Parse(string.IsNullOrEmpty(SourceObject.TakenAtUnixLike) ? "0" : SourceObject.TakenAtUnixLike),
                Height = SourceObject.Height,
                LikesCount = SourceObject.LikesCount,
                MediaType = SourceObject.MediaType,
                FilterType = SourceObject.FilterType,
                Width = SourceObject.Width,
                HasAudio = SourceObject.HasAudio,
                ViewCount = int.Parse(SourceObject.ViewCount.ToString(CultureInfo.InvariantCulture)),
                IsCommentsDisabled = SourceObject.IsCommentsDisabled,
                // new properties>
                CanViewerReshare = SourceObject.CanViewerReshare,
                CanViewerSave = SourceObject.CanViewerSave,
                CanViewMorePreviewComments = SourceObject.CanViewMorePreviewComments,
                CommentLikesEnabled = SourceObject.CommentLikesEnabled,
                MaxNumVisiblePreviewComments = SourceObject.MaxNumVisiblePreviewComments,
                HasMoreComments = SourceObject.HasMoreComments,
                CommentThreadingEnabled = SourceObject.CommentThreadingEnabled,
                Title = SourceObject.Title,
                ProductType = SourceObject.ProductType,
                NearlyCompleteCopyrightMatch = SourceObject.NearlyCompleteCopyrightMatch ?? false,
                NumberOfQualities = SourceObject.NumberOfQualities ?? 0,
                VideoDuration = SourceObject.VideoDuration ?? 0,
                HasViewerSaved = SourceObject.HasViewerSaved,
                DirectReplyToAuthorEnabled = SourceObject.DirectReplyToAuthorEnabled ?? false
            };
            if (!string.IsNullOrEmpty(SourceObject.TakenAtUnixLike))
                media.TakenAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.TakenAtUnixLike);

            if (!string.IsNullOrEmpty(SourceObject.DeviceTimeStampUnixLike))
                media.DeviceTimeStamp = DateTimeHelper.UnixTimestampToDateTime(SourceObject.DeviceTimeStampUnixLike);

            if (SourceObject.CarouselMedia != null)
                media.Carousel = ConvertersFabric.Instance.GetCarouselConverter(SourceObject.CarouselMedia).Convert();

            if (SourceObject.User != null)
                media.User = ConvertersFabric.Instance.GetUserConverter(SourceObject.User).Convert();

            if (SourceObject.Caption != null)
                media.Caption = ConvertersFabric.Instance.GetCaptionConverter(SourceObject.Caption).Convert();

            if (SourceObject.NextMaxId != null) media.NextMaxId = SourceObject.NextMaxId;

            if (SourceObject.Likers?.Count > 0)
                foreach (var liker in SourceObject.Likers)
                    media.Likers.Add(ConvertersFabric.Instance.GetUserShortConverter(liker).Convert());

            if (SourceObject.UserTagList?.In?.Count > 0)
                foreach (var tag in SourceObject.UserTagList.In)
                    media.UserTags.Add(ConvertersFabric.Instance.GetUserTagConverter(tag).Convert());

            if (SourceObject.ProductTags?.In?.Count > 0)
                foreach (var tag in SourceObject.ProductTags.In)
                    media.ProductTags.Add(ConvertersFabric.Instance.GetProductTagContainerConverter(tag).Convert());

            if (SourceObject.PreviewComments?.Count > 0)
                foreach (var comment in SourceObject.PreviewComments)
                    media.PreviewComments.Add(ConvertersFabric.Instance.GetCommentConverter(comment).Convert());

            if (SourceObject.Location != null)
                media.Location = ConvertersFabric.Instance.GetLocationConverter(SourceObject.Location).Convert();

            if (SourceObject.Images?.Candidates == null) return media;

            foreach (var image in SourceObject.Images.Candidates)
                media.Images.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));

            if (SourceObject.Videos == null) return media;

            foreach (var video in SourceObject.Videos)
                media.Videos.Add(new InstaVideo(video.Url, int.Parse(video.Width), int.Parse(video.Height),
                    video.Type));

            return media;
        }
    }
}