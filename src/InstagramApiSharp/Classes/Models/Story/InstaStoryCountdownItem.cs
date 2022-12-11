/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryCountdownItem
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Rotation { get; set; }

        public int IsPinned { get; set; }

        public int IsHidden { get; set; }

        public InstaStoryCountdownStickerItem CountdownSticker { get; set; }
    }
}
