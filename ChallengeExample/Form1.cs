using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Logger;
namespace ChallengeExample
{
    public partial class Form1 : Form
    {
        const string AppName = "Challenge Required";
        const string StateFile = "state.bin";
        readonly Uri InstagramUri = new Uri("https://www.instagram.com/");
        readonly Size NormalSize = new Size(432, 169);
        readonly Size ChallengeSize = new Size(432, 627);
        bool IsWebBrowserInUse = false;
        private static IInstaApi InstaApi;

        public Form1()
        {
            InitializeComponent();
        }

        private async void LoginButtonClick(object sender, EventArgs e)
        {
            // NOTE 1: I've added a WebBrowser control to pass challenge url and verify your account
            // Yout need this because you have to get cookies and document source text from webbrowser
            // and pass it to SetCookiesAndHtmlForChallenge function.

            var userSession = new UserSessionData
            {
                UserName = txtUser.Text,
                Password = txtPass.Text
            };

            InstaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                .Build();
            Text = $"{AppName} Connecting";
            try
            {
                if (File.Exists(StateFile))
                {
                    Debug.WriteLine("Loading state from file");
                    using (var fs = File.OpenRead(StateFile))
                    {
                        InstaApi.LoadStateDataFromStream(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (!InstaApi.IsUserAuthenticated)
            {
                var logInResult = await InstaApi.LoginAsync();
                Debug.WriteLine(logInResult.Value);
                if (logInResult.Succeeded)
                {
                    Text = $"{AppName} Connected";
                    // Save session 
                    var state = InstaApi.GetStateDataAsStream();
                    using (var fileStream = File.Create(StateFile))
                    {
                        state.Seek(0, SeekOrigin.Begin);
                        state.CopyTo(fileStream);
                    }
                }
                else
                {
                    if (logInResult.Value == InstaLoginResult.ChallengeRequired)
                    {
                        // Get challenge information
                        var instaChallenge = InstaApi.GetChallenge();
                        IsWebBrowserInUse = false;
                        WebBrowserRmt.Visible = true;
                        // Navigate to challenge Url 
                        WebBrowserRmt.Navigate(instaChallenge.Url);
                        Size = ChallengeSize;
                    }
                }
            }
            else
            {
                Text = $"{AppName} Connected";
            }
        }

        private async void WebBrowserRmtDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (sender == null)
                return;
            if (e == null)
                return;
            if (e.Url == null)
                return;
            if (e.Url.ToString() == InstagramUri.ToString() && !IsWebBrowserInUse)
            {
                // Get cookies from WebBrowser
                var cookies = GetUriCookies(InstagramUri);

                // Pass web browser document source and cookies to this function:
                // NOTE: Don't use WebBrowserRmt.Document.Cookie to get cookies because it's not getting full cookies
                var result = await InstaApi.SetCookiesAndHtmlForFbLoginAndChallenge(WebBrowserRmt.DocumentText, cookies);
                // You are logged in
                if (result.Succeeded)
                {
                    Text = $"{AppName} Connected";
                    // Save session
                    var state = InstaApi.GetStateDataAsStream();
                    using (var fileStream = File.Create(StateFile))
                    {
                        state.Seek(0, SeekOrigin.Begin);
                        state.CopyTo(fileStream);
                    }
                }
                else
                {
                    // there is an unknown error.
                    Text = $"{AppName} couldn't login";
                }

                Thread.Sleep(1500);
                WebBrowserRmt.Stop();
                WebBrowserRmt.Visible = false;
                IsWebBrowserInUse = true;
                Size = NormalSize;
            }
        }



        private async void GetFeedButtonClick(object sender, EventArgs e)
        {
            // Note2: A RichTextBox control added to show you some of feeds.

            if (InstaApi == null)
                MessageBox.Show("Login first.");
            if (!InstaApi.IsUserAuthenticated)
                MessageBox.Show("Login first.");

            var x = await InstaApi.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(1));
            Debug.WriteLine("Explore Feeds Result: " + x.Succeeded);

            if (x.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var media in x.Value.Medias)
                {
                    sb.AppendLine(DebugUtils.PrintMedia("Feed media", media));
                }
                RtBox.Text = sb.ToString();
                RtBox.Visible = true;
                Size = ChallengeSize;
            }
        }


        #region DllImport for getting full cookies from WebBrowser
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookieEx(string url,
            string cookieName, 
            StringBuilder cookieData,
            ref int size, 
            Int32 dwFlags,
            IntPtr lpReserved);

        private const Int32 InternetCookieHttponly = 0x2000;
        public static string GetUriCookies(Uri uri)
        {
            string cookies = "";
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return cookies;
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(
                    uri.ToString(),
                    null, cookieData,
                    ref datasize,
                    InternetCookieHttponly,
                    IntPtr.Zero))
                    return cookies;
            }
            if (cookieData.Length > 0)
            {
                cookies = cookieData.ToString();
            }
            return cookies;
        }
        #endregion
    }

    public static class DebugUtils
    {
        public static string PrintMedia(string header, InstaMedia media)
        {
            var content = $"{header}: {media.Caption?.Text.Truncate(30)}, {media.Code}";
            Debug.WriteLine(content);
            return content;
        }
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
}
