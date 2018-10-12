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
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Live api functions.
    /// </summary>
    public interface ILiveProcessor
    {
        /// <summary>
        /// Add an broadcast to post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastAddToPostLiveResponse>> AddToPostLiveAsync(string broadcastId);

        /// <summary>
        /// Post a new comment to broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="commentText">Comment text</param>
        /// <returns></returns>
        Task<IResult<InstaComment>> CommentAsync(string broadcastId, string commentText);

        // broadcast create, start, end
        /// <summary>
        /// Create live broadcast. After create an live broadcast you must call StartAsync.
        /// </summary>
        /// <param name="previewWidth">Preview width</param>
        /// <param name="previewHeight">Preview height</param>
        /// <param name="broadcastMessage">Broadcast start message</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastCreateResponse>> CreateAsync(int previewWidth = 720, int previewHeight = 1184, string broadcastMessage = "");

        /// <summary>
        /// Delete an broadcast from post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<bool>> DeletePostLiveAsync(string broadcastId);

        /// <summary>
        /// Disable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastCommentEnableDisableResponse>> DisableCommentsAsync(string broadcastId);

        /// <summary>
        /// Enable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastCommentEnableDisableResponse>> EnableCommentsAsync(string broadcastId);

        /// <summary>
        /// End live broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="endAfterCopyrightWarning">Copyright warning</param>
        /// <returns></returns>
        Task<IResult<bool>> EndAsync(string broadcastId, bool endAfterCopyrightWarning = false);

        /// <summary>
        /// Get broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="lastCommentTs">Last comment time stamp</param>
        /// <param name="commentsRequested">Comments requested count</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastCommentResponse>> GetCommentsAsync(string broadcastId, int lastCommentTs = 0, int commentsRequested = 4);

        /// <summary>
        /// Get discover top live.
        /// </summary>
        /// <returns></returns>
        Task<IResult<InstaDiscoverTopLiveResponse>> GetDiscoverTopLiveAsync();

        /// <summary>
        /// Get final viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastFinalViewerListResponse>> GetFinalViewerListAsync(string broadcastId);

        /// <summary>
        /// Get heart beat and viewer count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastLiveHeartBeatViewerCountResponse>> GetHeartBeatAndViewerCountAsync(string broadcastId);
        /// <summary>
        /// Get broadcast information.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<BroadcastInfoResponse>> GetInfoAsync(string broadcastId);

        /// <summary>
        ///     Get join requests to current live broadcast
        /// </summary>
        /// <param name="broadcastId">Broadcast</param>
        Task<IResult<InstaBroadcastFinalViewerListResponse>> GetJoinRequestsAsync(string broadcastId);

        /// <summary>
        /// Get broadcast like count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeTs">Like time stamp</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastLikeResponse>> GetLikeCountAsync(string broadcastId, int likeTs = 0);

        /// <summary>
        /// Get post live viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="maxId">Max id</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastViewerListResponse>> GetPostLiveViewerListAsync(string broadcastId, int? maxId = null);

        /// <summary>
        /// Get suggested broadcasts
        /// </summary>
        /// <returns></returns>
        Task<IResult<InstaBroadcastSuggestedResponse>> GetSuggestedBroadcastsAsync();
        /// <summary>
        /// Get top live status.
        /// </summary>
        /// <param name="broadcastIds">Broadcast ids</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastTopLiveStatusResponse>> GetTopLiveStatusAsync(params string[] broadcastIds);
        /// <summary>
        /// Get broadcast viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastViewerListResponse>> GetViewerListAsync(string broadcastId);
        /// <summary>
        /// Like broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeCount">Like count (from 1 to 6)</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastLikeResponse>> LikeAsync(string broadcastId, int likeCount = 1);

        /// <summary>
        /// Pin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastPinUnpinResponse>> PinCommentAsync(string broadcastId,string commentId);
        /// <summary>
        /// Start live broadcast. NOTE: YOU MUST CREATE AN BROADCAST FIRST(CreateAsync) AND THEN CALL THIS METHOD. 
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="sendNotifications">Send notifications</param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastStartResponse>> StartAsync(string broadcastId, bool sendNotifications);

        /// <summary>
        /// Unpin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastPinUnpinResponse>> UnPinCommentAsync(string broadcastId, string commentId);
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
