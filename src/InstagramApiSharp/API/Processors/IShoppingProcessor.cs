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
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using InstagramApiSharp.Helpers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using InstagramApiSharp.Converters;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.Models;
using System.Net;
using InstagramApiSharp.Converters.Json;
using InstagramApiSharp.Enums;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Shopping and commerce api functions.
    /// </summary>
    public interface IShoppingProcessor
    {
        /// <summary>
        ///     Get all user shoppable media by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserShoppableMediaAsync(string username, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get all user shoppable media by user id (pk)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserShoppableMediaByIdAsync(long userId, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get product info
        /// </summary>
        /// <param name="productId">Product id (get it from <see cref="InstaProduct.ProductId"/> )</param>
        /// <param name="mediaPk">Media Pk (get it from <see cref="InstaMedia.Pk"/>)</param>
        /// <param name="deviceWidth">Device width (pixel)</param>
        Task<IResult<InstaProductInfo>> GetProductInfoAsync(long productId, string mediaPk, int deviceWidth = 720);

        //Task<IResult<InstaProductInfo>> GetCatalogsAsync();

    }
}
