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
using InstagramApiSharp.Helpers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryCountdownStickerItemConverter : IObjectConverter<InstaStoryCountdownStickerItem, InstaStoryCountdownStickerItemResponse>
    {
        public InstaStoryCountdownStickerItemResponse SourceObject { get; set; }

        public InstaStoryCountdownStickerItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var countdownStickerItem = new InstaStoryCountdownStickerItem
            {
                CountdownId = SourceObject.CountdownId,
                DigitCardColor = SourceObject.DigitCardColor,
                DigitColor = SourceObject.DigitColor,
                EndBackgroundColor = SourceObject.EndBackgroundColor,
                EndTime = DateTimeHelper.FromUnixTimeSeconds(SourceObject.EndTime ?? DateTime.UtcNow.ToUnixTime()),
                FollowingEnabled = SourceObject.FollowingEnabled ?? false,
                IsOwner = SourceObject.IsOwner ?? false,
                StartBackgroundColor = SourceObject.StartBackgroundColor,
                Text = SourceObject.Text,
                TextColor = SourceObject.TextColor,
                ViewerIsFollowing = SourceObject.ViewerIsFollowing ?? false
            };

            return countdownStickerItem;
        }
    }
}
