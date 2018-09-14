using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaLocationFeedConverter : IObjectConverter<InstaLocationFeed, InstaLocationFeedResponse>
    {
        public InstaLocationFeedResponse SourceObject { get; set; }

        public InstaLocationFeed Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException("SourceObject");

            InstaMediaList ConvertMedia(List<InstaMediaItemResponse> mediasResponse)
            {
                var medias = new InstaMediaList();
                if (mediasResponse == null)
                    return medias;
                foreach (var instaUserFeedItemResponse in mediasResponse)
                {
                    if (instaUserFeedItemResponse?.Type != 0) continue;
                    var feedItem = ConvertersFabric.Instance.GetSingleMediaConverter(instaUserFeedItemResponse)
                        .Convert();
                    medias.Add(feedItem);
                }

                return medias;
            }

            var feed = new InstaLocationFeed
            {
                MediaCount = SourceObject.MediaCount,
                NextMaxId = SourceObject.NextMaxId,
                Medias = ConvertMedia(SourceObject.Items),
                RankedMedias = ConvertMedia(SourceObject.RankedItems),
                Location = ConvertersFabric.Instance.GetLocationConverter(SourceObject.Location).Convert(),
                Story = ConvertersFabric.Instance.GetStoryConverter(SourceObject.Story).Convert()
            };
            return feed;
        }
    }
}