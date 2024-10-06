/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryLinkStickerItem
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Rotation { get; set; }
        public bool IsPinned { get; set; }
        public bool IsHidden { get; set; }
        public bool IsSticker { get; set; } = true;
        public bool IsFbSticker { get; set; }
        public InstaStoryLink StoryLink { get; set; }
    }
}
