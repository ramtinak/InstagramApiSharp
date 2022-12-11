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
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models.Business
{
    internal class InstaBusinessPartnerContainer : InstaDefault
    {
        [JsonProperty("partners")]
        public InstaBusinessPartner[] Partners { get; set; }
    }
    public class InstaBusinessPartnersList : List<InstaBusinessPartner> { }
    public class InstaBusinessPartner
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("partner_name")]
        public string PartnerName { get; set; }
    }
}
