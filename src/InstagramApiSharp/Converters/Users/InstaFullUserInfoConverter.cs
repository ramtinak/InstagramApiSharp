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
using System.Collections.Generic;
using System.Linq;
namespace InstagramApiSharp.Converters
{
    internal class InstaFullUserInfoConverter : IObjectConverter<InstaFullUserInfo, InstaFullUserInfoResponse>
    {
        public InstaFullUserInfoResponse SourceObject { get; set; }

        public InstaFullUserInfo Convert()
        {
            var fullUserInfo = new InstaFullUserInfo
            {
                Status = SourceObject.Status
            };
            if (SourceObject.Feed != null)
            {
                fullUserInfo.Feed = new InstaFullUserInfoUserFeed
                {
                    AutoLoadMoreEnabled = SourceObject.Feed.AutoLoadMoreEnabled,
                    MoreAvailable = SourceObject.Feed.MoreAvailable,
                    NextMaxId = SourceObject.Feed.NextMaxId ?? string.Empty,
                    NextMinId = SourceObject.Feed.NextMinId ?? string.Empty,
                    NumResults = SourceObject.Feed.NumResults
                };
                if (SourceObject.Feed.Items != null && SourceObject.Feed.Items.Any())
                {
                    if (fullUserInfo.Feed.Items == null)
                        fullUserInfo.Feed.Items = new List<InstaMedia>();
                    foreach (var media in SourceObject.Feed.Items)
                    {
                        try
                        {
                            fullUserInfo.Feed.Items.Add(ConvertersFabric.Instance.GetSingleMediaConverter(media).Convert());
                        }
                        catch { }
                    }
                }
            }
            if (SourceObject.UserDetail != null && SourceObject.UserDetail.User != null)
            {
                try
                {
                    fullUserInfo.UserDetail = ConvertersFabric.Instance.GetUserInfoConverter(SourceObject.UserDetail).Convert();
                }
                catch { }
            }
            
            if (SourceObject.ReelFeed != null)
            {
                try
                {
                    fullUserInfo.ReelFeed = new InstaFullUserInfoUserStoryReel
                    {
                        CanReply = SourceObject.ReelFeed.CanReply,
                        CanReshare = SourceObject.ReelFeed.CanReshare,
                        ExpiringAt = SourceObject.ReelFeed.ExpiringAt.FromUnixTimeSeconds(),
                        HasBestiesMedia = SourceObject.ReelFeed.HasBestiesMedia,
                        Id = SourceObject.ReelFeed.Id,
                        LatestReelMedia = SourceObject.ReelFeed.LatestReelMedia,
                        PrefetchCount = SourceObject.ReelFeed.PrefetchCount,
                        ReelType = SourceObject.ReelFeed.ReelType,
                        Seen = SourceObject.ReelFeed.Seen ?? 0
                    };
                    if (SourceObject.ReelFeed.User != null)
                        fullUserInfo.ReelFeed.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.ReelFeed.User).Convert();

                    if (SourceObject.ReelFeed.Items != null && SourceObject.ReelFeed.Items.Any())
                    {
                        if (fullUserInfo.ReelFeed.Items == null)
                            fullUserInfo.ReelFeed.Items = new List<InstaStoryItem>();
                        foreach (var story in SourceObject.ReelFeed.Items)
                        {
                            try
                            {
                                fullUserInfo.ReelFeed.Items.Add(ConvertersFabric.Instance.GetStoryItemConverter(story).Convert());
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }

            if (SourceObject.UserStory != null)
            {
                fullUserInfo.UserStory = new InstaFullUserInfoUserStory();
                if (SourceObject.UserStory.Broadcast != null)
                {
                    try
                    {
                        fullUserInfo.UserStory.Broadcast = ConvertersFabric.Instance
                            .GetBroadcastListConverter(SourceObject.UserStory.Broadcast?.Broadcasts).Convert();
                    }
                    catch { }
                }
                if (SourceObject.UserStory.Reel != null)
                {
                    fullUserInfo.UserStory.Reel = new InstaFullUserInfoUserStoryReel
                    {
                        CanReply = SourceObject.UserStory.Reel.CanReply,
                        CanReshare = SourceObject.UserStory.Reel.CanReshare,
                        ExpiringAt = SourceObject.UserStory.Reel.ExpiringAt.FromUnixTimeSeconds(),
                        HasBestiesMedia = SourceObject.UserStory.Reel.HasBestiesMedia,
                        Id = SourceObject.UserStory.Reel.Id,
                        LatestReelMedia = SourceObject.UserStory.Reel.LatestReelMedia,
                        PrefetchCount = SourceObject.UserStory.Reel.PrefetchCount,
                        ReelType = SourceObject.UserStory.Reel.ReelType,
                        Seen = SourceObject.UserStory.Reel.Seen ?? 0
                    };
                    if (SourceObject.UserStory.Reel.User != null)
                        fullUserInfo.UserStory.Reel.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.UserStory.Reel.User).Convert();

                    if (SourceObject.UserStory.Reel.Items != null && SourceObject.UserStory.Reel.Items.Any())
                    {
                        if (fullUserInfo.UserStory.Reel.Items == null)
                            fullUserInfo.UserStory.Reel.Items = new List<InstaStoryItem>();
                        foreach (var story in SourceObject.UserStory.Reel.Items)
                        {
                            try
                            {
                                fullUserInfo.UserStory.Reel.Items.Add(ConvertersFabric.Instance.GetStoryItemConverter(story).Convert());
                            }
                            catch { }
                        }
                    }
                }

            }
            return fullUserInfo;
        }
    }
}
