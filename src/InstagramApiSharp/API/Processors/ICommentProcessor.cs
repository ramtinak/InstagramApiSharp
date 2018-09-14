using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.API.Processors
{
    public interface ICommentProcessor
    {
        /// <summary>
        ///     Get media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaCommentList>>
            GetMediaCommentsAsync(string mediaId, PaginationParameters paginationParameters);
        /// <summary>
        ///     Get media inline comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="targetCommentId">Target comment id</param>
       /// <param name="paginationParameters">Maximum amount of pages to load and start id</param>
        Task<IResult<InstaInlineCommentList>>
           GetMediaRepliesCommentsAsync(string mediaId, string targetCommentId, PaginationParameters paginationParameters);
        /// <summary>
        ///     Comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="text">Comment text</param>
        Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text);
        /// <summary>
        ///     Delete media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId);
        /// <summary>
        ///     Delete media comments(multiple)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentIds">Comment id</param>
        Task<IResult<bool>> DeleteMultipleCommentsAsync(string mediaId, params string[] commentIds);
        /// <summary>
        ///     Inline comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="targetCommentId">Target comment id</param>
        /// <param name="text">Comment text</param>
        Task<IResult<InstaComment>> ReplyCommentMediaAsync(string mediaId, string targetCommentId, string text);
        /// <summary>
        ///     Allow media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> EnableMediaCommentAsync(string mediaId);
        /// <summary>
        ///     Disable media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> DisableMediaCommentAsync(string mediaId);
        /// <summary>
        ///     Get media comments likers
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> GetMediaCommentLikersAsync(string mediaId);
        /// <summary>
        ///     Report media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> ReportCommentAsync(string mediaId, string commentId);
        /// <summary>
        ///     Like media comment
        /// </summary>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> LikeCommentAsync(string commentId);
        /// <summary>
        ///     Unlike media comment
        /// </summary>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> UnlikeCommentAsync(string commentId);
    }
}