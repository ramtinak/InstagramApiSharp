using System;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
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
        private readonly IInstaApi InstaApi;

        public Stories(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        public async Task DoShow()
        {
            var result = await InstaApi.StoryProcessor.GetStoryFeedAsync();
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

        public async void UploadPhoto()
        {
            var image = new InstaImage { Uri = @"c:\someawesomepicture.jpg" };

            var result = await InstaApi.StoryProcessor.UploadStoryPhotoAsync(image, "someawesomepicture");
            Console.WriteLine(result.Succeeded
                ? $"Story created: {result.Value.Media.Pk}"
                : $"Unable to upload photo story: {result.Info.Message}");
        }

        public async void UploadVideo()
        {
            var video = new InstaVideoUpload
            {
                Video = new InstaVideo(@"c:\video1.mp4", 0, 0),
                VideoThumbnail = new InstaImage(@"c:\video thumbnail 1.jpg", 0, 0)
            };
            var result = await InstaApi.MediaProcessor.UploadVideoAsync(video, "ramtinak");
            Console.WriteLine(result.Succeeded
                ? $"Story created: {result.Value.Pk}"
                : $"Unable to upload video story: {result.Info.Message}");
        }

        public async void UploadWithOptions()
        {
            // You can add hashtags or locations or poll questions to your photo/video stories!
            // Note that you must draw your hashtags/location names/poll questions in your image first and then upload it!

            var storyOptions = new InstaStoryUploadOptions();
            // Add hashtag
            storyOptions.Hashtags.Add(new InstaStoryHashtagUpload
            {
                X = 0.5, // center of image
                Y = 0.5, // center of image
                Z = 0,
                Width = 0.3148148,
                Height = 0.110367894,
                Rotation = 0,
                TagName = "IRAN"
            });

            // Add poll question
            storyOptions.Polls.Add(new InstaStoryPollUpload
            {
                X = 0.5, // center of image
                Y = 0.5, // center of image
                Z = 0,
                Width = 0.3148148,
                Height = 0.110367894,
                Rotation = 0,
                Question = "Do you love IRAN?",
                Answer1 = "Are", // "YES" answer
                Answer2 = "Na" // "NO" answer
            });

            // Add location
            var locationsResult = await InstaApi.LocationProcessor.SearchLocationAsync(0, 0, "kazeroun");
            var firstLocation = locationsResult.Value.FirstOrDefault();
            var locationId = firstLocation.ExternalId;

            storyOptions.Locations.Add(new InstaStoryLocationUpload
            {
                X = 0.5, // center of image
                Y = 0.5, // center of image
                Z = 0,
                Width = 0.3148148,
                Height = 0.110367894,
                Rotation = 0,
                LocationId = locationId
            });

            var image = new InstaImage { Uri = @"c:\someawesomepicture.jpg" };

            var result = await InstaApi.StoryProcessor.UploadStoryPhotoAsync(image, "someawesomepicture", storyOptions);
            // upload video
            //var result = await InstaApi.MediaProcessor.UploadVideoAsync(video, "ramtinak", storyOptions);
            Console.WriteLine(result.Succeeded
                ? $"Story created: {result.Value.Media.Pk}"
                : $"Unable to upload photo story: {result.Info.Message}");
        }
    }
}