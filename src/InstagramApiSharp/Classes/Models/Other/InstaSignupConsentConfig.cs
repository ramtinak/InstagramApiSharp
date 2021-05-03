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
    public class InstaSignupConsentConfig : InstaDefaultResponse
    {
        [JsonProperty("age_required")]
        public bool? AgeRequired { get; set; }

        [JsonProperty("gdpr_required")]
        public bool? GdprRequired { get; set; }

        [JsonProperty("tos_acceptance_not_required")]
        public bool? TosAcceptanceNotRequired { get; set; }
    }
}
