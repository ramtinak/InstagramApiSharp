using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class TwoFactorLoginInfo
    {
        [JsonProperty("obfuscated_phone_number")]
        public short ObfuscatedPhoneNumber { get; set; }

        [JsonProperty("show_messenger_code_option")]
        public bool ShowMessengerCodeOption { get; set; }

        [JsonProperty("two_factor_identifier")]
        public string TwoFactorIdentifier { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("phone_verification_settings")]
        public PhoneVerificationSettings PhoneVerificationSettings { get; set; }

        public static TwoFactorLoginInfo Empty => new TwoFactorLoginInfo();
    }
}