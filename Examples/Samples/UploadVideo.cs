/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
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
            var mediaVideo = new InstaVideo(@"c:\somevideo.mp4", 1080, 1080, 3);
            var mediaImage = new InstaImage
            {
                Height = 1080,
                Width = 1080,
                URI = new Uri(Path.GetFullPath(@"c:\RamtinJokar.jpg"), UriKind.Absolute).LocalPath
            };
            var result = await _instaApi.MediaProcessor.UploadVideoAsync(mediaVideo, mediaImage, "ramtinak");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload video: {result.Info.Message}");
        }
    }
}
