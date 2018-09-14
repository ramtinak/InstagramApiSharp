using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;
using System.Linq;
using System.Collections.Generic;
namespace InstagramApiSharp.Converters
{
    internal class InstaCommentConverter
        : IObjectConverter<InstaComment, InstaCommentResponse>
    {
        public InstaCommentResponse SourceObject { get; set; }

        public InstaComment Convert()
        {
            var comment = new InstaComment
            {
                BitFlags = SourceObject.BitFlags,
                ContentType = (InstaContentType) Enum.Parse(typeof(InstaContentType), SourceObject.ContentType, true),
                CreatedAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAt),
                CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUtc),
                LikesCount = SourceObject.LikesCount,
                Pk = SourceObject.Pk,
                Status = SourceObject.Status,
                Text = SourceObject.Text,
                Type = SourceObject.Type,
                UserId = SourceObject.UserId,
                User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert(),
                DidReportAsSpam = SourceObject.DidReportAsSpam,
                ChildCommentCount = SourceObject.ChildCommentCount,
                HasLikedComment = SourceObject.HasLikedComment,
                HasMoreHeadChildComments = SourceObject.HasMoreHeadChildComments,
                HasMoreTailChildComments = SourceObject.HasMoreTailChildComments
            };
            if (SourceObject.OtherPreviewUsers != null && SourceObject.OtherPreviewUsers.Any())
            {
                if (comment.OtherPreviewUsers == null)
                    comment.OtherPreviewUsers = new List<InstaUserShort>();
                foreach (var user in SourceObject.OtherPreviewUsers)
                    comment.OtherPreviewUsers.Add(ConvertersFabric.Instance.GetUserShortConverter(user).Convert());
            }
            if (SourceObject.PreviewChildComments != null && SourceObject.PreviewChildComments.Any())
            {
                if (comment.PreviewChildComments == null)
                    comment.PreviewChildComments = new List<InstaCommentShort>();

                foreach (var cm in SourceObject.PreviewChildComments)
                    comment.PreviewChildComments.Add(ConvertersFabric.Instance.GetCommentShortConverter(cm).Convert());
            }
            return comment;
        }
    }
}