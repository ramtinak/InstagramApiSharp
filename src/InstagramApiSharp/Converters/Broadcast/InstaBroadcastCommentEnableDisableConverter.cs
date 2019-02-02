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
    internal class InstaBroadcastCommentEnableDisableConverter : IObjectConverter<InstaBroadcastCommentEnableDisable, InstaBroadcastCommentEnableDisableResponse>
    {
        public InstaBroadcastCommentEnableDisableResponse SourceObject { get; set; }

        public InstaBroadcastCommentEnableDisable Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var enDis = new InstaBroadcastCommentEnableDisable
            {
                CommentMuted = SourceObject.CommentMuted
            };
            return enDis;
        }
    }
}
