using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class TwoFactorLoginSMSResponse
    {
        [JsonProperty("two_factor_required")]
        public bool TwoFactorRequired { get; set; }

        [JsonProperty("two_factor_info")]
        public TwoFactorLogin TwoFactorInfo { get; set; }
    }
}
