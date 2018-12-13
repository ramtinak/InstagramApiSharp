/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;
using System.Linq;

namespace InstagramApiSharp.Converters
{
    internal class InstaAnimatedImageUserConverter : IObjectConverter<InstaAnimatedImageUser, InstaAnimatedImageUserResponse>
    {
        public InstaAnimatedImageUserResponse SourceObject { get; set; }

        public InstaAnimatedImageUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var user = new InstaAnimatedImageUser
            {
                IsVerified = SourceObject.IsVerified,
                Username = SourceObject.Username
            };

            return user;
        }
    }
}
