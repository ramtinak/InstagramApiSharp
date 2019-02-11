/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaAccountDetailsResponse
    {
        [JsonProperty("date_joined")]
        public int? DateJoined { get; set; }
        [JsonProperty("former_username_info")]
        public InstaFormerUsernameInfoResponse FormerUsernameInfo { get; set; }
        [JsonProperty("primary_country_info")]
        public InstaPrimaryCountryInfoResponse PrimaryCountryInfo { get; set; }
        [JsonProperty("shared_follower_accounts_info")]
        public InstaSharedFollowerAccountsInfoResponse SharedFollowerAccountsInfo { get; set; }
        [JsonProperty("ads_info")]
        public InstaAdsInfoResponse AdsInfo { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaFormerUsernameInfoResponse
    {
        [JsonProperty("has_former_usernames")]
        public bool? HasFormerUsernames { get; set; }
    }

    public class InstaSharedFollowerAccountsInfoResponse
    {
        [JsonProperty("has_shared_follower_accounts")]
        public bool? HasSharedFollowerAccounts { get; set; }
    }
}
