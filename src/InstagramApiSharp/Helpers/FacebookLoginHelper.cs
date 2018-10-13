/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
namespace InstagramApiSharp.Helpers
{
    /// <summary>
    ///     Facebook login helper.
    ///     <para>Note: you must clear all caches from all urls in this class.</para>
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
        ///     Check FacebookLoginExample project to see how it's works.
        /// </summary>
        public static bool FirstStep(Uri uri)
        {
            return uri.ToString() == InstagramUriAddress.ToString() ||
                uri.ToString().StartsWith(FacebookTelemetryAddress.ToString()) ||
                uri.ToString().StartsWith(FacebookArbiterAddress.ToString()) ||
                        uri.ToString() == "https://www.instagram.com/#reactivated" ||
                        uri.ToString().StartsWith("https://www.instagram.com/accounts/onetap/");
        }
        /// <summary>
        ///     Check FacebookLoginExample project to see how it's works.
        /// </summary>
        public static bool SecondStep(string innerHtml)
        {
            return innerHtml.Contains("button") && innerHtml.Contains("_5f5mN       jIbKX KUBKM      yZn4P   ");
        }
        /// <summary>
        ///     Get logged in user response
        /// </summary>
        public static InstaWebBrowserResponse GetLoggedInUserResponse(string htmlSource)
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
                    var obj = JsonConvert.DeserializeObject<InstaWebBrowserResponse>(str);
                    return obj;
                }
            }
            catch (Exception) { System.Diagnostics.Debug.WriteLine("FacebookLoginHelper.GetLoggedInResponse exception"); }

            return null;
        }
        /*
        /// <summary>
        ///     Get facebook login uri
        /// </summary>
        public static Uri GetFacebookLoginUri()
        {
            try
            {
                var init = new JObject
                {
                    {"init", DateTime.UtcNow.ToUnixTimeMiliSeconds()}
                };
                if (Uri.TryCreate(string.Format(InstaApiConstants.FACEBOOK_LOGIN_URI,
                    init.ToString(Formatting.None)), UriKind.RelativeOrAbsolute, out Uri fbUri))
                    return fbUri;
            }
            catch { }
            return null;
        }
        /// <summary>
        ///     Get facebook user agent
        /// </summary>
        public static string GetFacebookUserAgent()
        {
            return ExtensionHelper.GenerateFacebookUserAgent();
        }
        /// <summary>
        ///     Get facebook account from HTML source
        /// </summary>
        /// <param name="htmlSource">Html source code</param>
        public static InstaFacebookAccountInfo GetAccountFromHtml(string htmlSource)
        {
            if (string.IsNullOrEmpty(htmlSource) || string.IsNullOrWhiteSpace(htmlSource))
                return null;

            try
            {
                var start = "</script><script id=\"u_0_l\">";
                var end = ";</script>";
                if (htmlSource.Contains(start))
                {
                    var str = htmlSource.Substring(htmlSource.IndexOf(start) + start.Length);
                    str = str.Substring(0, str.IndexOf(end));
                    start = ".handleDefines(";
                    end = ";new";
                    str = str.Substring(str.IndexOf(start) + start.Length);
                    str = str.Substring(0, str.IndexOf(end));
                    start = "{\"USER_ID\"";
                    end = "}";
                    //{"USER_ID":"100017040039489","ACCOUNT_ID":"100017040039489","NAME":"Marco Antonari","SHORT_NAME":"Marco","IS_MESSENGER_ONLY_USER":false,"IS_DEACTIVATED_ALLOWED_ON_MESSENGER":false}
                    var source = str.Substring(str.IndexOf(start));
                    source = source.Substring(0, source.IndexOf(end) + 1);
                    var obj = JsonConvert.DeserializeObject<InstaFacebookAccountInfo>(source);

                    //"dtsg_ag":{"token":"Adxvx0f7MDoCo3P0vfUwmdngpXIQ4lDdLpxmXCx-f6W4Xg:AdynVHeRdMu1puL5FdV5uhSvJm4HKoPBizFt8FUfebhOjw","valid_for":86400,"expire":1539519980}
                    start = "\"dtsg_ag\":";
                    str = str.Substring(str.IndexOf(start) + start.Length);
                    str = str.Substring(0, str.IndexOf(end) + 1);
                    var token = JsonConvert.DeserializeObject<InstaFacebookAccountToken>(str);
                    if (token == null || token != null && string.IsNullOrEmpty(token.Token))
                        return null;
                    obj.Token = token.Token;

                    return obj;
                }
            }
            catch (Exception) { System.Diagnostics.Debug.WriteLine("FacebookLoginHelper.GetAccountFromHtml exception"); }

            return null;
        }*/

    }
}
