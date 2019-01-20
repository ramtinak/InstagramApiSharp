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
    public class InstaStoryTalliesItemResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("font_size")]
        public float FontSize { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
