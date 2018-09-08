using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaActionLogResponse
    {
        [JsonProperty("description")] public string Description { get; set; }
    }
}
