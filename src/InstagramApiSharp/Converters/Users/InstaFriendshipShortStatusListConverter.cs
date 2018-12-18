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
using System.Linq;

namespace InstagramApiSharp.Converters.Users
{
    internal class InstaFriendshipShortStatusListConverter :
        IObjectConverter<InstaFriendshipShortStatusList, InstaFriendshipShortStatusListResponse>
    {
        public InstaFriendshipShortStatusListResponse SourceObject { get; set; }

        public InstaFriendshipShortStatusList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var friendships = new InstaFriendshipShortStatusList();
            if (SourceObject != null && SourceObject.Any())
            {
                foreach (var item in SourceObject)
                {
                    try
                    {
                        var friend = ConvertersFabric.Instance.GetSingleFriendshipShortStatusConverter(item).Convert();
                        friend.Pk = item.Pk;
                        friendships.Add(friend);
                    }
                    catch { }
                }
            }
            return friendships;
        }
    }
}
