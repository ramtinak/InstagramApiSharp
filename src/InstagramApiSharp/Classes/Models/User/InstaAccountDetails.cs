/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaAccountDetails
    {
        public DateTime DateJoined { get; set; }

        public bool HasFormerUsernames { get; set; } = false;

        public InstaPrimaryCountryInfo PrimaryCountryInfo { get; set; }

        public bool HasSharedFollowerAccounts { get; set; } = false;

        public InstaAdsInfo AdsInfo { get; set; }
    }
}
