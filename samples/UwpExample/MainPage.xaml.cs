/*
 * Created by Ramtin Jokar [Ramtinak@live.com] [Telegram: https://t.me/Ramtinak]
 * 
 * NOTE 1: Minimum target version must be 14393 (Anniversary update)
 * NOTE 2: These capabilities Internet(Client), Internet(Client, Server) should be checked.
 * 
 * If you want, upload videos and image together or single, check this example:
 * https://github.com/ramtinak/InstaPost/
 * NOTE 3: You cannot set Image.Uri or Video.Uri directly in .NET Core apps, you should
 * set VideoBytes for videos and ImageBytes for images.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using InstagramApiSharp.API;
using Windows.Storage;
using System.Diagnostics;
using InstagramApiSharp.Classes;
using InstagramApiSharp.API.Builder;
using Windows.Storage.Pickers;
using InstagramApiSharp.Classes.Models;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace UwpExample
{
    public sealed partial class MainPage : Page
    {
        private static IInstaApi InstaApi;
        const string StateFileName = "state.json";
        readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
        IReadOnlyList<StorageFile> SelectedFiles = null;
        public MainPage()
        {
            this.InitializeComponent();
            Debug.WriteLine(LocalFolder.Path);
            Loaded += MainPageLoaded;
        }

        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            LoadSession();
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameText.Text))
            {
                "Please type your username.".ShowERR();
                return;
            }
            if (string.IsNullOrEmpty(PasswordText.Password))
            {
                "Please type your password.".ShowERR();
                return;
            }
            "".ChangeAppTitle();
            try
            {
                var userSession = new UserSessionData
                {
                    UserName = UsernameText.Text,
                    Password = PasswordText.Password
                };
                InstaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .Build();
                
                $"Connecting...".ChangeAppTitle();
                var loginResult = await InstaApi.LoginAsync();
                if (loginResult.Succeeded)
                {
                    "Connected".ChangeAppTitle();
                    SaveSession();
                }
                else
                {
                    switch (loginResult.Value)
                    {
                        case InstaLoginResult.InvalidUser:
                            "".ChangeAppTitle();
                            "Username is invalid.".ShowERR();
                            break;
                        case InstaLoginResult.BadPassword:
                            "".ChangeAppTitle();
                            "Password is wrong.".ShowERR();
                            break;
                        case InstaLoginResult.Exception:
                            "".ChangeAppTitle();
                            ("Exception throws:\n" + loginResult.Info?.Message).ShowERR();
                            break;
                        case InstaLoginResult.LimitError:
                            "".ChangeAppTitle();
                            "Limit error (you should wait 10 minutes).".ShowERR();
                            break;
                        case InstaLoginResult.ChallengeRequired:
                            "".ChangeAppTitle();
                            ("Challenge required.\r\nPlease see these example to understand how challenge handles works:\r\n" +
                            "https://github.com/ramtinak/InstagramApiSharp/tree/master/ChallengeRequireExample\r\n" +
                            "https://github.com/ramtinak/InstaPost/").ShowERR();
                            break;
                        case InstaLoginResult.TwoFactorRequired:
                            "".ChangeAppTitle();
                            ("Two factor authentication required.\r\nPlease see these example to understand how two factor authentication handles works:\r\n" +
                             "https://github.com/ramtinak/InstagramApiSharp/tree/master/TwoFactorSample\r\n" +
                             "https://github.com/ramtinak/InstaPost/").ShowERR();
                            break;
                    }


                }
            }
            catch (Exception ex) { ex.PrintException("LoginButtonClick").ShowERR(); }

        }

        private async void SelectFilesButtonClick(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            SelectedFiles = await filePicker.PickMultipleFilesAsync();
            if (SelectedFiles == null)
            {
                UploadButton.IsEnabled = false;
                return;
            }

            if (SelectedFiles.Count > 9)
            {
                "Only 9 files can select".ShowERR();
                SelectedFiles = null;
                UploadButton.IsEnabled = false;
            }
            else
                UploadButton.IsEnabled = true;

        }

        private async void UploadButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedFiles == null)
            {
                "Please select at least one file.".ShowERR();
                return;
            }
            if (UploadButton.Content.ToString().StartsWith("Uploading") ||
                 UploadButton.Content.ToString().StartsWith("Preparing"))
            {
                "Uploading....".ShowERR();
                return;
            }
            var caption = CaptionText.Text;
            if (SelectedFiles.Count == 0)
            {
                var file = SelectedFiles.FirstOrDefault();
                var fileBytes = (await FileIO.ReadBufferAsync(file)).ToArray();
                UploadButton.Content = "Uploading photo...";
                var img = new InstaImage
                {
                    // Set image bytes
                    ImageBytes = fileBytes,
                    // Note: you should set Uri path !
                    Uri = file.Path
                };
                var up = await InstaApi.MediaProcessor.UploadPhotoAsync(img, caption);
                if (up.Succeeded)
                    "Your photo uploaded successfully.".ShowMsg();
                else
                    up.Info.Message.ShowERR();
                UploadButton.Content = "Upload";
            }
            else
            {
                // album
                var videos = new List<InstaVideoUpload>();
                var images = new List<InstaImage>();

                //// How set videos?
                //videos.Add(new InstaVideoUpload
                //{
                //    // video
                //    Video = new InstaVideo
                //    {
                //        // set video bytes
                //        VideoBytes = VIDEOBYTES
                //        // Note: you should set Uri path ! you can set a random path
                //        //Uri = VIDEO PATH 
                //    },
                //    // video thumbnail image 
                //    VideoThumbnail = new InstaImage
                //    {
                //        // set video thumbnail image bytes
                //        ImageBytes = THUMBNAILIMAGEBYTES,
                //        // Note: you should set Uri path ! you can set a random path
                //        //Uri = THUMBNAIL PATH 
                //    }
                //});
                foreach (var file in SelectedFiles)
                {
                    var fileBytes = (await FileIO.ReadBufferAsync(file)).ToArray();
                    var img = new InstaImage
                    {
                        // Set image bytes
                        ImageBytes = fileBytes,
                        // Note: you should set Uri path ! you can set random path
                        Uri = file.Path
                    };
                    images.Add(img);
                }
                UploadButton.Content = "Uploading album...";

                var up = await InstaApi.MediaProcessor.UploadAlbumAsync(images.ToArray(),
                    videos.ToArray(), caption);

                if (up.Succeeded)
                    "Your album uploaded successfully.".ShowMsg();
                else
                    up.Info.Message.ShowERR();
                UploadButton.Content = "Upload";
            }

        }
        /// <summary>
        /// Load InstaApi session
        /// </summary>
        async void LoadSession()
        {
            try
            {
                var file = await LocalFolder.GetFileAsync(StateFileName);
                var json = await FileIO.ReadTextAsync(file);
                if (string.IsNullOrEmpty(json))
                    return;

                var userSession = new UserSessionData
                {
                    // no need to set username password
                    // but we have to set something in it
                    UserName = "Username",
                    Password = "Password"
                };
                InstaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .Build();
                InstaApi.LoadStateDataFromString(json);
                if (!InstaApi.IsUserAuthenticated)
                {
                    InstaApi = null;
                    return;
                }
                UsernameText.Text = InstaApi.GetLoggedUser().UserName;
                PasswordText.Password = InstaApi.GetLoggedUser().Password;
                "Connected".ChangeAppTitle();

            }
            catch { InstaApi = null; }
        }
        /// <summary>
        /// Save InstaApi session
        /// </summary>
        async void SaveSession()
        {
            if (InstaApi == null)
                return;
            if (!InstaApi.IsUserAuthenticated)
                return;

            try
            {
                // ReplaceExisting must be set!!!!
                var file = await LocalFolder.CreateFileAsync(StateFileName, CreationCollisionOption.ReplaceExisting);
                // Get state as string (note: in net core you can't get as Stream)
                var json = InstaApi.GetStateDataAsString();
                await FileIO.WriteTextAsync(file, json, Windows.Storage.Streams.UnicodeEncoding.Utf8);
                //// OR
                //byte[] fileBytes = Encoding.UTF8.GetBytes(json);
                //using (var stream = await file.OpenStreamForWriteAsync())
                //{
                //    stream.Write(fileBytes, 0, fileBytes.Length);
                //}
            }
            catch { }
        }

 
    }
}
