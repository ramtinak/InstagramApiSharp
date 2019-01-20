/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaReelStoryMediaViewersConverter : IObjectConverter<InstaReelStoryMediaViewers, InstaReelStoryMediaViewersResponse>
    {
        public InstaReelStoryMediaViewersResponse SourceObject { get; set; }

        public InstaReelStoryMediaViewers Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var reelViewers = new InstaReelStoryMediaViewers
            {
                NextMaxId = SourceObject.NextMaxId,
                TotalScreenshotCount = SourceObject.TotalScreenshotCount,
                TotalViewerCount = SourceObject.TotalViewerCount,
                UserCount = SourceObject.UserCount
            };

            if (SourceObject.Users?.Count > 0)
                foreach (var user in SourceObject.Users)
                    reelViewers.Users.Add(ConvertersFabric.Instance.GetUserShortConverter(user).Convert());

            if (SourceObject.UpdatedMedia != null)
                reelViewers.UpdatedMedia = ConvertersFabric.Instance.GetStoryItemConverter(SourceObject.UpdatedMedia).Convert();

            return reelViewers;
        }
    }
}
