using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes
{
    public class CreationResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("account_created")]
        public bool AccountCreated { get; set; }
    }
}
