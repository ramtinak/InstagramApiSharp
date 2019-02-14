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
using InstagramApiSharp.Helpers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaVisualMediaConverter : IObjectConverter<InstaVisualMedia, InstaVisualMediaResponse>
    {
        public InstaVisualMediaResponse SourceObject { get; set; }

        public InstaVisualMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var visualMedia = new InstaVisualMedia
            {
                Height = SourceObject.Height ?? 0,
                InstaIdentifier = SourceObject.InstaIdentifier,
                MediaType = SourceObject.MediaType,
                MediaId = SourceObject.MediaId,
                TrackingToken = SourceObject.TrackingToken,
                Width = SourceObject.Width ?? 0
            };

            if (SourceObject.UrlExpireAtSecs != null)
                visualMedia.UrlExpireAt = SourceObject.UrlExpireAtSecs.Value.FromUnixTimeSeconds();

            if (SourceObject.Images?.Candidates != null)
                foreach (var image in SourceObject.Images.Candidates)
                    visualMedia.Images.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));

            if (SourceObject.Videos?.Count > 0)
                foreach (var video in SourceObject.Videos)
                    visualMedia.Videos.Add(new InstaVideo(video.Url, int.Parse(video.Width), int.Parse(video.Height),
                        video.Type));

            return visualMedia;
        }
    }
}
