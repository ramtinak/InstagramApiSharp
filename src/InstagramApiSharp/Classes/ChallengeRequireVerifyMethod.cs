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
    ///// <summary>
    /////{
    /////    "step_name": "select_verify_method",
    /////    "step_data": {
    /////        "choice": "1",
    /////        "fb_access_token": "None",
    /////        "big_blue_token": "None",
    /////        "google_oauth_token": "true",
    /////        "email": "r*******r@yahoo.com",
    /////        "phone_number": "+98 *** *** **06"
    /////    },
    /////    "user_id": 7405924766,
    /////    "nonce_code": "jygT2JF4dA",
    /////    "status": "ok"
    /////}
    /////
    ///// Vase id haei ke na phone set kardan, na email
    /////
    /////{
    /////	"step_name": "submit_phone",
    /////	"step_data": {
    /////		"phone_number": "None"
    /////	},
    /////	"user_id": 8536818704,
    /////	"nonce_code": "vAs9yOWHBz",
    /////	"status": "ok"
    /////}
    ///// </summary>
    public class ChallengeRequireVerifyMethod
    {
        [JsonProperty("step_name")]
        public string StepName { get; set; }
        [JsonProperty("step_data")]
        public ChallengeRequireStepData StepData { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("nonce_code")]
        public string NonceCode { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        internal string Message { get; set; }

        public bool SubmitPhoneRequired => StepName == "submit_phone";
    }

    public class ChallengeRequireStepData
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
