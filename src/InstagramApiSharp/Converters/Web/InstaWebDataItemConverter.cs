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
    internal class InstaWebDataItemConverter : IObjectConverter<InstaWebDataItem, InstaWebDataItemResponse>
    {
        public InstaWebDataItemResponse SourceObject { get; set; }

        public InstaWebDataItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var data = new InstaWebDataItem
            {
                Text = SourceObject.Text
            };

            if (SourceObject.Timestamp != null)
                data.Time = SourceObject.Timestamp.Value.FromUnixTimeSeconds();
            else
                data.Time = DateTime.MinValue;
  
            return data;
        }
    }
}
