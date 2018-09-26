using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaCommentShortConverter : IObjectConverter<InstaCommentShort, InstaCommentShortResponse>
    {
        public InstaCommentShortResponse SourceObject { get; set; }

        public InstaCommentShort Convert()
        {
            if (SourceObject == null)
                return null;
            var shortComment = new InstaCommentShort
            {
                CommentLikeCount = SourceObject.CommentLikeCount,
                ContentType = (InstaContentType)Enum.Parse(typeof(InstaContentType), SourceObject.ContentType, true),
                CreatedAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAt),
                CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUtc),
                Pk = SourceObject.Pk,
                Status = SourceObject.Status,
                Text = SourceObject.Text,
                Type = SourceObject.Type,
                User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert(),
                HasLikedComment = SourceObject.HasLikedComment,
                MediaId = SourceObject.MediaId,
                ParentCommentId = SourceObject.ParentCommentId
            };
            return shortComment;
        }
    }
}
