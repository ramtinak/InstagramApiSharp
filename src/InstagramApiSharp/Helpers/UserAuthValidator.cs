/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes;
using System;

namespace InstagramApiSharp.Helpers
{
    public class UserAuthValidate
    {
        public bool IsUserAuthenticated { get; internal set; }
        public UserSessionData User { get; internal set; }
    }
    public static class UserAuthValidator
    {
        public static void Validate(UserAuthValidate userAuthValidate)
        {
            ValidateUser(userAuthValidate.User);
            ValidateLoggedIn(userAuthValidate.IsUserAuthenticated);
        }
        public static void Validate(UserSessionData user, bool isUserAuthenticated)
        {
            ValidateUser(user);
            ValidateLoggedIn(isUserAuthenticated);
        }
        private static void ValidateUser(UserSessionData user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("user name and password must be specified");
        }

        private static void ValidateLoggedIn(bool isUserAuthenticated)
        {
            if (!isUserAuthenticated)
                throw new ArgumentException("user must be authenticated");
        }
    }
}
