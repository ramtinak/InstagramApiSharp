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
    public class InstaUserShortFriendshipList : List<InstaUserShortFriendship> { }
    public class InstaUserShortFriendship : InstaUserShort
    {
        public InstaFriendshipShortStatus FriendshipStatus { get; set; }
    }
}
