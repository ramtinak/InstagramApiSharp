using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface ICommentProcessor
    {
        Task<IResult<InstaCommentList>>
            GetMediaCommentsAsync(string mediaId, PaginationParameters paginationParameters);

        Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text);
        Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId);
    }
}