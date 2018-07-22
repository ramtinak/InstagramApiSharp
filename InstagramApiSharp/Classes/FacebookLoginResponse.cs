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
    internal class FacebookLoginResponse
    {
        [JsonProperty("logged_in_user")]
        public LoggedInUser LoggedInUser { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("fb_user_id")]
        public string FbUserId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    internal class LoggedInUser
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }
        [JsonProperty("fbuid")]
        public string FBuid { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }

}
