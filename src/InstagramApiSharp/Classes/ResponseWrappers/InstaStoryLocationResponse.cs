using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryLocationResponse
    {
        [JsonProperty("rotation")] public long Rotation { get; set; }

        [JsonProperty("is_pinned")] public long IsPinned { get; set; }

        [JsonProperty("height")] public double Height { get; set; }

        [JsonProperty("location")] public InstaLocationResponse Location { get; set; }

        [JsonProperty("x")] public double X { get; set; }

        [JsonProperty("width")] public double Width { get; set; }

        [JsonProperty("y")] public double Y { get; set; }
    }
}