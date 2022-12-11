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
    public class InstaFriendshipFullStatusContainerResponse
    {
        [JsonProperty("friendship_status")]
        public InstaFriendshipFullStatusResponse FriendshipStatus { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaFriendshipFullStatusResponse
    {
        [JsonProperty("following")]
        public bool? Following { get; set; }
        [JsonProperty("followed_by")]
        public bool? FollowedBy { get; set; }
        [JsonProperty("blocking")]
        public bool? Blocking { get; set; }
        [JsonProperty("muting")]
        public bool? Muting { get; set; }
        [JsonProperty("is_private")]
        public bool? IsPrivate { get; set; }
        [JsonProperty("incoming_request")]
        public bool? IncomingRequest { get; set; }
        [JsonProperty("outgoing_request")]
        public bool? OutgoingRequest { get; set; }
        [JsonProperty("is_bestie")]
        public bool? IsBestie { get; set; }
    }
}
