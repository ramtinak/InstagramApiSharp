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
        ///     Clear datas and cached from this address
        /// </summary>
        public static readonly Uri FacebookMobileAddress = new Uri("https://m.facebook.com/");
        /// <summary>
        ///     Clear datas and cached from this address
        /// </summary>
        public static readonly Uri FacebookAddress = new Uri("https://facebook.com/");
        /// <summary>
        ///     Clear datas and cached from this address
        /// </summary>
        public static readonly Uri FacebookAddressWithWWWAddress = new Uri("https://www.facebook.com/");

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
            return /*ExtensionHelper.GenerateFacebookUserAgent()*/ InstaApiConstants.FACEBOOK_USER_AGENT_DEFAULT;
        }
        public static bool IsLoggedIn(string html)
        {
            //https://m.facebook.com/v2.3/dialog/oauth/confirm
            return html.Contains("window.location.href=\"fbconnect://success#") || html.Contains("window.location.href=\"fbconnect:\\/\\/success");
        }
        public static string GetAccessToken(string html)
        {
            try
            {
                var start = "type=\"text/javascript\">window.location.href=\"fbconnect:\\/\\/success";
                var end = "</script>";
                if (html.Contains(start))
                {
                    var str = html.Substring(html.IndexOf(start) + start.Length);
                    str = str.Substring(0, str.IndexOf(end));
                    //&access_token=EAABwzLixnjYBALFoqFT7uqZCVCCcPM3HZAEXwSUZB3qBi1OxHP6OYP5hEYpzkNEej465HwMiMbvvvz9GyzWhby14KtdwdoiW83xZAzIaThIBTwZDZD&
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
