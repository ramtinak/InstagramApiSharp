/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Linq;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaPlaceListConverter : IObjectConverter<InstaPlaceList, InstaPlaceListResponse>
    {
        public InstaPlaceListResponse SourceObject { get; set; }

        public InstaPlaceList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var list = new InstaPlaceList
            {
                HasMore = SourceObject.HasMore ?? false,
                RankToken = SourceObject.RankToken,
                Status = SourceObject.Status
            };
            if (SourceObject.Items != null && SourceObject.Items.Any())
            {
                foreach (var place in SourceObject.Items)
                {
                    try
                    {
                        list.Items.Add(ConvertersFabric.Instance.GetPlaceConverter(place).Convert());
                    }
                    catch { }
                }
                list.ExcludeList = SourceObject.ExcludeList;
            }
            return list;
        }
    }
}
