/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaProductInfoResponse : InstaDefault
    {
        [JsonProperty("product_item")] public InstaProductResponse Product { get; set; }

        [JsonProperty("other_product_items")] public List<InstaProductResponse> OtherProductItems { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("more_from_business")] public InstaProductMediaListResponse MoreFromBusiness { get; set; }
    }

    public class InstaProductMediaListResponse : BaseLoadableResponse
    {
        [JsonProperty("items")] public List<InstaMediaItemResponse> Medias { get; set; } = new List<InstaMediaItemResponse>();
    }

}
