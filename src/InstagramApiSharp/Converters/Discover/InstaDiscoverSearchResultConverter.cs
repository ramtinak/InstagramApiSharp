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
    internal class InstaDiscoverSearchResultConverter : IObjectConverter<InstaDiscoverSearchResult, InstaDiscoverSearchResultResponse>
    {
        public InstaDiscoverSearchResultResponse SourceObject { get; set; }

        public InstaDiscoverSearchResult Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var result = new InstaDiscoverSearchResult
            {
                HasMoreAvailable = SourceObject.HasMore ?? false,
                RankToken = SourceObject.RankToken,
                NumResults = SourceObject.NumResults ?? 0,
            };
            if (SourceObject.Users != null && SourceObject.Users.Any())
            {
                foreach (var user in SourceObject.Users)
                {
                    try
                    {
                        result.Users.Add(ConvertersFabric.Instance.GetUserConverter(user).Convert());
                    }
                    catch { }
                }
            }
            return result;
        }
    }
}
