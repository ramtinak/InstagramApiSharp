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
    public class AccountTwoFactorSmsResponse
    {
        [JsonProperty("phone_verification_settings")]
        public AccountPhoneVerificationSettings PhoneVerificationSettings { get; set; }
        [JsonProperty("obfuscated_phone_number")]
        public string ObfuscatedPhoneNumber { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class AccountPhoneVerificationSettings
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
