using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaTVSearch
    {
        [JsonProperty("results")]
        public List<InstaTVSearchResult> Results { get; set; }
        [JsonProperty("num_results")]
        public int NumResults { get; set; }
        [JsonProperty("rank_token")]
        public string Rank_token { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
    }

    public class InstaTVSearchResult
    {
        [JsonProperty("type")]
        internal string Type { get; set; }
        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
        [JsonProperty("channel")]
        public InstaTVChannel Channel { get; set; }
    }

}
