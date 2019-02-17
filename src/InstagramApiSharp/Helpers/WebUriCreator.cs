/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace InstagramApiSharp.Helpers
{
    internal class WebUriCreator
    {
        public static Uri GetAccountsDataUri()
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_ACCOUNT_DATA, out var instaUri))
                throw new Exception("Cant create URI for accounts data page");
            return instaUri;
        }

        public static Uri GetCurrentFollowRequestsUri(string cursor = null)
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_CURRENT_FOLLOW_REQUEST, out var instaUri))
                throw new Exception("Cant create URI for current follow requests");
            string query = string.Empty;
            if (cursor.IsNotEmpty())
                query = string.Format(InstaApiConstants.WEB_CURSOR, Uri.EscapeUriString(cursor));

            return new UriBuilder(instaUri) { Query = query }.Uri;
        }

        public static Uri GetFormerBiographyTextsUri(string cursor = null)
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_FORMER_BIO_TEXTS, out var instaUri))
                throw new Exception("Cant create URI for former biography texts");
            string query = string.Empty;
            if (cursor.IsNotEmpty())
                query = string.Format(InstaApiConstants.WEB_CURSOR, Uri.EscapeUriString(cursor));

            return new UriBuilder(instaUri) { Query = query }.Uri;
        }

        public static Uri GetFormerBiographyLinksUri(string cursor = null)
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_FORMER_BIO_LINKS, out var instaUri))
                throw new Exception("Cant create URI for former biography links");
            string query = string.Empty;
            if (cursor.IsNotEmpty())
                query = string.Format(InstaApiConstants.WEB_CURSOR, Uri.EscapeUriString(cursor));

            return new UriBuilder(instaUri) { Query = query }.Uri;
        }
    }
}
