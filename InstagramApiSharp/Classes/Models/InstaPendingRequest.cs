using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaPendingRequest
    {
        [JsonProperty("big_list")]
        public bool BigList { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("suggested_users")]
        public Suggested_Users SuggestedUsers { get; set; }
        [JsonProperty("truncate_follow_requests_at_index")]
        public int TruncateFollowRequestsAtIndex { get; set; }
        [JsonProperty("users")]
        public InstaUserShort[] Users { get; set; }
    }
    
    public class Suggested_Users
    {
        public string netego_type { get; set; }
        public object[] suggestions { get; set; }
    }
    
}
