using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBusinessValidateUrl
    {
        [JsonProperty("error_msg")]
        public string ErrorMessage { get; set; }
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
    }
}
