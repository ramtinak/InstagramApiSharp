/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaContact
    {
        [JsonProperty("phone_numbers")]
        public List<string> PhoneNumbers { get; set; }
        [JsonProperty("email_addresses")]
        public List<string> EmailAddresses { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
    public class InstaContactList : List<InstaContact> { }

    public class InstaUserContact : InstaUserShort
    {
        public string ExtraDisplayName { get; set; }


        public bool HasExtraInfo
        {
            get
            {
                return !string.IsNullOrEmpty(ExtraDisplayName);
            }
        }
    }
    public class InstaContactUserList : List<InstaUserContact> { }

}
