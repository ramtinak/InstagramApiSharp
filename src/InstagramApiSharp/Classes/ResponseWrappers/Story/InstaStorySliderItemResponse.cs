using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaStorySliderItemResponse
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
        [JsonProperty("slider_sticker")]
        public InstaStorySliderStickerItemResponse SliderSticker { get; set; }
    }
}
