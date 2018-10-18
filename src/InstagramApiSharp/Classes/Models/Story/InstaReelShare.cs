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
    public class InstaReelShare
    {
        public string Text { get; set; }

        public string Type { get; set; }

        public long ReelOwnerId { get; set; }

        public bool IsReelPersisted { get; set; }

        public string ReelType { get; set; }

        public InstaStoryItem Media { get; set; }
    }
}
