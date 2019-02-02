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
    internal class InstaBroadcastLikeConverter : IObjectConverter<InstaBroadcastLike, InstaBroadcastLikeResponse>
    {
        public InstaBroadcastLikeResponse SourceObject { get; set; }

        public InstaBroadcastLike Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var broadcastLike = new InstaBroadcastLike
            {
                BurstLikes = SourceObject.BurstLikes,
                Likes = SourceObject.Likes,
                LikeTime = DateTimeHelper.FromUnixTimeSeconds(SourceObject.LikeTs ?? DateTime.Now.ToUnixTime())
            };
            return broadcastLike;
        }
    }
}
