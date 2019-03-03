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
    public class InstaPhoneVerificationSettings
    {
        [JsonProperty("max_sms_count")] public string MaxSmsCount { get; set; }

        [JsonProperty("resend_sms_delay_sec")] public int? ResendSmsDelaySeconds { get; set; }

        [JsonProperty("robocall_after_max_sms")]
        public bool? RobocallAfterMaxSms { get; set; }

        [JsonProperty("robocall_count_down_time")]
        public int? RobocallCountDownTime { get; set; }
    }
}