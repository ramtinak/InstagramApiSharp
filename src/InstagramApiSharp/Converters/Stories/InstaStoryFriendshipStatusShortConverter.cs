/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryFriendshipStatusShortConverter : IObjectConverter<InstaStoryFriendshipStatusShort, InstaStoryFriendshipStatusShortResponse>
    {
        public InstaStoryFriendshipStatusShortResponse SourceObject { get; set; }

        public InstaStoryFriendshipStatusShort Convert()
        {
            var storyFriendshipStatusShort = new InstaStoryFriendshipStatusShort
            {
                Following = SourceObject.Following,
                OutgoingRequest = SourceObject.OutgoingRequest ?? false,
                Muting = SourceObject.Muting ?? false,
                IsMutingReel = SourceObject.IsMutingReel ?? false
            };
            return storyFriendshipStatusShort;
        }
    }
}
