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
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class UploadVideo : IDemoSample
    {
        private readonly IInstaApi InstaApi;

        public UploadVideo(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }
        public async Task DoShow()
        {
            var video = new InstaVideoUpload
            {
                // leave zero, if you don't know how height and width is it.
                Video = new InstaVideo(@"c:\video1.mp4", 0, 0),
                VideoThumbnail = new InstaImage(@"c:\video thumbnail 1.jpg", 0, 0)
            };
            // Add user tag (tag people)
            video.UserTags.Add(new InstaUserTagVideoUpload
            {
                Username = "rmt4006"
            });
            var result = await InstaApi.MediaProcessor.UploadVideoAsync(video, "ramtinak");
            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload video: {result.Info.Message}");
        }
    }
}
