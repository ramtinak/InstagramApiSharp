using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface IFeedProcessor
    {
        /// <summary>
        ///     Get tag feed by tag value asynchronously
        /// </summary>
        /// <param name="tag">Tag value</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaTagFeed" />
        /// </returns>
        Task<IResult<InstaTagFeed>> GetTagFeedAsync(string tag, PaginationParameters paginationParameters);
        /// <summary>
        ///     Get user timeline feed (feed of recent posts from users you follow) asynchronously.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get user explore feed (Explore tab info) asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns><see cref="InstaExploreFeed" /></returns>
        Task<IResult<InstaExploreFeed>> GetExploreFeedAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get activity of following asynchronously
        /// </summary>
        /// <param name="paginationParameters"></param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityFeedAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get recent activity info asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetRecentActivityFeedAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get feed of media your liked.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetLikeFeedAsync(PaginationParameters paginationParameters);
    }
}