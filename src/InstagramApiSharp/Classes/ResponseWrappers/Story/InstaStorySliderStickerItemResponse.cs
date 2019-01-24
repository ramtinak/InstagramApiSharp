using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStorySliderStickerItemResponse
    {
        [JsonProperty("text_color")]
        public string TextColor { get; set; }
        [JsonProperty("slider_id")]
        public long SliderId { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("emoji")]
        public string Emoji { get; set; }
        [JsonProperty("viewer_can_vote")]
        public bool ViewerCanVote { get; set; }
        [JsonProperty("slider_vote_count")]
        public long? SliderVoteCount { get; set; }
        [JsonProperty("slider_vote_average")]
        public double? SliderVoteAverage { get; set; }
    }
}
