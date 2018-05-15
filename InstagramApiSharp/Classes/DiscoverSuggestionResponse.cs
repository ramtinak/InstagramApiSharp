using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class DiscoverSuggestionResponse : DiscoverRecentSearchsResponse
    {
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
    }
}
