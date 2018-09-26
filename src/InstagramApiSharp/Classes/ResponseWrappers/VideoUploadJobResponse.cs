/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
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
