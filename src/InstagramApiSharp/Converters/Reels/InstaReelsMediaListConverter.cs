/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharpMine
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;
using System.Linq;

namespace InstagramApiSharp.Converters
{
    public class InstaReelsMediaListConverter : IObjectConverter<InstaReelsMediaList, InstaReelsMediaListResponse>
    {
        public InstaReelsMediaListResponse SourceObject { get; set; }

        public InstaReelsMediaList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("SourceObject");

            var reelsMediaList = new InstaReelsMediaList
            {
                MoreAvailable = SourceObject.PagingInfo?.MoreAvailable ?? false,
                NextMaxId = SourceObject.PagingInfo?.MaxId
            };

            if (SourceObject.Medias?.Count > 0)
                reelsMediaList.Medias.AddRange(
                    SourceObject.Medias.Select(x => x.Media)
                    .Select(ConvertersFabric.Instance.GetSingleMediaConverter)
                        .Select(converter => converter.Convert()));
            return reelsMediaList;
        }
    }
}
