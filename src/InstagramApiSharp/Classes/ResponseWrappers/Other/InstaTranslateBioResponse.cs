﻿/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTranslateBioResponse : InstaDefault
    {
        [JsonProperty("translation")] public string Translation { get; set; }
    }
}
