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
    public class CheckEmailRegistration
    {
        [JsonProperty("valid")]
        public bool Valid { get; set; }
        [JsonProperty("available")]
        public bool Available { get; set; }
        [JsonProperty("confirmed")]
        public bool Confirmed { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
        //[JsonProperty("tos_version")]
        //public string TOsVersion { get; set; }
        //[JsonProperty("gdpr_required")]
        //public bool GdprRequired { get; set; }
        [JsonProperty("username_suggestions_with_metadata")]
        public RegistrationSuggestionsList UsernameSuggestionsWithMetadata { get; set; }

        [JsonProperty("suggestions_with_metadata")]
        public RegistrationSuggestionsList SuggestionsWithMetadata { get; set; }

    }

    public class RegistrationSuggestionResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("suggestions_with_metadata")]
        public RegistrationSuggestionsList SuggestionsWithMetadata { get; set; }

    }
    public class RegistrationSuggestionsList
    {
        [JsonProperty("suggestions")]
        public RegistrationSuggestion[] Suggestions { get; set; }
    }

    public class RegistrationSuggestion
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("prototype")]
        public string Prototype { get; set; }
    }

}
