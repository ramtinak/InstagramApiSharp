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
        Task<IResult<InstaBusinessPartnersList>> GetBusinessPartnersButtonsAsync();
        /// <summary>
        ///     Validate an uri for an button(instagram partner)
        ///     <para>Note: Use <see cref="IBusinessProcessor.GetBusinessPartnersButtonsAsync"/> to get business buttons(instagram partner) list!</para>
        /// </summary>
        /// <param name="desirePartner">Desire partner (Use <see cref="IBusinessProcessor.GetBusinessPartnersButtonsAsync"/> to get business buttons(instagram partner) list!)</param>
        /// <param name="uri">Uri to check (Must be related to desire partner!)</param>
        Task<IResult<bool>> ValidateUrlAsync(InstaBusinessPartner desirePartner, Uri uri);
        /// <summary>
        ///     Remove button from your business account
        /// </summary>
        Task<IResult<InstaBusinessUser>> RemoveBusinessButtonAsync();
        /// <summary>
        ///     Add button to your business account
        /// </summary>
        /// <param name="businessPartner">Desire partner button (Use <see cref="IBusinessProcessor.GetBusinessPartnersButtonsAsync"/> to get business buttons(instagram partner) list!)</param>
        /// <param name="uri">Uri (related to Business partner button)</param>
        Task<IResult<InstaBusinessUser>> AddOrChangeBusinessButtonAsync(InstaBusinessPartner businessPartner, Uri uri);
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
        /// <param name="categoryId">Category id (Use <see cref="IBusinessProcessor.GetCategoriesAsync"/> to get category id)</param>
        Task<IResult<InstaBusinessCategoryList>> GetSubCategoriesAsync(string categoryId);
        /// <summary>
        ///     Search location for business account
        /// </summary>
        /// <param name="cityOrTown">City/town name</param>
        Task<IResult<InstaBusinessCityLocationList>> SearchCityLocationAsync(string cityOrTown);
        /// <summary>
        ///     Change business category
        ///     <para>Note: Get it from <see cref="IBusinessProcessor.GetSubCategoriesAsync(string)"/></para>
        /// </summary>
        /// <param name="subCategoryId">Sub category id (Get it from <see cref="IBusinessProcessor.GetSubCategoriesAsync(string)"/>)
        /// </param>
        Task<IResult<InstaBusinessUser>> ChangeBusinessCategoryAsync(string subCategoryId);
        /// <summary>
        ///     Update business information
        /// </summary>
        /// <param name="phoneNumberWithCountryCode">Phone number with country code [set null if you don't want to change it]</param>
        /// <param name="cityLocation">City Location (get it from <see cref="IBusinessProcessor.SearchCityLocationAsync(string)"/>)</param>
        /// <param name="streetAddress">Street address</param>
        /// <param name="zipCode">Zip code</param>
        /// <param name="businessContactType">Phone contact type (<see cref="InstaUserInfo.BusinessContactMethod"/>) [set null if you don't want to change it]</param>
        Task<IResult<InstaBusinessUser>> UpdateBusinessInfoAsync(string phoneNumberWithCountryCode,
            InstaBusinessCityLocation cityLocation,
            string streetAddress, string zipCode,
            InstaBusinessContactType? businessContactType);
        /// <summary>
        ///     Remove business location
        /// </summary>
        Task<IResult<InstaBusinessUser>> RemoveBusinessLocationAsync();
        /// <summary>
        ///     Get logged in business account information
        /// </summary>
        Task<IResult<InstaUserInfo>> GetBusinessAccountInformationAsync();
    }
}
