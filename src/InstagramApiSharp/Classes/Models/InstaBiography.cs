using System;
using System.Collections.Generic;
using System.Text;
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
