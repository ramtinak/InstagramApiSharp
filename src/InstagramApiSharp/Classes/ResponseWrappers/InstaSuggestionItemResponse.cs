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
    public class InstaSuggestionItemResponse
    {
        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("algorithm")] public string Algorithm { get; set; }

        [JsonProperty("social_context")] public string SocialContext { get; set; }

        [JsonProperty("icon")] public string Icon { get; set; }

        [JsonProperty("caption")] public string Caption { get; set; }

        [JsonProperty("media_ids")] public string[] MediaIds { get; set; }

        [JsonProperty("thumbnail_urls")] public string[] ThumbnailUrls { get; set; }

        [JsonProperty("large_urls")] public string[] LargeUrls { get; set; }

        [JsonProperty("media_infos")] public List<InstaMediaItemResponse> MediaInfos { get; set; }

        [JsonProperty("value")] public float? Value { get; set; }

        [JsonProperty("is_new_suggestion")] public bool IsNewSuggestion { get; set; }

        [JsonProperty("uuid")] public string Uuid { get; set; }
    }
    public class InstaSuggestionItemListResponse : List<InstaSuggestionItemResponse> { }

}
