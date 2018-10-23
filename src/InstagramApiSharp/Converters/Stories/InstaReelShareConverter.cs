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
    internal class InstaReelShareConverter : IObjectConverter<InstaReelShare, InstaReelShareResponse>
    {
        public InstaReelShareResponse SourceObject { get; set; }

        public InstaReelShare Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var reelShare = new InstaReelShare
            {
                IsReelPersisted = SourceObject.IsReelPersisted ?? false,
                ReelOwnerId = SourceObject.ReelOwnerId,
                ReelType = SourceObject.ReelType,
                Text = SourceObject.Text,
                Type = SourceObject.Type
            };
            try
            {
                reelShare.Media = ConvertersFabric.Instance.GetStoryItemConverter(SourceObject.Media).Convert();
            }
            catch { }
            return reelShare;
        }
    }
}
