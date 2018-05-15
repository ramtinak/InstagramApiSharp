using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class VideoUploadJobResponse
    {
        [JsonProperty("video_upload_urls")]
        public List<VideoUploadUrl> VideoUploadUrls { get; set; }
        [JsonProperty("upload_id")]
        public string UploadId { get; set; }
        [JsonProperty("xsharing_nonces")]
        public object XSharingNonces { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class VideoUploadUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("job")]
        public string Job { get; set; }
        [JsonProperty("expires")]
        public double Expires { get; set; }
    }
}
