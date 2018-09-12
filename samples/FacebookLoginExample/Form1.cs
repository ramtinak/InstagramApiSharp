/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        // Facebook examples
        // Facebook login is not a built-in implements in InstagramApiSharp but you can
        // use it easily to login with Facebook.
        // Please read all comments one by one to know how can you add it to your projects.

        // Note 1: if you in Iran, you cannot test this example without VPN.
        // only VPN works for winform/wpf apps. TunnelPlus or SSL Tunnel won't work.
        Timer FacebookTimer = new Timer();
        bool FacebookFirstTime = true;
        IInstaApi InstaApi;
        const string StateFile = "state.bin";

        public Form1()
        {
            InitializeComponent();
            FacebookTimer.Interval = 15 * 1000; // 15 seconds, it's up to user network speed
            FacebookTimer.Tick += FacebookTimerTick;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadingPanel.Dock = FacebookWebBrowser.Dock = DockStyle.Fill;
        }
        private async void FacebookLoginButton_Click(object sender, EventArgs e)
        {
            LoadingPanel.Visible = true;
            await Task.Delay(1500);
            FacebookFirstTime = true;
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
            // all cached and cookies for facebook and instagram addresses.
            // WebBrowser control uses Internet Explorer so we need to clean up.
            WebBrowserHelper.ClearForSpecificUrl(FacebookLoginHelper.InstagramUriWithoutWWWAddress.ToString());
            WebBrowserHelper.ClearForSpecificUrl(FacebookLoginHelper.InstagramUriAddress.ToString());
            WebBrowserHelper.ClearForSpecificUrl(FacebookLoginHelper.InstagramApiAddress.ToString());
            WebBrowserHelper.ClearForSpecificUrl(FacebookLoginHelper.FacebookAddress.ToString());
            WebBrowserHelper.ClearForSpecificUrl(FacebookLoginHelper.FacebookMobileAddress.ToString());

            // wait 3.5 second
            System.Threading.Thread.Sleep(3500);
            // navigate to instagram site
            FacebookWebBrowser.Navigate(FacebookLoginHelper.InstagramUriAddress);
            do
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            while (FacebookWebBrowser.ReadyState != WebBrowserReadyState.Complete);
        }
        private async void FacebookWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs args)
        {
            if (FacebookLoginHelper.FirstStep(args.Url))
            {
                if (FacebookFirstTime)
                {
                    FacebookFirstTime = false;
                    FacebookTimer.Start();
                }
                else
                {
                    var cookies = GetUriCookies(FacebookLoginHelper.InstagramUriAddress);
                    var html = FacebookWebBrowser.DocumentText;
                    var response = FacebookLoginHelper.GetLoggedInUserResponse(html);
                    if (response != null && response.Config != null && response.Config.Viewer != null)
                    {
                        var username = response.Config.Viewer.Username;
                        // logged in successfully
                        FacebookWebBrowser.Visible = false;
                        // we don't have password so we fill it up with fake password
                        var userSession = new UserSessionData
                        {
                            UserName = username,
                            Password = "alakimasalan"
                        };
                        // note: you cannot change password while you logged in with facebook account.


                        // build InstaApi
                        InstaApi = InstaApiBuilder.CreateBuilder()
                            .SetUser(userSession)
                            .UseLogger(new DebugLogger(LogLevel.Exceptions))
                            .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                            .Build();
                        LoadingPanel.Visible = false;
                        // pass information to InstaApi
                        var result = await InstaApi.SetCookiesAndHtmlForFacebookLogin(response, cookies, true);
                        if(result.Value)
                        {
                            // Save session 
                            var state = InstaApi.GetStateDataAsStream();
                            using (var fileStream = File.Create(StateFile))
                            {
                                state.Seek(0, SeekOrigin.Begin);
                                state.CopyTo(fileStream);
                            }
                            // save session as json
                            //var str = InstaApi.GetStateDataAsString();
                            //File.WriteAllText("abc.json", str);
                            // visible get some feed button
                            GetFeedButton.Visible = true;
                        }
                        else
                            $"An error has occured.".Output();
                    }
                }
            }

        }
        private async void FacebookTimerTick(object sender, EventArgs e)
        {
            FacebookTimer.Stop();
            try
            {
                bool first = true;
                LBLRepeat:
                var forms = FacebookWebBrowser.Document.Forms;
                if (forms != null && forms.Count > 0)
                {
                    foreach (HtmlElement item in forms)
                    {
                        foreach (HtmlElement it in item.All)
                        {
                            var innerHtml = it.InnerHtml;
                            if (FacebookLoginHelper.SecondStep(innerHtml))
                            {
                                var btn = it.GetElementsByTagName("button");
                                btn[0].InvokeMember("click");
                                break;
                            }
                        }
                    }
                }

                await Task.Delay(2500);
                LoadingPanel.Visible = false;
                await Task.Delay(1500);
                //sometimes we need to press 2 time
                if (first)
                {
                    first = false;

                    goto LBLRepeat;
                }
            }
            catch
            {
                await Task.Delay(2500);
                LoadingPanel.Visible = false;
                await Task.Delay(1500);
            }
        }
        private async void GetFeedButtonClick(object sender, EventArgs e)
        {
            // Note2: A RichTextBox control added to show you some of feeds.

            if (InstaApi == null)
                MessageBox.Show("Login first.");
            if (!InstaApi.IsUserAuthenticated)
                MessageBox.Show("Login first.");

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
