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
using InstagramApiSharp.Classes.Models.Hashtags;
using InstagramApiSharp.Classes.ResponseWrappers;


namespace InstagramApiSharp.Converters.Hashtags
{
    internal class InstaHashtagMediaConverter : IObjectConverter<InstaHashtagMedia, InstaHashtagMediaListResponse>
    {
        public InstaHashtagMediaListResponse SourceObject { get; set; }

        public InstaHashtagMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var media = new InstaHashtagMedia
            {
                AutoLoadMoreEnabled = SourceObject.AutoLoadMoreEnabled ?? false,
                MoreAvailable = SourceObject.MoreAvailable,
                NextMaxId = SourceObject.NextMaxId,
                NextMediaIds = SourceObject.NextMediaIds,
                NextPage = SourceObject.NextPage ?? 0
            };
            if (SourceObject.Sections != null)
            {
                foreach (var section in SourceObject.Sections)
                {
                    try
                    {
                        foreach (var item in section.LayoutContent.Medias)
                        {
                            try
                            {
                                media.Medias.Add(ConvertersFabric.Instance.GetSingleMediaConverter(item.Media).Convert());

                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }

            return media;
        }
    }
}
