using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class AccountTwoFactorResponse
    {
        [JsonProperty("backup_codes")]
        public List<string> BackupCodes { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
