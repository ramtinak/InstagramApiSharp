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
    internal class InstaHashtagStoryConverter : IObjectConverter<InstaHashtagStory, InstaHashtagStoryResponse>
    {
        public InstaHashtagStoryResponse SourceObject { get; set; }

        public InstaHashtagStory Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var hashtagStory = new InstaHashtagStory
            {
                CanReply = SourceObject.CanReply,
                CanReshare = SourceObject.CanReshare,
                ExpiringAt = SourceObject.ExpiringAt.FromUnixTimeSeconds(),
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia,
                Muted = SourceObject.Muted,
                PrefetchCount = SourceObject.PrefetchCount,
                ReelType = SourceObject.ReelType,
                UniqueIntegerReelId = SourceObject.UniqueIntegerReelId,
                Owner = ConvertersFabric.Instance.GetHashtagOwnerConverter(SourceObject.Owner).Convert()
            };
            try
            {
                foreach (var story in SourceObject.Items)
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
