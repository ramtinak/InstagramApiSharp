/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Enums;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserLookup
    {
        public bool MultipleUsersFound { get; set; }

        public bool EmailSent { get; set; }

        public bool SmsSent { get; set; }

        public InstaLookupType LookupSourceType { get; set; }

        public string CorrectedInput { get; set; }

        /// <summary>
        ///     Note: This always is null except when <see cref="LookupSourceType"/> is Username
        /// </summary>
        public InstaUserShort User { get; set; }

        public bool HasValidPhone { get; set; }

        public bool CanEmailReset { get; set; }

        public bool CanSmsReset { get; set; }

        public bool CanWaReset { get; set; }

        //public string UserId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
