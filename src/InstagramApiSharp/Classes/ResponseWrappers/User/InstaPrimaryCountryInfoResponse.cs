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
    public class InstaPrimaryCountryInfoResponse
    {
        [JsonProperty("is_visible")]
        public bool? IsVisible { get; set; }
        [JsonProperty("has_country")]
        public bool? HasCountry { get; set; }
        [JsonProperty("country_name")]
        public string CountryName { get; set; }
    }
}
