using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;
using System.Collections.Generic;
namespace InstagramApiSharp.Converters
{
    internal class InstaHighlightConverter : IObjectConverter<InstaHighlightFeeds, InstaHighlightFeedsResponse>
    {
        public InstaHighlightFeedsResponse SourceObject { get; set; }

        public InstaHighlightFeeds Convert()
        {
            var highlight = new InstaHighlightFeeds
            {
                ShowEmptyState = SourceObject.ShowEmptyState,
                Status = SourceObject.Status
            };
            highlight.Items = new List<InstaHighlightFeed>();
            foreach (var item in SourceObject.Items)
            {
                var hLight = new InstaHighlightFeed
                {
                    CanReply = item.CanReply,
                    CanReshare = item.CanReshare,
                    HighlightId = item.Id,
                    LatestReelMedia = item.LatestReelMedia,
                    MediaCount = item.MediaCount,
                    PrefetchCount = item.PrefetchCount,
                    RankedPosition = item.RankedPosition,
                    ReelType = item.ReelType,
                    Seen = item.Seen,
                    SeenRankedPosition = item.SeenRankedPosition,
                    Title = item.Title
                };
                
                hLight.CoverMedia = new InstaHighlightCoverMedia
                {
                     CropRect = item.CoverMedia.CropRect,
                     MediaId = item.CoverMedia.MediaId
                };
                if(item.CoverMedia.CroppedImageVersion!= null)
                    hLight.CoverMedia.CroppedImage = new InstaImage(item.CoverMedia.CroppedImageVersion.Url, int.Parse(item.CoverMedia.CroppedImageVersion.Width),int.Parse(item.CoverMedia.CroppedImageVersion.Height));
                if (item.CoverMedia.FullImageVersion != null)
                    hLight.CoverMedia.Image = new InstaImage(item.CoverMedia.FullImageVersion.Url, int.Parse(item.CoverMedia.FullImageVersion.Width), int.Parse(item.CoverMedia.FullImageVersion.Height));
                var userConverter = ConvertersFabric.Instance.GetUserShortConverter(item.User);
                hLight.User = userConverter.Convert();

                highlight.Items.Add(hLight);
            }
            return highlight;
        }
    }
}
