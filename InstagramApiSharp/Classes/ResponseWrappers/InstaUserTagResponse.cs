using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaUserTagResponse
    {
        [JsonProperty("position")] public double[] Position { get; set; }

        [JsonProperty("time_in_video")] public string TimeInVideo { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }
    }
}