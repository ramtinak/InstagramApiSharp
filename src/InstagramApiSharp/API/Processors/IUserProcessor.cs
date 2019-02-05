using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     User api functions.
    /// </summary>
    public interface IUserProcessor
    {
        /// <summary>
        ///     Accept user friendship requst.
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<InstaFriendshipStatus>> AcceptFriendshipRequestAsync(long userId);

        /// <summary>
        ///     Add new best friend (besties)
        /// </summary>
        /// <param name="userIds">User ids (pk) to add</param>
        Task<IResult<InstaFriendshipShortStatusList>> AddBestFriendsAsync(params long[] userIds);

        /// <summary>
        ///     Block user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipFullStatus>> BlockUserAsync(long userId);

        /// <summary>
        ///     Delete an user from your best friend (besties) lists
        /// </summary>
        /// <param name="userIds">User ids (pk) to add</param>
        Task<IResult<InstaFriendshipShortStatusList>> DeleteBestFriendsAsync(params long[] userIds);

        /// <summary>
        ///     Favorite user (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<bool>> FavoriteUserAsync(long userId);
        
        /// <summary>
        ///     Favorite user stories (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<bool>> FavoriteUserStoriesAsync(long userId);

        /// <summary>
        ///     Follow user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipFullStatus>> FollowUserAsync(long userId);

        /// <summary>
        ///     Get self best friends (besties)
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetBestFriendsAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get best friends (besties) suggestions
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetBestFriendsSuggestionsAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get blocked users
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaBlockedUsers" />
        /// </returns>
        Task<IResult<InstaBlockedUsers>> GetBlockedUsersAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get currently logged in user info asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaCurrentUser" />
        /// </returns>
        Task<IResult<InstaCurrentUser>> GetCurrentUserAsync();

        /// <summary>
        ///     Get followers list for currently logged in user asynchronously
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetCurrentUserFollowersAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get activity of following asynchronously
        /// </summary>
        /// <param name="paginationParameters"></param>
        Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityFeedAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get friendship status for given user id.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        /// <returns>
        ///     <see cref="InstaStoryFriendshipStatus" />
        /// </returns>
        Task<IResult<InstaStoryFriendshipStatus>> GetFriendshipStatusAsync(long userId);
        /// <summary>
        ///     Get friendship status for multiple user ids.
        /// </summary>
        /// <param name="userIds">List of user identifier (PK)</param>
        /// <returns>
        ///     <see cref="InstaFriendshipShortStatusList" />
        /// </returns>
        Task<IResult<InstaFriendshipShortStatusList>> GetFriendshipStatusesAsync(params long[] userIds);
        /// <summary>
        ///     Get full user info (user info, feeds, stories, broadcasts)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<InstaFullUserInfo>> GetFullUserInfoAsync(long userId);

        /// <summary>
        ///     Get pending friendship requests.
        /// </summary>
        Task<IResult<InstaPendingRequest>> GetPendingFriendRequestsAsync();

        /// <summary>
        ///     Get activity of current user asynchronously
        /// </summary>
        /// <param name="paginationParameters"></param>
        Task<IResult<InstaActivityFeed>> GetRecentActivityFeedAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get suggestion details
        /// </summary>
        /// <param name="userIds">List of user ids (pk)</param>
        Task<IResult<InstaSuggestionItemList>> GetSuggestionDetailsAsync(params long[] userIds);

        /// <summary>
        ///     Get suggestion users
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        Task<IResult<InstaSuggestions>> GetSuggestionUsersAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get user info by its user name asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        Task<IResult<InstaUser>> GetUserAsync(string username);
       
        /// <summary>
        ///     Get user from a nametag image
        /// </summary>
        /// <param name="nametagImage">Nametag image</param>
        Task<IResult<InstaUser>> GetUserFromNametagAsync(InstaImage nametagImage);

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
            PaginationParameters paginationParameters, string searchQuery = "", bool mutualsfirst = false);

        /// <summary>
        ///     Get followers list by user id(pk) asynchronously
        /// </summary>
        /// <param name="userId">User id(pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followers</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetUserFollowersByIdAsync(long userId,
            PaginationParameters paginationParameters, string searchQuery = "", bool mutualsfirst = false);

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

        /// <summary>
        ///     Get following list by user id(pk) asynchronously
        /// </summary>
        /// <param name="userId">User id(pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <param name="searchQuery">Search string to locate specific followings</param>
        /// <returns>
        ///     <see cref="InstaUserShortList" />
        /// </returns>
        Task<IResult<InstaUserShortList>> GetUserFollowingByIdAsync(long userId,
            PaginationParameters paginationParameters, string searchQuery = "");

        /// <summary>
        ///     Gets the user extended information (followers count, following count, bio, etc) by user identifier.
        /// </summary>
        /// <param name="pk">User Id, like "123123123"</param>
        /// <returns></returns>
        Task<IResult<InstaUserInfo>> GetUserInfoByIdAsync(long pk);

        /// <summary>
        ///     Gets the user extended information (followers count, following count, bio, etc) by username.
        /// </summary>
        /// <param name="username">Username, like "instagram"</param>
        /// <returns></returns>
        Task<IResult<InstaUserInfo>> GetUserInfoByUsernameAsync(string username);

        /// <summary>
        ///     Get all user media by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get all user media by user id (pk) asynchronously
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserMediaByIdAsync(long userId, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get all user shoppable media by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserShoppableMediaAsync(string username, PaginationParameters paginationParameters);

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
        /// <summary>
        ///     Get user tags by username asynchronously
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserTagsAsync(long userId, PaginationParameters paginationParameters);
        /// <summary>
        ///     Ignore user friendship requst.
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<InstaFriendshipFullStatus>> IgnoreFriendshipRequestAsync(long userId);

        /// <summary>
        ///     Hide my story from specific user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaStoryFriendshipStatus>> HideMyStoryFromUserAsync(long userId);

        /// <summary>
        ///     Mark user as overage
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<bool>> MarkUserAsOverageAsync(long userId);

        /// <summary>
        ///     Mute friend's stories, so you won't see their stories in latest stories tab
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<InstaStoryFriendshipStatus>> MuteFriendStoryAsync(long userId);

        /// <summary>
        ///     Mute user media (story, post or all)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="unmuteOption">Unmute option</param>
        Task<IResult<InstaStoryFriendshipStatus>> MuteUserMediaAsync(long userId, InstaMuteOption muteOption);

        /// <summary>
        ///     Report user
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<bool>> ReportUserAsync(long userId);

        /// <summary>
        ///     Stop block user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipFullStatus>> UnBlockUserAsync(long userId);

        /// <summary>
        ///     Unfavorite user (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<bool>> UnFavoriteUserAsync(long userId);

        /// <summary>
        ///     Unfavorite user stories (user must be in your following list)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<bool>> UnFavoriteUserStoriesAsync(long userId);

        /// <summary>
        ///     Stop follow user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipFullStatus>> UnFollowUserAsync(long userId);
        
        /// <summary>
        ///     Unhide my story from specific user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaStoryFriendshipStatus>> UnHideMyStoryFromUserAsync(long userId);

        /// <summary>
        ///     Unmute friend's stories, so you will be able to see their stories in latest stories tab once again
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<InstaStoryFriendshipStatus>> UnMuteFriendStoryAsync(long userId);

        /// <summary>
        ///     Unmute user media (story, post or all)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        /// <param name="unmuteOption">Unmute option</param>
        Task<IResult<InstaStoryFriendshipStatus>> UnMuteUserMediaAsync(long userId, InstaMuteOption unmuteOption);

        /// <summary>
        ///     Remove an follower from your followers
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<InstaFriendshipStatus>> RemoveFollowerAsync(long userId);

        /// <summary>
        ///     Translate biography of someone
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<string>> TranslateBiographyAsync(long userId);
    }
}