using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaUserResponse : InstaUserShortResponse
    {
        [JsonProperty("friendship_status")] public InstaFriendshipStatusResponse FriendshipStatus { get; set; }

        [JsonProperty("has_anonymous_profile_picture")]
        public bool HasAnonymousProfilePicture { get; set; }

        [JsonProperty("follower_count")] public int FollowersCount { get; set; }

        [JsonProperty("byline")] public string FollowersCountByLine { get; set; }

        [JsonProperty("social_context")] public string SocialContext { get; set; }

        [JsonProperty("search_social_context")]
        public string SearchSocialContext { get; set; }

        [JsonProperty("mutual_followers_count")]
        public string MulualFollowersCount { get; set; }

        [JsonProperty("unseen_count")] public int UnseenCount { get; set; }
    }
}