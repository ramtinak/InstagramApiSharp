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
    public class InstaAccountCreation
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("account_created")]
        public bool AccountCreated { get; set; }
        [JsonProperty("created_user")]
        public InstaUserShortResponse CreatedUser { get; set; }
    }
    internal class InstaAccountCreationResponse : InstaAccountCreation
    {
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
        [JsonProperty("errors")]
        public InstaAccountCreationErrors Errors { get; set; }
    }

    public class InstaAccountCreationErrors
    {
        [JsonProperty("username")]
        public string[] Username { get; set; }
    }

}
