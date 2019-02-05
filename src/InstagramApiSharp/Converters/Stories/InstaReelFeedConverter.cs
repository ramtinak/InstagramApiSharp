using System;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaReelFeedConverter : IObjectConverter<InstaReelFeed, InstaReelFeedResponse>
    {
        public InstaReelFeedResponse SourceObject { get; set; }

        public InstaReelFeed Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var reelFeed = new InstaReelFeed
            {
                CanReply = SourceObject.CanReply,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject?.ExpiringAt ?? 0),
                HasBestiesMedia = SourceObject.HasBestiesMedia,
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia ?? 0,
                PrefetchCount = SourceObject.PrefetchCount,
                Seen = SourceObject.Seen ?? 0,
                User = ConvertersFabric.Instance.GetUserShortFriendshipFullConverter(SourceObject.User).Convert()
            };
            try
            {
                if (!string.IsNullOrEmpty(SourceObject.CanReshare))
                    reelFeed.CanReshare = bool.Parse(SourceObject.CanReshare);
            }
            catch { }
            if (SourceObject.Items != null && SourceObject.Items.Any())
                foreach (var item in SourceObject.Items)
                    try
                    {
                        reelFeed.Items.Add(ConvertersFabric.Instance.GetStoryItemConverter(item).Convert());
                    }
                    catch { }
            return reelFeed;
        }
    }
}