using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Converters
{
    internal class InstaSuggestionItemConverter : IObjectConverter<InstaSuggestionItem, InstaSuggestionItemResponse>
    {
        public InstaSuggestionItemResponse SourceObject { get; set; }

        public InstaSuggestionItem Convert()
        {
            var suggestion = new InstaSuggestionItem
            {
                Caption = SourceObject.Caption,
                IsNewSuggestion = SourceObject.IsNewSuggestion,
                SocialContext = SourceObject.SocialContext,
                User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert(),
            };
            return suggestion;
        }
    }
}
