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
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaUserLookupResponse : InstaDefault
    {
        [JsonProperty("multiple_users_found")]  public bool MultipleUsersFound { get; set; }

        [JsonProperty("email_sent")] public bool EmailSent { get; set; }

        [JsonProperty("sms_sent")]  public bool SmsSent { get; set; }

        [JsonProperty("lookup_source")] public string LookupSource { get; set; }

        [JsonProperty("corrected_input")]  public string CorrectedInput { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("has_valid_phone")] public bool HasValidPhone { get; set; }

        [JsonProperty("can_email_reset")] public bool CanEmailReset { get; set; }

        [JsonProperty("can_sms_reset")]  public bool CanSmsReset { get; set; }

        [JsonProperty("can_wa_reset")]  public bool CanWaReset { get; set; }

        [JsonProperty("user_id")] public string UserId { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }
    }
}
