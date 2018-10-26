using System;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryFeedConverter : IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse>
    {
        public InstaStoryFeedResponse SourceObject { get; set; }

        public InstaStoryFeed Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException($"Source object");
            var feed = new InstaStoryFeed
            {
                FaceFilterNuxVersion = SourceObject.FaceFilterNuxVersion,
                HasNewNuxStory = SourceObject.HasNewNuxStory,
                StickerVersion = SourceObject.StickerVersion,
                StoryRankingToken = SourceObject.StoryRankingToken
            };

            if (SourceObject.Tray != null && SourceObject.Tray.Any())
            {
                foreach (var itemResponse in SourceObject.Tray)
                {
                    var reel = itemResponse.ToObject<InstaReelFeedResponse>();
                    if (reel.Id.ToLower().StartsWith("tag:"))
                        feed.HashtagStories.Add(ConvertersFabric.Instance
                            .GetHashtagStoryConverter(itemResponse.ToObject<InstaHashtagStoryResponse>()).Convert());
                    else
                        feed.Items.Add(ConvertersFabric.Instance.GetReelFeedConverter(reel).Convert());
                }
            }

            if (SourceObject.Broadcasts != null && SourceObject.Broadcasts.Any())
                feed.Broadcasts = SourceObject.Broadcasts;

            return feed;
        }
    }
}