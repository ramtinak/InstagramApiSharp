/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaAccountTwoFactorSms
    {
        [JsonProperty("phone_verification_settings")]
        public InstaAccountPhoneVerificationSettings PhoneVerificationSettings { get; set; }
        [JsonProperty("obfuscated_phone_number")]
        public string ObfuscatedPhoneNumber { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
    }

    public class InstaAccountPhoneVerificationSettings
    {
        [JsonProperty("max_sms_count")]
        public int MaxSmsCount { get; set; }
        [JsonProperty("resend_sms_delay_sec")]
        public int ResendSmsDelaySec { get; set; }
        [JsonProperty("robocall_after_max_sms")]
        public bool RobocallAfterMaxSms { get; set; }
        [JsonProperty("robocall_count_down_time_sec")]
        public int RobocallCountDownTimeSec { get; set; }
    }

}
