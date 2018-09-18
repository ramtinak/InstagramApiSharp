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
    public class AccountCheckResponse
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("available")]
        public bool Available { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
        [JsonProperty("error_type")]
        internal string ErrorType { get; set; }
    }

}
