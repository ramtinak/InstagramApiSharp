using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface ILocationProcessor
    {
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
        ///     Gets the feed of particular location.
        /// </summary>
        /// <param name="locationId">Location identifier</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     Location feed
        /// </returns>
        Task<IResult<InstaLocationFeed>> GetLocationFeedAsync(long locationId, PaginationParameters paginationParameters);
        /// <summary>
        ///     Search user by location
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="desireUsername">Desire username</param>
        /// <param name="count">Maximum user count</param>
        Task<IResult<InstaUserSearchLocation>> SearchUserByLocationAsync(double latitude, double longitude, string desireUsername, int count = 50);
    }
}