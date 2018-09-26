using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaSuggestionItemResponse
    {
        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        //[JsonProperty("algorithm")] public string Algorithm { get; set; }

        [JsonProperty("social_context")] public string SocialContext { get; set; }

        //[JsonProperty("icon")] public string Icon { get; set; }

        [JsonProperty("caption")] public string Caption { get; set; }

        //[JsonProperty("media_ids")] public object[] media_ids { get; set; }

        //[JsonProperty("thumbnail_urls")] public object[] thumbnail_urls { get; set; }

        //[JsonProperty("large_urls")] public object[] large_urls { get; set; }

        //[JsonProperty("media_infos")] public object[] media_infos { get; set; }

        //[JsonProperty("value")] public float value { get; set; }

        [JsonProperty("is_new_suggestion")] public bool IsNewSuggestion { get; set; }

        //[JsonProperty("uuid")] public string Uuid { get; set; }
    }
    

}
