/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryQuestionInfoResponse
    {
        [JsonProperty("question_id")]
        public long QuestionId { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("question_type")]
        public string QuestionType { get; set; }
        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }
        [JsonProperty("text_color")]
        public string TextColor { get; set; }
        [JsonProperty("responders")]
        public List<InstaStoryQuestionResponderResponse> Responders { get; set; }
        [JsonProperty("max_id")]
        public string MaxId { get; set; }
        [JsonProperty("more_available")]
        public bool? MoreAvailable { get; set; }
        [JsonProperty("question_response_count")]
        public int? QuestionResponseCount { get; set; }
        [JsonProperty("latest_question_response_time")]
        public long? LatestQuestionResponseTime { get; set; }
    }
}
