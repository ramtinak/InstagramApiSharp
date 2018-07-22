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
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class BroadcastSuggestedResponse
    {
        [JsonProperty("broadcasts")]
        public List<InstaBroadcast> Broadcasts { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
