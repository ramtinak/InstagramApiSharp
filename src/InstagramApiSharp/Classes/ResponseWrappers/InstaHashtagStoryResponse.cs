/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaHashtagStoryContainerResponse
    {
        [JsonProperty("story")]
        public InstaHashtagStoryResponse Story { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaHashtagStoryResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("latest_reel_media")]
        public int LatestReelMedia { get; set; }
        [JsonProperty("expiring_at")]
        public long ExpiringAt { get; set; }
        //[JsonProperty("seen")]
        //public object Seen { get; set; }
        [JsonProperty("can_reply")]
        public bool CanReply { get; set; }
        [JsonProperty("can_reshare")]
        public bool CanReshare { get; set; }
        [JsonProperty("reel_type")]
        public string ReelType { get; set; }
        [JsonProperty("owner")]
        public InstaHashtagOwnerResponse Owner { get; set; }
        [JsonProperty("items")]
        public List<InstaStoryItemResponse> Items { get; set; }
        [JsonProperty("prefetch_count")]
        public int PrefetchCount { get; set; }
        [JsonProperty("unique_integer_reel_id")]
        public long UniqueIntegerReelId { get; set; }
        [JsonProperty("muted")]
        public bool Muted { get; set; }
    }

    public class InstaHashtagOwnerResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("pk")]
        public string Pk { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }
        [JsonProperty("profile_pic_username")]
        public string ProfilePicUsername { get; set; }
    }

}
