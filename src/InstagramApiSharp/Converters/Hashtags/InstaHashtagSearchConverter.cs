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
    internal class InstaHashtagSearchConverter : IObjectConverter<InstaHashtagSearch, InstaHashtagSearchResponse>
    {
        public InstaHashtagSearchResponse SourceObject { get; set; }

        public InstaHashtagSearch Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException($"Source object");

            var tags = new InstaHashtagSearch();

            tags.MoreAvailable = SourceObject.MoreAvailable.GetValueOrDefault(false);
            tags.RankToken = SourceObject.RankToken;
            tags.AddRange(SourceObject.Tags.Select(tag =>
                ConvertersFabric.Instance.GetHashTagConverter(tag).Convert()));

            return tags;
        }
    }
}
