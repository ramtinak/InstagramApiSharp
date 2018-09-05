using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaCollectionsResponse : BaseLoadableResponse
    {
        [JsonProperty("items")] public List<InstaCollectionItemResponse> Items { get; set; }
    }
}