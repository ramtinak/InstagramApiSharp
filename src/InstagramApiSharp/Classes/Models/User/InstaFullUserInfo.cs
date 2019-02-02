/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.ResponseWrappers;
using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaFullUserInfo
    {
        public InstaUserInfo UserDetail { get; set; }

        internal string Status { get; set; }

        public InstaFullUserInfoUserFeed Feed { get; set; }

        public InstaFullUserInfoUserStoryReel ReelFeed { get; set; }

        public InstaFullUserInfoUserStory UserStory { get; set; }
    }

    public class InstaFullUserInfoUserStory
    {
        public InstaFullUserInfoUserStoryReel Reel { get; set; }

        public InstaBroadcastList Broadcast { get; set; }
    }

    public class InstaFullUserInfoUserFeed
    {
        public int NumResults { get; set; }

        public bool MoreAvailable { get; set; }

        public string NextMaxId { get; set; }

        public string NextMinId { get; set; }

        public bool AutoLoadMoreEnabled { get; set; }

        public List<InstaMedia> Items { get; set; } = new List<InstaMedia>();
    }

    public class InstaFullUserInfoUserStoryReel
    {
        public InstaUserShort User { get; set; }

        public List<InstaStoryItem> Items { get; set; } = new List<InstaStoryItem>();

        public long Id { get; set; }

        public int? LatestReelMedia { get; set; }

        public DateTime ExpiringAt { get; set; }

        public long Seen { get; set; }

        public bool CanReply { get; set; }

        public bool CanReshare { get; set; }

        public string ReelType { get; set; }

        public int PrefetchCount { get; set; }

        public bool HasBestiesMedia { get; set; }
    }
}
