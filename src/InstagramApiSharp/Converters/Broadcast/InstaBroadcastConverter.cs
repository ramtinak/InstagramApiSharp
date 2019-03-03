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
    internal class InstaBroadcastConverter : IObjectConverter<InstaBroadcast, InstaBroadcastResponse>
    {
        public InstaBroadcastResponse SourceObject { get; set; }

        public InstaBroadcast Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var broadcast = new InstaBroadcast
            {
                DashManifest = SourceObject.DashManifest,
                BroadcastMessage = SourceObject.BroadcastMessage,
                BroadcastStatus = SourceObject.BroadcastStatus,
                CoverFrameUrl = SourceObject.CoverFrameUrl,
                DashAbrPlaybackUrl = SourceObject.DashAbrPlaybackUrl,
                DashPlaybackUrl = SourceObject.DashPlaybackUrl,
                Id = SourceObject.Id,
                InternalOnly = SourceObject.InternalOnly,
                MediaId = SourceObject.MediaId,
                OrganicTrackingToken = SourceObject.OrganicTrackingToken,
                PublishedTime = DateTimeHelper.FromUnixTimeSeconds(SourceObject.PublishedTime ?? DateTime.Now.ToUnixTime()),
                RtmpPlaybackUrl = SourceObject.RtmpPlaybackUrl,
                ViewerCount = SourceObject.ViewerCount
            };

            if (SourceObject.BroadcastOwner != null)
                broadcast.BroadcastOwner = ConvertersFabric.Instance
                    .GetUserShortFriendshipFullConverter(SourceObject.BroadcastOwner).Convert();
            return broadcast;
        }
    }
}
