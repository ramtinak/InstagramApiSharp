using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public enum GenderType
    {
        //Gender (1 = male, 2 = female, 3 = unknown)
        Male = 1,
        Female = 2,
        Unknown = 3
    }
    internal class AccountDefaultResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    internal class AccountArchiveStoryResponse
    {
        [JsonProperty("reel_auto_archive")]
        public string ReelAutoArchive { get; set; }
        [JsonProperty("message_prefs")]
        public string MessagePrefs { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    
    public enum MessageRepliesType
    {
        [JsonProperty("everyone")]
        Everyone,
        [JsonProperty("following")]
        Following,
        [JsonProperty("off")]
        Off
    }

}
