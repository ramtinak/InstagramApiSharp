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
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.Models
{
    public class InstaBroadcast
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("rtmp_playback_url")]
        public string RtmpPlaybackUrl { get; set; }
        [JsonProperty("dash_playback_url")]
        public string DashPlaybackUrl { get; set; }
        [JsonProperty("dash_abr_playback_url")]
        public string DashAbrPlaybackUrl { get; set; }
        [JsonProperty("broadcast_status")]
        public string BroadcastStatus { get; set; }
        [JsonProperty("viewer_count")]
        public float ViewerCount { get; set; }
        [JsonProperty("internal_only")]
        public bool InternalOnly { get; set; }
        [JsonProperty("cover_frame_url")]
        public string CoverFrameUrl { get; set; }
        [JsonProperty("broadcast_owner")]
        public BroadcastUser BroadcastOwner { get; set; }
        [JsonProperty("published_time")]
        public int PublishedTime { get; set; }
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
        [JsonProperty("broadcast_message")]
        public string BroadcastMessage { get; set; }
        [JsonProperty("organic_tracking_token")]
        public string OrganicTrackingToken { get; set; }
    }

    public class BroadcastUser
    {
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }
        [JsonProperty("profile_pic_id")]
        public string ProfilePicId { get; set; }
        [JsonProperty("friendship_status")]
        public FriendshipStatus FriendshipStatus { get; set; }
        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }
    }

    public class FriendshipStatus
    {
        [JsonProperty("following")]
        public bool Following { get; set; }
        [JsonProperty("followed_by")]
        public bool FollowedBy { get; set; }
        [JsonProperty("blocking")]
        public bool Blocking { get; set; }
        [JsonProperty("muting")]
        public bool Muting { get; set; }
        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }
        [JsonProperty("incoming_request")]
        public bool IncomingRequest { get; set; }
        [JsonProperty("outgoing_request")]
        public bool OutgoingRequest { get; set; }
        [JsonProperty("is_bestie")]
        public bool IsBestie { get; set; }
    }

}
