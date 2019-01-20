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
    internal class InstaStoryPollVoterInfoItemConverter : IObjectConverter<InstaStoryPollVoterInfoItem, InstaStoryPollVoterInfoItemResponse>
    {
        public InstaStoryPollVoterInfoItemResponse SourceObject { get; set; }

        public InstaStoryPollVoterInfoItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var voterInfoItem = new InstaStoryPollVoterInfoItem
            {
                LatestPollVoteTime = DateTimeHelper.FromUnixTimeSeconds(SourceObject.LatestPollVoteTime ?? DateTime.Now.ToUnixTime()),
                MaxId = SourceObject.MaxId,
                MoreAvailable = SourceObject.MoreAvailable,
                PollId = SourceObject.PollId
            };

            if (SourceObject.Voters?.Count > 0)
                foreach (var voter in SourceObject.Voters)
                    voterInfoItem.Voters.Add(ConvertersFabric.Instance.GetStoryPollVoterItemConverter(voter).Convert());

            return voterInfoItem;
        }
    }
}
