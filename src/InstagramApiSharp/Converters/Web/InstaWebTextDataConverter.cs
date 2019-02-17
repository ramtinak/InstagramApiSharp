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
using InstagramApiSharp.Classes.Models.Web;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.Web;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaWebTextDataConverter : IObjectConverter<InstaWebTextDataList, InstaWebSettingsPageResponse>
    {
        public InstaWebSettingsPageResponse SourceObject { get; set; }

        public InstaWebTextDataList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var list = new InstaWebTextDataList();
            if (SourceObject.Data.Data?.Count > 0)
            {
                foreach (var item in SourceObject.Data.Data)
                {
                    if (item.Text.IsNotEmpty())
                        list.Items.Add(item.Text);
                }
                list.CursorId = SourceObject.Data.Cursor;
            }
            return list;
        }
    }
}
