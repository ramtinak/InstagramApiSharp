/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    public interface ILiveProcessor
    {
        /// <summary>
        /// Get heart beat and viewer count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastLiveHeartBeatViewerCountResponse>> GetHeartBeatAndViewerCountAsync(string broadcastId);
        /// <summary>
        /// Get final viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastFinalViewerListResponse>> GetFinalViewerListAsync(string broadcastId);
        /// <summary>
        /// Get suggested broadcasts
        /// </summary>
        /// <returns></returns>
        Task<IResult<BroadcastSuggestedResponse>> GetSuggestedBroadcastsAsync();
        /// <summary>
        /// Get discover top live.
        /// </summary>
        /// <returns></returns>
        Task<IResult<DiscoverTopLiveResponse>> GetDiscoverTopLiveAsync();
        /// <summary>
        /// Get top live status.
        /// </summary>
        /// <param name="broadcastIds">Broadcast ids</param>
        /// <returns></returns>
        Task<IResult<BroadcastTopLiveStatusResponse>> GetTopLiveStatusAsync(params string[] broadcastIds);
        /// <summary>
        /// Get broadcast information.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastInfoResponse>> GetInfoAsync(string broadcastId);
        /// <summary>
        /// Get broadcast viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastViewerListResponse>> GetViewerListAsync(string broadcastId);
        /// <summary>
        /// Get post live viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="maxId">Max id</param>
        /// <returns></returns>
        Task<IResult<BroadcastViewerListResponse>> GetPostLiveViewerListAsync(string broadcastId, int? maxId = null);
        /// <summary>
        /// Post a new comment to broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="commentText">Comment text</param>
        /// <returns></returns>
        Task<IResult<InstaComment>> CommentAsync(string broadcastId, string commentText);
        /// <summary>
        /// Pin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<IResult<BroadcastPinUnpinResponse>> PinCommentAsync(string broadcastId,string commentId);
        /// <summary>
        /// Unpin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<IResult<BroadcastPinUnpinResponse>> UnPinCommentAsync(string broadcastId, string commentId);
        /// <summary>
        /// Get broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="lastCommentTs">Last comment time stamp</param>
        /// <param name="commentsRequested">Comments requested count</param>
        /// <returns></returns>
        Task<IResult<BroadcastCommentResponse>> GetCommentsAsync(string broadcastId, int lastCommentTs = 0, int commentsRequested = 4);
        /// <summary>
        /// Enable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastCommentEnableDisableResponse>> EnableCommentsAsync(string broadcastId);
        /// <summary>
        /// Disable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastCommentEnableDisableResponse>> DisableCommentsAsync(string broadcastId);
        /// <summary>
        /// Like broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeCount">Like count (from 1 to 6)</param>
        /// <returns></returns>
        Task<IResult<BroadcastLikeResponse>> LikeAsync(string broadcastId,int likeCount = 1);
        /// <summary>
        /// Get broadcast like count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeTs">Like time stamp</param>
        /// <returns></returns>
        Task<IResult<BroadcastLikeResponse>> GetLikeCountAsync(string broadcastId, int likeTs = 0);
        /// <summary>
        /// Add an broadcast to post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastAddToPostLiveResponse>> AddToPostLiveAsync(string broadcastId);
        /// <summary>
        /// Delete an broadcast from post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<bool>> DeletePostLiveAsync(string broadcastId);
        /// <summary>
        ///     Get join requests to current live broadcast
        /// </summary>
        /// <param name="broadcastId">Broadcast</param>
        Task<IResult<BroadcastFinalViewerListResponse>> GetJoinRequestsAsync(string broadcastId);



        // broadcast create, start, end
        /// <summary>
        /// Create live broadcast. After create an live broadcast you must call StartAsync.
        /// </summary>
        /// <param name="previewWidth">Preview width</param>
        /// <param name="previewHeight">Preview height</param>
        /// <param name="broadcastMessage">Broadcast start message</param>
        /// <returns></returns>
        Task<IResult<BroadcastCreateResponse>> CreateAsync(int previewWidth = 720, int previewHeight = 1184, string broadcastMessage = "");
        /// <summary>
        /// Start live broadcast. NOTE: YOU MUST CREATE AN BROADCAST FIRST(CreateAsync) AND THEN CALL THIS METHOD. 
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="sendNotifications">Send notifications</param>
        /// <returns></returns>
        Task<IResult<BroadcastStartResponse>> StartAsync(string broadcastId, bool sendNotifications);
        /// <summary>
        /// End live broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="endAfterCopyrightWarning">Copyright warning</param>
        /// <returns></returns>
        Task<IResult<bool>> EndAsync(string broadcastId, bool endAfterCopyrightWarning = false);

        
        /*
        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        Task<IResult<object>> GetPostLiveLikesAsync(string broadcastId, int startingOffset = 0, string encodingTag = "instagram_dash_remuxed");
        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        Task<IResult<object>> GetPostLiveCommentsAsync(string broadcastId, int startingOffset = 0, string encodingTag = "instagram_dash_remuxed");
        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        Task<IResult<object>> NotifyToFriendsAsync();
        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        Task<IResult<object>> SeenBroadcastAsync(string broadcastId, string pk);*/
    }
}
