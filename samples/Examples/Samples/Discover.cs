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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Processors;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Enums;
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
        private readonly IInstaApi InstaApi;

        public Discover(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        public async Task DoShow()
        {
            // get currently logged in user
            var currentUser = await InstaApi.GetCurrentUserAsync();
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
            var result = await InstaApi.DiscoverProcessor.GetRecentSearchesAsync();
            if (result.Succeeded)
            {
                Console.WriteLine("Recent search count: " + result.Value.Recent?.Count);
                if (result.Value.Recent?.Count > 0)
                    Console.WriteLine("First recent search: " + result.Value.Recent?.FirstOrDefault()?.User?.UserName);
            }
            else
                Console.WriteLine("Error while getting recent search: " + result.Info.Message);
        }

        public async void ClearRecentSearches()
        {
            var result = await InstaApi.DiscoverProcessor.ClearRecentSearchsAsync();
            if (result.Succeeded)
            {
                Console.WriteLine("Recent search cleared.");
            }
            else
                Console.WriteLine("Error while clearing recent searchs: " + result.Info.Message);
        }

        public async void SuggestedSearches()
        {
            var result = await InstaApi.DiscoverProcessor.GetSuggestedSearchesAsync(InstaDiscoverSearchType.Blended);
            if (result.Succeeded)
            {
                Console.WriteLine("Suggested search count: " + result.Value.Suggested?.Count);
                if (result.Value.Suggested?.Count > 0)
                    Console.WriteLine("First suggested search: " + result.Value.Suggested?.FirstOrDefault()?.User?.UserName);
            }
            else
                Console.WriteLine("Error while getting suggested searchs: " + result.Info.Message);
        }

        public async void SearchUser()
        {
            var search = "iran";
            var count = 30;
            var result = await InstaApi.DiscoverProcessor.SearchPeopleAsync(search, count);
            if (result.Succeeded)
            {
                Console.WriteLine("User search count: " + result.Value.Users?.Count);
                if (result.Value.Users?.Count > 0)
                    Console.WriteLine("First search user: " + result.Value.Users?.FirstOrDefault()?.UserName);
            }
            else
                Console.WriteLine("Error while searching users: " + result.Info.Message);
        }

    }
}
