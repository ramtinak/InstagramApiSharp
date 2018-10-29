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
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Instagram TV api functions.
    /// </summary>
    public interface ITVProcessor
    {
        /// <summary>
        ///     Get channel by user id (pk) => channel owner
        /// </summary>
        /// <param name="userId">User id (pk) => channel owner</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaTVChannel>> GetChannelByIdAsync(long userId, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get channel by <seealso cref="InstaTVChannelType"/>
        /// </summary>
        /// <param name="channelType">Channel type</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaTVChannel>> GetChannelByTypeAsync(InstaTVChannelType channelType, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get suggested searches
        /// </summary>
        Task<IResult<InstaTVSearch>> GetSuggestedSearchesAsync();

        /// <summary>
        ///     Get TV Guide (gets popular and suggested channels)
        /// </summary>
        Task<IResult<InstaTV>> GetTVGuideAsync();
        /// <summary>
        ///     Search channels
        /// </summary>
        /// <param name="query">Channel or username</param>
        Task<IResult<InstaTVSearch>> SearchAsync(string query);
        /// <summary>
        ///     Upload video to Instagram TV
        /// </summary>
        /// <param name="video">Video to upload (aspect ratio is very important for thumbnail and video | range 0.5 - 1.0 | Width = 480, Height = 852)</param>
        /// <param name="title">Title</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideoUpload video, string title, string caption);
        /// <summary>
        ///     Upload video to Instagram TV with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload (aspect ratio is very important for thumbnail and video | range 0.5 - 1.0 | Width = 480, Height = 852)</param>
        /// <param name="title">Title</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, string title, string caption);
    }
}
