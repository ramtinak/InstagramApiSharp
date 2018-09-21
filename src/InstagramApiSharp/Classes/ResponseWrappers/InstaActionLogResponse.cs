using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaActionLogResponse
    {
        [JsonProperty("description")] public string Description { get; set; }
    }
}
