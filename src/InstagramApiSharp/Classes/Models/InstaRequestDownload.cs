using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaRequestDownloadData
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = false;
        [JsonProperty("status")]
        internal string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
