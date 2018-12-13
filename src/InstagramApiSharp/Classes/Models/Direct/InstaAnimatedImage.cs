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
    public class InstaAnimatedImage
    {
        public string Id { get; set; }

        public InstaAnimatedImageMedia Media { get; set; }

        public bool IsRandom { get; set; }

        public bool IsSticker { get; set; }

        public InstaAnimatedImageUser User { get; set; }
    }
}
