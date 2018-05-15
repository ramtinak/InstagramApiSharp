using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class DicoverDefaultResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}