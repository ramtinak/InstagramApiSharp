using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaCommentListConverter : IObjectConverter<InstaCommentList, InstaCommentListResponse>
    {
        public InstaCommentListResponse SourceObject { get; set; }

        public InstaCommentList Convert()
        {
            var commentList = new InstaCommentList
            {
                Caption = SourceObject.Caption != null
                    ? ConvertersFabric.Instance.GetCaptionConverter(SourceObject.Caption).Convert()
                    : null,
                CaptionIsEdited = SourceObject.CaptionIsEdited,
                LikesEnabled = SourceObject.LikesEnabled,
                MoreComentsAvailable = SourceObject.MoreComentsAvailable,
                MoreHeadLoadAvailable = SourceObject.MoreHeadLoadAvailable,
                NextId = SourceObject.NextMaxId
            };
            if (SourceObject.Comments == null || !(SourceObject?.Comments?.Count > 0)) return commentList;
            foreach (var commentResponse in SourceObject.Comments)
            {
                var converter = ConvertersFabric.Instance.GetCommentConverter(commentResponse);
                commentList.Comments.Add(converter.Convert());
            }

            return commentList;
        }
    }
}