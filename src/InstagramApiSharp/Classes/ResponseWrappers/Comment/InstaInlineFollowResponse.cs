/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaInlineFollowResponse
    {
        [JsonProperty("outgoing_request")] public bool IsOutgoingRequest { get; set; }

        [JsonProperty("following")] public bool IsFollowing { get; set; }

        [JsonProperty("user_info")] public InstaUserShortResponse UserInfo { get; set; }
    }
}