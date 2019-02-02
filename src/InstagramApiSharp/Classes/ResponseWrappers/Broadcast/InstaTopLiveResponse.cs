using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTopLiveResponse
    {
        [JsonProperty("ranked_position")] public int RankedPosition { get; set; }

        [JsonProperty("broadcast_owners")]
        public List<InstaUserShortFriendshipFullResponse> BroadcastOwners { get; set; } = new List<InstaUserShortFriendshipFullResponse>();
    }
}