using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Helpers
{
    /// <summary>
    /// Facebook login helper.
    /// <para>Note: you must clear all caches from all urls in this class.</para>
    /// </summary>
    public class FacebookLoginHelper
    {
        public static readonly Uri InstagramApiAddress = new Uri(InstaApiConstants.INSTAGRAM_URL);
        public static readonly Uri InstagramAccountAddress = new Uri("https://www.instagram.com/accounts/");
        public static readonly Uri FacebookMobileAddress = new Uri("https://m.facebook.com/");
        public static readonly Uri FacebookAddress = new Uri("https://facebook.com/");
        public static readonly Uri FacebookAddressWithWWWAddress = new Uri("https://www.facebook.com/");
        public static readonly Uri InstagramUriAddress = new Uri("https://www.instagram.com/");
        public static readonly Uri InstagramUriWithoutWWWAddress = new Uri("https://instagram.com/");

        static readonly Uri FacebookTelemetryAddress = new Uri("https://connect.facebook.net/log/fbevents_telemetry/");
        static readonly Uri FacebookArbiterAddress = new Uri("https://staticxx.facebook.com/connect/xd_arbiter/");
        /// <summary>
        /// Check FacebookLoginExample project to see how it's works.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool FirstStep(Uri uri)
        {
            return uri.ToString() == InstagramUriAddress.ToString() ||
                uri.ToString().StartsWith(FacebookTelemetryAddress.ToString()) ||
                uri.ToString().StartsWith(FacebookArbiterAddress.ToString()) ||
                        uri.ToString() == "https://www.instagram.com/#reactivated" ||
                        uri.ToString().StartsWith("https://www.instagram.com/accounts/onetap/");
        }
        /// <summary>
        /// Check FacebookLoginExample project to see how it's works.
        /// </summary>
        /// <param name="innerHtml"></param>
        /// <returns></returns>
        public static bool SecondStep(string innerHtml)
        {
            return innerHtml.Contains("button") && innerHtml.Contains("_5f5mN       jIbKX KUBKM      yZn4P   ");
        }
        /// <summary>
        /// Get logged in user response
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <returns></returns>
        public static WebBrowserResponse GetLoggedInUserResponse(string htmlSource)
        {
            if (string.IsNullOrEmpty(htmlSource) || string.IsNullOrWhiteSpace(htmlSource))
                return null;

            try
            {
                var start = "<script type=\"text/javascript\">window._sharedData";
                var end = ";</script>";
                if (htmlSource.Contains(start))
                {
                    var str = htmlSource.Substring(htmlSource.IndexOf(start) + start.Length);
                    str = str.Substring(0, str.IndexOf(end));
                    str = str.Substring(str.IndexOf("=") + 2);
                    var obj = JsonConvert.DeserializeObject<WebBrowserResponse>(str);
                    return obj;
                }
            }
            catch (Exception) { System.Diagnostics.Debug.WriteLine("FacebookLoginHelper.GetLoggedInResponse exception"); }

            return null;
        }
    }
}
