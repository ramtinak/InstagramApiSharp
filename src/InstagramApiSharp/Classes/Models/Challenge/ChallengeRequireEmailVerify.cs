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
    public class InstaChallengeRequireEmailVerify
    {
        [JsonProperty("step_name")]
        public string StepName { get; set; }
        [JsonProperty("step_data")]
        public InstaChallengeRequireStepDataEmailVerify StepData { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("nonce_code")]
        public string NonceCode { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class InstaChallengeRequireStepDataEmailVerify
    {
        [JsonProperty("security_code")]
        public string SecurityCode { get; set; }
        [JsonProperty("resend_delay")]
        public int ResendDelay { get; set; }
        [JsonProperty("contact_point")]
        public string ContactPoint { get; set; }
        [JsonProperty("form_type")]
        public string FormType { get; set; }
    }

}
