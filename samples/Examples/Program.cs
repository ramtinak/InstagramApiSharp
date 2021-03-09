using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using Examples.Samples;
using InstagramApiSharp.Logger;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples
{
    class Program
    {
        private static IInstaApi InstaApi;
        public static string entry = "";
        private static UserSessionData user;
        private static IResult<InstaLoginResult> loginRequest;

        static void Main(string[] args)
        {
            var result = Task.Run(MainAsync).GetAwaiter().GetResult();
            if (result)
                return;
            Console.ReadLine();
        }
        public static async Task<bool> MainAsync()
        {
            Console.WriteLine("Starting demo of InstagramApiSharp project");

            user = new UserSessionData
            {
                UserName = "your usernam",
                Password = "your password"
            };


            do
            {
                Console.WriteLine("Trying to Loge in to " + user.UserName);
                InstaApi = InstaApiBuilder.CreateBuilder().SetUser(user).UseLogger(new DebugLogger(LogLevel.Exceptions)).Build();
                await InstaApi.SendRequestsBeforeLoginAsync();
                await Task.Delay(5000);
                loginRequest = await InstaApi.LoginAsync();
            }
            while (!loginRequest.Succeeded);
            if (loginRequest.Succeeded)
            {
                Console.WriteLine("Logged in to " + user.UserName);
                //     username = user.UserName;
                InstaApi.SetRequestDelay(RequestDelay.FromSeconds(1, 2));
            }
            else
            {
                Console.WriteLine("Error" + loginRequest.Info.Message);
            }

            
            Console.WriteLine("Press 1 to start basic demo samples");
            Console.WriteLine("Press 2 to start upload photo demo sample");
            Console.WriteLine("Press 3 to start comment media demo sample");
            Console.WriteLine("Press 4 to start stories demo sample");
            Console.WriteLine("Press 5 to start demo with saving state of API instance");
            Console.WriteLine("Press 6 to start messaging demo sample");
            Console.WriteLine("Press 7 to start location demo sample");
            Console.WriteLine("Press 8 to start collections demo sample");
            Console.WriteLine("Press 9 to start upload video demo sample");
            Console.WriteLine("Press 0 to start live video and save it afterwards.");

            var samplesMap = new Dictionary<ConsoleKey, IDemoSample>
            {
                [ConsoleKey.D1] = new Basics(InstaApi),
                [ConsoleKey.D2] = new UploadPhoto(InstaApi),
                [ConsoleKey.D3] = new CommentMedia(InstaApi),
                [ConsoleKey.D4] = new Stories(InstaApi),
                [ConsoleKey.D5] = new SaveLoadState(InstaApi),
                [ConsoleKey.D6] = new Messaging(InstaApi),
                [ConsoleKey.D7] = new LocationSample(InstaApi),
                [ConsoleKey.D8] = new CollectionSample(InstaApi),
                [ConsoleKey.D9] = new UploadVideo(InstaApi),
                [ConsoleKey.D0] = new StartLive(InstaApi)
            };
            var key = Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            if (samplesMap.ContainsKey(key.Key))
                await samplesMap[key.Key].DoShow();
            Console.WriteLine("Done. Press esc key to exit...");

            key = Console.ReadKey();
            return key.Key == ConsoleKey.Escape;

        }
    }
}
