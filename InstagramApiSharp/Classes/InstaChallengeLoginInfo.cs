using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class InstaChallengeLoginInfo
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("api_path")]
        public string ApiPath { get; set; }
        [JsonProperty("hide_webview_header")]
        public bool HideWebviewHeader { get; set; }
        [JsonProperty("lock")]
        public bool Lock { get; set; }
        [JsonProperty("logout")]
        public bool Logout { get; set; }
        [JsonProperty("native_flow")]
        public bool NativeFlow { get; set; }

    }
}
