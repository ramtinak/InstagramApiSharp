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
    internal class InstaUserContactListConverter : IObjectConverter<InstaContactUserList, InstaContactUserListResponse>
    {
        public InstaContactUserListResponse SourceObject { get; set; }

        public InstaContactUserList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var userList = new InstaContactUserList();
            try
            {
                foreach (var item in SourceObject.Items)
                {
                    try
                    {
                        userList.Add(ConvertersFabric.Instance.GetSingleUserContactConverter(item.User).Convert());
                    }
                    catch { }
                }
            }
            catch { }
            return userList;
        }
    }
}
