using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBrandedContentResponse
    {
        [JsonProperty("require_approval")]
        public bool RequireApproval { get; set; }
        [JsonProperty("whitelisted_users")]
        public List<InstaUserShortResponse> WhitelistedUsers { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
