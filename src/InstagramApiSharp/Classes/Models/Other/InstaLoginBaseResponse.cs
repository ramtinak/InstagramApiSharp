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

        [JsonProperty("two_factor_info")] public InstaTwoFactorLoginInfo TwoFactorLoginInfo { get; set; }

        #endregion

        #region Challenge

        [JsonProperty("challenge")] public InstaChallengeLoginInfo Challenge { get; set; }
        
        #endregion

        [JsonProperty("lock")] public bool? Lock { get; set; }

        [JsonProperty("checkpoint_url")] public string CheckpointUrl { get; set; }
    }
}