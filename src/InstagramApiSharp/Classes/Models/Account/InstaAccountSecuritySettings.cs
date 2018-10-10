/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
   
    public class InstaAccountSecuritySettings
    {
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("country_code")]
        public int CountryCode { get; set; }
        [JsonProperty("national_number")]
        public long NationalNumber { get; set; }
        [JsonProperty("is_phone_confirmed")]
        public bool IsPhoneConfirmed { get; set; }
        [JsonProperty("is_two_factor_enabled")]
        public bool IsTwoFactorEnabled { get; set; }
        [JsonProperty("backup_codes")]
        public List<string> BackupCodes { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
    }

}
