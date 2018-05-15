using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryFeedConverter : IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse>
    {
        public InstaStoryFeedResponse SourceObject { get; set; }

        public InstaStoryFeed Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var feed = new InstaStoryFeed
            {
                FaceFilterNuxVersion = SourceObject.FaceFilterNuxVersion,
                HasNewNuxStory = SourceObject.HasNewNuxStory,
                StickerVersion = SourceObject.StickerVersion,
                StoryRankingToken = SourceObject.StoryRankingToken
            };

            if (SourceObject.Tray != null)
                foreach (var itemResponse in SourceObject.Tray)
                    feed.Items.Add(ConvertersFabric.Instance.GetReelFeedConverter(itemResponse).Convert());
            return feed;
        }
    }
}