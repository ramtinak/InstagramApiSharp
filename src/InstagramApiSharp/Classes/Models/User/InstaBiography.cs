/*
 * Developer: Ramtin Jokar[Ramtinak@live.com] [My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaBiography
    {
        [JsonProperty("user")]
        public InstaBiographyUser User { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaBiographyUser
    {
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("biography_with_entities")]
        public InstaBiographyEntities BiographyWithEntities { get; set; }
    }

    public class InstaBiographyEntities
    {
        [JsonProperty("nux_type")]
        public string NuxType { get; set; }
        [JsonProperty("raw_text")]
        public string Text { get; set; }
        [JsonProperty("Entities")]
        public InstaBiograpyEntity[] Entities { get; set; }
    }

    public class InstaBiograpyEntity
    {
        [JsonProperty("hashtag")]
        public InstaBiographyItem Hashtag { get; set; }
        [JsonProperty("user")]
        public InstaBiographyItem User { get; set; }
    }

    public class InstaBiographyItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
