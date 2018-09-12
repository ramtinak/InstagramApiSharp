using System;
using System.IO;
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
    internal class UploadPhoto : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public UploadPhoto(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var mediaImage = new InstaImage
            {  
                // leave zero, if you don't know how height and width is it.
                Height = 1080,
                Width = 1080,
                Uri = @"c:\someawesomepicture.jpg"
            };
            var result = await _instaApi.MediaProcessor.UploadPhotoAsync(mediaImage, "someawesomepicture");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload photo: {result.Info.Message}");
        }
    }
}