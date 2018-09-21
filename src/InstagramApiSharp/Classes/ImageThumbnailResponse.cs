using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class ImageThumbnailResponse
    {
        [JsonProperty("upload_id")]
        public string UploadId { get; set; }
        [JsonProperty("xsharing_nonces")]
        public object XSharingNonces { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
