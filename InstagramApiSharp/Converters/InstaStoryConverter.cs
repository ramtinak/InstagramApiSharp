using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryConverter : IObjectConverter<InstaStory, InstaStoryResponse>
    {
        public InstaStoryResponse SourceObject { get; set; }

        public InstaStory Convert()
        {
            if (SourceObject == null) return null;
            var story = new InstaStory
            {
                CanReply = SourceObject.CanReply,
                ExpiringAt = SourceObject.ExpiringAt.FromUnixTimeSeconds(),
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia,
                Muted = SourceObject.Muted,
                PrefetchCount = SourceObject.PrefetchCount,
                RankedPosition = SourceObject.RankedPosition,
                Seen = (SourceObject.Seen ?? 0).FromUnixTimeSeconds(),
                SeenRankedPosition = SourceObject.SeenRankedPosition,
                SocialContext = SourceObject.SocialContext,
                SourceToken = SourceObject.SourceToken
            };
            if (SourceObject.Owner != null)
                story.Owner = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.Owner).Convert();

            if (SourceObject.User != null)
                story.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert();

            if (SourceObject.Items != null)
                foreach (var item in SourceObject.Items)
                    story.Items.Add(ConvertersFabric.Instance.GetSingleMediaConverter(item).Convert());
            return story;
        }
    }
}