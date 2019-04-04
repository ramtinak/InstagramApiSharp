/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaTopicalExploreFeedConverter : IObjectConverter<InstaTopicalExploreFeed, InstaTopicalExploreFeedResponse>
    {
        public InstaTopicalExploreFeedResponse SourceObject { get; set; }

        public InstaTopicalExploreFeed Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("SourceObject");

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

            var feed = new InstaTopicalExploreFeed
            {
                NextMaxId = SourceObject.NextMaxId,
                AutoLoadMoreEnabled = SourceObject.AutoLoadMoreEnabled,
                ResultsCount = SourceObject.ResultsCount,
                MoreAvailable = SourceObject.MoreAvailable,
                MaxId = SourceObject.MaxId,
                RankToken = SourceObject.RankToken,
                HasShoppingChannelContent = SourceObject.HasShoppingChannelContent ?? false
            };
            if (SourceObject.TVChannels?.Count > 0)
            {
                foreach (var channel in SourceObject.TVChannels)
                {
                    try
                    {
                        feed.TVChannels.Add(ConvertersFabric.Instance.GetTVChannelConverter(channel).Convert());
                    }
                    catch { }
                }
            }
            if (SourceObject.Clusters?.Count > 0)
            {
                foreach (var cluster in SourceObject.Clusters)
                {
                    try
                    {
                        feed.Clusters.Add(ConvertersFabric.Instance.GetExploreClusterConverter(cluster).Convert());
                    }
                    catch { }
                }
            }
            if (SourceObject.Channel != null)
                feed.Channel = ConvertersFabric.Instance.GetChannelConverter(SourceObject.Channel).Convert();

            feed.Medias.AddRange(ConvertMedia(SourceObject.Medias));
            return feed;
        }
    }
}
