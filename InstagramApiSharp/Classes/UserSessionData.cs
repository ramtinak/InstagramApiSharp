using System;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.Classes
{
    [Serializable]
    public class UserSessionData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public InstaUserShort LoggedInUder { get; set; }

        public string RankToken { get; set; }
        public string CsrfToken { get; set; }

        public static UserSessionData Empty => new UserSessionData();

        public static UserSessionData ForUsername(string username)
        {
            return new UserSessionData {UserName = username};
        }

        public UserSessionData WithPassword(string password)
        {
            Password = password;
            return this;
        }
    }
}