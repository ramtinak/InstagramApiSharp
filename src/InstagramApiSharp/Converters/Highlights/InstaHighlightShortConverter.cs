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
using System.Collections.Generic;
using InstagramApiSharp.Helpers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaHighlightShortConverter : IObjectConverter<InstaHighlightShort, InstaHighlightShortResponse>
    {
        public InstaHighlightShortResponse SourceObject { get; set; }

        public InstaHighlightShort Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var highlight = new InstaHighlightShort
            {
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia,
                MediaCount = SourceObject.MediaCount,
                ReelType = SourceObject.ReelType,
                Time = DateTimeHelper.FromUnixTimeSeconds(SourceObject.Timestamp ?? 0)
            };
            return highlight;
        }
    }
}
