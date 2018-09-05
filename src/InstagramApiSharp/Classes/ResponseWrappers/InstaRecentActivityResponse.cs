using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaRecentActivityResponse : BaseLoadableResponse
    {
        public bool IsOwnActivity { get; set; } = false;

        [JsonProperty("stories")]
        public List<InstaRecentActivityFeedResponse> Stories { get; set; }
            = new List<InstaRecentActivityFeedResponse>();
    }
}