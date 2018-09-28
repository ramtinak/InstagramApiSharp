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
using InstagramApiSharp.Classes.Models.Business;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.Business;
using InstagramApiSharp.Enums;
using System;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    public interface IBusinessProcessor
    {
        /// <summary>
        ///     Get statistics of current account
        /// </summary>
        Task<IResult<InstaStatistics>> GetStatisticsAsync();
        /// <summary>
        ///     Get media insight
        /// </summary>
        /// <param name="mediaPk">Media PK (<see cref="InstaMedia.Pk"/>)</param>
        Task<IResult<InstaMediaInsights>> GetMediaInsightsAsync(string mediaPk);
        /// <summary>
        ///     Get full media insights
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        Task<IResult<InstaFullMediaInsights>> GetFullMediaInsightsAsync(string mediaId);


    }
}
