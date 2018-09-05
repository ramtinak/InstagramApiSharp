/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class AccountUserResponse
    {
        [JsonProperty("user")]
        public AccountUser User { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class AccountUser
    {
        [JsonProperty("has_biography_translation")]
        public bool HasBiographyTranslation { get; set; }
        //public School school { get; set; }
        [JsonProperty("show_conversion_edit_entry")]
        public bool ShowConversionEditEntry { get; set; }
        [JsonProperty("pk")]
        public long Pk { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("has_anonymous_profile_picture")]
        public bool HasAnonymousProfilePicture { get; set; }
        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }
        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }
        [JsonProperty("profile_pic_id")]
        public string ProfilePicId { get; set; }
        [JsonProperty("allowed_commenter_type")]
        public string AllowedCommenterType { get; set; }
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }
        [JsonProperty("external_lynx_url")]
        public string ExternalLynxUrl { get; set; }
        //public Hd_Profile_Pic_Url_Info hd_profile_pic_url_info { get; set; }
        //public Hd_Profile_Pic_Versions[] hd_profile_pic_versions { get; set; }


        [JsonProperty("reel_auto_archive")]
        public string ReelAutoArchive { get; set; }
        [JsonProperty("birthday")]
        public object Birthday { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("country_code")]
        public int CountryCode { get; set; }
        [JsonProperty("national_number")]
        public long NationalNumber { get; set; }
        [JsonProperty("gender")]
        public int Gender { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("can_link_entities_in_bio")]
        public bool CanLinkEntitiesInBio { get; set; }
        [JsonProperty("max_num_linked_entities_in_bio")]
        public int MaxNumLinkedEntitiesInBio { get; set; }
    }

    //public class School
    //{
    //}

    //public class Hd_Profile_Pic_Url_Info
    //{
    //    public int height { get; set; }
    //    public string url { get; set; }
    //    public int width { get; set; }
    //}

    //public class Hd_Profile_Pic_Versions
    //{
    //    public int height { get; set; }
    //    public string url { get; set; }
    //    public int width { get; set; }
    //}

}
