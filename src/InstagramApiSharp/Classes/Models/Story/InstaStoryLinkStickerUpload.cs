/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Enums;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryLinkStickerUpload
    {
        public double X { get; set; } = 0.4974145;
        public double Y { get; set; } = 0.76786435;
        public double Z { get; set; } = 0;
        public double Width { get; set; } = 0.22498246;
        public double Height { get; set; } = 0.042;
        public double Rotation { get; set; } = 0.0;
        public uint SelectedIndex { get; set; } = 0;
        public uint TapState { get; set; } = 0;
        public string TapStateStrId { get; set; } = "link_sticker_default";
        public string CustomStickerText { get; set; }
        public InstaStoryLinkType LinkType { get; set; }
        public string Url { get; set; }
        internal bool IsSticker { get; set; } = true;
    }
}