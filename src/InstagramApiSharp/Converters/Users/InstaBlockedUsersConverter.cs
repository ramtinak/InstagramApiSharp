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

namespace InstagramApiSharp.Converters
{
    internal class InstaBlockedUsersConverter : IObjectConverter<InstaUserShortList, InstaBlockedUsersResponse>
    {
        public InstaBlockedUsersResponse SourceObject { get; set; }

        public InstaUserShortList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var blockedUsers = new InstaUserShortList
            {
                NextMaxId = SourceObject.MaxId
            };

            if (SourceObject.BlockedList != null && SourceObject.BlockedList.Any())
            {
                foreach (var user in SourceObject.BlockedList)
                    blockedUsers.Add(ConvertersFabric.Instance.GetUserShortConverter(user).Convert());
            }

            return blockedUsers;
        }
    }
}
