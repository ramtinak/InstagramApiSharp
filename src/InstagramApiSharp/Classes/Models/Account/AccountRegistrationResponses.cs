/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class InstaCheckEmailRegistration
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
        [JsonProperty("tos_version")]
        public string TosVersion { get; set; }
        [JsonProperty("gdpr_required")]
        public bool GdprRequired { get; set; }
        [JsonProperty("username_suggestions_with_metadata")]
        public InstaRegistrationSuggestionsList UsernameSuggestionsWithMetadata { get; set; }

        [JsonProperty("suggestions_with_metadata")]
        public InstaRegistrationSuggestionsList SuggestionsWithMetadata { get; set; }

    }

    public class InstaRegistrationSuggestionResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("suggestions_with_metadata")]
        public InstaRegistrationSuggestionsList SuggestionsWithMetadata { get; set; }

    }
    public class InstaRegistrationSuggestionsList
    {
        [JsonProperty("suggestions")]
        public InstaRegistrationSuggestion[] Suggestions { get; set; }
    }

    public class InstaRegistrationSuggestion
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("prototype")]
        public string Prototype { get; set; }
    }

}
