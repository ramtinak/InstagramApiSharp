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
    internal class Stories : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public Stories(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var result = await _instaApi.StoryProcessor.GetStoryFeedAsync();
            if (!result.Succeeded)
            {
                Console.WriteLine($"Unable to get story feed: {result.Info}");
                return;
            }
            var storyFeed = result.Value;
            Console.WriteLine($"Got {storyFeed.Items.Count} story reels.");
            foreach (var feedItem in storyFeed.Items)
            {
                Console.WriteLine($"User: {feedItem.User.FullName}");
                foreach (var item in feedItem.Items)
                    Console.WriteLine(
                        $"Story item: {item.Caption?.Text ?? item.Code}, images:{item.ImageList?.Count ?? 0}, videos: {item.VideoList?.Count ?? 0}");
            }
        }
    }
}