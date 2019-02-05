using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryFeedResponse : BaseStatusResponse
    {
        [JsonProperty("face_filter_nux_version")]
        public int FaceFilterNuxVersion { get; set; }

        [JsonProperty("has_new_nux_story")] public bool HasNewNuxStory { get; set; }

        [JsonProperty("story_ranking_token")] public string StoryRankingToken { get; set; }

        [JsonProperty("sticker_version")] public int StickerVersion { get; set; }

        [JsonProperty("tray")] public List</*InstaReelFeedResponse*/JToken> Tray { get; set; }

        [JsonProperty("broadcasts")] public List<InstaBroadcastResponse> Broadcasts { get; set; }

        [JsonProperty("post_live")] public InstaBroadcastAddToPostLiveContainerResponse PostLives { get; set; }
    }
}