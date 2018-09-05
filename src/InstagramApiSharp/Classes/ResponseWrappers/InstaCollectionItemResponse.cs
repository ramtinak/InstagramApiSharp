using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaCollectionItemResponse : BaseLoadableResponse
    {
        [JsonProperty("collection_id")] public long CollectionId { get; set; }

        [JsonProperty("collection_name")] public string CollectionName { get; set; }

        [JsonProperty("has_related_media")] public bool HasRelatedMedia { get; set; }

        [JsonProperty("cover_media")] public InstaCoverMediaResponse CoverMedia { get; set; }

        [JsonProperty("items")] public InstaMediaListResponse Media { get; set; }
    }
}