/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
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
                ChildCommentCount = SourceObject.ChildCommentCount,
                HasMoreHeadChildComments = SourceObject.HasMoreHeadChildComments,
                HasMoreTailChildComments = SourceObject.HasMoreTailChildComments,
                NumTailChildComments = SourceObject.NumTailChildComments,
                NextMaxId = SourceObject.NextMaxId,
                NextMinId = SourceObject.NextMinId
            };
            if (SourceObject.ParentComment != null)
            {
                try
                {
                    inline.ParentComment = ConvertersFabric.Instance.GetCommentConverter(SourceObject.ParentComment).Convert();
                }
                catch { }

            }
            if (SourceObject.ChildComments != null && SourceObject.ChildComments.Any())
            {
                foreach (var cmt in SourceObject.ChildComments)
                {
                    try
                    {
                        inline.ChildComments.Add(ConvertersFabric.Instance.GetCommentConverter(cmt).Convert());
                    }
                    catch { }
                }
            }
            //if (!string.IsNullOrEmpty(SourceObject.NextMinId))
            //{
            //    try
            //    {
            //        var convertedNextId = JsonConvert.DeserializeObject<InstaInlineCommentNextIdResponse>(SourceObject.NextMinId);
            //        inline.NextMinId = convertedNextId.BifilterToken;
            //    }
            //    catch { }
            //}
            //if (!string.IsNullOrEmpty(SourceObject.NextMaxId))
            //{
            //    try
            //    {
            //        var convertedNextId = JsonConvert.DeserializeObject<InstaInlineCommentNextIdResponse>(SourceObject.NextMaxId);
            //        inline.NextMaxId = convertedNextId.ServerCursor;
            //    }
            //    catch { }
            //}
            return inline;
        }
    }
}
