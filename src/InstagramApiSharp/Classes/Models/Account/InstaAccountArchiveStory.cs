using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Models
{
    internal class InstaAccountArchiveStory
    {
        [JsonProperty("reel_auto_archive")]
        public string ReelAutoArchive { get; set; }
        [JsonProperty("message_prefs")]
        public string MessagePrefs { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
