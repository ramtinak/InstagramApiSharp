using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    //public class InstaRavenMediaResponse
    //{
    //    //[JsonProperty("media_type")]
    //    //public InstaMediaType MediaType { get; set; }
    //}
    public class InstaRavenMediaActionSummaryResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
