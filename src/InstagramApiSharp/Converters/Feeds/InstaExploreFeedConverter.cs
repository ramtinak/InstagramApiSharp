using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaExploreFeedConverter : IObjectConverter<InstaExploreFeed, InstaExploreFeedResponse>
    {
        public InstaExploreFeedResponse SourceObject { get; set; }

        public InstaExploreFeed Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException("SourceObject");

            List<InstaMedia> ConvertMedia(List<InstaMediaItemResponse> mediasResponse)
            {
                var medias = new List<InstaMedia>();
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

            var feed = new InstaExploreFeed
            {
                NextMaxId = SourceObject.NextMaxId,
                AutoLoadMoreEnabled = SourceObject.AutoLoadMoreEnabled,
                ResultsCount = SourceObject.ResultsCount,
                MoreAvailable = SourceObject.MoreAvailable,
                MaxId = SourceObject.MaxId,
                RankToken = SourceObject.RankToken
            };
            if (SourceObject.Items?.StoryTray != null)
                feed.StoryTray = ConvertersFabric.Instance.GetStoryTrayConverter(SourceObject.Items.StoryTray)
                    .Convert();
            if (SourceObject.Items?.Channel != null)
                feed.Channel = ConvertersFabric.Instance.GetChannelConverter(SourceObject.Items.Channel).Convert();

            feed.Medias.AddRange(ConvertMedia(SourceObject.Items?.Medias));
            return feed;
        }
    }
}