/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class InstaStorySettings
    {
        /// <summary>
        /// In dar asl hamon MessagePrefs hast ke tabdil be message replies type shode
        /// </summary>
        [JsonIgnore()]
        public InstaMessageRepliesType MessagePrefsType
        {
            get
            {
                switch (MessagePrefs)
                {
                    default:
                    case "everyone":
                        return InstaMessageRepliesType.Everyone;
                    case "following":
                        return InstaMessageRepliesType.Following;
                    case "off":
                        return InstaMessageRepliesType.Off;
                }
            }
        }
    
        [JsonProperty("message_prefs")]
        public string MessagePrefs { get; set; }
        [JsonProperty("blocked_reels")]
        public AccountBlockedReels BlockedReels { get; set; }
        [JsonProperty("besties")]
        public AccountBesties Besties { get; set; }
        [JsonProperty("persist_stories_to_private_profile")]
        public bool PersistStoriesToPrivateProfile { get; set; }
        [JsonProperty("reel_auto_archive")]
        public string ReelAutoArchive { get; set; }
        [JsonProperty("allow_story_reshare")]
        public bool AllowStoryReshare { get; set; }
        [JsonProperty("save_to_camera_roll")]
        public bool SaveToCameraRoll { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class AccountBlockedReels
    {
        [JsonProperty("users")]
        public List<BroadcastUser> Users { get; set; }
        [JsonProperty("big_list")]
        public bool BigList { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
    }

    public class AccountBesties
    {
        [JsonProperty("users")]
        public List<BroadcastUser> Users { get; set; }
        [JsonProperty("big_list")]
        public bool BigList { get; set; }
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
    }

}
