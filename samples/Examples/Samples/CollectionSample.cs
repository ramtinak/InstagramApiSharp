using System;
using System.Threading.Tasks;
using InstagramApiSharp.API;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class CollectionSample : IDemoSample
    {
        private readonly IInstaApi InstaApi;

        public CollectionSample(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        public async Task DoShow()
        {
            // get all collections of current user
            var collections = await InstaApi.CollectionProcessor.GetCollectionsAsync(InstagramApiSharp.PaginationParameters.MaxPagesToLoad(1));
            Console.WriteLine($"Loaded {collections.Value.Items.Count} collections for current user");
            foreach (var instaCollection in collections.Value.Items)
            {
                Console.WriteLine($"Collection: name={instaCollection.CollectionName}, id={instaCollection.CollectionId}");
            }
        }
    }
}