using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes
{
    [Serializable]
    public class TwoFactorLogin
    {
        [JsonProperty("obfuscated_phone_number")]
        public short ObfuscatedPhoneNumber { get; set; }

        [JsonProperty("show_messenger_code_option")]
        public bool ShowMessengerCodeOption { get; set; }

        [JsonProperty("two_factor_identifier")]
        public string TwoFactorIdentifier { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("sms_two_factor_on")]
        public bool SMSTwoFactorOn { get; set; }

        [JsonProperty("totp_two_factor_on")]
        public bool TotpTwoFactorOn { get; set; }

        [JsonProperty("show_new_login_screen")]
        public bool ShowNewLoginScreen { get; set; }
    }
}
