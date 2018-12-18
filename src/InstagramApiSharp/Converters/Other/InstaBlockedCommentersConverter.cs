/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaBlockedCommentersConverter : IObjectConverter<InstaUserShortList, InstaBlockedCommentersResponse>
    {
        public InstaBlockedCommentersResponse SourceObject { get; set; }

        public InstaUserShortList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("SourceObject");

            var users = new InstaUserShortList();

            if (SourceObject.BlockedCommenters?.Count > 0)
            {
                foreach (var user in SourceObject.BlockedCommenters)
                    users.Add(ConvertersFabric.Instance.GetUserShortConverter(user).Convert());
            }

            return users;
        }
    }
}
