using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Models
{
    public class TwoFactorRegenBackupCodes
    {
        [JsonProperty("backup_codes")]
        public string[] BackupCodes { get; set; }
        [JsonProperty("status")]
        internal string Status { get; set; }
    }
}
