using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    internal class InstaExtraResponse
    {
        [JsonExtensionData]
        internal IDictionary<string, JToken> Extras { get; set; }
    }
}
