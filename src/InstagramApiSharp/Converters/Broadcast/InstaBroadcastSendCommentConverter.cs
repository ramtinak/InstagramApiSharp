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
    internal class InstaBroadcastSendCommentConverter : IObjectConverter<InstaBroadcastSendComment, InstaBroadcastSendCommentResponse>
    {
        public InstaBroadcastSendCommentResponse SourceObject { get; set; }

        public InstaBroadcastSendComment Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var broadcastSendComment = new InstaBroadcastSendComment
            {
                MediaId = SourceObject.MediaId,
                ContentType = SourceObject.ContentType,
                CreatedAt = DateTimeHelper.FromUnixTimeSeconds(SourceObject.CreatedAt ?? DateTime.Now.ToUnixTime()),
                CreatedAtUtc = DateTimeHelper.FromUnixTimeSeconds(SourceObject.CreatedAtUtc ?? DateTime.UtcNow.ToUnixTime()),
                Pk = SourceObject.Pk,
                Text = SourceObject.Text,
                Type = SourceObject.Type
            };
            if (SourceObject.User != null)
                broadcastSendComment.User = ConvertersFabric.Instance
                    .GetUserShortFriendshipFullConverter(SourceObject.User).Convert();

            return broadcastSendComment;
        }
    }
}
