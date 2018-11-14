/*
 * Developer: Ramtin Jokar[Ramtinak@live.com] [My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserTagUpload
    {
        public string Username { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        internal long Pk { get; set; } = -1;
    }
}
