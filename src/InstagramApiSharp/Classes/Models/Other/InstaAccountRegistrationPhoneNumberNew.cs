/*
 * This file is a part of private version of InstagramApiSharp's project
 * 
 * 
 * Developer: Ramtin Jokar [ Ramtinak@live.com ]
 * 
 * 
 * IRANIAN DEVELOPERS (c) 2021
 */

using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class InstaAccountRegistrationPhoneNumberNew : InstaDefaultResponse
    {
        [JsonProperty("error_source")]
        internal string ErrorSource { get; set; }
        [JsonProperty("error_type")]
        internal string ErrorType { get; set; }
        [JsonProperty("tos_version")]
        public string TosVersion { get; set; }
        [JsonProperty("gdpr_required")]
        public bool GdprRequired { get; set; }
    }
}