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
        public List<InstaSuggestionItemResponse> Suggestions { get; set; } = new List<InstaSuggestionItemResponse>();
    }
    
}
