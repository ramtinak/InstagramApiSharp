using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBlockedUserInfoResponse
    {
        [JsonProperty("username")] public string UserName { get; set; }

        [JsonProperty("profile_pic_url")] public string ProfilePicture { get; set; }
        
        [JsonProperty("full_name")] public string FullName { get; set; }
        
        [JsonProperty("is_private")] public bool IsPrivate { get; set; }

        [JsonProperty("user_id")] public long Pk { get; set; }

        [JsonProperty("block_at")] public long BlockedAt { get; set; }
    }
}
