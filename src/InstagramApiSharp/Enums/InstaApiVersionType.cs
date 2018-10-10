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
using System.Text;

namespace InstagramApiSharp.Enums
{
    //[Serializable]
    public enum InstaApiVersionType
    {
        /// <summary>
        ///     Default api version. v44.0.0.9.93 => No more consent required error.
        /// </summary>
        Version44 = 0,
        /// <summary>
        ///     Api version => No more consent required error.
        /// </summary>
        Version35 = 1,
        /// <summary>
        ///     Api version 61.0.0.19.86 => Has consent required for unverified accounts.
        /// </summary>
        Version61 = 2
    }
}
