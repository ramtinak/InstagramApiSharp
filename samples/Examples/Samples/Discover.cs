/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Processors;
using InstagramApiSharp.Classes;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class Discover : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public Discover(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            // get currently logged in user
            var currentUser = await _instaApi.GetCurrentUserAsync();
            Console.WriteLine(
                $"Logged in: username - {currentUser.Value.UserName}, full name - {currentUser.Value.FullName}");

            Console.WriteLine("See Samples/Discover.cs to see how it's works");
            Console.WriteLine("Discover functions: ");
            Console.WriteLine(@"GetRecentSearchsAsync
ClearRecentSearchsAsync
GetSuggestedSearchesAsync
SearchPeopleAsync");
        }

        public async void RecentSearches()
        {
            var result = await _instaApi.DiscoverProcessor.GetRecentSearchsAsync();
            if (result.Succeeded)
            {
                Console.WriteLine("Recent search count: " + result.Value.Recent?.Count);
                if (result.Value.Recent?.Count > 0)
                    Console.WriteLine("First recent search: " + result.Value.Recent?.FirstOrDefault()?.User?.Username);
            }
            else
                Console.WriteLine("Error while getting recent search: " + result.Info.Message);
        }

        public async void ClearRecentSearches()
        {
            var result = await _instaApi.DiscoverProcessor.ClearRecentSearchsAsync();
            if (result.Succeeded)
            {
                Console.WriteLine("Recent search cleared.");
            }
            else
                Console.WriteLine("Error while clearing recent searchs: " + result.Info.Message);
        }

        public async void SuggestedSearches()
        {
            var result = await _instaApi.DiscoverProcessor.GetSuggestedSearchesAsync(DiscoverSearchType.Blended);
            if (result.Succeeded)
            {
                Console.WriteLine("Suggested search count: " + result.Value.Recent?.Count);
                if (result.Value.Recent?.Count > 0)
                    Console.WriteLine("First suggested search: " + result.Value.Recent?.FirstOrDefault()?.User?.Username);
            }
            else
                Console.WriteLine("Error while getting suggested searchs: " + result.Info.Message);
        }

        public async void SearchUser()
        {
            var search = "iran";
            var count = 30;
            var result = await _instaApi.DiscoverProcessor.SearchPeopleAsync(search, count);
            if (result.Succeeded)
            {
                Console.WriteLine("User search count: " + result.Value.Users?.Count);
                if (result.Value.Users?.Count > 0)
                    Console.WriteLine("First search user: " + result.Value.Users?.FirstOrDefault()?.Username);
            }
            else
                Console.WriteLine("Error while searching users: " + result.Info.Message);
        }

    }
}
