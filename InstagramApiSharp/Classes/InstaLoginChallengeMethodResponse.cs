using System;

namespace InstaAPI.Classes
{
    class InstaLoginChallengeMethodResponse
    {
        public string step_name { get; set; }
        public Step_Data step_data { get; set; }
        public long user_id { get; set; }
        public string nonce_code { get; set; }
        public string status { get; set; }
    }
    public class Step_Data
    {
        public string choice { get; set; }
        public string fb_access_token { get; set; }
        public string big_blue_token { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
    }
}
