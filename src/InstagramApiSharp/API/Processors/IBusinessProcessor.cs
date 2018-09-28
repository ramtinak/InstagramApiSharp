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
        Task<IResult<InstaStatistics>> GetStatisticsAsync();
        //Task<IResult<object>> GetInsightsAsync(DateTime date);

    }
}
