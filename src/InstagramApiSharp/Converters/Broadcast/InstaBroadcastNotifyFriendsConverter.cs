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
    internal class InstaBroadcastNotifyFriendsConverter : IObjectConverter<InstaBroadcastNotifyFriends, InstaBroadcastNotifyFriendsResponse>
    {
        public InstaBroadcastNotifyFriendsResponse SourceObject { get; set; }

        public InstaBroadcastNotifyFriends Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var broadcastNotifyFriends = new InstaBroadcastNotifyFriends
            {
                OnlineFriendsCount = SourceObject.OnlineFriendsCount ?? 0,
                Text = SourceObject.Text
            };

            try
            {
                if (SourceObject.Friends?.Count > 0)
                    foreach (var friend in SourceObject.Friends)
                    {
                        broadcastNotifyFriends.Friends.Add(ConvertersFabric.Instance
                         .GetUserShortFriendshipFullConverter(friend).Convert());
                    }
            }
            catch { }
            return broadcastNotifyFriends;
        }
    }
}
