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
    internal class InstaBroadcastInfoConverter : IObjectConverter<InstaBroadcastInfo, InstaBroadcastInfoResponse>
    {
        public InstaBroadcastInfoResponse SourceObject { get; set; }

        public InstaBroadcastInfo Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var unixTime = DateTime.Now.ToUnixTime();
            var broadcastInfo = new InstaBroadcastInfo
            {
                BroadcastMessage = SourceObject.BroadcastMessage,
                BroadcastStatus = SourceObject.BroadcastStatus,
                CoverFrameUrl = SourceObject.CoverFrameUrl,
                DashManifest = SourceObject.DashManifest,
                EncodingTag = SourceObject.EncodingTag,
                Id = SourceObject.Id,
                InternalOnly = SourceObject.InternalOnly,
                MediaId = SourceObject.MediaId,
                NumberOfQualities = SourceObject.NumberOfQualities,
                OrganicTrackingToken = SourceObject.OrganicTrackingToken,
                TotalUniqueViewerCount = SourceObject.TotalUniqueViewerCount,
                ExpireAt = DateTimeHelper.FromUnixTimeSeconds(SourceObject.ExpireAt ?? unixTime),
                PublishedTime = DateTimeHelper.FromUnixTimeSeconds(SourceObject.PublishedTime ?? unixTime),
            };

            if (SourceObject.BroadcastOwner != null)
                broadcastInfo.BroadcastOwner = ConvertersFabric.Instance
                    .GetUserShortFriendshipFullConverter(SourceObject.BroadcastOwner).Convert();
            return broadcastInfo;
        }
    }
}
