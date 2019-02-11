using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.Models.Hashtags;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Hashtag api functions.
    /// </summary>
    public interface IHashtagProcessor
    {
        /// <summary>
        ///     Follow a hashtag
        /// </summary>
        /// <param name="tagname">Tag name</param>
        Task<IResult<bool>> FollowHashtagAsync(string tagname);

        /// <summary>
        ///     Get following hashtags information
        /// </summary>
        /// <param name="userId">User identifier (pk)</param>
        /// <returns>
        ///     List of hashtags
        /// </returns>
        Task<IResult<InstaHashtagSearch>> GetFollowingHashtagsInfoAsync(long userId);

        /// <summary>
        ///     Gets the hashtag information by user tagname.
        /// </summary>
        /// <param name="tagname">Tagname</param>
        /// <returns>Hashtag information</returns>
        Task<IResult<InstaHashtag>> GetHashtagInfoAsync(string tagname);

        /// <summary>
        ///     Get stories of an hashtag
        /// </summary>
        /// <param name="tagname">Tag name</param>
        Task<IResult<InstaHashtagStory>> GetHashtagStoriesAsync(string tagname);

        /// <summary>
        ///     Get recent hashtag media list
        /// </summary>
        /// <param name="tagname">Tag name</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaSectionMedia>> GetRecentHashtagMediaListAsync(string tagname, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get suggested hashtags
        /// </summary>
        /// <returns>
        ///     List of hashtags
        /// </returns>
        Task<IResult<InstaHashtagSearch>> GetSuggestedHashtagsAsync();

        /// <summary>
        ///     Get top (ranked) hashtag media list
        /// </summary>
        /// <param name="tagname">Tag name</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaSectionMedia>> GetTopHashtagMediaListAsync(string tagname, PaginationParameters paginationParameters);

        /// <summary>
        ///     Searches for specific hashtag by search query.
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="excludeList">Array of numerical hashtag IDs (ie "17841562498105353") to exclude from the response, allowing you to skip tags from a previous call to get more results</param>
        /// <param name="rankToken">The rank token from the previous page's response</param>
        /// <returns>
        ///     List of hashtags
        /// </returns>
        Task<IResult<InstaHashtagSearch>> SearchHashtagAsync(string query, IEnumerable<long> excludeList = null, string rankToken = null);
        /// <summary>
        ///     Unfollow a hashtag
        /// </summary>
        /// <param name="tagname">Tag name</param>
        Task<IResult<bool>> UnFollowHashtagAsync(string tagname);
    }
}