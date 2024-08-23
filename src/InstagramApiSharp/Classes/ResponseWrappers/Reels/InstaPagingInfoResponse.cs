/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * IRANIAN DEVELOPERS
 */
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaPagingInfoResponse
    {
        [JsonProperty("max_id")]
        public string MaxId { get; set; }
        [JsonProperty("more_available")]
        public bool? MoreAvailable { get; set; }
    }
}
