/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBrandedContent
    {
        public bool RequireApproval { get; set; }

        public List<InstaUserShort> WhitelistedUsers { get; set; } = new List<InstaUserShort>();
    }
}
