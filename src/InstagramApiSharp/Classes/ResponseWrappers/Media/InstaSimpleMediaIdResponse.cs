/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaSimpleMediaIdResponse : InstaDefaultResponse
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
    }
}
