using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaInboxMediaResponse
    {
        [JsonProperty("image_versions2")] public InstaImageCandidatesResponse ImageCandidates { get; set; }

        [JsonProperty("original_width")] public long OriginalWidth { get; set; }

        [JsonProperty("original_height")] public long OriginalHeight { get; set; }

        [JsonProperty("media_type")] public InstaMediaType MediaType { get; set; }

        [JsonProperty("video_versions")] public List<InstaVideoResponse> Videos { get; set; }
    }
}