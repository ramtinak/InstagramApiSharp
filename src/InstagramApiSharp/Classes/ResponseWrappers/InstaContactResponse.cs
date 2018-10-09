/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaContactUserListResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("items")]
        public List<InstaContactUserResponse> Items { get; set; }
    }

    public class InstaContactUserResponse
    {
        [JsonProperty("user")]
        public InstaUserContactResponse User { get; set; }
    }
    public class InstaUserContactResponse : InstaUserShortResponse
    {
        [JsonProperty("extra_display_name")]
        public string ExtraDisplayName { get; set; }
    }

}
