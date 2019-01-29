using System;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Messaging (direct) api functions.
    /// </summary>
    public interface IMessagingProcessor
    {
        /// <summary>
        ///     Add users to group thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="userIds">User ids (pk)</param>
        Task<IResult<InstaDirectInboxThread>> AddUserToGroupThreadAsync(string threadId, params long[] userIds);
        /// <summary>
        ///     Approve direct pending request
        /// </summary>
        /// <param name="threadId">Thread ids</param>
        Task<IResult<bool>> ApproveDirectPendingRequestAsync(params string[] threadIds);

        /// <summary>
        ///     Decline all direct pending requests
        /// </summary>
        Task<IResult<bool>> DeclineAllDirectPendingRequestsAsync();

        /// <summary>
        ///     Decline direct pending requests
        /// </summary>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> DeclineDirectPendingRequestsAsync(params string[] threadIds);

        /// <summary>
        ///     Delete direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> DeleteDirectThreadAsync(string threadId);

        /// <summary>
        ///     Delete self message in direct
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> DeleteSelfMessageAsync(string threadId, string itemId);

        /// <summary>
        ///     Get direct inbox threads for current user asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaDirectInboxContainer" />
        /// </returns>
        Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync(PaginationParameters paginationParameters);
        /// <summary>
        ///     Get direct inbox thread by its id asynchronously
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        Task<IResult<InstaDirectInboxThread>> GetDirectInboxThreadAsync(string threadId, PaginationParameters paginationParameters);
        /// <summary>
        ///     Get direct pending inbox threads for current user asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaDirectInboxContainer" />
        /// </returns>
        Task<IResult<InstaDirectInboxContainer>> GetPendingDirectAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get ranked recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRankedRecipientsAsync();

        /// <summary>
        ///     Get ranked recipients (threads and users) asynchronously
        ///     <para>Note: Some recipient has User, some recipient has Thread</para>
        /// </summary>
        /// <param name="username">Username to search</param>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRankedRecipientsByUsernameAsync(string username);

        /// <summary>
        ///     Get recent recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRecentRecipientsAsync();

        /// <summary>
        ///     Get direct users presence
        ///     <para>Note: You can use this function to find out who is online and who isn't.</para>
        /// </summary>
        Task<IResult<InstaUserPresenceList>> GetUsersPresenceAsync();

        /// <summary>
        ///     Leave from group thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> LeaveGroupThreadAsync(string threadId);

        /// <summary>
        ///     Like direct message in a thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="itemId">Item id (message id)</param>
        Task<IResult<bool>> LikeThreadMessageAsync(string threadId, string itemId);

        /// <summary>
        ///     Mark direct message as seen
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="itemId">Message id (item id)</param>
        Task<IResult<bool>> MarkDirectThreadAsSeenAsync(string threadId, string itemId);

        /// <summary>
        ///     Mute direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> MuteDirectThreadAsync(string threadId);

        /// <summary>
        ///     Send disappearing photo to direct thread (video will remove after user saw it)
        /// </summary>
        /// <param name="image">Image to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> SendDirectDisappearingPhotoAsync(InstaImage image,
     InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds);

        /// <summary>
        ///     Send disappearing photo to direct thread (video will remove after user saw it) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Image to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> SendDirectDisappearingPhotoAsync(Action<InstaUploaderProgress> progress, InstaImage image,
     InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds);

        /// <summary>
        ///     Send disappearing video to direct thread (video will remove after user saw it)
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        /// <returns></returns>
        Task<IResult<bool>> SendDirectDisappearingVideoAsync(InstaVideoUpload video,
       InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds);

        /// <summary>
        ///     Send disappearing video to direct thread (video will remove after user saw it) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="viewMode">View mode</param>
        /// <param name="threadIds">Thread ids</param>
        /// <returns></returns>
        Task<IResult<bool>> SendDirectDisappearingVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video,
       InstaViewMode viewMode = InstaViewMode.Replayable, params string[] threadIds);

        /// <summary>
        ///     Send hashtag to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="hashtag">Hashtag to send</param>
        /// <param name="threadIds">Thread ids</param>
        /// <returns>Returns True if hashtag sent</returns>
        Task<IResult<bool>> SendDirectHashtagAsync(string text, string hashtag, params string[] threadIds);

        /// <summary>
        ///     Send hashtag to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="hashtag">Hashtag to send</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="recipients">Recipients ids</param>
        /// <returns>Returns True if hashtag sent</returns>
        Task<IResult<bool>> SendDirectHashtagAsync(string text, string hashtag, string[] threadIds, string[] recipients);

        /// <summary>
        ///     Send hashtag to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="hashtag">Hashtag to send</param>
        /// <param name="recipients">Recipients ids</param>
        /// <returns>Returns True if hashtag sent</returns>
        Task<IResult<bool>> SendDirectHashtagToRecipientsAsync(string text, string hashtag, params string[] recipients);

        /// <summary>
        ///     Send link address to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="link">Link to send (only one link will approved)</param>
        /// <param name="threadIds">Thread ids</param>
        /// <returns>Returns True if link sent</returns>
        Task<IResult<bool>> SendDirectLinkAsync(string text, string link, params string[] threadIds);
        
        /// <summary>
        ///     Send link address to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="link">Link to send (only one link will approved)</param>
        /// <param name="threadIds">Thread ids</param>
        /// <param name="recipients">Recipients ids</param>
        /// <returns>Returns True if link sent</returns>
        Task<IResult<bool>> SendDirectLinkAsync(string text, string link, string[] threadIds, string[] recipients);

        /// <summary>
        ///     Send link address to direct thread
        /// </summary>
        /// <param name="text">Text to send</param>
        /// <param name="link">Link to send (only one link will approved)</param>
        /// <param name="recipients">Recipients ids</param>
        /// <returns>Returns True if link sent</returns>
        Task<IResult<bool>> SendDirectLinkToRecipientsAsync(string text, string link, params string[] recipients);

        /// <summary>
        ///     Send location to direct thread
        /// </summary>
        /// <param name="externalId">External id (get it from <seealso cref="LocationProcessor.SearchLocationAsync"/></param>
        /// <param name="threadIds">Thread ids</param>
        /// <returns>Returns True if location sent</returns>
        Task<IResult<bool>> SendDirectLocationAsync(string externalId, params string[] threadIds);

        /// <summary>
        ///     Send photo to direct thread (single) with progress
        /// </summary>
        /// <param name="image">Image to upload</param>
        /// <param name="threadId">Thread id</param>
        /// <returns>Returns True is sent</returns>
        Task<IResult<bool>> SendDirectPhotoAsync(InstaImage image, string threadId);

        /// <summary>
        ///     Send photo to direct thread (single)
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Image to upload</param>
        /// <param name="threadId">Thread id</param>
        /// <returns>Returns True is sent</returns>
        Task<IResult<bool>> SendDirectPhotoAsync(Action<InstaUploaderProgress> progress, InstaImage image, string threadId);

        /// <summary>
        ///     Send photo to multiple recipients (multiple user)
        /// </summary>
        /// <param name="image">Image to upload</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        /// <returns>Returns True is sent</returns>
        Task<IResult<bool>> SendDirectPhotoToRecipientsAsync(InstaImage image, params string[] recipients);

        /// <summary>
        ///     Send photo to multiple recipients (multiple user) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Image to upload</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        /// <returns>Returns True is sent</returns>
        Task<IResult<bool>> SendDirectPhotoToRecipientsAsync(Action<InstaUploaderProgress> progress, InstaImage image, params string[] recipients);

        /// <summary>
        ///     Send profile to direct thread
        /// </summary>
        /// <param name="userIdToSend">User id to send</param>
        /// <param name="threadIds">Thread ids</param>
        /// <returns>Returns True if profile sent</returns>
        Task<IResult<bool>> SendDirectProfileAsync(long userIdToSend, params string[] threadIds);

        /// <summary>
        ///     Send profile to direct thrad
        /// </summary>
        /// <param name="userIdToSend">User id to send</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        Task<IResult<bool>> SendDirectProfileToRecipientsAsync(long userIdToSend, string recipients);

        /// <summary>
        ///     Send direct text message to provided users and threads
        /// </summary>
        /// <param name="recipients">Comma-separated users PK</param>
        /// <param name="threadIds">Message thread ids</param>
        /// <param name="text">Message text</param>
        /// <returns>List of threads</returns>
        Task<IResult<InstaDirectInboxThreadList>> SendDirectTextAsync(string recipients, string threadIds,
            string text);

        /// <summary>
        ///     Send video to direct thread (single)
        /// </summary>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> SendDirectVideoAsync(InstaVideoUpload video, string threadId);

        /// <summary>
        ///     Send video to direct thread (single) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> SendDirectVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, string threadId);

        /// <summary>
        ///     Send video to multiple recipients (multiple user)
        /// </summary>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        Task<IResult<bool>> SendDirectVideoToRecipientsAsync(InstaVideoUpload video, params string[] recipients);

        /// <summary>
        ///     Send video to multiple recipients (multiple user) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload (no need to set thumbnail)</param>
        /// <param name="recipients">Recipients (user ids/pk)</param>
        Task<IResult<bool>> SendDirectVideoToRecipientsAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, params string[] recipients);

        /// <summary>
        ///     Share media to direct thread
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="mediaType">Media type</param>
        /// <param name="text">Text to send</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> ShareMediaToThreadAsync(string mediaId, InstaMediaType mediaType, string text, params string[] threadIds);

        /// <summary>
        ///     Share media to user id
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="mediaType">Media type</param>
        /// <param name="text">Text to send</param>
        /// <param name="userIds">User ids (pk)</param>
        Task<IResult<bool>> ShareMediaToUserAsync(string mediaId, InstaMediaType mediaType, string text, params long[] userIds);

        [Obsolete("ShareUserAsync is deprecated. Use SendDirectProfileAsync instead.")]
        /// <summary>
        ///     Share an user
        /// </summary>
        /// <param name="userIdToSend">User id(PK)</param>
        /// <param name="threadId">Thread id</param>
        Task<IResult<InstaSharing>> ShareUserAsync(string userIdToSend, string threadId);

        /// <summary>
        ///     UnLike direct message in a thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="itemId">Item id (message id)</param>
        Task<IResult<bool>> UnLikeThreadMessageAsync(string threadId, string itemId);
        /// <summary>
        ///     Unmute direct thread
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> UnMuteDirectThreadAsync(string threadId);

        /// <summary>
        ///     Update direct thread title (for groups)
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <param name="title">New title</param>
        Task<IResult<bool>> UpdateDirectThreadTitleAsync(string threadId, string title);
        
        /// <summary>
        ///     Send a like to the conversation
        /// </summary>
        /// <param name="threadId">Thread id</param>
        Task<IResult<bool>> SendDirectLikeAsync(string threadId);
    }
}