using System;
using System.IO;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class UploadPhoto : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public UploadPhoto(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var mediaImage = new InstaImageUpload
            {  
                // leave zero, if you don't know how height and width is it.
                Height = 1080,
                Width = 1080,
                Uri = @"c:\someawesomepicture.jpg"
            };
            // Add user tag (tag people)
            mediaImage.UserTags.Add(new InstaUserTagUpload
            {
                Username = "rmt4006",
                X = 0.5,
                Y = 0.5
            });
            var result = await _instaApi.MediaProcessor.UploadPhotoAsync(mediaImage, "someawesomepicture");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload photo: {result.Info.Message}");
        }

        public async Task DoShowWithProgress()
        {
            var mediaImage = new InstaImageUpload
            {
                // leave zero, if you don't know how height and width is it.
                Height = 1080,
                Width = 1080,
                Uri = @"c:\someawesomepicture.jpg"
            };
            // Add user tag (tag people)
            mediaImage.UserTags.Add(new InstaUserTagUpload
            {
                Username = "rmt4006",
                X = 0.5,
                Y = 0.5
            });
            // Upload photo with progress
            var result = await _instaApi.MediaProcessor.UploadPhotoAsync(UploadProgress, mediaImage, "someawesomepicture");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload photo: {result.Info.Message}");
        }
        void UploadProgress(InstaUploaderProgress progress)
        {
            if (progress == null)
                return;
            Console.WriteLine($"{progress.Name} {progress.UploadState}");
        }
    }
}