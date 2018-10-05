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
        /// <summary>
        ///     Star direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> StarDirectThreadAsync(string threadId);
        /// <summary>
        ///     Unstar direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> UnStarDirectThreadAsync(string threadId);
        /// <summary>
        ///     Get promotable media feeds
        /// </summary>
        Task<IResult<InstaMediaList>> GetPromotableMediaFeedsAsync();

        /// <summary>
        ///     Get business get buttons (partners)
        /// </summary>
        Task<IResult<InstaBusinessPartnersList>> GetBusinessButtonsAsync();
        /// <summary>
        ///     Validate an uri for an button(instagram partner)
        ///     <para>Note: Use <see cref="IBusinessProcessor.GetBusinessButtonsAsync"/> to get business buttons(instagram partner) list!</para>
        /// </summary>
        /// <param name="desirePartner">Desire partner (Use <see cref="IBusinessProcessor.GetBusinessButtonsAsync"/> to get business buttons(instagram partner) list!)</param>
        /// <param name="uri">Uri to check</param>
        Task<IResult<bool>> ValidateUrlAsync(InstaBusinessPartner desirePartner, Uri uri);
        /// <summary>
        ///     Remove button from your business account
        /// </summary>
        Task<IResult<bool>> RemoveBusinessButtonAsync();
        /// <summary>
        ///     Get suggested categories
        /// </summary>
        Task<IResult<InstaBusinessSugesstedCategoryList>> GetSuggestedCategoriesAsync();
        /// <summary>
        ///     Get all categories
        /// </summary>
        Task<IResult<InstaBusinessCategoryList>> GetCategoriesAsync();
        /// <summary>
        ///     Get sub categories of an category
        /// </summary>
        /// <param name="categoryId">Category id</param>
        Task<IResult<InstaBusinessCategoryList>> GetSubCategoriesAsync(string categoryId);

    }
}
