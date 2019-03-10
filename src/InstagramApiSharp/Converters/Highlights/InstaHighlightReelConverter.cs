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
using System.Collections.Generic;
namespace InstagramApiSharp.Converters
{
    internal class InstaHighlightReelConverter : IObjectConverter<InstaHighlightSingleFeed, InstaHighlightReelResponse>
    {
        public InstaHighlightReelResponse SourceObject { get; set; }

        public InstaHighlightSingleFeed Convert()
        {
            if (SourceObject.Reel == null)
                return null;
            var hLight = new InstaHighlightSingleFeed
            {
                CanReply = SourceObject.Reel.CanReply,
                CanReshare = SourceObject.Reel.CanReshare,
                HighlightId = SourceObject.Reel.Id,
                LatestReelMedia = SourceObject.Reel.LatestReelMedia,
                MediaCount = SourceObject.Reel.MediaCount,
                PrefetchCount = SourceObject.Reel.PrefetchCount,
                RankedPosition = SourceObject.Reel.RankedPosition,
                ReelType = SourceObject.Reel.ReelType,
                Seen = SourceObject.Reel.Seen,
                SeenRankedPosition = SourceObject.Reel.SeenRankedPosition,
                Title = SourceObject.Reel.Title
            };

            hLight.CoverMedia = new InstaHighlightCoverMedia
            {
                CropRect = SourceObject.Reel.CoverMedia.CropRect,
                MediaId = SourceObject.Reel.CoverMedia.MediaId
            };

            if (SourceObject.Reel.CoverMedia.CroppedImageVersion != null)
                hLight.CoverMedia.CroppedImage = new InstaImage(SourceObject.Reel.CoverMedia.CroppedImageVersion.Url, int.Parse(SourceObject.Reel.CoverMedia.CroppedImageVersion.Width), int.Parse(SourceObject.Reel.CoverMedia.CroppedImageVersion.Height));
            if (SourceObject.Reel.CoverMedia.FullImageVersion != null)
                hLight.CoverMedia.Image = new InstaImage(SourceObject.Reel.CoverMedia.FullImageVersion.Url, int.Parse(SourceObject.Reel.CoverMedia.FullImageVersion.Width), int.Parse(SourceObject.Reel.CoverMedia.FullImageVersion.Height));
            var userConverter = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.Reel.User);
            hLight.User = userConverter.Convert();

            hLight.Items = new List<InstaStoryItem>();
            if (SourceObject.Reel.Items != null)
                foreach (var media in SourceObject.Reel.Items)
                    hLight.Items.Add(ConvertersFabric.Instance.GetStoryItemConverter(media).Convert());
            return hLight;
        }
    }

}
