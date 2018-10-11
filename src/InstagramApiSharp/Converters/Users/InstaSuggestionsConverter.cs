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

namespace InstagramApiSharp.Converters.Users
{
    internal class InstaSuggestionsConverter : IObjectConverter<InstaSuggestions, InstaSuggestionUserContainerResponse>
    {
        public InstaSuggestionUserContainerResponse SourceObject { get; set; }

        public InstaSuggestions Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var suggest = new InstaSuggestions
            {
                MoreAvailable = SourceObject.MoreAvailable,
                NextMaxId = SourceObject.MaxId ?? string.Empty
            };
            try
            {
                if (SourceObject.SuggestedUsers != null && SourceObject.SuggestedUsers?.Suggestions != null &&
                SourceObject.SuggestedUsers?.Suggestions?.Count > 0)
                {
                    suggest.SuggestedUsers = ConvertersFabric.Instance
                        .GetSuggestionItemListConverter(SourceObject.SuggestedUsers.Suggestions).Convert();
                }
                if (SourceObject.NewSuggestedUsers != null && SourceObject.NewSuggestedUsers?.Suggestions != null &&
                    SourceObject.NewSuggestedUsers?.Suggestions?.Count > 0)
                {
                    suggest.NewSuggestedUsers = ConvertersFabric.Instance
                        .GetSuggestionItemListConverter(SourceObject.NewSuggestedUsers.Suggestions).Convert();
                }
            }
            catch { }
            return suggest;
        }
    }
}
