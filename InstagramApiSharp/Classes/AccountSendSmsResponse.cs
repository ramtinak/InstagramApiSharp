using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes
{
    public class AccountSendSmsResponse
    {
        [JsonProperty("phone_number_valid")]
        public bool PhoneNumberValid { get; set; }
        [JsonProperty("phone_verification_settings")]
        public AccountPhoneVerificationSettings PhoneVerificationSettings { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
