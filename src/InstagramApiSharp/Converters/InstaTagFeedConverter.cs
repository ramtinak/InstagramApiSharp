using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaTagFeedConverter : IObjectConverter<InstaTagFeed, InstaTagFeedResponse>
    {
        public InstaTagFeedResponse SourceObject { get; set; }

        public InstaTagFeed Convert()
        {
            if (SourceObject?.Medias == null)
                throw new ArgumentNullException("InstaFeedResponse or its media list");
            var feed = new InstaTagFeed();

            List<InstaMedia> ConvertMedia(List<InstaMediaItemResponse> mediasResponse)
            {
                var medias = new List<InstaMedia>();
                foreach (var instaUserFeedItemResponse in mediasResponse)
                {
                    if (instaUserFeedItemResponse?.Type != 0) continue;
                    var feedItem = ConvertersFabric.Instance.GetSingleMediaConverter(instaUserFeedItemResponse)
                        .Convert();
                    medias.Add(feedItem);
                }

                return medias;
            }

            feed.RankedMedias.AddRange(ConvertMedia(SourceObject.RankedItems));
            feed.Medias.AddRange(ConvertMedia(SourceObject.Medias));
            feed.NextMaxId = SourceObject.NextMaxId;
            foreach (var story in SourceObject.Stories)
            {
                var feedItem = ConvertersFabric.Instance.GetStoryConverter(story).Convert();
                feed.Stories.Add(feedItem);
            }

            return feed;
        }
    }
}