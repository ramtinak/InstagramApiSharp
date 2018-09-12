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
    internal class UploadAlbum : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public UploadAlbum(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var images = new InstaImage[]
            {
                new InstaImage
                {
                    // leave zero, if you don't know how height and width is it.
                    Height = 0,
                    Width = 0,
                    Uri = @"c:\image1.jpg"
                },
                new InstaImage
                {
                    // leave zero, if you don't know how height and width is it.
                    Height = 0,
                    Width = 0,
                    Uri = @"c:\image2.jpg"
                }
            };

            var videos = new InstaVideoUpload[]
            {
                new InstaVideoUpload
                {
                     // leave zero, if you don't know how height and width is it.
                    Video = new InstaVideo(@"c:\video1.mp4", 0, 0),
                    VideoThumbnail = new InstaImage(@"c:\video thumbnail 1.jpg", 0, 0)
                },
                new InstaVideoUpload
                {
                     // leave zero, if you don't know how height and width is it.
                    Video = new InstaVideo(@"c:\video2.mp4", 0, 0),
                    VideoThumbnail = new InstaImage(@"c:\video thumbnail 2.jpg", 0, 0)
                }
            };
            var result = await _instaApi.MediaProcessor.UploadAlbumAsync(images, 
                videos, 
                "Hey, this my first album upload via InstagramApiSharp library.");


            Console.WriteLine(result.Succeeded
                ? $"Media created: {result.Value.Pk}, {result.Value.Caption}"
                : $"Unable to upload photo: {result.Info.Message}");
        }
    }
}
