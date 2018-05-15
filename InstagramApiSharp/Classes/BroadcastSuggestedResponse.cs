using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class BroadcastSuggestedResponse
    {
        [JsonProperty("broadcasts")]
        public List<InstaBroadcast> Broadcasts { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
