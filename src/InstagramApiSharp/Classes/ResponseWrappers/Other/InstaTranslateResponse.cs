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

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaTranslateBioResponse : InstaDefault
    {
        [JsonProperty("translation")] public string Translation { get; set; }
    }

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
