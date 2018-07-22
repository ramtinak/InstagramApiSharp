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
    ///// <summary>
    /////{
    /////    "step_name": "verify_email",
    /////    "step_data": {
    /////        "security_code": "None",
    /////        "resend_delay": 60,
    /////        "contact_point": "r*******r@yahoo.com",
    /////        "form_type": "email"
    /////    },
    /////    "user_id": 7405924766,
    /////    "nonce_code": "jygT2JF4dA",
    /////    "status": "ok"
    /////} 
    ///// </summary>
    public class ChallengeRequireEmailVerify
    {
        [JsonProperty("step_name")]
        public string StepName { get; set; }
        [JsonProperty("step_data")]
        public ChallengeRequireStepDataEmailVerify StepData { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("nonce_code")]
        public string NonceCode { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class ChallengeRequireStepDataEmailVerify
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
