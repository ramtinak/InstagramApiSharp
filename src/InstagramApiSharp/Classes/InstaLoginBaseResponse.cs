using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    internal class InstaLoginBaseResponse
    {
        #region InvalidCredentials

        [JsonProperty("invalid_credentials")] public bool InvalidCredentials { get; set; }

        [JsonProperty("error_type")] public string ErrorType { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("help_url")] public string HelpUrl { get; set; }
        #endregion

        #region 2 Factor Authentication

        [JsonProperty("two_factor_required")] public bool TwoFactorRequired { get; set; }

        [JsonProperty("two_factor_info")] public TwoFactorLoginInfo TwoFactorLoginInfo { get; set; }

        [JsonProperty("challenge")] public InstaChallengeLoginInfo Challenge { get; set; }

        #endregion
    }
}