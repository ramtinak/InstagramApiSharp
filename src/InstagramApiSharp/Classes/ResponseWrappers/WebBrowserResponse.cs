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
    public class InstaWebBrowserResponse
    {
        [JsonProperty("config")]
        public InstaWebBrowserResponseConfig Config { get; set; }
    }

    public class InstaWebBrowserResponseConfig
    {
        [JsonProperty("csrf_token")]
        public string CsrfToken { get; set; }
        [JsonProperty("viewer")]
        public InstaWebBrowserResponseViewer Viewer { get; set; }
    }

    public class InstaWebBrowserResponseViewer
    {
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("external_url")]
        public object ExternalUrl { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("has_profile_pic")]
        public bool HasProfilePic { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }
        [JsonProperty("profile_pic_url_hd")]
        public string ProfilePicUrlHd { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }


}
