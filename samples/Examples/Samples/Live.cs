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
using InstagramApiSharp;
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
    internal class Live : IDemoSample
    {
        private readonly IInstaApi InstaApi;

        public Live(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        public async Task DoShow()
        {
            // get currently logged in user
            var currentUser = await InstaApi.GetCurrentUserAsync();
            Console.WriteLine(
                $"Logged in: username - {currentUser.Value.UserName}, full name - {currentUser.Value.FullName}");

            Console.WriteLine("See Samples/Live.cs to see how it's works");
            Console.WriteLine("Live functions: ");
            Console.WriteLine(@"GetHeartBeatAndViewerCountAsync
GetFinalViewerListAsync
GetSuggestedBroadcastsAsync
GetDiscoverTopLiveAsync
GetTopLiveStatusAsync
GetInfoAsync
GetViewerListAsync
GetPostLiveViewerListAsync
CommentAsync
PinCommentAsync
UnPinCommentAsync
GetCommentsAsync
EnableCommentsAsync
DisableCommentsAsync
LikeAsync
GetLikeCountAsync
AddToPostLiveAsync
DeletePostLiveAsync
CreateAsync
StartAsync
EndAsync");
        }

        public async void SuggestedBroadcasts()
        {
            var result = await InstaApi.LiveProcessor.GetSuggestedBroadcastsAsync();
            if (result.Succeeded)
            {
                Console.WriteLine("Suggested broadcast count: " + result.Value?.Count);
                if (result.Value?.Count > 0)
                    Console.WriteLine("First suggested broadcast message: " + result.Value?.FirstOrDefault()?.BroadcastMessage);
            }
            else
                Console.WriteLine("Error while suggested broadcasts: " + result.Info.Message);
        }

        public async void DiscoverTopLive()
        {
            var result = await InstaApi.LiveProcessor.GetDiscoverTopLiveAsync(PaginationParameters.MaxPagesToLoad(1));
            if (result.Succeeded)
            {
                Console.WriteLine("Discover top lives count: " + result.Value.Broadcasts?.Count);
                if (result.Value.Broadcasts?.Count > 0)
                    Console.WriteLine("First discover top live broadcast message: " + result.Value.Broadcasts?.FirstOrDefault()?.BroadcastMessage);
            }
            else
                Console.WriteLine("Error while discover top lives: " + result.Info.Message);
        }

        public async void TopLiveStatus()
        {
            var result = await InstaApi.LiveProcessor.GetTopLiveStatusAsync("broadcastsID1", "broadcastID2");
            if (result.Succeeded)
            {
                Console.WriteLine("Discover top lives count: " + result.Value?.Count);
                if (result.Value?.Count > 0)
                    Console.WriteLine("First top live broadcast status: " + result.Value?.FirstOrDefault()?.BroadcastStatus);
            }
            else
                Console.WriteLine("Error while top live status: " + result.Info.Message);
        }

        public async void BroadcastInfo()
        {
            var result = await InstaApi.LiveProcessor.GetInfoAsync("broadcastID");
            if (result.Succeeded)
            {
                Console.WriteLine($"Broadcast info for {result.Value.Id}");

                Console.WriteLine("BroadcastMessage: " + result.Value.BroadcastMessage);
                Console.WriteLine("BroadcastOwner: " + result.Value.BroadcastOwner);
                Console.WriteLine("BroadcastStatus: " + result.Value.BroadcastStatus);
                Console.WriteLine("CoverFrameUrl: " + result.Value.CoverFrameUrl);
            }
            else
                Console.WriteLine("Error while Broadcast info: " + result.Info.Message);
        }

        public async void CommentBroadcast()
        {
            var commentText = "Ramtin your good! keep it up!";
            var result = await InstaApi.LiveProcessor.CommentAsync("broadcastID", commentText);
            if (result.Succeeded)
            {
                Console.WriteLine($"Send new comment to broadcast");
                Console.WriteLine("CommentStatus: " + result.Value.Status);
                Console.WriteLine("CommentText: " + result.Value.Text);
                Console.WriteLine("CommentUser: " + result.Value.User.UserName);
            }
            else
                Console.WriteLine("Error while send new comment to broadcast: " + result.Info.Message);
        }

        public async void LikeBroadcast()
        {
            var likeCount = 6; // from 1 to 6
            var result = await InstaApi.LiveProcessor.LikeAsync("broadcastID", likeCount);
            if (result.Succeeded)
            {
                Console.WriteLine($"Like broadcast");
                Console.WriteLine("Likes: " + result.Value.Likes);
            }
            else
                Console.WriteLine("Error while like broadcast: " + result.Info.Message);
        }

        public async void StartLiveBroadcastAndOtherFunctions()
        {
            Console.WriteLine("Be aware some of this methods only works on your own broadcasts!!!!");
            // live broadcast
            // first you need to call CreateAsync
            var result = await InstaApi.LiveProcessor.CreateAsync(720, 1184, "My new live broadcast");
            if (result.Succeeded)
            {
                var broadcastId = result.Value.BroadcastId.ToString();
                // second you need to call StartAsync to instagram know you start filming!
                await InstaApi.LiveProcessor.StartAsync(broadcastId, true);
                Console.WriteLine("Broadcast " + result.Value.BroadcastId + " started");
                // use uploadurl to stream your video to instagram
                // note: I really don't know how RTMP server works, so there is no
                // code for streaming your video
                Console.WriteLine("UploadUrl: " + result.Value.UploadUrl);
                // if you know FFMPEG library, you can use this command:
                // -rtbufsize 256M -re -i YOURFILE -acodec libmp3lame -ar 44100 -b:a 128k -pix_fmt yuv420p -profile:v baseline -s 720x1280 -bufsize 6000k -vb 400k -maxrate 1500k -deinterlace -vcodec libx264 -preset veryfast -g 30 -r 30 -f flv UPLOADURL


                // get heart beat and viewer count (works if you are broadcast owner)
                await InstaApi.LiveProcessor.GetHeartBeatAndViewerCountAsync(broadcastId);


                // get viewer list
                await InstaApi.LiveProcessor.GetViewerListAsync(broadcastId);


                // get post live viewer list
                await InstaApi.LiveProcessor.GetPostLiveViewerListAsync(broadcastId, 10);


                // Pin comment from broadcast
                await InstaApi.LiveProcessor.PinCommentAsync(broadcastId, "commentID"); 
                // UnPin comment from broadcast
                await InstaApi.LiveProcessor.UnPinCommentAsync(broadcastId, "commentID");


                // get broadcast comments
                await InstaApi.LiveProcessor.GetCommentsAsync(broadcastId);


                // enable broadcast comments
                await InstaApi.LiveProcessor.EnableCommentsAsync(broadcastId);
                // disable broadcast comments
                await InstaApi.LiveProcessor.DisableCommentsAsync(broadcastId);


                // get broadcast likes count
                await InstaApi.LiveProcessor.GetLikeCountAsync(broadcastId, 0);


                // add broadcast to post live
                await InstaApi.LiveProcessor.AddToPostLiveAsync(broadcastId);
                // delete broadcast from post live
                await InstaApi.LiveProcessor.DeletePostLiveAsync(broadcastId);




                // end live broadcast
                await InstaApi.LiveProcessor.EndAsync(broadcastId);

                // after you ended your live broadcast, you should call this
                await InstaApi.LiveProcessor.GetFinalViewerListAsync(broadcastId);

            }
            else
                Console.WriteLine("Error while creating live broadcast: " + result.Info.Message);
        }
    }
}
