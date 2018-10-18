using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaOwnerResponse
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("pk")] public long Pk { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("profile_pic_url")] public string ProfilePicUrl { get; set; }

        [JsonProperty("profile_pic_username")] public string ProfilePicUsername { get; set; }

        [JsonProperty("short_name")] public string ShortName { get; set; }

        [JsonProperty("lng")] public double Lng { get; set; }

        [JsonProperty("lat")] public double Lat { get; set; }
    }
}