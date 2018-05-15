using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class AccountConfirmEmailResponse
    {
        [JsonProperty("is_email_legit")]
        public bool IsEmailLegit { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
