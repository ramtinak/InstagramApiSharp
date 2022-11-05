/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserChaining : InstaUserShort
    {
        public string ChainingInfo { get; set; }

        public string ProfileChainingSecondaryLabel { get; set; }
    }

    public class InstaUserChainingList : List<InstaUserChaining>
    {
        internal string Status { get; set; }

        public bool IsBackup { get; set; }
    }
}
