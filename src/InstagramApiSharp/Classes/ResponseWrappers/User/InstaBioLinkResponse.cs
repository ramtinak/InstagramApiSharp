/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBioLinkResponse
    {
        [JsonProperty("link_id")]
        public long LinkId { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("lynx_url")]
        public string LynxUrl { get; set; }
        [JsonProperty("link_type")]
        public string LinkType { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("media_type")]
        public string MediaType { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        [JsonProperty("is_pinned")]
        public bool? IsPinned { get; set; }
        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("open_external_url_with_in_app_browser")]
        public bool OpenExternalUrlWithInAppBrowser { get; set; }
    }
}
