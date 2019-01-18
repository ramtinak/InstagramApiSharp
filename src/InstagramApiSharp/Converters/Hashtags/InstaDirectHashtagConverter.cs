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
    internal class InstaDirectHashtagConverter : IObjectConverter<InstaDirectHashtag, InstaDirectHashtagResponse>
    {
        public InstaDirectHashtagResponse SourceObject { get; set; }

        public InstaDirectHashtag Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var hashtag = new InstaDirectHashtag
            {
                Name = SourceObject.Name,
                MediaCount = SourceObject.MediaCount
            };

            if (SourceObject.Media != null)
            {
                hashtag.Media = ConvertersFabric.Instance.GetSingleMediaConverter(SourceObject.Media).Convert();

            }
            return hashtag;
        }
    }
}
