/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryCountdownUpload
    {
        public double X { get; set; } = 0.5;
        public double Y { get; set; } = 0.5;
        public double Z { get; set; } = 0;

        public double Width { get; set; } = 0.7972222;
        public double Height { get; set; } = 0.21962096;
        public double Rotation { get; set; } = 0.0;

        public DateTime EndTime { get; set; } = DateTime.UtcNow.AddDays(1);
        public string Text { get; set; }
        public string StartBackgroundColor { get; set; } = "#ffffff";
        public string EndBackgroundColor { get; set; } = "#ffffff";
        public string TextColor { get; set; } = "#000000";

        public string DigitColor { get; set; } = "#4286f4";
        public string DigitCardColor { get; set; } = "#42dcf4";

        public bool FollowingEnabled { get; set; } = true;

        public bool IsSticker { get; set; } = false;
    }
}
