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
using InstagramApiSharp.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    public interface ITVProcessor
    {
        /// <summary>
        ///     Get TV Guide (gets popular and suggested channels)
        /// </summary>
        Task<IResult<InstaTV>> GetTVGuideAsync();
        /// <summary>
        ///     Get channel by <seealso cref="InstaTVChannelType"/>
        /// </summary>
        /// <param name="channelType">Channel type</param>
        /// <param name="paginationParameters">Pagination parameters</param>
        Task<IResult<InstaTVChannel>> GetChannelByTypeAsync(InstaTVChannelType channelType, PaginationParameters paginationParameters);
        /// <summary>
        ///     Get channel by user id (pk) => channel owner
        /// </summary>
        /// <param name="userId">User id (pk) => channel owner</param>
        /// <param name="paginationParameters">Pagination parameters</param>
        Task<IResult<InstaTVChannel>> GetChannelByIdAsync(long userId, PaginationParameters paginationParameters);
        /// <summary>
        ///     Get suggested searches
        /// </summary>
        Task<IResult<InstaTVSearch>> SuggestedSearchesAsync();
        /// <summary>
        ///     Search channels
        /// </summary>
        /// <param name="query">Channel or username</param>
        Task<IResult<InstaTVSearch>> SearchAsync(string query);

    }
}
