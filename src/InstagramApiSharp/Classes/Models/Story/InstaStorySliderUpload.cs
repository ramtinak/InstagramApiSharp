﻿/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStorySliderUpload
    {
        public double X { get; set; } = 0.5;
        public double Y { get; set; } = 0.5;
        public double Z { get; set; } = 0;

        public double Width { get; set; } = 0.7972222;
        public double Height { get; set; } = 0.21962096;
        public double Rotation { get; set; } = 0.0;

        public string Question { get; set; }

        public string BackgroundColor { get; set; } = "#ffffff";
        public string Emoji { get; set; } = "😍";

        public string TextColor { get; set; } = "#000000";

        public bool IsSticker { get; set; } = false;
    }
}
