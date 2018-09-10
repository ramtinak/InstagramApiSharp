using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
namespace InstagramApiSharp.Classes.Models
{

    public class InstaTV
    {
        [JsonProperty("channels")]
        public List<InstaTVChannel> Channels { get; set; }
        [JsonProperty("my_channel")]
        public InstaTVSelfChannel MyChannel { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
        //public Badging badging { get; set; }
        //public Composer composer { get; set; }

    }
    public class InstaTVChannel
    {
        [JsonIgnore]
        public InstaTVChannelType Type { get { return PrivateType.GetChannelType(); } }
        [JsonProperty("type")]
        internal string PrivateType { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("items")]
        public List<InstaMediaItemResponse> Items { get; set; }
        [JsonProperty("more_available")]
        public bool HasMoreAvailable { get; set; }
        [JsonProperty("max_id")]
        public string MaxId { get; set; }
        //public Seen_State1 seen_state { get; set; }
    }
    public class InstaTVSelfChannel : InstaTVChannel
    {
        [JsonProperty("user_dict")]
        public InstaTVUser User { get; set; }
    }
    public class InstaTVUser
    {
        [JsonProperty("show_insights_terms")]
        public bool ShowInsightsTerms { get; set; }
        [JsonProperty("can_boost_post")]
        public bool CanBoostPost { get; set; }
        [JsonProperty("following_tag_count")]
        public int FollowingTagCount { get; set; }
        [JsonProperty("follower_count")]
        public int FollowerCount { get; set; }
        [JsonProperty("media_count")]
        public int MediaCount { get; set; }
        [JsonProperty("can_see_organic_insights")]
        public bool CanSeeOrganicInsights { get; set; }
        [JsonProperty("geo_media_count")]
        public int GeoMediaCount { get; set; }
        [JsonProperty("has_biography_translation")]
        public bool HasBiographyTranslation { get; set; }
        [JsonProperty("can_link_entities_in_bio")]
        public bool CanLinkEntitiesInBio { get; set; }
        [JsonProperty("has_placed_orders")]
        public bool HasPlacedOrders { get; set; }
        [JsonProperty("following_count")]
        public int FollowingCount { get; set; }
        [JsonProperty("biography_with_entities")]
        public InstaBiographyEntities BiographyWithEntities { get; set; }
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("external_lynx_url")]
        public string ExternalLynxUrl { get; set; }
        [JsonProperty("reel_auto_archive")]
        public string ReelAutoArchive { get; set; }
        [JsonProperty("has_anonymous_profile_picture")]
        public bool HasAnonymousProfilePicture { get; set; }
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("allowed_commenter_type")]
        public string AllowedCommenterType { get; set; }
        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("profile_pic_id")]
        public string ProfilePicId { get; set; }
        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }
        [JsonProperty("total_igtv_videos")]
        public int TotalIGTVVideosCount { get; set; }
    }



}
