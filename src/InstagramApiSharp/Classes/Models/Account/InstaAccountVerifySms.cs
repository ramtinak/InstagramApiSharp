/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaAccountVerifySms
    {
        [JsonProperty("verified")]
        public bool Verified { get; set; }
        [JsonProperty("errors")]
        public InstaAccountVerifySmsErrors Errors { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
        [JsonProperty("error_type")]
        internal string ErrorType { get; set; }
    }

    public class InstaAccountVerifySmsErrors
    {
        [JsonProperty("verification_code")]
        public List<string> VerificationCode { get; set; }
    }

}
