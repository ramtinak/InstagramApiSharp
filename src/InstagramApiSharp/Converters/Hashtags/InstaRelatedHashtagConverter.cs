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

namespace InstagramApiSharp.Converters.Hashtags
{
    class InstaRelatedHashtagConverter : IObjectConverter<InstaRelatedHashtag, InstaRelatedHashtagResponse>
    {
        public InstaRelatedHashtagResponse SourceObject { get; set; }

        public InstaRelatedHashtag Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var relatedHashtag = new InstaRelatedHashtag
            {
                Id = SourceObject.Id,
                Name = SourceObject.Name,
                Type = SourceObject.Type
            };
            return relatedHashtag;
        }
    }
}
