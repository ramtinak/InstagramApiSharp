using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
namespace InstagramApiSharp.Converters
{
    internal class InstaInlineCommentListConverter
        : IObjectConverter<InstaInlineCommentList, InstaInlineCommentListResponse>
    {
        public InstaInlineCommentListResponse SourceObject { get; set; }

        public InstaInlineCommentList Convert()
        {
            if (SourceObject == null)
                return null;
            var inline = new InstaInlineCommentList
            {
                CanViewMorePreviewComments = SourceObject.CanViewMorePreviewComments,
                CaptionIsEdited = SourceObject.CaptionIsEdited,
                CommentCount = SourceObject.CommentCount,
                CommentLikesEnabled = SourceObject.CommentLikesEnabled,
                HasMoreComments = SourceObject.HasMoreComments,
                HasMoreHeadloadComments = SourceObject.HasMoreHeadloadComments,
                InitiateAtTop = SourceObject.InitiateAtTop,
                InsertNewCommentToTop = SourceObject.InsertNewCommentToTop,
                MediaHeaderDisplay = SourceObject.MediaHeaderDisplay,
                ThreadingEnabled = SourceObject.ThreadingEnabled
            };
            if (SourceObject.Comments != null && SourceObject.Comments.Any())
            {
                foreach (var cmt in SourceObject.Comments)
                {
                    try
                    {
                        inline.Comments.Add(ConvertersFabric.Instance.GetCommentConverter(cmt).Convert());
                    }
                    catch { }
                }
            }

            if (SourceObject.PreviewComments != null && SourceObject.PreviewComments.Any())
            {
                foreach (var cmt in SourceObject.PreviewComments)
                {
                    try
                    {
                        inline.PreviewComments.Add(ConvertersFabric.Instance.GetCommentConverter(cmt).Convert());
                    }
                    catch { }
                }
            }
            if (SourceObject.Caption != null)
            {
                try
                {
                    inline.Caption = ConvertersFabric.Instance.GetCaptionConverter(SourceObject.Caption).Convert();
                }
                catch { }
            }
            if (!string.IsNullOrEmpty(SourceObject.NextMinId))
            {
                try
                {
                    var convertedNextId = JsonConvert.DeserializeObject<InstaInlineCommentNextIdResponse>(SourceObject.NextMinId);
                    inline.NextMinId = convertedNextId.BifilterToken;
                }
                catch { }
            }
            if (!string.IsNullOrEmpty(SourceObject.NextMaxId))
            {
                try
                {
                    var convertedNextId = JsonConvert.DeserializeObject<InstaInlineCommentNextIdResponse>(SourceObject.NextMaxId);
                    inline.NextMaxId = convertedNextId.ServerCursor;
                }
                catch { }
            }

            //var comment = new InstaComment
            //{
            //    BitFlags = SourceObject.BitFlags,
            //    ContentType = (InstaContentType)Enum.Parse(typeof(InstaContentType), SourceObject.ContentType, true),
            //    CreatedAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAt),
            //    CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUtc),
            //    LikesCount = SourceObject.LikesCount,
            //    Pk = SourceObject.Pk,
            //    Status = SourceObject.Status,
            //    Text = SourceObject.Text,
            //    Type = SourceObject.Type,
            //    UserId = SourceObject.UserId,
            //    User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert(),
            //    DidReportAsSpam = SourceObject.DidReportAsSpam,
            //    ChildCommentCount = SourceObject.ChildCommentCount,
            //    HasLikedComment = SourceObject.HasLikedComment,
            //    HasMoreHeadChildComments = SourceObject.HasMoreHeadChildComments,
            //    HasMoreTailChildComments = SourceObject.HasMoreTailChildComments,
            //    NextMaxChildCursor = SourceObject.NextMaxChildCursor,
            //    NumTailChildComments = SourceObject.NumTailChildComments
            //};
            return inline;
        }
    }
}
