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
    public class InstaBroadcastNotifyFriends
    {
        public string Text { get; set; }

        public List<InstaUserShortFriendshipFull> Friends { get; set; } = new List<InstaUserShortFriendshipFull>();

        public int OnlineFriendsCount { get; set; }
    }
}
