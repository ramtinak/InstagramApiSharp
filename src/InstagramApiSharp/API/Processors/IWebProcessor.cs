/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Logger;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using InstagramApiSharp.Helpers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers;
using System.Collections.Generic;
using InstagramApiSharp.Classes.Models.Web;

namespace InstagramApiSharp.API.Processors
{
    public interface IWebProcessor
    {
        /// <summary>
        ///     Get self account information like joined date or switched to business account date
        /// </summary>
        Task<IResult<InstaWebAccountInfo>> GetAccountInfoAsync();
        /// <summary>
        ///     Get self account follow requests
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaWebTextDataList>> GetFollowRequestsAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get former biography texts
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaWebData>> GetFormerBiographyTextsAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get former biography links
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaWebData>> GetFormerBiographyLinksAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get former usernames
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaWebData>> GetFormerUsernamesAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get former full names
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaWebData>> GetFormerFullNamesAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get former phone numbers
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaWebData>> GetFormerPhoneNumbersAsync(PaginationParameters paginationParameters);




    }
}
