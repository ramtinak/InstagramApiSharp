/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTopicalExploreFeedResponse : BaseLoadableResponse
    {
        [JsonProperty("clusters")] public List<InstaTopicalExploreClusterResponse> Clusters { get; set; } = new List<InstaTopicalExploreClusterResponse>();

        [JsonIgnore] public List<InstaMediaItemResponse> Medias { get; set; } = new List<InstaMediaItemResponse>();

        [JsonIgnore] public InstaChannelResponse Channel { get; set; }

        [JsonIgnore] public List<InstaTVChannelResponse> TVChannels { get; set; } = new List<InstaTVChannelResponse>();

        [JsonProperty("max_id")] public string MaxId { get; set; }

        [JsonProperty("has_shopping_channel_content")] public bool? HasShoppingChannelContent { get; set; }
    }
}
