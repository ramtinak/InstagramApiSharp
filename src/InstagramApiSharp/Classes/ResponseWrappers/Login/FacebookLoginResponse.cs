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
namespace InstagramApiSharp.Classes
{
    internal class InstaFacebookLoginResponse
    {
        [JsonProperty("logged_in_user")]
        public InstaUserShortResponse LoggedInUser { get; set; }
        [JsonProperty("code")]
        public int? Code { get; set; }
        [JsonProperty("fb_user_id")]
        public string FbUserId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("created_user")]
        public InstaUserShortResponse CreatedUser { get; set; }
        [JsonProperty("multiple_users_on_device")]
        public bool? MultipleUsersOnDevice { get; set; }
    }

    internal class InstaFacebookRegistrationResponse
    {
        [JsonProperty("account_created")]
        public bool? AccountCreated { get; set; }
        [JsonProperty("dryrun_passed")]
        public bool? DryrunPassed { get; set; }
        [JsonProperty("tos_version")]
        public string TosVersion { get; set; }
        [JsonProperty("gdpr_required")]
        public bool? GdprRequired { get; set; }
        [JsonProperty("fb_user_id")]
        public string FbUserId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("username_suggestions_with_metadata")]
        public InstaRegistrationSuggestionsList UsernameSuggestionsWithMetadata { get; set; }
    }
}
