using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaDiscoverTopSearchesResponse
    {
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
        [JsonProperty("list")]
        public List<InstaDiscoverSearchesResponse> TopResults { get; set; }
        [JsonProperty("has_more")]
        public bool HasMoreItems { get; set; }
        [JsonProperty("clear_client_cache")]
        public bool ClearClientCache { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
