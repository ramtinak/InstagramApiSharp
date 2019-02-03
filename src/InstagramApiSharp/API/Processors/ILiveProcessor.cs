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
        ///     Add an broadcast to post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<InstaBroadcastAddToPostLive>> AddToPostLiveAsync(string broadcastId);

        /// <summary>
        ///     Post a new comment to broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="commentText">Comment text</param>
        Task<IResult<InstaComment>> CommentAsync(string broadcastId, string commentText);

        // broadcast create, start, end
        /// <summary>
        ///     Create live broadcast. After create an live broadcast you must call StartAsync.
        /// </summary>
        /// <param name="previewWidth">Preview width</param>
        /// <param name="previewHeight">Preview height</param>
        /// <param name="broadcastMessage">Broadcast start message</param>
        Task<IResult<InstaBroadcastCreate>> CreateAsync(int previewWidth = 720, int previewHeight = 1184, string broadcastMessage = "");

        /// <summary>
        ///     Delete an broadcast from post live.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<bool>> DeletePostLiveAsync(string broadcastId);

        /// <summary>
        ///     Disable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<InstaBroadcastCommentEnableDisable>> DisableCommentsAsync(string broadcastId);

        /// <summary>
        ///     Enable broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<InstaBroadcastCommentEnableDisable>> EnableCommentsAsync(string broadcastId);

        /// <summary>
        ///     End live broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="endAfterCopyrightWarning">Copyright warning</param>
        Task<IResult<bool>> EndAsync(string broadcastId, bool endAfterCopyrightWarning = false);

        /// <summary>
        ///     Get broadcast comments.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="lastCommentTs">Last comment time stamp</param>
        /// <param name="commentsRequested">Comments requested count</param>
        Task<IResult<InstaBroadcastCommentList>> GetCommentsAsync(string broadcastId, string lastCommentTs = "", int commentsRequested = 4);

        /// <summary>
        ///     Get discover top live.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaDiscoverTopLive>> GetDiscoverTopLiveAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get final viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<InstaUserShortList>> GetFinalViewerListAsync(string broadcastId);

        /// <summary>
        ///     Get heart beat and viewer count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<InstaBroadcastLiveHeartBeatViewerCount>> GetHeartBeatAndViewerCountAsync(string broadcastId);
        
        /// <summary>
        ///     Get broadcast information.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<InstaBroadcastInfo>> GetInfoAsync(string broadcastId);

        /// <summary>
        ///     Get join requests to current live broadcast
        /// </summary>
        /// <param name="broadcastId">Broadcast</param>
        Task<IResult<InstaUserShortList>> GetJoinRequestsAsync(string broadcastId);

        /// <summary>
        ///     Get broadcast like count.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeTs">Like time stamp</param>
        Task<IResult<InstaBroadcastLike>> GetLikeCountAsync(string broadcastId, int likeTs = 0);

        /// <summary>
        ///     Get post live viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="maxId">Max id</param>
        Task<IResult<InstaUserShortList>> GetPostLiveViewerListAsync(string broadcastId, int? maxId = null);

        /// <summary>
        ///     Get suggested broadcasts
        /// </summary>
        Task<IResult<InstaBroadcastList>> GetSuggestedBroadcastsAsync();
        /// <summary>
        ///     Get top live status.
        /// </summary>
        /// <param name="broadcastIds">Broadcast ids</param>
        Task<IResult<InstaBroadcastTopLiveStatusList>> GetTopLiveStatusAsync(params string[] broadcastIds);
        /// <summary>
        ///     Get broadcast viewer list.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        Task<IResult<InstaUserShortList>> GetViewerListAsync(string broadcastId);
        /// <summary>
        ///     Like broadcast.
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="likeCount">Like count (from 1 to 6)</param>
        Task<IResult<InstaBroadcastLike>> LikeAsync(string broadcastId, int likeCount = 1);

        /// <summary>
        ///     Pin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<IResult<InstaBroadcastPinUnpin>> PinCommentAsync(string broadcastId,string commentId);
        /// <summary>
        ///     Start live broadcast. NOTE: YOU MUST CREATE AN BROADCAST FIRST(CreateAsync) AND THEN CALL THIS METHOD. 
        /// </summary>
        /// <param name="broadcastId">Broadcast id</param>
        /// <param name="sendNotifications">Send notifications</param>
        Task<IResult<InstaBroadcastStart>> StartAsync(string broadcastId, bool sendNotifications);

        /// <summary>
        ///     Share an live broadcast to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="broadcastId">Broadcast id to send ( <see cref="InstaBroadcast.Id"/> )</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> ShareLiveToDirectThreadAsync(string text, string broadcastId, params string[] threadIds);

        /// <summary>
        ///     Share an live broadcast to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="broadcastId">Broadcast id to send ( <see cref="InstaBroadcast.Id"/> )</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="recipients">Recipients ids</param>
        Task<IResult<bool>> ShareLiveToDirectThreadAsync(string text, string broadcastId, string[] threadIds, string[] recipients);

        /// <summary>
        ///     Share an live broadcast to direct recipients
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="broadcastId">Broadcast id to send ( <see cref="InstaBroadcast.Id"/> )</param>
        /// <param name="recipients">Recipients ids</param>
        Task<IResult<bool>> ShareLiveToDirectRecipientAsync(string text, string broadcastId, params string[] recipients);

        /// <summary>
        ///     Unpin comment from broadcast.
        /// </summary>
        /// <param name="broadcastId"></param>
        /// <param name="commentId"></param>
        Task<IResult<InstaBroadcastPinUnpin>> UnPinCommentAsync(string broadcastId, string commentId);
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
