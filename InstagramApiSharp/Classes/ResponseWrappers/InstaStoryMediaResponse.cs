using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryMediaResponse
    {
        [JsonProperty("media")] public InstaStoryItemResponse Media { get; set; }
    }
}