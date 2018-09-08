using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

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
                HasMoreTailChildComments = SourceObject.HasMoreTailChildComments,
                NextMaxChildCursor = SourceObject.NextMaxChildCursor,
                NumTailChildComments = SourceObject.NumTailChildComments
            };
            return comment;
        }
    }
}