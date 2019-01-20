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
    public class InstaStoryPollItem
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Rotation { get; set; }

        public int IsPinned { get; set; }

        public int IsHidden { get; set; }

        public InstaStoryPollStickerItem PollSticker { get; set; }
    }
}
