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
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaDirectBroadcastConverter : IObjectConverter<InstaDirectBroadcast, InstaDirectBroadcastResponse>
    {
        public InstaDirectBroadcastResponse SourceObject { get; set; }

        public InstaDirectBroadcast Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var broadcast = new InstaDirectBroadcast
            {
                Broadcast = SourceObject.Broadcast,
                Text = SourceObject.Text,
                IsLinked = SourceObject.IsLinked ?? false,
                Message = SourceObject.Message,
                Title = SourceObject.Title
            };

            return broadcast;
        }
    }
}
