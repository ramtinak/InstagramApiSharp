using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        private static IInstaApi InstaApi;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var abc = @"shbid=8693; mid=WxEvMgAEAAF6OouE6Xi1MicpaMC6; csrftoken=dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW; ds_user_id=5318277344; shbts=1527853226.7332308; rur=PRN; mcd=3; urlgen=""{\""time\"": 1527853224\054 \""62.102.131.34\"": 50810}:1fOiPy:gdAKL7gBXkaKGo6aQcklNh6XDM8""; mid=WthhtwAEAAHptI0kYzH49v8tgYMw; csrftoken=dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW";
            //shbid=                8693;
            //mid=                  WxEvMgAEAAF6OouE6Xi1MicpaMC6; 
            //csrftoken=            dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW; 
            //ds_user_id=           5318277344; 
            //shbts=                1527853226.7332308; 
            //rur=                  PRN;
            //mcd=                  3; 
            //urlgen=               "{\"time\": 1527853224\054 \"62.102.131.34\": 50810}:1fOiPy:gdAKL7gBXkaKGo6aQcklNh6XDM8";
            //mid=                  WthhtwAEAAHptI0kYzH49v8tgYMw;
            //csrftoken=            dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW
            var removeStart = "urlgen=";
            var removeEnd = "; ";
            var t = abc.Substring(abc.IndexOf(removeStart) + 0);
            t = t.Substring(0, t.IndexOf(removeEnd) + 2);
            Debug.WriteLine(t);
            abc = abc.Replace(t, "");
            Debug.WriteLine(abc);
            //this.Close();
            var list = new List<string>(abc.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
            var dic = new Dictionary<string, string>();
            //foreach (var item in list)
            //{
            //    var spl = item.Split('=');
            //    //dic.Add(spl[0], spl[1]);
            //    Debug.WriteLine(spl[0] + "\t\t\t" + spl[1]);
            //    Debug.WriteLine("");
            //}

            //foreach (var item in dic)
            //{
            //    Debug.WriteLine(item.Key + "\t\t\t" + item.Value);
            //    Debug.WriteLine("");
            //}
            Debug.WriteLine("List: " + list.Count);
            Debug.WriteLine("Dic: " + dic.Count);
        }

        private  void button1_Click(object sender, EventArgs eq)
        { }
        private void button2_Click(object sender, EventArgs e) { }

        private  void button3_Click(object sender, EventArgs e)
        {
          
            //var x = await InstaApi.SendVerifyForChallenge(textBox1.Text);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var userSession = new UserSessionData
            {
                UserName = "rmt4006",
                Password = "pytop298"
            };

            // create new InstaApi instance using Builder
            InstaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
                                                                 //.SetRequestDelay(TimeSpan.FromSeconds(2))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                .Build();
            var stateFile = Application.StartupPath + "\\bb.json";
            Debug.WriteLine("InstaApi.IsUserAuthenticated:  " + InstaApi.IsUserAuthenticated);


            if (!InstaApi.IsUserAuthenticated)
            {
                // login
                Debug.WriteLine($"Logging in as {userSession.UserName}");
                var logInResult = await InstaApi.LoginAsync();
                Debug.WriteLine("logInResult:  " + logInResult.Succeeded);
                if (!logInResult.Succeeded)
                {
                    if (logInResult.Value == InstaLoginResult.TwoFactorRequired)
                    {
                        //var t = await InstaApi.GetChallengeChoices();
                    }
                    else
                    {
                        Debug.WriteLine($"Unable to login: {logInResult.Info.Message}");
                        var s = InstaApi.GetChallenge();
                        //Debug.WriteLine(s.url);
                        //var t = await InstaApi.GetChallengeChoices();
                        //if (t.Succeeded)
                        //{
                        //    Debug.WriteLine("t succed");
                        //    var x = await InstaApi.SendUserChoiceForChallenge(int.Parse(t.Value.choice));

                        //    Debug.WriteLine("x succed");

                        //}
                        //else
                        //    Debug.WriteLine("t unsuc");
                        flag = false;
                        webBrowser1.Navigate(s.Url/*,null,null, "User-Agent: " +USER_AGENT*/);
                        webBrowser1.Visible = true;
                        //return;
                        //var t = await InstaApi.GetChallengeChoices();
                        //if (t.Succeeded)
                        //{
                        //    Debug.WriteLine("t succed");
                        //    var x = await InstaApi.SendUserChoiceForChallenge(int.Parse(t.Value.choice));

                        //    Debug.WriteLine("x succed");

                        //}
                        //else
                        //    Debug.WriteLine("t unsuc");

                        //return;
                    }
                }
                else
                {
                    Debug.WriteLine($"Logged in done");

                }
            }
            else
            {
                Debug.WriteLine($"Logged in done");

            }
        }
 

        /// <summary>
        /// Gets the URI cookie container.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;
            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(
                    uri.ToString(),
                    null, cookieData,
                    ref datasize,
                    InternetCookieHttponly,
                    IntPtr.Zero))
                    return null;
            }
            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                Debug.WriteLine(cookieData.ToString());
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookieEx(
 string url,
 string cookieName,
 StringBuilder cookieData,
 ref int size,
 Int32 dwFlags,
 IntPtr lpReserved);

        private const Int32 InternetCookieHttponly = 0x2000;
        public static string GetUriCookies(Uri uri)
        {
            string cookies = "";
            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(
                    uri.ToString(),
                    null, cookieData,
                    ref datasize,
                    InternetCookieHttponly,
                    IntPtr.Zero))
                    return null;
            }
            if (cookieData.Length > 0)
            {
                Debug.WriteLine(cookieData.ToString());
                cookies = cookieData.ToString();
            }
            return cookies;
        }

        public const string USER_AGENT =
            "Instagram 12.0.0.7.91 Android (23/6.0.1; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_NZ)";

        bool flag = false;
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Debug.WriteLine("url: " + e.Url);
            Debug.WriteLine("cookie: " + webBrowser1.Document.Cookie);
            //url: https://www.instagram.com/
            //cookie: shbid=8693; mid=WxEvMgAEAAF6OouE6Xi1MicpaMC6; csrftoken=dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW; ds_user_id=5318277344; shbts=1527853222.6987925; rur=PRN; mcd=3; mid=WthhtwAEAAHptI0kYzH49v8tgYMw; csrftoken=dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW

            //url: about:blank
            //cookie: shbid=8693; mid=WxEvMgAEAAF6OouE6Xi1MicpaMC6; csrftoken=dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW; ds_user_id=5318277344; shbts=1527853226.7332308; rur=PRN; mcd=3; urlgen="{\"time\": 1527853224\054 \"62.102.131.34\": 50810}:1fOiPy:gdAKL7gBXkaKGo6aQcklNh6XDM8"; mid=WthhtwAEAAHptI0kYzH49v8tgYMw; csrftoken=dLukIyVhtNoQ3Za8zLUswDXs25uRWjiW

            if (e.Url.ToString() == "about:blank" && !flag)
                //if (e.Url.ToString() == "https://www.instagram.com/"|| e.Url.ToString() == "https://www.instagram.com")
            {
               var cookies = GetUriCookies(new Uri("https://www.instagram.com/"));
                //InstaApi.SetCookiesForChallenge(webBrowser1.DocumentText, webBrowser1.Document.Cookie);

                InstaApi.SetCookiesAndHtmlForChallenge(webBrowser1.DocumentText, cookies);
                Debug.WriteLine("SetCookiesForChallenge");
                webBrowser1.Stop();
                webBrowser1.Visible = false;
                flag = true;
            }
            Debug.WriteLine("");
            Debug.WriteLine("");

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var x = await InstaApi.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(1));
            Debug.WriteLine("Suc: " + x.Succeeded);
            if(x.Succeeded)
            {
                Debug.WriteLine("Medias: " + x.Value.Medias.Count);
                foreach (var item in x.Value.Medias)
                {
                    Debug.WriteLine(item.Caption);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //var state = InstaApi.GetStateDataAsString();
            //Debug.WriteLine($"button6_Click");

            //File.WriteAllText("bb.json", state);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //var source = richTextBox1.Text;//.Replace("\"", "'");
            //var start = "<script type=\"text/javascript\">window._sharedData";
            //var end = ";</script>";
            //if (source.Contains(start))
            //{
            //    var str = source.Substring(source.IndexOf(start) + start.Length);
            //    str = str.Substring(0, str.IndexOf(end));
            //    //<script type="text/javascript">window._sharedData = 
            //    str = str.Substring(str.IndexOf("=")+2);
            //    Debug.WriteLine(str);
            //    var o = JsonConvert.DeserializeObject<WebBrowserResponse>(str);
            //    Debug.WriteLine("CsrfToken :  " + o.Config.CsrfToken);
            //    Debug.WriteLine("Biography :  " + o.Config.Viewer.Biography);
            //    Debug.WriteLine("ExternalUrl :  " + o.Config.Viewer.ExternalUrl);
            //    Debug.WriteLine("FullName :  " + o.Config.Viewer.FullName);
            //    Debug.WriteLine("HasProfilePic :  " + o.Config.Viewer.HasProfilePic);
            //    Debug.WriteLine("Id :  " + o.Config.Viewer.Id);
            //    Debug.WriteLine("ProfilePicUrl :  " + o.Config.Viewer.ProfilePicUrl);
            //    Debug.WriteLine("ProfilePicUrlHd :  " + o.Config.Viewer.ProfilePicUrlHd);
            //    Debug.WriteLine("Username :  " + o.Config.Viewer.Username);


            //}
        }
    }
}

