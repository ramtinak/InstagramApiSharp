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
    public class InstaCheckAgeEligibility : InstaDefaultResponse
    {
        [JsonProperty("eligible_to_register")]
        public bool? EligibleToRegister { get; set; }

        [JsonProperty("parental_consent_required")]
        public bool? ParentalConsentRequired { get; set; }
    }
}
