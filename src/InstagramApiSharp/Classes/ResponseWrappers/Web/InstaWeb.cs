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
using Newtonsoft.Json;
using InstagramApiSharp.Enums;

namespace InstagramApiSharp.Classes.ResponseWrappers.Web
{
    public class InstaWebContainerResponse
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("config")]
        public InstaWebConfigResponse Config { get; set; }
        [JsonProperty("entry_data")]
        public InstaWebEntryDataResponse Entry { get; set; }
    }

    public class InstaWebConfigResponse
    {
        [JsonProperty("viewer")]
        public InstaUserShortResponse Viewer { get; set; }
    }

    public class InstaWebEntryDataResponse
    {
        [JsonProperty("SettingsPages")]
        public List<InstaWebSettingsPageResponse> SettingsPages { get; set; } = new List<InstaWebSettingsPageResponse>();
    }

    public class InstaWebSettingsPageResponse
    {
        [JsonProperty("is_blocked")]
        public bool? IsBlocked { get; set; }
        [JsonProperty("page_name")]
        public string PageName { get; set; }
        internal InstaWebType PageType
        {
            get
            {
                if (string.IsNullOrEmpty(PageName))
                    return InstaWebType.Unknown;
                try
                {
                    var name = PageName.Replace("_", "");

                    return (InstaWebType)Enum.Parse(typeof(InstaWebType), name, true);
                }
                catch { }
                return InstaWebType.Unknown;
            }
        }
        [JsonProperty("date_joined")]
        public InstaWebDataResponse DateJoined { get; set; }
        [JsonProperty("switched_to_business")]
        public InstaWebDataResponse SwitchedToBusiness { get; set; }
        [JsonProperty("data")]
        public InstaWebDataListResponse Data { get; set; }
    }
    
    public class InstaWebDataResponse
    {
        [JsonProperty("link")]
        public object Link { get; set; }
        [JsonProperty("data")]
        public InstaWebDataItemResponse Data { get; set; }
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
    }

    public class InstaWebDataListResponse
    {
        [JsonProperty("link")]
        public object Link { get; set; }
        [JsonProperty("data")]
        public List<InstaWebDataItemResponse> Data { get; set; } = new List<InstaWebDataItemResponse>();
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
    }
    public class InstaWebDataItemResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }
    }
}
