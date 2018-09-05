using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaCurrentUserResponse : InstaUserShortResponse
    {
        [JsonProperty("has_anonymous_profile_picture")]
        public bool HasAnonymousProfilePicture { get; set; }

        [JsonProperty("show_conversion_edit_entry")]
        public bool ShowConversationEditEntry { get; set; }

        [JsonProperty("birthday")] public string Birthday { get; set; }

        [JsonProperty("biography")] public string Biography { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }

        [JsonProperty("country_code")] public int CountryCode { get; set; }

        [JsonProperty("national_number")] public long NationalNumber { get; set; }

        [JsonProperty("gender")] public int Gender { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("hd_profile_pic_versions")]
        public ImageResponse[] HDProfilePicVersions { get; set; }

        [JsonProperty("hd_profile_pic_url_info")]
        public ImageResponse HDProfilePicture { get; set; }

        [JsonProperty("external_url")] public string ExternalURL { get; set; }
    }
}