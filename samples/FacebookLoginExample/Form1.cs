/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.SessionHandlers;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace FacebookLoginExample
{
    public partial class Form1 : Form
    {
        const string AppName = "Facebook login example";
        // Facebook examples
        // Facebook login is not a built-in implements in InstagramApiSharp but you can
        // use it easily to login with Facebook.
        // Please read all comments one by one to know how can you add it to your projects.

        // Note 1: if you in Iran, you cannot test this example without VPN.
        // only VPN works for winform/wpf apps. TunnelPlus or SSL Tunnel won't work.

        IInstaApi InstaApi;
        const string StateFile = "state.bin";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FacebookWebBrowser.Dock = DockStyle.Fill;
        }
        private async void FacebookLoginButton_Click(object sender, EventArgs e)
        {
            await Task.Delay(1500);
            // visible fb web browser
            FacebookWebBrowser.Visible = true;
            // suppress script errors
            FacebookWebBrowser.ScriptErrorsSuppressed = true;
            try
            {
                // remove handler
                FacebookWebBrowser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(FacebookWebBrowserDocumentCompleted);
            }
            catch { }
            // add handler
            FacebookWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(FacebookWebBrowserDocumentCompleted);

            // Every time we want to login with another facebook account, we need to clear
            // all cached and cookies for facebook addresses.
            // WebBrowser control uses Internet Explorer so we need to clean up.
            WebBrowserHelper.ClearForSpecificUrl(InstaFbHelper.FacebookAddressWithWWWAddress.ToString());
            WebBrowserHelper.ClearForSpecificUrl(InstaFbHelper.FacebookAddress.ToString());
            WebBrowserHelper.ClearForSpecificUrl(InstaFbHelper.FacebookMobileAddress.ToString());

            // wait 3.5 second
            System.Threading.Thread.Sleep(3500);

            var facebookLoginUri = InstaFbHelper.GetFacebookLoginUri();
            var userAgent = InstaFbHelper.GetFacebookUserAgent();
            
            FacebookWebBrowser.Navigate(facebookLoginUri, null, null, string.Format("\r\nUser-Agent: {0}\r\n", userAgent));

            do
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            while (FacebookWebBrowser.ReadyState != WebBrowserReadyState.Complete);
        }
        private async void FacebookWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs args)
        {
            try
            {
                var html = FacebookWebBrowser.DocumentText;
                if (InstaFbHelper.IsLoggedIn(html))
                {
                    var cookies = GetUriCookies(args.Url);
                    var fbToken = InstaFbHelper.GetAccessToken(html);

                    InstaApi = BuildApi();
                    Text = $"{AppName} Connecting";
                    var loginResult = await InstaApi.LoginWithFacebookAsync(fbToken, cookies);

                    if (loginResult.Succeeded)
                    {
                        Text = $"{AppName} Connected";
                        GetFeedButton.Visible = true;
                        SaveSession();
                    }
                    else
                    {
                        switch (loginResult.Value)
                        {
                            case InstaLoginResult.BadPassword:
                                MessageBox.Show("Wrong Password");
                                break;
                            case InstaLoginResult.ChallengeRequired:
                            case InstaLoginResult.TwoFactorRequired:
                                MessageBox.Show("You must combine Challenge Example to your project");
                                break;
                            default:
                                MessageBox.Show($"ERR: {loginResult.Value}\r\n{loginResult.Info.Message}");
                                break;
                        }
                        Text = $"{AppName} ERROR";
                    }

                }
            }
            catch { }
        }
        private async void GetFeedButtonClick(object sender, EventArgs e)
        {
            // Note2: A RichTextBox control added to show you some of feeds.

            if (InstaApi == null)
            {
                MessageBox.Show("Login first.");
                return;
            }
            if (!InstaApi.IsUserAuthenticated)
            {
                MessageBox.Show("Login first.");
                return;
            }

            var x = await InstaApi.FeedProcessor.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(1));

            if (x.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb2.AppendLine("Like 5 Media>");
                foreach (var item in x.Value.Medias.Take(5))
                {
                    // like media...
                    var liked = await InstaApi.MediaProcessor.LikeMediaAsync(item.InstaIdentifier);
                    sb2.AppendLine($"{item.InstaIdentifier} liked? {liked.Succeeded}");
                }

                sb.AppendLine(("Explore Feeds Result: " + x.Succeeded).Output());
                foreach (var media in x.Value.Medias)
                {
                    sb.AppendLine(DebugUtils.PrintMedia("Feed media", media));
                }
                RtBox.Text = sb2.ToString() + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                RtBox.Text += sb.ToString();
                RtBox.Visible = true;
            }
        }

        IInstaApi BuildApi()
        {
            return InstaApiBuilder.CreateBuilder()
                .SetUser(UserSessionData.ForUsername("FAKEUSERNAME").WithPassword("FAKEPASS"))
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                // Session handler, set a file path to save/load your state/session data
                .SetSessionHandler(new FileSessionHandler() { FilePath = StateFile })
                .Build();
        }
        void LoadSession()
        {
            InstaApi?.SessionHandler?.Load();

            //// Old load session
            //try
            //{
            //    if (File.Exists(StateFile))
            //    {
            //        Debug.WriteLine("Loading state from file");
            //        using (var fs = File.OpenRead(StateFile))
            //        {
            //            InstaApi.LoadStateDataFromStream(fs);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}
        }
        void SaveSession()
        {
            if (InstaApi == null)
                return;
            if (!InstaApi.IsUserAuthenticated)
                return;
            InstaApi.SessionHandler.Save();

            //// Old save session 
            //var state = InstaApi.GetStateDataAsStream();
            //using (var fileStream = File.Create(StateFile))
            //{
            //    state.Seek(0, SeekOrigin.Begin);
            //    state.CopyTo(fileStream);
            //}
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
            content.Output();
            return content;
        }
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
}
