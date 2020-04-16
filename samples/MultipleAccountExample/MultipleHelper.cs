using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.SessionHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleAccountExample
{
    public class InstaApiList : List<IInstaApi>
    {
        //public IInstaApi CurrentInstaApi { get; set; }
        public void SaveSessions()
        {
            Helper.CreateAccountDirectory();
            if (this == null)
                return;
            if(this.Any())
            {
                foreach(var instaApi in this)
                {
                    var state = instaApi.GetStateDataAsStream();
                    var path = Path.Combine(Helper.AccountPathDirectory, $"{instaApi.GetLoggedUser().UserName}{Helper.SessionExtension}");
                    using (var fileStream = File.Create(path))
                    {
                        state.Seek(0, SeekOrigin.Begin);
                        state.CopyTo(fileStream);
                    }
                }
            }
        }
    }
    static class MultipleHelper
    {
        public static ObservableCollection<string> LoggedInUsers = new ObservableCollection<string>();
        public static InstaApiList ApiList { get; private set; } = new InstaApiList();

        static public void LoadSessions()
        {
            ApiList = new InstaApiList();
            if (Directory.Exists(Helper.AccountPathDirectory))
            {
                var files = Directory.GetFiles(Helper.AccountPathDirectory);
                if (files?.Length > 0)
                {
                    foreach (var path in files)
                    {
                        if (Path.GetExtension(path).ToLower() == Helper.SessionExtension)
                        {
                            // load session!
                            var api = BuildApi();
                            var sessionHandler = new FileSessionHandler { FilePath = path, InstaApi = api };

                            api.SessionHandler = sessionHandler;

                            api.SessionHandler.Load();
                            if (api.IsUserAuthenticated)
                            {
                                LoggedInUsers.Add(api.GetLoggedUser().LoggedInUser.UserName.ToLower());
                                ApiList.Add(api);
                            }

                        }
                    }
                }
            }
            else
                Directory.CreateDirectory(Helper.AccountPathDirectory);
        }


        static public IInstaApi BuildApi(string username = null, string password = null)
        {
            var fakeUserData = UserSessionData.ForUsername(username ?? "FAKEUSER").WithPassword(password ?? "FAKEPASS");
            return InstaApiBuilder.CreateBuilder()
                             .SetUser(fakeUserData)
                             .Build();
        }
    }
}
