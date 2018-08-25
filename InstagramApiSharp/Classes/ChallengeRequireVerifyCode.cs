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
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    ///// <summary>
    /////{
    /////    "message": "Please check the code we sent you and try again.",
    /////    "status": "fail"
    /////}
    /////<para></para>
    /////{
    /////    "logged_in_user": {
    /////        "is_business": false,
    /////        "show_insights_terms": false,
    /////        "can_see_organic_insights": false,
    /////        "nametag": {
    /////            "mode": 1,
    /////            "gradient": 0,
    /////            "emoji": "\ud83d\ude00",
    /////            "selfie_sticker": 0
    /////        },
    /////        "profile_pic_id": "1797965765704346931_7405924766",
    /////        "is_verified": false,
    /////        "is_private": true,
    /////        "has_anonymous_profile_picture": false,
    /////        "pk": 7405924766,
    /////        "can_boost_post": false,
    /////        "full_name": "",
    /////        "username": "freemtprotos",
    /////        "profile_pic_url": "https://scontent-frx5-1.cdninstagram.com/vp/5dd46fe09e1a35fa24ca783785860642/5BF1AEBF/t51.2885-19/s150x150/34870813_236930056861157_9221677749565915136_n.jpg",
    /////        "allowed_commenter_type": "any",
    /////        "reel_auto_archive": "on",
    /////        "allow_contacts_sync": false,
    /////        "phone_number": "+989174314006",
    /////        "country_code": 98,
    /////        "national_number": 9174314006
    /////    },
    /////    "action": "close",
    /////    "auto_login": true,
    /////    "status": "ok"
    /////}
    ///// </summary>
    public class ChallengeRequireVerifyCode
    {
        [JsonIgnore]
        public bool IsLoggedIn { get { return LoggedInUser != null || Status.ToLower() == "ok"; } }
        [JsonProperty("logged_in_user")]
        public /*InstaUserInfoResponse*/InstaUserShortResponse LoggedInUser { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("action")]
        internal string Action { get; set; }
    }

}
