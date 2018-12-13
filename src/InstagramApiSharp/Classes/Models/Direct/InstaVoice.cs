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
    public class InstaVoice
    {
        public string Id { get; set; }

        public int MediaType { get; set; }

        public string ProductType { get; set; }

        public InstaAudio Audio { get; set; }

        public string OrganicTrackingToken { get; set; }

        public InstaFriendshipStatus FriendshipStatus { get; set; }
    }
}
