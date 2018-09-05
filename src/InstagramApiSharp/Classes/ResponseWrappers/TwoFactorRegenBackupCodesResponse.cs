using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class TwoFactorRegenBackupCodesResponse
    {
        [JsonProperty("backup_codes")]
        public string[] BackupCodes { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

    }

}
