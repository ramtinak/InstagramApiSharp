/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes.ResponseWrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes
{
    public class AccountCreation
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("account_created")]
        public bool AccountCreated { get; set; }
        [JsonProperty("created_user")]
        public InstaUserShortResponse CreatedUser { get; set; }
    }
    internal class AccountCreationResponse : AccountCreation
    {
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
        [JsonProperty("errors")]
        public AccountCreationErrors Errors { get; set; }
    }

    public class AccountCreationErrors
    {
        [JsonProperty("username")]
        public string[] Username { get; set; }
    }

}
