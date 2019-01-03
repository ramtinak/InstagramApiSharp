/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Discover api functions.
    /// </summary>
    public interface IDiscoverProcessor
    {
        /// <summary>
        ///     Clear Recent searches
        /// </summary>
        Task<IResult<bool>> ClearRecentSearchsAsync();

        /// <summary>
        ///     Get discover user chaining list 
        /// </summary>
        Task<IResult<InstaUserChainingList>> GetChainingUsersAsync();

        /// <summary>
        ///     Get recent searches
        /// </summary>
        Task<IResult<InstaDiscoverRecentSearches>> GetRecentSearchesAsync();

        /// <summary>
        /// Get top searches
        /// </summary>
        /// <param name="querry">querry string of the search</param>
        /// <param name="searchType">Search type(only blended and users works)</param>
        /// <param name="timezone_offset">Timezone offset of the search region (GMT Offset * 60 * 60 - Like Tehran GMT +3:30 = 3.5* 60*60 = 12600)</param>
        /// <returns></returns>
        Task<IResult<InstaDiscoverTopSearches>> GetTopSearchesAsync(string querry = "", InstaDiscoverSearchType searchType = InstaDiscoverSearchType.Users, int timezone_offset = 12600);

        /// <summary>
        ///     Get suggested searches
        /// </summary>
        /// <param name="searchType">Search type(only blended and users works)</param>
        Task<IResult<InstaDiscoverSuggestedSearches>> GetSuggestedSearchesAsync(InstaDiscoverSearchType searchType =
            InstaDiscoverSearchType.Users);
        /// <summary>
        ///     Search user people
        /// </summary>
        /// <param name="query">Query to search</param>
        /// <param name="count">Count</param>
        Task<IResult<InstaDiscoverSearchResult>> SearchPeopleAsync(string query, int count = 50);
        #region Other functions

        /// <summary>
        ///     Sync your phone contact list to instagram
        ///     <para>Note:You can find your friends in instagram with this function</para>
        /// </summary>
        /// <param name="instaContacts">Contact list</param>
        Task<IResult<InstaContactUserList>> SyncContactsAsync(params InstaContact[] instaContacts);
        /// <summary>
        ///     Sync your phone contact list to instagram
        ///     <para>Note:You can find your friends in instagram with this function</para>
        /// </summary>
        /// <param name="instaContacts">Contact list</param>
        Task<IResult<InstaContactUserList>> SyncContactsAsync(InstaContactList instaContacts);

        #endregion Other functions

        ///// <summary>
        ///// NOT COMPLETE
        ///// </summary>
        //Task<IResult<object>> DiscoverPeopleAsync();

    }
}
