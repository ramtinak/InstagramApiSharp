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
    public class InstaAnimatedImageMedia
    {
        public string Url { get; set; }

        public double Width { get; set; } = 0;

        public double Height { get; set; } = 0;

        public double Size { get; set; }

        public string Mp4Url { get; set; }

        public double Mp4Size { get; set; }

        public string WebpUrl { get; set; }

        public double WebpSize { get; set; }
    }
}
