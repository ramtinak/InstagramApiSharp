using System;

using Newtonsoft.Json;
namespace InstaAPI.Classes
{
    class InstaLoginChallengeMethodResponse
    {
        [JsonProperty("step_name")]
        public string StepName { get; set; }
        [JsonProperty("step_data")]
        public StepData StepData { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("nonce_code")]
        public string NonceCode { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class StepData
    {
        [JsonProperty("choice")]
        public string Choice { get; set; }
        [JsonProperty("fb_access_token")]
        public string FbAccessToken { get; set; }
        [JsonProperty("big_blue_token")]
        public string BigBlueToken { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
