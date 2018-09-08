using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaPermalinkResponse : BaseStatusResponse
    {
        [JsonProperty("permalink")] public string Permalink { get; set; }
    }
}