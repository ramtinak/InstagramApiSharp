﻿/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStoryCountdownItemResponse
    {
        [JsonProperty("x")]
        public float X { get; set; }
        [JsonProperty("y")]
        public float Y { get; set; }
        [JsonProperty("z")]
        public float Z { get; set; }
        [JsonProperty("width")]
        public float Width { get; set; }
        [JsonProperty("height")]
        public float Height { get; set; }
        [JsonProperty("rotation")]
        public float Rotation { get; set; }
        [JsonProperty("is_pinned")]
        public int IsPinned { get; set; }
        [JsonProperty("is_hidden")]
        public int IsHidden { get; set; }
        [JsonProperty("countdown_sticker")]
        public InstaStoryCountdownStickerItemResponse CountdownSticker { get; set; }
    }
}
