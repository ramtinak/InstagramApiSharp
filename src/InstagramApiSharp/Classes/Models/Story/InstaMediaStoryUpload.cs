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
    public class InstaMediaStoryUpload
    {
        public double X { get; set; } = 0.5;
        public double Y { get; set; } = 0.499812593703148;

        public double Width { get; set; } = 0.5;
        public double Height { get; set; } = 0.5;
        public double Rotation { get; set; } = 0.0;
        /// <summary>
        ///     Get it from <see cref="InstaMedia.Pk"/>
        /// </summary>
        public long MediaPk { get; set; }

        public bool IsSticker { get; set; } = false;
    }
}
