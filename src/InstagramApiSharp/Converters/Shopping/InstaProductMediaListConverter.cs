/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaProductMediaListConverter : IObjectConverter<InstaProductMediaList, InstaProductMediaListResponse>
    {
        public InstaProductMediaListResponse SourceObject { get; set; }

        public InstaProductMediaList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var productMedia = new InstaProductMediaList
            {
                AutoLoadMoreEnabled = SourceObject.AutoLoadMoreEnabled,
                MoreAvailable = SourceObject.MoreAvailable,
                NextMaxId = SourceObject.NextMaxId,
                ResultsCount = SourceObject.ResultsCount,
                TotalCount = SourceObject.TotalCount
            };
            if (SourceObject.Medias != null && SourceObject.Medias.Any())
                foreach (var media in SourceObject.Medias)
                    productMedia.Medias.Add(ConvertersFabric.Instance.GetSingleMediaConverter(media).Convert());
            
            return productMedia;
        }
    }
}
