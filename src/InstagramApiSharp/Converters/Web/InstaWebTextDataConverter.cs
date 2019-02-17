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
using InstagramApiSharp.Classes.ResponseWrappers.Web;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaWebTextDataConverter : IObjectConverter<InstaWebTextData, InstaWebSettingsPageResponse>
    {
        public InstaWebSettingsPageResponse SourceObject { get; set; }

        public InstaWebTextData Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var list = new InstaWebTextData();
            if (SourceObject.Data.Data?.Count > 0)
            {
                foreach (var item in SourceObject.Data.Data)
                {
                    if (item.Text.IsNotEmpty())
                        list.Items.Add(item.Text);
                }
                list.MaxId = SourceObject.Data.Cursor;
            }
            return list;
        }
    }
}
