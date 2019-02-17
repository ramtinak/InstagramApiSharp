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
    public class InstaWebAccountInfo
    {
        public DateTime? JoinedDate { get; set; } = DateTime.MinValue;

        public DateTime? SwitchedToBusinessDate { get; set; } = DateTime.MinValue;
    }
}
