/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    public interface IReelProcessor 
    {
        Task<IResult<string>> GetClipsCreationInterestPickerAsync();
        Task<IResult<string>> GetClipsInfoForCreationAsync(string mediaId = null);
        /// <summary>
        ///     Get user's reels clips (medias)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaReelsMediaList>> GetUserReelsClipsAsync(long userId, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get user's reels clips (medias)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IResult<InstaReelsMediaList>> GetUserReelsClipsAsync(long userId, PaginationParameters paginationParameters,
            CancellationToken cancellationToken);

        /// <summary>
        ///     Mark reel feed as seen
        /// </summary>
        /// <param name="mediaPkImpression">Media pk (from <see cref="InstaMedia.Pk"/> )</param>
        Task<IResult<bool>> MarkReelAsSeenAsync(string mediaPkImpression);

        /// <summary>
        ///     Explore reel feeds
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaReelsMediaList>> GetReelsClipsAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Explore reel feeds
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IResult<InstaReelsMediaList>> GetReelsClipsAsync(PaginationParameters paginationParameters,
            CancellationToken cancellationToken);
    }
}
