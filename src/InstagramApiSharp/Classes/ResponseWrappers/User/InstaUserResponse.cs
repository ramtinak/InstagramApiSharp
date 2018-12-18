/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaUserContainerResponse : InstaDefault
    {
        [JsonProperty("user")] public InstaUserResponse User { get; set; }
    }
    public class InstaUserResponse : InstaUserShortResponse
    {
        [JsonProperty("friendship_status")] public InstaFriendshipShortStatusResponse FriendshipStatus { get; set; }

        [JsonProperty("has_anonymous_profile_picture")] public bool HasAnonymousProfilePicture { get; set; }

        [JsonProperty("follower_count")] public int FollowersCount { get; set; }

        [JsonProperty("byline")] public string FollowersCountByLine { get; set; }

        [JsonProperty("social_context")] public string SocialContext { get; set; }

        [JsonProperty("search_social_context")] public string SearchSocialContext { get; set; }

        [JsonProperty("mutual_followers_count")] public string MulualFollowersCount { get; set; }

        [JsonProperty("unseen_count")] public int UnseenCount { get; set; }
    }
}