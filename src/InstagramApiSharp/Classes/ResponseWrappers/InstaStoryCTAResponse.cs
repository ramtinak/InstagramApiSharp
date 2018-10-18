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
    public class InstaStoryCTAContainerResponse
    {
        [JsonProperty("links")] public InstaStoryCTAResponse[] Links { get; set; }
    }
    public class InstaStoryCTAResponse
    {
        [JsonProperty("linkType")] public int LinkType { get; set; }

        [JsonProperty("webUri")] public string WebUri { get; set; }

        [JsonProperty("androidClass")] public string AndroidClass { get; set; }

        [JsonProperty("package")] public string Package { get; set; }

        [JsonProperty("deeplinkUri")] public string DeeplinkUri { get; set; }

        [JsonProperty("callToActionTitle")] public string CallToActionTitle { get; set; }

        [JsonProperty("redirectUri")] public object RedirectUri { get; set; }

        [JsonProperty("leadGenFormId")] public string LeadGenFormId { get; set; }

        [JsonProperty("igUserId")] public string IgUserId { get; set; }
    }
}
