/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.API;
using System;


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
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_CURRENT_FOLLOW_REQUESTS, out var instaUri))
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

        public static Uri GetFormerUsernamesUri(string cursor = null)
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_FORMER_USERNAMES, out var instaUri))
                throw new Exception("Cant create URI for former usernames");
            string query = string.Empty;
            if (cursor.IsNotEmpty())
                query = string.Format(InstaApiConstants.WEB_CURSOR, Uri.EscapeUriString(cursor));

            return new UriBuilder(instaUri) { Query = query }.Uri;
        }

        public static Uri GetFormerFullNamesUri(string cursor = null)
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_FORMER_FULL_NAMES, out var instaUri))
                throw new Exception("Cant create URI for former full names");
            string query = string.Empty;
            if (cursor.IsNotEmpty())
                query = string.Format(InstaApiConstants.WEB_CURSOR, Uri.EscapeUriString(cursor));

            return new UriBuilder(instaUri) { Query = query }.Uri;
        }

        public static Uri GetFormerPhoneNumbersUri(string cursor = null)
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_FORMER_PHONES, out var instaUri))
                throw new Exception("Cant create URI for former phone numbers");
            string query = string.Empty;
            if (cursor.IsNotEmpty())
                query = string.Format(InstaApiConstants.WEB_CURSOR, Uri.EscapeUriString(cursor));

            return new UriBuilder(instaUri) { Query = query }.Uri;
        }

        public static Uri GetFormerEmailsUri(string cursor = null)
        {
            if (!Uri.TryCreate(InstaApiConstants.InstagramWebUri, InstaApiConstants.WEB_FORMER_EMAILS, out var instaUri))
                throw new Exception("Cant create URI for former emails");
            string query = string.Empty;
            if (cursor.IsNotEmpty())
                query = string.Format(InstaApiConstants.WEB_CURSOR, Uri.EscapeUriString(cursor));

            return new UriBuilder(instaUri) { Query = query }.Uri;
        }
    }
}
