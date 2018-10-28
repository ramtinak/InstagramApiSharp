/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.ResponseWrappers;
namespace InstagramApiSharp.Helpers
{
    public class InstaFbHelper
    {
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
        }

        public static bool IsLoggedIn(string html)
        {
            //https://m.facebook.com/v2.3/dialog/oauth/confirm
            return html.Contains("window.location.href=\"fbconnect://success#");
        }
        public static string GetAccessToken(string html)
        {
            try
            {
                var start = "<script type=\"text/javascript\">window.location.href=\"";
                var end = "\";</script>";
                if (html.Contains(start))
                {
                    var str = html.Substring(html.IndexOf(start) + start.Length);
                    str = str.Substring(0, str.IndexOf(end));
                    //&access_token=EAABwzLixnjYBALFoqFT7uqZCVCCcPM3HZAEXwSUZB3qBi1OxHP6OYP5hEYpzkNEej465HwMiMGKZB03auMnGRZBnY84nuah66erskm6ZCxTDExQ4korBhDnB0k2a2Mz9GyzWN9EZAZBVI5vfTJAPB9y14KtdwdoiW83xZAzIaThIBTwZDZD&
                    start = "&access_token=";
                    end = "&";
                    var token = str.Substring(str.IndexOf(start) + start.Length);
                    token = token.Substring(0, token.IndexOf(end));
                    return token;
                }
            }
            catch { }
            return null;
        }

    }
}
