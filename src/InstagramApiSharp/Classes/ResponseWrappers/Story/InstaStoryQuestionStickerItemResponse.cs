using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryQuestionStickerItemResponse
    {
        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }
        [JsonProperty("question_id")]
        public long QuestionId { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("question_type")]
        public string QuestionType { get; set; }
        [JsonProperty("text_color")]
        public string TextColor { get; set; }
        [JsonProperty("viewer_can_interact")]
        public bool ViewerCanInteract { get; set; }
    }
}
