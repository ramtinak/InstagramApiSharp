using System;
using System.Net;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes
{
    [Serializable]
    internal class StateData
    {
        public AndroidDevice DeviceInfo { get; set; }
        public UserSessionData UserSession { get; set; }
        public bool IsAuthenticated { get; set; }
        public CookieContainer Cookies { get; set; }
        public List<Cookie> RawCookies { get; set; }
    }
}