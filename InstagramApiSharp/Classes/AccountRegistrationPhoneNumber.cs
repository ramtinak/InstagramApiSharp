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
    internal class AccountRegistrationPhoneNumber
    {
        [JsonProperty("message")]
        public AccountRegistrationPhoneNumberMessage Message { get; set; }
        [JsonProperty("error_source")]
        public string ErrorSource { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
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
