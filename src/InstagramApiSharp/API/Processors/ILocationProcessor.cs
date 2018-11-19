using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Location api functions.
    /// </summary>
    public interface ILocationProcessor
    {
        /// <summary>
        ///     Gets the feed of particular location.
        /// </summary>
        /// <param name="locationId">Location identifier</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     Location feed
        /// </returns>
        Task<IResult<InstaLocationFeed>> GetLocationFeedAsync(long locationId, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get location(place) information by external id or facebook places id
        ///     <para>Get external id from this function: <see cref="ILocationProcessor.SearchLocationAsync(double, double, string)"/></para>
        ///     <para>Get facebook places id from this function: <see cref="ILocationProcessor.SearchPlacesAsync(double, double, string)(double, double, string)"/></para>
        /// </summary>
        /// <param name="externalIdOrFacebookPlacesId">
        ///     External id or facebook places id of an location/place
        ///     <para>Get external id from this function: <see cref="ILocationProcessor.SearchLocationAsync(double, double, string)"/></para>
        ///     <para>Get facebook places id from this function: <see cref="ILocationProcessor.SearchPlacesAsync(double, double, string)(double, double, string)"/></para>
        /// </param>
        Task<IResult<InstaPlaceShort>> GetLocationInfo(string externalIdOrFacebookPlacesId);

        /// <summary>
        ///     Searches for specific location by provided geo-data or search query.
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="query">Search query</param>
        /// <returns>
        ///     List of locations (short format)
        /// </returns>
        Task<IResult<InstaLocationShortList>> SearchLocationAsync(double latitude, double longitude, string query);
        /// <summary>
        ///     Search user by location
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="desireUsername">Desire username</param>
        /// <param name="count">Maximum user count</param>
        Task<IResult<InstaUserSearchLocation>> SearchUserByLocationAsync(double latitude, double longitude, string desireUsername, int count = 50);
        /// <summary>
        ///     Search places in facebook
        ///     <para>Note: This works for non-facebook accounts too!</para>
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <returns>
        ///     <see cref="InstaPlaceList" />
        /// </returns>
        Task<IResult<InstaPlaceList>> SearchPlacesAsync(double latitude, double longitude);
        /// <summary>
        ///     Search places in facebook
        ///     <para>Note: This works for non-facebook accounts too!</para>
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="query">Query to search (city, country or ...)</param>
        /// <returns>
        ///     <see cref="InstaPlaceList" />
        /// </returns>
        Task<IResult<InstaPlaceList>> SearchPlacesAsync(double latitude, double longitude, string query);

    }
}