/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryPollUpload
    {
        public double X { get; set; } = 0.5;
        public double Y { get; set; } = 0.5;
        public double Z { get; set; } = 0;

        public double Width { get; set; } = 0.67407405;
        public double Height { get; set; } = 0.12417219;
        public double Rotation { get; set; } = 0.0;

        public string Question { get; set; }

        public string Answer1 { get; set; } = "YES";
        public string Answer2 { get; set; } = "NO";

        public double Answer1FontSize { get; set; } = 35.0;
        public double Answer2FontSize { get; set; } = 35.0;

        public bool IsSticker { get; set; } = false;
    }
}
