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
    public class InstaChallengeRequireVerifyMethod
    {
        [JsonProperty("step_name")]
        public string StepName { get; set; }
        [JsonProperty("step_data")]
        public InstaChallengeRequireStepData StepData { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("nonce_code")]
        public string NonceCode { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
        [JsonProperty("message")]
        internal string Message { get; set; }

        public bool SubmitPhoneRequired => StepName == "submit_phone";
    }

    public class InstaChallengeRequireStepData
    {
        [JsonProperty("choice")]
        public string Choice { get; set; }
        [JsonProperty("fb_access_token")]
        public string FbAccessToken { get; set; }
        [JsonProperty("big_blue_token")]
        public string BigBlueToken { get; set; }
        [JsonProperty("google_oauth_token")]
        public string GoogleOauthToken { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        public bool SubmitPhoneRequired => PhoneNumber.ToLower().Contains("none");
    }

}
