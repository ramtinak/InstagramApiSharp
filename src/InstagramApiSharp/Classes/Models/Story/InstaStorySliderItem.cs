using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStorySliderItem
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Rotation { get; set; }

        public int IsPinned { get; set; }

        public int IsHidden { get; set; }

        public InstaStorySliderStickerItem SliderSticker { get; set; }
    }
}
