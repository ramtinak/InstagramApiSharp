/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaAnimatedImageMediaConverter : IObjectConverter<InstaAnimatedImageMedia, InstaAnimatedImageMediaResponse>
    {
        public InstaAnimatedImageMediaResponse SourceObject { get; set; }

        public InstaAnimatedImageMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var animatedMedia = new InstaAnimatedImageMedia
            {
                Height = double.Parse(SourceObject.Height ?? "0"),
                Mp4Url = SourceObject.Mp4,
                Mp4Size = double.Parse(SourceObject.Mp4Size ?? "0"),
                Size = double.Parse(SourceObject.Size ?? "0"),
                Url = SourceObject.Url,
                WebpUrl = SourceObject.Webp,
                WebpSize = double.Parse(SourceObject.WebpSize ?? "0"),
                Width = double.Parse(SourceObject.Width ?? "0")
            };

            return animatedMedia;
        }
    }
}
