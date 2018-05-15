using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class DiscoverSearchResponse
    {
        [JsonProperty("num_results")]
        public int NumResults { get; set; }
        [JsonProperty("users")]
        public List<BroadcastUser> Users { get; set; }
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        [JsonProperty("rank_token")]
        public string RankToken { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
