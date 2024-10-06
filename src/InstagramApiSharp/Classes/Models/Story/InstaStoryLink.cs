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
    public class InstaStoryLink
    {
        public InstaStoryLinkType LinkType { get; set; }
        public string OriginalLinkType { get; set; }
        public string Url { get; set; }
        public string LinkTitle { get; set; }
    }
}
