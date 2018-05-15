using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Converters
{
    internal class InstaRecentActivityConverter :
        IObjectConverter<InstaRecentActivityFeed, InstaRecentActivityFeedResponse>
    {
        public InstaRecentActivityFeedResponse SourceObject { get; set; }

        public InstaRecentActivityFeed Convert()
        {
            var activityStory = new InstaRecentActivityFeed
            {
                Pk = SourceObject.Pk,
                Type = SourceObject.Type,
                ProfileId = SourceObject.Args.ProfileId,
                ProfileImage = SourceObject.Args.ProfileImage,
                Text = SourceObject.Args.Text,
                TimeStamp = DateTimeHelper.UnixTimestampToDateTime(SourceObject.Args.TimeStamp)
            };
            if (SourceObject.Args.Links != null)
                foreach (var instaLinkResponse in SourceObject.Args.Links)
                    activityStory.Links.Add(new InstaLink
                    {
                        Start = instaLinkResponse.Start,
                        End = instaLinkResponse.End,
                        Id = instaLinkResponse.Id,
                        Type = instaLinkResponse.Type
                    });
            if (SourceObject.Args.InlineFollow != null)
            {
                activityStory.InlineFollow = new InstaInlineFollow
                {
                    IsFollowing = SourceObject.Args.InlineFollow.IsFollowing,
                    IsOutgoingRequest = SourceObject.Args.InlineFollow.IsOutgoingRequest
                };
                if (SourceObject.Args.InlineFollow.UserInfo != null)
                    activityStory.InlineFollow.User =
                        ConvertersFabric.Instance.GetUserShortConverter(SourceObject.Args.InlineFollow.UserInfo)
                            .Convert();
            }

            return activityStory;
        }
    }
}