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
using System.Text;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaProductTagsContainerResponse
    {
        [JsonProperty("in")]
        public List<InstaProductContainerResponse> In { get; set; }
    }
    public class InstaProductContainerResponse
    {
        [JsonProperty("product")]
        public InstaProductResponse Product { get; set; }
        [JsonProperty("position")]
        public double[] Position { get; set; }
    }

    public class InstaProductResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("current_price")]
        public string CurrentPrice { get; set; }
        [JsonProperty("full_price")]
        public string FullPrice { get; set; }
        [JsonProperty("product_id")]
        public long ProductId { get; set; }
        [JsonProperty("has_viewer_saved")]
        public bool HasViewerSaved { get; set; }
        [JsonProperty("main_image")]
        public InstaProductImageResponse MainImage { get; set; }
        [JsonProperty("thumbnail_image")]
        public InstaProductImageResponse ThumbnailImage { get; set; }
        [JsonProperty("review_status")]
        public string ReviewStatus { get; set; }
        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }
        [JsonProperty("checkout_style")]
        public string CheckoutStyle { get; set; }
        [JsonProperty("merchant")]
        public InstaMerchantResponse Merchant { get; set; }
        [JsonProperty("product_images")]
        public List<InstaProductImageResponse> ProductImages { get; set; }
        [JsonProperty("product_appeal_review_status")]
        public string ProductAppealReviewStatus { get; set; }
        [JsonProperty("full_price_stripped")]
        public string FullPriceStripped { get; set; }
        [JsonProperty("current_price_stripped")]
        public string CurrentPriceStripped { get; set; }
    }

    public class InstaProductImageResponse
    {
        [JsonProperty("image_versions2")]
        public InstaImageCandidatesResponse Images { get; set; }
    }
    

    public class InstaMerchantResponse
    {
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicture { get; set; }
    }
}
