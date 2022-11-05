/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Logger;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace TwoFactorSample
{
    public partial class Form1 : Form
    {
        const string AppName = "Two Factor";
        const string StateFile = "state.bin";
        private static IInstaApi InstaApi;
        //307, 280
        readonly Size NormalSize = new Size(307, 150);
        readonly Size TwoFactorSize = new Size(307, 280);

        public Form1()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            Size = NormalSize;
            var userSession = new UserSessionData
            {
                UserName = txtUsername.Text,
                Password = txtPassword.Text
            };

            InstaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                .Build();
            Text = $"{AppName} Connecting";
            LoadSession();

            if (!InstaApi.IsUserAuthenticated)
            {
                var logInResult = await InstaApi.LoginAsync();
                Debug.WriteLine(logInResult.Value);
                if (logInResult.Succeeded)
                {
                    Text = $"{AppName} Connected";
                    // Save session 
                    SaveSession();
                }
                else
                {
                    // two factor is required
                    if (logInResult.Value == InstaLoginResult.TwoFactorRequired)
                    {
                        // open a box so user can send two factor code
                        Size = TwoFactorSize;
                    }
                }
            }
            else
            {
                Text = $"{AppName} Connected";
            }
        }

        private async void TwoFactorButton_Click(object sender, EventArgs e)
        {
            if (InstaApi == null)
                return;
            if (string.IsNullOrEmpty(txtTwoFactorCode.Text))
            {
                MessageBox.Show("Please type your two factor code and then press Auth button.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // send two factor code
            var twoFactorLogin = await InstaApi.TwoFactorLoginAsync(txtTwoFactorCode.Text);
            if (twoFactorLogin.Succeeded)
            {
                // connected
                // save session
                SaveSession();
                Text = $"{AppName} Connected";
                Size = NormalSize;
            }
            else
            {
                MessageBox.Show(twoFactorLogin.Info.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadSession()
        {
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
        }
        void SaveSession()
        {
            if (InstaApi == null)
                return;
            var state = InstaApi.GetStateDataAsStream();
            using (var fileStream = File.Create(StateFile))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
            }
        }
    }
}
