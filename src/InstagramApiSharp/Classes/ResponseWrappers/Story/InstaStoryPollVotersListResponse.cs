/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryPollVotersListContainerResponse : InstaDefault
    {
        [JsonProperty("voter_info")]
        public InstaStoryPollVotersListResponse VoterInfo { get; set; }
    }
    public class InstaStoryPollVotersListResponse
    {
        [JsonProperty("poll_id")]
        public long PollId { get; set; }
        [JsonProperty("voters")]
        public List<InstaStoryPollVoterItemResponse> Voters { get; set; } = new List<InstaStoryPollVoterItemResponse>();
        [JsonProperty("max_id")]
        public string MaxId { get; set; }
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }
        [JsonProperty("latest_poll_vote_time")]
        public long? LatestPollVoteTime { get; set; }
    }
}
