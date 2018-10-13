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
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Classes.Models.Business
{
    public class InstaBusinessCategory
    {
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
    }
    public class InstaBusinessCategoryList : List<InstaBusinessCategory> { }

  
    public class InstaBusinessSuggestedCategory : InstaBusinessCategory
    {
        [JsonProperty("super_cat_name")]
        public string SuperCatName { get; set; }
        [JsonProperty("super_cat_id")]
        public string SuperCatIid { get; set; }
    }
    public class InstaBusinessSuggestedCategoryList : List<InstaBusinessSuggestedCategory> { }


    internal class InstaBusinessCategoryContainer
    {
        [JsonExtensionData]
        internal IDictionary<string, JToken> Extras { get; set; }
    }
}
