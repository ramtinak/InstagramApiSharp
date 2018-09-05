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

namespace InstagramApiSharp.Classes.Models
{
    public class InstaContactUserListResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("items")]
        public InstaContactUserList Items { get; set; }
    }
    public class InstaContactUserList : List<InstaContactUser> { }

    public class InstaContactUser
    {
        [JsonProperty("user")]
        public InstaUserContact User { get; set; }
    }
    public class InstaUserContact : InstaUserShortResponse
    {
        private string extraDisplay;
        [JsonProperty("extra_display_name")]
        public string ExtraDisplayName
        {
            get { return extraDisplay; }
            set { extraDisplay = value; }
        }

        //private bool hasExtra_ = false;
        [JsonIgnore]
        public bool HasExtraInfo
        {
            get
            {
                if (string.IsNullOrEmpty(ExtraDisplayName))
                    return false;

                return !string.IsNullOrEmpty(ExtraDisplayName);
            }
        }
    }
    public class InstaContactList : List<InstaContact> { }

    public class InstaContact
    {
        [JsonProperty("phone_numbers")]
        public string[] PhoneNumbers { get; set; }
        [JsonProperty("email_addresses")]
        public string[] EmailAddresses { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}
