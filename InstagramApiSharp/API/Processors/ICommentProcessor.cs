using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.API.Processors
{
    public interface ICommentProcessor
    {
        Task<IResult<InstaCommentList>>
            GetMediaCommentsAsync(string mediaId, PaginationParameters paginationParameters);
        Task<IResult<InstaInlineCommentListResponse>>
           GetMediaInlineCommentsAsync(string mediaId, string targetCommentId, PaginationParameters paginationParameters);

        Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text);
        Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId);
        Task<IResult<InstaComment>> InlineCommentMediaAsync(string mediaId, string targetCommentId, string text);
        Task<IResult<bool>> EnableMediaCommentAsync(string mediaId);
        Task<IResult<bool>> DisableMediaCommentAsync(string mediaId);
        Task<IResult<bool>> GetMediaCommentLikersAsync(string mediaId);
        Task<IResult<bool>> ReportCommentAsync(string mediaId, string commentId);
    }
}