using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class InstaChallengeLoginInfo
    {
        public string url { get; set; }
        public string api_path { get; set; }
        public bool hide_webview_header { get; set; }
        [JsonProperty("lock")]
        public bool Lock { get; set; }
        public bool logout { get; set; }
        public bool native_flow { get; set; }

    }
}
