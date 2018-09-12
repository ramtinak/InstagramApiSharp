using System;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class SaveLoadState : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public SaveLoadState(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var result = await _instaApi.GetCurrentUserAsync();
            if (!result.Succeeded)
            {
                Console.WriteLine($"Unable to get current user using current API instance: {result.Info}");
                return;
            }
            Console.WriteLine($"Got current user: {result.Value.UserName} using existing API instance");
            var stream = _instaApi.GetStateDataAsStream();
            //// for .net core you should use this method:
            // var json = _instaApi.GetStateDataAsString();
            var anotherInstance = InstaApiBuilder.CreateBuilder()
                .SetUser(UserSessionData.Empty)
                .SetRequestDelay(RequestDelay.FromSeconds(2,2))
                .Build();
            anotherInstance.LoadStateDataFromStream(stream);
            //// for .net core you should use this method:
            // anotherInstance.LoadStateDataFromString(json);
            var anotherResult = await anotherInstance.GetCurrentUserAsync();
            if (!anotherResult.Succeeded)
            {
                Console.WriteLine($"Unable to get current user using current API instance: {result.Info}");
                return;
            }
            Console.WriteLine(
                $"Got current user: {anotherResult.Value.UserName} using new API instance without re-login");
        }
    }
}