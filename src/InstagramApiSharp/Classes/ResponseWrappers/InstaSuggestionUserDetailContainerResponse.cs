using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaSuggestionUserDetailContainerResponse : InstaDefault
    {
        [JsonProperty("items")]
        public InstaSuggestionItemListResponse Items { get; set; } = new InstaSuggestionItemListResponse();
    }
}
