using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaLastSeen : InstaLastSeenItemResponse
    {
        public long PK { get; set; }

        public DateTime SeenTime { get; set; }
    }
    public class InstaLastSeenAtResponse
    {
        [JsonExtensionData]
        internal IDictionary<string, JToken> Extras { get; set; }
    }
    public class InstaLastSeenItemResponse
    {
        [JsonProperty("timestamp")]
        internal string TimestampPrivate { get; set; }
        [JsonProperty("item_id")]
        public string ItemId { get; set; }
    }

}
