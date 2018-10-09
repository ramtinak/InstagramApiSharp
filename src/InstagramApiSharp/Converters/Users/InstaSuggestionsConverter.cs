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
                    foreach (var item in SourceObject.SuggestedUsers.Suggestions)
                    {
                        try
                        {
                            var convertedItem = ConvertersFabric.Instance.GetSuggestionItemConverter(item).Convert();
                            suggest.SuggestedUsers.Add(convertedItem);
                        }
                        catch { }
                    }
                }
                if (SourceObject.NewSuggestedUsers != null && SourceObject.NewSuggestedUsers?.Suggestions != null &&
                    SourceObject.NewSuggestedUsers?.Suggestions?.Count > 0)
                {
                    foreach (var item in SourceObject.NewSuggestedUsers.Suggestions)
                    {
                        try
                        {
                            var convertedItem = ConvertersFabric.Instance.GetSuggestionItemConverter(item).Convert();
                            suggest.NewSuggestedUsers.Add(convertedItem);
                        }
                        catch { }
                    }
                }
            }
            catch { }
            return suggest;
        }
    }
}
