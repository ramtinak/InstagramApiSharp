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
    internal class InstaSuggestionItemListConverter : IObjectConverter<InstaSuggestionItemList, InstaSuggestionItemListResponse>
    {
        public InstaSuggestionItemListResponse SourceObject { get; set; }

        public InstaSuggestionItemList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var suggest = new InstaSuggestionItemList();

            if (SourceObject != null && SourceObject?.Count > 0)
            {
                foreach (var item in SourceObject)
                {
                    try
                    {
                        var convertedItem = ConvertersFabric.Instance.GetSuggestionItemConverter(item).Convert();
                        suggest.Add(convertedItem);
                    }
                    catch { }
                }
            }
            return suggest;
        }
    }
}
