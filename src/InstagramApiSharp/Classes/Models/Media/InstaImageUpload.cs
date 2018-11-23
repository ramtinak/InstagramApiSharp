/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaImageUpload
    {
        /// <summary>
        ///     Image uri => Optional (if using <see cref="ImageBytes"/>)
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        ///     Image width => Optional
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        ///     Image height => Optional
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        ///     Image bytes => Optional (if using <see cref="Uri"/>)
        /// </summary>
        public byte[] ImageBytes { get; set; }
        /// <summary>
        ///     User tags => Optional
        /// </summary>
        public List<InstaUserTagUpload> UserTags { get; set; } = new List<InstaUserTagUpload>();
        /// <summary>
        ///     Create an instance of <see cref="InstaImageUpload"/>
        /// </summary>
        public InstaImageUpload() { }
        /// <summary>
        ///     Create an instance of <see cref="InstaImageUpload"/>
        /// </summary>
        /// <param name="uri">Image uri</param>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        public InstaImageUpload(string uri, int width = 0, int height = 0)
        {
            Uri = uri;
            Width = width;
            Height = height;
        }
    }
}
