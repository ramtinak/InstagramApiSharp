using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{

    public class BroadcastStartResponse
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
