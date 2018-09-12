/*
 * Created by Ramtin Jokar [Ramtinak@live.com] [Telegram: https://t.me/Ramtinak]
 * 
 * NOTE 1: Minimum target version must be 16299 (Fall creators update)
 * NOTE 2: These capabilities Internet(Client), Internet(Client, Server) should be checked.
 * 
 * If you want, upload videos and image together or single, check this example:
 * https://github.com/ramtinak/InstaPost/
 * NOTE 3: You cannot set Image.Uri or Video.Uri directly in .NET Core apps, you should
 * set VideoBytes for videos and ImageBytes for images.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace UwpExample
{
    enum FileType
    {
        Video,
        Image
    }
    static class Helper
    {
        public static FileType GetFileType(string path)
        {
            var ext = Path.GetExtension(path).ToLower().Replace(".", "");
            return ext == "mp4" || ext == "mov" ? FileType.Video : FileType.Image;
        }
        public static string Print(this object obj)
        {
            var content = Convert.ToString(obj);
            Debug.WriteLine(content);
            return content;
        }
        public static void ShowERR(this string msg)
        {
            ShowMsg(msg, "ERR");
        }
        public static async void ShowMsg(this string msg, string title = "")
        {
            await new MessageDialog(msg, title).ShowAsync();
        }
        /// <summary>
        /// Change app title
        /// </summary>
        /// <param name="newTitle"></param>
        public static void ChangeAppTitle(this string newTitle)
        {
            var appView = ApplicationView.GetForCurrentView();
            appView.Title = newTitle;
        }
        public static string PrintException(this Exception ex, string name = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{name} exception thrown: {ex.Message}");
            sb.AppendLine($"Source: {ex.Source}");
            sb.AppendLine($"Stack trace: {ex.StackTrace}");
            sb.AppendLine();
            return sb.Print();
        }

    }
}
