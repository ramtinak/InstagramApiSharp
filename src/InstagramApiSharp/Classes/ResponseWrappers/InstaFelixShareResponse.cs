using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaFelixShareResponse
    {
        [JsonProperty("video")] public InstaMediaItemResponse Video { get; set; }
    }
}
