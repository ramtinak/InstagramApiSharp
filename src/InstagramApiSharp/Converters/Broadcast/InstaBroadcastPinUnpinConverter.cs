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
    internal class InstaBroadcastPinUnpinConverter : IObjectConverter<InstaBroadcastPinUnpin, InstaBroadcastPinUnpinResponse>
    {
        public InstaBroadcastPinUnpinResponse SourceObject { get; set; }

        public InstaBroadcastPinUnpin Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var broadcastPinUnpin = new InstaBroadcastPinUnpin
            {
                CommentId = SourceObject.CommentId
            };

            return broadcastPinUnpin;
        }
    }
}
