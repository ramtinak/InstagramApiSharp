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
    internal class InstaAnimatedImageConverter : IObjectConverter<InstaAnimatedImage, InstaAnimatedImageResponse>
    {
        public InstaAnimatedImageResponse SourceObject { get; set; }

        public InstaAnimatedImage Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var animatedImage = new InstaAnimatedImage
            {
                Id = SourceObject.Id,
                IsRandom = SourceObject.IsRandom ?? false,
                IsSticker = SourceObject.IsSticker ?? false
            };

            if (SourceObject.Images != null && SourceObject.Images?.Media != null)
                animatedImage.Media = ConvertersFabric.Instance.GetAnimatedImageMediaConverter(SourceObject.Images.Media).Convert();

            if (SourceObject.User != null)
                animatedImage.User = ConvertersFabric.Instance.GetAnimatedImageUserConverter(SourceObject.User).Convert();

            return animatedImage;
        }
    }
}
