/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryQuestionResponder
    {
        public string ResponseText { get; set; }

        public bool HasSharedResponse { get; set; }

        public long Id { get; set; }

        public InstaUserShort User { get; set; }

        public DateTime Time { get; set; }
    }
}
