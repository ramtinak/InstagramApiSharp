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
    internal class InstaDiscoverTopLiveConverter : IObjectConverter<InstaDiscoverTopLive, InstaDiscoverTopLiveResponse>
    {
        public InstaDiscoverTopLiveResponse SourceObject { get; set; }

        public InstaDiscoverTopLive Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var discoverTopLive = new InstaDiscoverTopLive
            {
                AutoLoadMoreEnabled = SourceObject.AutoLoadMoreEnabled,
                MoreAvailable = SourceObject.MoreAvailable,
                NextMaxId = SourceObject.NextMaxId
            };

            if (SourceObject.Broadcasts?.Count > 0)
                discoverTopLive.Broadcasts = ConvertersFabric.Instance.GetBroadcastListConverter(SourceObject.Broadcasts).Convert();

            if (SourceObject.PostLiveBroadcasts?.Count > 0)
                foreach (var postLive in SourceObject.PostLiveBroadcasts)
                    discoverTopLive.PostLiveBroadcasts.Add(ConvertersFabric.Instance.GetBroadcastPostLiveConverter(postLive).Convert());

            return discoverTopLive;
        }
    }
}
