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
        /// <summary>
        ///     Api instance (one instance per Instagram user)
        /// </summary>
        private static IInstaApi InstaApi;

        static void Main(string[] args)
        {
            var result = Task.Run(MainAsync).GetAwaiter().GetResult();
            if (result)
                return;
            Console.ReadKey();
        }
        public static async Task<bool> MainAsync()
        {
            try
            {
                Console.WriteLine("Starting demo of InstagramApiSharp project");
                // create user session data and provide login details
                var userSession = new UserSessionData
                {
                    UserName = "Username",
                    Password = "Password"
                };
                // if you want to set custom device (user-agent) please check this:
                // https://github.com/ramtinak/InstagramApiSharp/wiki/Set-custom-device(user-agent)

                var delay = RequestDelay.FromSeconds(2, 2);
                // create new InstaApi instance using Builder
                InstaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .UseLogger(new DebugLogger(LogLevel.All)) // use logger for requests and debug messages
                    .SetRequestDelay(delay)
                    .Build();
                // create account
                // to create new account please check this:
                // https://github.com/ramtinak/InstagramApiSharp/wiki/Create-new-account
                const string stateFile = "state.bin";
                try
                {
                    if (File.Exists(stateFile))
                    {
                        Console.WriteLine("Loading state from file");
                        using (var fs = File.OpenRead(stateFile))
                        {
                            InstaApi.LoadStateDataFromStream(fs);
                            // in .net core or uwp apps don't use LoadStateDataFromStream
                            // use this one:
                            // _instaApi.LoadStateDataFromString(new StreamReader(fs).ReadToEnd());
                            // you should pass json string as parameter to this function.
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (!InstaApi.IsUserAuthenticated)
                {
                    // login
                    Console.WriteLine($"Logging in as {userSession.UserName}");
                    delay.Disable();
                    var logInResult = await InstaApi.LoginAsync();
                    delay.Enable();
                    if (!logInResult.Succeeded)
                    {
                        Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                        return false;
                    }
                }
                var state = InstaApi.GetStateDataAsStream();
                // in .net core or uwp apps don't use GetStateDataAsStream.
                // use this one:
                // var state = _instaApi.GetStateDataAsString();
                // this returns you session as json string.
                using (var fileStream = File.Create(stateFile))
                {
                    state.Seek(0, SeekOrigin.Begin);
                    state.CopyTo(fileStream);
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
                    [ConsoleKey.D9] = new UploadVideo(InstaApi)
                };
                var key = Console.ReadKey();
                Console.WriteLine(Environment.NewLine);
                if (samplesMap.ContainsKey(key.Key))
                    await samplesMap[key.Key].DoShow();
                Console.WriteLine("Done. Press esc key to exit...");

                key = Console.ReadKey();
                return key.Key == ConsoleKey.Escape;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // perform that if user needs to logged out
                // var logoutResult = Task.Run(() => _instaApi.LogoutAsync()).GetAwaiter().GetResult();
                // if (logoutResult.Succeeded) Console.WriteLine("Logout succeed");
            }
            return false;
        }
    }
}
