using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using InstagramApiSharp.API.Processors;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.API
{
    public interface IInstaApi
    {
        #region Properties

        /// <summary>
        ///     Indicates whether user authenticated or not
        /// </summary>
        bool IsUserAuthenticated { get; }

        #endregion
        /// <summary>
        ///     Live api functions
        /// </summary>
        ILiveProcessor LiveProcessor { get; }
        /// <summary>
        ///     Discover api functions.
        /// </summary>
        IDiscoverProcessor DiscoverProcessor { get; }
        /// <summary>
        ///     Account api functions.
        /// </summary>
        IAccountProcessor AccountProcessor { get; }
        /// <summary>
        ///     Story api functions.
        /// </summary>
        IStoryProcessor StoryProcessor { get; }
        /// <summary>
        ///     Media api functions.
        /// </summary>
        IMediaProcessor MediaProcessor { get; }
        /// <summary>
        ///     Comments api functions.
        /// </summary>
        ICommentProcessor CommentProcessor { get; }
        /// <summary>
        ///     Messaging (direct) api functions.
        /// </summary>
        IMessagingProcessor MessagingProcessor { get; }
        /// <summary>
        ///     Feed api functions.
        /// </summary>
        IFeedProcessor FeedProcessor { get; }
        /// <summary>
        ///     Collection api functions.
        /// </summary>
        ICollectionProcessor CollectionProcessor { get; }
        /// <summary>
        ///     Location api functions.
        /// </summary>
        ILocationProcessor LocationProcessor { get; }
        /// <summary>
        ///     Hashtag api functions.
        /// </summary>
        IHashtagProcessor HashtagProcessor { get; }
        /// <summary>
        ///     User api functions.
        /// </summary>
        IUserProcessor UserProcessor { get; }
        UserSessionData GetLoggedUser();
        /// <summary>
        ///     Get current state info as Memory stream
        /// </summary>
        /// <returns>State data</returns>
        Stream GetStateDataAsStream();
        /// <summary>
        ///     Get current state info as Json string
        /// </summary>
        /// <returns>State data</returns>
        string GetStateDataAsString();
        /// <summary>
        ///     Get challenge login information for grabbing challenge url.
        /// </summary>
        /// <returns></returns>
        //InstaChallengeLoginInfo GetChallenge();
        /// <summary>
        ///     Get challenge require(checkpoint required) options.
        /// </summary>
        /// <returns></returns>
        Task<IResult<ChallengeRequireVerifyMethod>> GetChallengeRequireVerifyMethodAsync();
        Task<IResult<ChallengeRequireVerifyMethod>> ResetChallengeRequireVerifyMethodAsync();
        Task<IResult<ChallengeRequireSMSVerify>> RequestVerifyCodeToSMSForChallengeRequireAsync();
        Task<IResult<ChallengeRequireEmailVerify>> RequestVerifyCodeToEmailForChallengeRequireAsync();
        Task<IResult<ChallengeRequireVerifyCode>> VerifyCodeForChallengeRequireAsync(string verifyCode);
        /// <summary>
        ///     Set cookie and html document to verify login information.
        /// </summary>
        /// <param name="htmlDocument">Html document source</param>
        /// <param name="cookies">Cookies from webview or webbrowser control</param>
        /// <returns>True if logged in, False if not</returns>
        Task<IResult<bool>> SetCookiesAndHtmlForFbLoginAndChallenge(string htmlDocument, string cookies ,bool validate = false);
        /// <summary>
        ///     Set cookie and web browser response object to verify login information.
        /// </summary>
        /// <param name="webBrowserResponse">Web browser response object</param>
        /// <param name="cookies">Cookies from webview or webbrowser control</param>
        /// <returns>True if logged in, False if not</returns>
        Task<IResult<bool>> SetCookiesAndHtmlForFbLoginAndChallenge(WebBrowserResponse webBrowserResponse, string cookies, bool validate = false);
        /// <summary>
        ///     Set state data from provided stream
        /// </summary>
        void LoadStateDataFromStream(Stream data);
        /// <summary>
        ///     Set state data from provided json string
        /// </summary>
        void LoadStateDataFromString(string data);

        #region Async Members
        Task<IResult<InstaFriendshipStatus>> AcceptFriendshipRequest(long UserID);
        Task<IResult<InstaFriendshipStatus>> IgnoreFriendshipRequest(long UserID);
        Task<IResult<InstaPendingRequest>> GetPendingFriendRequests();
        /// <summary>
        ///     Create a new instagram account
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="firstName">First name (optional)</param>
        /// <returns></returns>
        Task<IResult<CreationResponse>> CreateNewAccount(string username, string password, string email, string firstName);
        /// <summary>
        ///     Share an user
        /// </summary>
        /// <param name="userIdToSend">User id(PK)</param>
        /// <param name="threadId">Thread id</param>
        /// <returns></returns>
        Task<IResult<InstaSharing>> ShareUserAsync(string userIdToSend, string threadId);
        
        /// <summary>
        ///     Login using given credentials asynchronously
        /// </summary>
        /// <param name="isNewLogin"></param>
        /// <returns>
        ///     Success --> is succeed
        ///     TwoFactorRequired --> requires 2FA login.
        ///     BadPassword --> Password is wrong
        ///     InvalidUser --> User/phone number is wrong
        ///     Exception --> Something wrong happened
        /// </returns>
        Task<IResult<InstaLoginResult>> LoginAsync(bool isNewLogin = true);

        /// <summary>
        ///     2-Factor Authentication Login using a verification code
        ///     Before call this method, please run LoginAsync first.
        /// </summary>
        /// <param name="verificationCode">Verification Code sent to your phone number</param>
        /// <returns>
        ///     Success --> is succeed
        ///     InvalidCode --> The code is invalid
        ///     CodeExpired --> The code is expired, please request a new one.
        ///     Exception --> Something wrong happened
        /// </returns>
        Task<IResult<InstaLoginTwoFactorResult>> TwoFactorLoginAsync(string verificationCode);

        /// <summary>
        ///     Get Two Factor Authentication details
        /// </summary>
        /// <returns>
        ///     An instance of TwoFactorLoginInfo if success.
        ///     A null reference if not success; in this case, do LoginAsync first and check if Two Factor Authentication is
        ///     required, if not, don't run this method
        /// </returns>
        Task<IResult<TwoFactorLoginInfo>> GetTwoFactorInfoAsync();

        /// <summary>
        ///     Logout from instagram asynchronously
        /// </summary>
        /// <returns>True if logged out without errors</returns>
        Task<IResult<bool>> LogoutAsync();
        [Obsolete("GetUserTimelineFeedAsync is deprecated, please use FeedProcessor.GetUserTimelineFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get user timeline feed (feed of recent posts from users you follow) asynchronously.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(PaginationParameters paginationParameters);
        [Obsolete("GetExploreFeedAsync is deprecated, please use FeedProcessor.GetExploreFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get user explore feed (Explore tab info) asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns><see cref="InstaExploreFeed" />></returns>
        Task<IResult<InstaExploreFeed>> GetExploreFeedAsync(PaginationParameters paginationParameters);
        [Obsolete("GetUserMediaAsync is deprecated, please use UserProcessor.GetUserMediaAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get all user media by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, PaginationParameters paginationParameters);
        [Obsolete("GetMediaByIdAsync is deprecated, please use MediaProcessor.GetMediaByIdAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get media by its id asynchronously
        /// </summary>
        /// <param name="mediaId">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId);
        [Obsolete("GetUserAsync is deprecated, please use UserProcessor.GetUserAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get user info by its user name asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        Task<IResult<InstaUser>> GetUserAsync(string username);
        /// <summary>
        ///     Get currently logged in user info asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaCurrentUser" />
        /// </returns>
        Task<IResult<InstaCurrentUser>> GetCurrentUserAsync();
        [Obsolete("GetTagFeedAsync is deprecated, please use FeedProcessor.GetTagFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get tag feed by tag value asynchronously
        /// </summary>
        /// <param name="tag">Tag value</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaTagFeed" />
        /// </returns>
        Task<IResult<InstaTagFeed>> GetTagFeedAsync(string tag, PaginationParameters paginationParameters);
        [Obsolete("GetUserFollowersAsync is deprecated, please use UserProcessor.GetUserFollowersAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get followers list by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followers</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetUserFollowersAsync(string username,
            PaginationParameters paginationParameters, string searchQuery = "");
        [Obsolete("GetUserFollowingAsync is deprecated, please use UserProcessor.GetUserFollowingAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get following list by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followings</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetUserFollowingAsync(string username,
            PaginationParameters paginationParameters, string searchQuery = "");
        [Obsolete("GetCurrentUserFollowersAsync is deprecated, please use UserProcessor.GetCurrentUserFollowersAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get followers list for currently logged in user asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetCurrentUserFollowersAsync(PaginationParameters paginationParameters);
        [Obsolete("GetUserTagsAsync is deprecated, please use UserProcessor.GetUserTagsAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get user tags by username asynchronously
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserTagsAsync(string username, PaginationParameters paginationParameters);
        [Obsolete("GetDirectInboxAsync is deprecated, please use MessagingProcessor.GetDirectInboxAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get direct inbox threads for current user asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaDirectInboxContainer" />
        /// </returns>
        Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync(PaginationParameters paginationParameters);

        [Obsolete("GetDirectInboxAsync is deprecated, please use MessagingProcessor.GetDirectInboxAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get direct inbox thread by its id asynchronously
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        Task<IResult<InstaDirectInboxThread>> GetDirectInboxThreadAsync(string threadId, PaginationParameters paginationParameters);

        [Obsolete("SendDirectMessage is deprecated, please use MessagingProcessor.SendDirectMessage instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Send direct message to provided users and threads
        /// </summary>
        /// <param name="recipients">Comma-separated users PK</param>
        /// <param name="threadIds">Message thread ids</param>
        /// <param name="text">Message text</param>
        /// <returns>List of threads</returns>
        Task<IResult<InstaDirectInboxThreadList>> SendDirectMessage(string recipients, string threadIds, string text);

        [Obsolete("GetRecentRecipientsAsync is deprecated, please use MessagingProcessor.GetRecentRecipientsAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get recent recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRecentRecipientsAsync();

        [Obsolete("GetRankedRecipientsAsync is deprecated, please use MessagingProcessor.GetRankedRecipientsAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get ranked recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRankedRecipientsAsync();
        [Obsolete("GetRecentActivityAsync is deprecated, please use FeedProcessor.GetRecentActivityFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get recent activity info asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetRecentActivityAsync(PaginationParameters paginationParameters);
        [Obsolete("GetFollowingRecentActivityAsync is deprecated, please use FeedProcessor.GetFollowingRecentActivityFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get activity of following asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityAsync(PaginationParameters paginationParameters);
        [Obsolete("LikeMediaAsync is deprecated, please use MediaProcessor.LikeMediaAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Like media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> LikeMediaAsync(string mediaId);
        [Obsolete("UnLikeMediaAsync is deprecated, please use MediaProcessor.UnLikeMediaAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Remove like from media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> UnLikeMediaAsync(string mediaId);
        [Obsolete("FollowUserAsync is deprecated, please use UserProcessor.FollowUserAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Follow user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipStatus>> FollowUserAsync(long userId);
        [Obsolete("UnFollowUserAsync is deprecated, please use UserProcessor.UnFollowUserAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Stop follow user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipStatus>> UnFollowUserAsync(long userId);
        [Obsolete("BlockUserAsync is deprecated, please use UserProcessor.BlockUserAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Block user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipStatus>> BlockUserAsync(long userId);
        [Obsolete("UnBlockUserAsync is deprecated, please use UserProcessor.UnBlockUserAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Stop block user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipStatus>> UnBlockUserAsync(long userId);
        [Obsolete("GetMediaCommentsAsync is deprecated, please use CommentProcessor.GetMediaCommentsAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaCommentList>> GetMediaCommentsAsync(string mediaId, PaginationParameters paginationParameters);
        [Obsolete("EnableMediaCommentAsync is deprecated, please use CommentProcessor.EnableMediaCommentAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Allow media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> EnableMediaCommentAsync(string mediaId);
        [Obsolete("DisableMediaCommentAsync is deprecated, please use CommentProcessor.DisableMediaCommentAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Disable media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> DisableMediaCommentAsync(string mediaId);
        [Obsolete("GetMediaInlineCommentsAsync is deprecated, please use CommentProcessor.GetMediaInlineCommentsAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get media inline comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="targetCommentId">Target comment id</param>
        /// <param name="paginationParameters">Maximum amount of pages to load and start id</param>
        /// <returns></returns>
        Task<IResult<InstaInlineCommentListResponse>> GetMediaInlineCommentsAsync(string mediaId, string targetCommentId,
            PaginationParameters paginationParameters);
        [Obsolete("GetMediaLikersAsync is deprecated, please use MediaProcessor.GetMediaLikersAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get users (short) who liked certain media. Normaly it return around 1000 last users.
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<InstaLikersList>> GetMediaLikersAsync(string mediaId);
        [Obsolete("SetAccountPrivateAsync is deprecated, please use AccountProcessor.SetAccountPrivateAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Set current account private
        /// </summary>
        Task<IResult<InstaUserShort>> SetAccountPrivateAsync();
        [Obsolete("SetAccountPublicAsync is deprecated, please use AccountProcessor.SetAccountPublicAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Set current account public
        /// </summary>
        Task<IResult<InstaUserShort>> SetAccountPublicAsync();
        [Obsolete("CommentMediaAsync is deprecated, please use CommentProcessor.CommentMediaAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="text">Comment text</param>
        Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text);
        [Obsolete("GetMediaCommentLikersAsync is deprecated, please use CommentProcessor.GetMediaCommentLikersAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get media comments likers
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> GetMediaCommentLikersAsync(string mediaId);
        /// <summary>
        ///     Report media comment
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        //Task<IResult<bool>> ReportCommentAsync(string mediaId, string commentId);
        [Obsolete("InlineCommentMediaAsync is deprecated, please use CommentProcessor.InlineCommentMediaAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Inline comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="targetCommentId">Target comment id</param>
        /// <param name="text">Comment text</param>
        /// <returns></returns>
        Task<IResult<InstaComment>> InlineCommentMediaAsync(string mediaId, string targetCommentId, string text);
        [Obsolete("DeleteCommentAsync is deprecated, please use CommentProcessor.DeleteCommentAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Delete comment from media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId);

        [Obsolete("UploadVideoAsync is deprecated, please use MediaProcessor.UploadVideoAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Upload video
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="imageThumbnail">Image thumbnail</param>
        /// <param name="caption">Caption</param>
        /// <returns></returns>
        Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideo video, InstaImage imageThumbnail, string caption);
        [Obsolete("UploadPhotoAsync is deprecated, please use MediaProcessor.UploadPhotoAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Upload photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImage image, string caption);
        [Obsolete("UploadPhotosAlbumAsync is deprecated, please use MediaProcessor.UploadPhotosAlbumAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Upload photo
        /// </summary>
        /// <param name="images">Array of photos to upload</param>
        /// <param name="caption">Caption</param>
        /// <returns></returns>
        Task<IResult<InstaMedia>> UploadPhotosAlbumAsync(InstaImage[] images, string caption);

        //[Obsolete("ConfigurePhotoAsync is deprecated, and will be deleted in the next update.")]
        ///// <summary>
        /////     Configure photo
        ///// </summary>
        ///// <param name="image">Photo to configure</param>
        ///// <param name="uploadId">Upload id</param>
        ///// <param name="caption">Caption</param>
        ///// <returns></returns>
        //Task<IResult<InstaMedia>> ConfigurePhotoAsync(InstaImage image, string uploadId, string caption);

        //[Obsolete("ConfigureAlbumAsync is deprecated, and will be deleted in the next update.")]
        ///// <summary>
        /////     Configure photos for Album
        ///// </summary>
        ///// <param name="uploadId">Array of upload IDs to configure</param>
        ///// ///
        ///// <param name="caption">Caption</param>
        ///// <returns></returns>
        //Task<IResult<InstaMedia>> ConfigureAlbumAsync(string[] uploadId, string caption);
        [Obsolete("GetStoryFeedAsync is deprecated, please use StoryProcessor.GetStoryFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get user story feed (stories from users followed by current user).
        /// </summary>
        Task<IResult<InstaStoryFeed>> GetStoryFeedAsync();
        [Obsolete("GetUserStoryAsync is deprecated, please use StoryProcessor.GetUserStoryAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get the story by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        Task<IResult<InstaStory>> GetUserStoryAsync(long userId);
        [Obsolete("UploadStoryPhotoAsync is deprecated, please use StoryProcessor.UploadStoryPhotoAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Upload story photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(InstaImage image, string caption);

        /// <summary>
        ///     Configure story photo
        /// </summary>
        /// <param name="image">Photo to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        /// <returns></returns>
        //Task<IResult<InstaStoryMedia>> ConfigureStoryPhotoAsync(InstaImage image, string uploadId, string caption);

        /// <summary>
        ///     Change password
        /// </summary>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">
        ///     The new password (shouldn't be the same old password, and should be a password you never used
        ///     here)
        /// </param>
        /// <returns>Return true if the password is changed</returns>
        Task<IResult<bool>> ChangePasswordAsync(string oldPassword, string newPassword);
        [Obsolete("DeleteMediaAsync is deprecated, please use MediaProcessor.DeleteMediaAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Delete a media (photo or video)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="mediaType">The type of the media</param>
        /// <returns>Return true if the media is deleted</returns>
        Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType);
        [Obsolete("EditMediaAsync is deprecated, please use MediaProcessor.EditMediaAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Edit the caption of the media (photo/video)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="caption">The new caption</param>
        /// <returns>Return true if everything is ok</returns>
        Task<IResult<bool>> EditMediaAsync(string mediaId, string caption);
        [Obsolete("GetLikeFeedAsync is deprecated, please use FeedProcessor.GetLikeFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get feed of media your liked.
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetLikeFeedAsync(PaginationParameters paginationParameters);
        [Obsolete("GetFriendshipStatusAsync is deprecated, please use UserProcessor.GetFriendshipStatusAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get friendship status for given user id.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        /// <returns>
        ///     <see cref="InstaFriendshipStatus" />
        /// </returns>
        Task<IResult<InstaFriendshipStatus>> GetFriendshipStatusAsync(long userId);
        [Obsolete("GetUserStoryFeedAsync is deprecated, please use StoryProcessor.GetUserStoryFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get user story reel feed. Contains user info last story including all story items.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        /// <returns></returns>
        Task<IResult<InstaReelFeed>> GetUserStoryFeedAsync(long userId);
        [Obsolete("GetCollectionAsync is deprecated, please use CollectionProcessor.GetCollectionAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get your collection for given collection id
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollectionItem" />
        /// </returns>
        Task<IResult<InstaCollectionItem>> GetCollectionAsync(long collectionId);
        [Obsolete("GetCollectionsAsync is deprecated, please use CollectionProcessor.GetCollectionsAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get your collections
        /// </summary>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollections" />
        /// </returns>
        Task<IResult<InstaCollections>> GetCollectionsAsync();
        [Obsolete("CreateCollectionAsync is deprecated, please use CollectionProcessor.CreateCollectionAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Create a new collection
        /// </summary>
        /// <param name="collectionName">The name of the new collection</param>
        /// <returns>
        ///     <see cref="T:InstagramApiSharp.Classes.Models.InstaCollectionItem" />
        /// </returns>
        Task<IResult<InstaCollectionItem>> CreateCollectionAsync(string collectionName);
        [Obsolete("DeleteCollectionAsync is deprecated, please use CollectionProcessor.DeleteCollectionAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Delete your collection for given collection id
        /// </summary>
        /// <param name="collectionId">Collection ID to delete</param>
        /// <returns>true if succeed</returns>
        Task<IResult<bool>> DeleteCollectionAsync(long collectionId);

        [Obsolete("GetMediaIdFromUrlAsync is deprecated, please use MediaProcessor.GetMediaIdFromUrlAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get media ID from an url (got from "share link")
        /// </summary>
        /// <param name="uri">Uri to get media ID</param>
        /// <returns>Media ID</returns>
        Task<IResult<string>> GetMediaIdFromUrlAsync(Uri uri);
        [Obsolete("GetShareLinkFromMediaIdAsync is deprecated, please use MediaProcessor.GetShareLinkFromMediaIdAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Get share link from media Id
        /// </summary>
        /// <param name="mediaId">media ID</param>
        /// <returns>Share link as Uri</returns>
        Task<IResult<Uri>> GetShareLinkFromMediaIdAsync(string mediaId);
        [Obsolete("AddItemsToCollectionAsync is deprecated, please use CollectionProcessor.AddItemsToCollectionAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Adds items to collection asynchronous.
        /// </summary>
        /// <param name="collectionId">Collection identifier.</param>
        /// <param name="mediaIds">Media id list.</param>
        /// <returns></returns>
        Task<IResult<InstaCollectionItem>> AddItemsToCollectionAsync(long collectionId, params string[] mediaIds);
        [Obsolete("SearchLocation is deprecated, please use LocationProcessor.SearchLocationAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Searches for specific location by provided geo-data or search query.
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="query">Search query</param>
        /// <returns>List of locations (short format)</returns>
        Task<IResult<InstaLocationShortList>> SearchLocation(double latitude, double longitude, string query);
        [Obsolete("GetLocationFeed is deprecated, please use LocationProcessor.GetLocationFeedAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Gets the feed of particular location.
        /// </summary>
        /// <param name="locationId">Location identifier</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>Location feed</returns>
        Task<IResult<InstaLocationFeed>> GetLocationFeed(long locationId, PaginationParameters paginationParameters);
        [Obsolete("SearchHashtag is deprecated, please use HashtagProcessor.SearchHashtagAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Searches for specific hashtag by search query.
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="excludeList">Array of numerical hashtag IDs (ie "17841562498105353") to exclude from the response, allowing you to skip tags from a previous call to get more results</param>
        /// <param name="rankToken">The rank token from the previous page's response</param>
        /// <returns>List of hashtags</returns>
        Task<IResult<InstaHashtagSearch>> SearchHashtag(string query, IEnumerable<long> excludeList = null, string rankToken = null);
        [Obsolete("GetHashtagInfo is deprecated, please use HashtagProcessor.GetHashtagInfoAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Gets the hashtag information by user tagname.
        /// </summary>
        /// <param name="tagname">Tagname</param>
        /// <returns>Hashtag information</returns>
        Task<IResult<InstaHashtag>> GetHashtagInfo(string tagname);
        [Obsolete("GetUserInfoByIdAsync is deprecated, please use UserProcessor.GetUserInfoByIdAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Gets the user extended information (followers count, following count, bio, etc) by user identifier.
        /// </summary>
        /// <param name="pk">User Id, like "123123123"</param>
        /// <returns></returns>
        Task<IResult<InstaUserInfo>> GetUserInfoByIdAsync(long pk);
        [Obsolete("GetUserInfoByUsernameAsync is deprecated, please use UserProcessor.GetUserInfoByUsernameAsync instead.\r\nThis will be deleted in the next update.")]
        /// <summary>
        ///     Gets the user extended information (followers count, following count, bio, etc) by username.
        /// </summary>
        /// <param name="username">Username, like "instagram"</param>
        /// <returns></returns>
        Task<IResult<InstaUserInfo>> GetUserInfoByUsernameAsync(string username);

        #endregion
    }
}