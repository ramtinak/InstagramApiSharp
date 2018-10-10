using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class TwoFactorLoginSMS
    {
        [JsonProperty("two_factor_required")]
        public bool TwoFactorRequired { get; set; }

        [JsonProperty("two_factor_info")]
        public InstaTwoFactorLogin TwoFactorInfo { get; set; }
    }
}
