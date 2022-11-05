/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryVoterItemResponse
    {
        [JsonProperty("user")]
        public InstaUserShortFriendshipResponse User { get; set; }
        [JsonProperty("vote")]
        public double? Vote { get; set; }
        [JsonProperty("ts")]
        public long Ts { get; set; }
    }
}
