using System;
using System.IO;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;

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
                Height = 1080,
                Width = 1080,
                URI = new Uri(Path.GetFullPath(@"c:\someawesomepicture.jpg"), UriKind.Absolute).LocalPath
            };
            var result = await _instaApi.UploadPhotoAsync(mediaImage, "someawesomepicture");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload photo: {result.Info.Message}");
        }
    }
}