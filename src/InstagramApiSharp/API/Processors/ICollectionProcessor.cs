using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Collection api functions.
    /// </summary>
    public interface ICollectionProcessor
    {
        /// <summary>
        ///     Adds items to collection asynchronous.
        /// </summary>
        /// <param name="collectionId">Collection identifier.</param>
        /// <param name="mediaIds">Media id list.</param>
        Task<IResult<InstaCollectionItem>> AddItemsToCollectionAsync(long collectionId, params string[] mediaIds);

        /// <summary>
        ///     Create a new collection
        /// </summary>
        /// <param name="collectionName">The name of the new collection</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollectionItem" />
        /// </returns>
        Task<IResult<InstaCollectionItem>> CreateCollectionAsync(string collectionName);

        /// <summary>
        ///     Delete your collection for given collection id
        /// </summary>
        /// <param name="collectionId">Collection ID to delete</param>
        /// <returns>true if succeed</returns>
        Task<IResult<bool>> DeleteCollectionAsync(long collectionId);

        /// <summary>
        ///     Edit a collection
        /// </summary>
        /// <param name="collectionId">Collection ID to edit</param>
        /// <param name="name">New name for giving collection (set null if you don't want to change it)</param>
        /// <param name="photoCoverMediaId">
        ///     New photo cover media Id (get it from <see cref="InstaMedia.InstaIdentifier"/>) => Optional
        ///     <para>Important note: media id must be exists in giving collection!</para>
        /// </param>
        Task<IResult<InstaCollectionItem>> EditCollectionAsync(long collectionId, string name, string photoCoverMediaId = null);

        /// <summary>
        ///     Get your collection for given collection id
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <param name="paginationParameters">Pagination parameters: next max id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollectionItem" />
        /// </returns>
        Task<IResult<InstaCollectionItem>> GetSingleCollectionAsync(long collectionId,
            PaginationParameters paginationParameters);

        /// <summary>
        ///     Get your collections
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next max id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollections" />
        /// </returns>
        Task<IResult<InstaCollections>> GetCollectionsAsync(PaginationParameters paginationParameters);
    }
}