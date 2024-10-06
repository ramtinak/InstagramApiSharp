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
    public class InstaStoryLinkResponse
    {
        [JsonProperty("link_type")]
        public string LinkType { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("link_title")]
        public string LinkTitle { get; set; }
    }
}
