/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaSuggestionUserContainerResponse
    {
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }
        [JsonProperty("max_id")]
        public string MaxId { get; set; }
        [JsonProperty("suggested_users")]
        public InstaSuggestionResponse SuggestedUsers { get; set; } = new InstaSuggestionResponse();
        [JsonProperty("new_suggested_users")]
        public InstaSuggestionResponse NewSuggestedUsers { get; set; } = new InstaSuggestionResponse();
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaSuggestionResponse
    {
        [JsonProperty("suggestions")]
        public InstaSuggestionItemListResponse Suggestions { get; set; } = new InstaSuggestionItemListResponse();
    }
    
}
