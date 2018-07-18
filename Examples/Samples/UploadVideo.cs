using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Samples
{
    internal class UploadVideo : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public UploadVideo(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }
        public async Task DoShow()
        {
            var video1 = new InstaVideo(@"C:\Users\Ramti\Downloads\Telegram Desktop\video201711290001.wmv", 1080, 1080, 4) {Length =14 };
            var video2 = new InstaVideo(@"C:\Users\Ramti\Downloads\Telegram Desktop\video_2018-05-29_19-33-38.mp4", 1080, 1080, 4) { Length = 46 };

            var image1 = new InstaImage(@"C:\Users\Ramti\Downloads\Telegram Desktop\IMG-20150704-WA0041.jpg", 1080, 1080);
            var image2 = new InstaImage(@"C:\Users\Ramti\Downloads\Telegram Desktop\26fehdnii9ttnh400i.jpg", 1080, 1080);
            Console.WriteLine("pekh");
            var result = await _instaApi.MediaProcessor.UploadAlbumAsync(new InstaImage[] {image2 },new InstaVideo[] {video2 }, "aaaaaaaaaaaaaaaaaaaaaaaaazzzzzzzzz");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload video: {result.Info.Message}");
        }
        public async Task DoShow2()
        {
            var mediaVideo = new InstaVideo(@"c:\somevideo.mp4", 1080, 1080, 3);
            var mediaImage = new InstaImage
            {
                Height = 1080,
                Width = 1080,
                URI = new Uri(Path.GetFullPath(@"c:\RamtinJokar.jpg"), UriKind.Absolute).LocalPath
            };
            var result = await _instaApi.UploadVideoAsync(mediaVideo, mediaImage, "ramtinak");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload video: {result.Info.Message}");
        }
    }
}
