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
using InstagramApiSharp.Helpers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryPollVoterItemConverter : IObjectConverter<InstaStoryVoterItem, InstaStoryVoterItemResponse>
    {
        public InstaStoryVoterItemResponse SourceObject { get; set; }

        public InstaStoryVoterItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var voterItem = new InstaStoryVoterItem
            {
                Vote = SourceObject.Vote ?? 0,
                Time = DateTimeHelper.FromUnixTimeSeconds(SourceObject.Ts),
                User = ConvertersFabric.Instance.GetUserShortFriendshipConverter(SourceObject.User).Convert()
            };

            return voterItem;
        }
    }
}
