/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
 
using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTranslateResponse
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("translation")] public string Translation { get; set; }
    }

    public class InstaTranslateContainerResponse : InstaDefault
    {
        [JsonProperty("comment_translations")] public List<InstaTranslateResponse> Translations { get; set; }
    }
}
