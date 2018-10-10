/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.Models.Hashtags;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters.Hashtags
{
    class InstaHashtagStoryConverter : IObjectConverter<InstaHashtagStory, InstaHashtagStoryContainerResponse>
    {
        public InstaHashtagStoryContainerResponse SourceObject { get; set; }

        public InstaHashtagStory Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var source = SourceObject.Story;
            var hashtagStory = new InstaHashtagStory
            {
                CanReply = source.CanReply,
                 CanReshare = source.CanReshare,
                 ExpiringAt = source.ExpiringAt.FromUnixTimeSeconds(),
                 Id = source.Id,
                 LatestReelMedia = source.LatestReelMedia,
                 Muted = source.Muted,
                 PrefetchCount = source.PrefetchCount,
                 ReelType = source.ReelType,
                 UniqueIntegerReelId = source.UniqueIntegerReelId,
                 Owner = ConvertersFabric.Instance.GetHashtagOwnerConverter(source.Owner).Convert()
            };
            try
            {
                foreach (var story in source.Items)
                {
                    try
                    {
                        hashtagStory.Items.Add(ConvertersFabric.Instance.GetStoryItemConverter(story).Convert());
                    }
                    catch { }
                }
            }
            catch { }
            return hashtagStory;
        }
    }
}
