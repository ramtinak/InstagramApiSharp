using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface ICollectionProcessor
    {
        Task<IResult<InstaCollectionItem>> GetCollectionAsync(long collectionId);

        Task<IResult<InstaCollections>> GetCollectionsAsync();

        Task<IResult<InstaCollectionItem>> CreateCollectionAsync(string collectionName);

        Task<IResult<bool>> DeleteCollectionAsync(long collectionId);

        Task<IResult<InstaCollectionItem>> AddItemsToCollectionAsync(long collectionId, params string[] mediaIds);
    }
}