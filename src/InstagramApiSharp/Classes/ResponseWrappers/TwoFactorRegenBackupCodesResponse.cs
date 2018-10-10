/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

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
