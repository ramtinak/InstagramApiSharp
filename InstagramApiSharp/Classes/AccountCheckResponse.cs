using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class AccountCheckResponse
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("available")]
        public bool Available { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
    }

}
