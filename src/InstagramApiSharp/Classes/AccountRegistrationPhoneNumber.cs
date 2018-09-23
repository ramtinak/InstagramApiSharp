/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    internal class AccountRegistrationPhoneNumber
    {
        [JsonProperty("message")]
        internal AccountRegistrationPhoneNumberMessage Message { get; set; }
        [JsonProperty("error_source")]
        internal string ErrorSource { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
        [JsonProperty("error_type")]
        internal string ErrorType { get; set; }
        [JsonProperty("tos_version")]
        public string TosVersion { get; set; }
        [JsonProperty("gdpr_required")]
        public bool GdprRequired { get; set; }
        [JsonIgnore]
        public bool Succeed => Status.ToLower() == "ok" ? true : false;
    }

    internal class AccountRegistrationPhoneNumberMessage
    {
        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }




    internal class AccountRegistrationPhoneNumberVerifySms
    {
        [JsonProperty("nonce_valid")]
        public bool NonceValid { get; set; }
        [JsonProperty("verified")]
        public bool Verified { get; set; }
        [JsonProperty("errors")]
        public AccountRegistrationVerifyPhoneNumberErrors Errors { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
    }

    internal class AccountRegistrationVerifyPhoneNumberErrors
    {
        [JsonProperty("nonce")]
        public string[] Nonce { get; set; }
    }

}
