/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    public enum DiscoverSearchType
    {
        //'blended', 'users', 'hashtags', 'places'
        Blended,
        Users,
        Hashtags,
        Places
    }
    public interface IDiscoverProcessor
    {
        /// <summary>
        /// Get recent searches
        /// </summary>
        /// <returns></returns>
        Task<IResult<DiscoverRecentSearchsResponse>> GetRecentSearchsAsync();
        /// <summary>
        /// Clear Recent searches
        /// </summary>
        /// <returns></returns>
        Task<IResult<bool>> ClearRecentSearchsAsync();
        /// <summary>
        /// Get suggested searches
        /// </summary>
        /// <param name="searchType">Search type(only blended and users works)</param>
        /// <returns></returns>
        Task<IResult<DiscoverSuggestionResponse>> GetSuggestedSearchesAsync(DiscoverSearchType searchType);
        /// <summary>
        /// Search user people
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <param name="count">Count</param>
        /// <returns></returns>
        Task<IResult<DiscoverSearchResponse>> SearchPeopleAsync(string content, int count = 30);



        ///// <summary>
        ///// NOT COMPLETE
        ///// </summary>
        ///// <returns></returns>
        //Task<IResult<object>> DiscoverPeopleAsync();

    }
}
