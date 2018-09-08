/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class AccountVerifySmsResponse
    {
        [JsonProperty("verified")]
        public bool Verified { get; set; }
        [JsonProperty("errors")]
        public AccountVerifySmsErrors Errors { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
    }

    public class AccountVerifySmsErrors
    {
        [JsonProperty("verification_code")]
        public List<string> VerificationCode { get; set; }
    }

}
