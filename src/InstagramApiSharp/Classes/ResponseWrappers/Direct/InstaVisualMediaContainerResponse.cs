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
    public class InstaVisualMediaContainerResponse
    {
        [JsonProperty("url_expire_at_secs")] public long? UrlExpireAtSecs { get; set; }

        [JsonProperty("media")] public InstaVisualMediaResponse Media { get; set; }

        [JsonProperty("seen_count")] public int? SeenCount { get; set; }

        [JsonProperty("replay_expiring_at_us")] public long? ReplayExpiringAtUs { get; set; }

        [JsonProperty("view_mode")] public string ViewMode { get; set; }

        [JsonProperty("seen_user_ids")] public List<long> SeenUserIds { get; set; }
    }
}
