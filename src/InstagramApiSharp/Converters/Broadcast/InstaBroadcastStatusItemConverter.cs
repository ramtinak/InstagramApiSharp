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
    internal class InstaBroadcastStatusItemConverter : IObjectConverter<InstaBroadcastStatusItem, InstaBroadcastStatusItemResponse>
    {
        public InstaBroadcastStatusItemResponse SourceObject { get; set; }

        public InstaBroadcastStatusItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var broadcastStatusItem = new InstaBroadcastStatusItem
            {
                BroadcastStatus = SourceObject.BroadcastStatus,
                CoverFrameUrl = SourceObject.CoverFrameUrl,
                HasReducedVisibility = SourceObject.HasReducedVisibility,
                Id = SourceObject.Id,
                ViewerCount = SourceObject.ViewerCount
            };

            return broadcastStatusItem;
        }
    }
}
