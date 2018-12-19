using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Comments api functions.
    /// </summary>
    public interface ICommentProcessor
    {

        /// <summary>
        ///     Block an user from commenting to medias
        /// </summary>
        /// <param name="userIds">User ids (pk)</param>
        Task<IResult<bool>> BlockUserCommentingAsync(params long[] userIds);

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
        ///     Disable media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> DisableMediaCommentAsync(string mediaId);

        /// <summary>
        ///     Allow media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> EnableMediaCommentAsync(string mediaId);

        /// <summary>
        ///     Get blocked users from commenting
        /// </summary>
        Task<IResult<InstaUserShortList>> GetBlockedCommentersAsync();

        /// <summary>
        ///     Get media comments likers
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<InstaLikersList>> GetMediaCommentLikersAsync(string mediaId);

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
        ///     Like media comment
        /// </summary>
        /// <param name="commentId">pass Pk.Tostring() for commentId</param>
        Task<IResult<bool>> LikeCommentAsync(string commentId);

        /// <summary>
        ///     Inline comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="targetCommentId">Target comment id</param>
        /// <param name="text">Comment text</param>
        Task<IResult<InstaComment>> ReplyCommentMediaAsync(string mediaId, string targetCommentId, string text);

        /// <summary>
        ///     Report media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> ReportCommentAsync(string mediaId, string commentId);

        /// <summary>
        ///     Unblock an user from commenting to medias
        /// </summary>
        /// <param name="userIds">User ids (pk)</param>
        Task<IResult<bool>> UnblockUserCommentingAsync(params long[] userIds);

        /// <summary>
        ///     Unlike media comment
        /// </summary>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> UnlikeCommentAsync(string commentId);

        /// <summary>
        ///     Translate comment or captions
        ///     <para>Note: use this function to translate captions too! (i.e: <see cref="InstaCaption.Pk"/>)</para>
        /// </summary>
        /// <param name="commentIds">Comment id(s) (Array of <see cref="InstaComment.Pk"/>)</param>
        Task<IResult<InstaTranslateList>> TranslateCommentAsync(params long[] commentIds);
    }
}