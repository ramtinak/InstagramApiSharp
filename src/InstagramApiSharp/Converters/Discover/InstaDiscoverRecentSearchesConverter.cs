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
using InstagramApiSharp.Helpers;
using System.Linq;

namespace InstagramApiSharp.Converters
{
    internal class InstaDiscoverRecentSearchesConverter : IObjectConverter<InstaDiscoverRecentSearches, InstaDiscoverRecentSearchesResponse>
    {
        public InstaDiscoverRecentSearchesResponse SourceObject { get; set; }

        public InstaDiscoverRecentSearches Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var recents = new InstaDiscoverRecentSearches();
            if (SourceObject.Recent != null && SourceObject.Recent.Any())
            {
                foreach (var search in SourceObject.Recent)
                {
                    try
                    {
                        recents.Recent.Add(ConvertersFabric.Instance.GetDiscoverSearchesConverter(search).Convert());
                    }
                    catch { }
                }
            }
            return recents;
        }
    }
}
