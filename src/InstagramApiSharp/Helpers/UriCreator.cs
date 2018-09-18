using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;
using System;
using System.Collections.Generic;

namespace InstagramApiSharp.Helpers
{
    internal class UriCreator
    {
        private static readonly Uri BaseInstagramUri = new Uri(InstaApiConstants.INSTAGRAM_URL);

        public static Uri GetMediaUri(string mediaId)
        {
            return Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_MEDIA, mediaId),
                out var instaUri)
                ? instaUri
                : null;
        }

        public static Uri GetSearchTagUri(string tag, int count, IEnumerable<long> excludeList, string rankToken)
        {
            excludeList = excludeList ?? new List<long>();
            var excludeListStr = $"[{String.Join(",", excludeList)}]";
            if (!Uri.TryCreate(BaseInstagramUri,
                string.Format(InstaApiConstants.SEARCH_TAGS, tag, count),
                out var instaUri))
                throw new Exception("Cant create search tag URI");
            return instaUri
                .AddQueryParameter("exclude_list", excludeListStr)
                .AddQueryParameter("rank_token", rankToken);
        }

        public static Uri GetTagInfoUri(string tag)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_TAG_INFO, tag), out var instaUri))
                throw new Exception("Cant create tag info URI");
            return instaUri;
        }

        public static Uri GetUserUri(string username)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SEARCH_USERS, out var instaUri))
                throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) { Query = $"q={username}" };
            return userUriBuilder.Uri;
        }

        public static Uri GetUserInfoByIdUri(long pk)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_INFO_BY_ID, pk), out var instaUri))
                throw new Exception("Cant create user info by identifier URI");
            return instaUri;
        }

        public static Uri GetUserInfoByUsernameUri(string username)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_INFO_BY_USERNAME, username), out var instaUri))
                throw new Exception("Cant create user info by username URI");
            return instaUri;
        }

        public static Uri GetUserFeedUri(string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out var instaUri))
                throw new Exception("Cant create timeline feed URI");
            return !string.IsNullOrEmpty(maxId)
                ? new UriBuilder(instaUri) { Query = $"max_id={maxId}" }.Uri
                : instaUri;
        }

        public static Uri GetUserMediaListUri(long userPk, string nextId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USEREFEED + userPk, out var instaUri))
                throw new Exception("Cant create URI for user media retrieval");
            return !string.IsNullOrEmpty(nextId)
                ? new UriBuilder(instaUri) { Query = $"max_id={nextId}" }.Uri
                : instaUri;
        }
        public static Uri GetCreateAccountUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_CREATE, out var instaUri))
                throw new Exception("Cant create URI for user creation");
            return instaUri;
        }
        public static Uri GetCheckEmailUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USER_CHECK_EMAIL, out var instaUri))
                throw new Exception("Cant create URI for check email");
            return instaUri;
        }
        public static Uri GetLoginUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGIN, out var instaUri))
                throw new Exception("Cant create URI for user login");
            return instaUri;
        }

        public static Uri GetTwoFactorLoginUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_2FA_LOGIN, out var instaUri))
                throw new Exception("Cant create URI for user 2FA login");
            return instaUri;
        }

        public static Uri GetTimelineWithMaxIdUri(string nextId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out var instaUri))
                throw new Exception("Cant create search URI for timeline");
            var uriBuilder = new UriBuilder(instaUri) { Query = $"max_id={nextId}" };
            return uriBuilder.Uri;
        }

        public static Uri GetCurrentUserUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CURRENTUSER, out var instaUri))
                throw new Exception("Cant create URI for current user info");
            return instaUri;
        }

        public static Uri GetUserFollowersUri(long userPk, string rankToken, string searchQuery, string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_FOLLOWERS, userPk, rankToken),
                out var instaUri))
                throw new Exception("Cant create URI for user followers");
            return instaUri
                .AddQueryParameterIfNotEmpty("max_id", maxId)
                .AddQueryParameterIfNotEmpty("query", searchQuery);
        }

        public static Uri GetUserFollowingUri(long userPk, string rankToken, string searchQuery, string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_FOLLOWING, userPk, rankToken),
                out var instaUri))
                throw new Exception("Cant create URI for user following");
            return instaUri
                .AddQueryParameterIfNotEmpty("max_id", maxId)
                .AddQueryParameterIfNotEmpty("query", searchQuery);
        }

        public static Uri GetTagFeedUri(string tag, string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_TAG_FEED, tag), out var instaUri))
                throw new Exception("Cant create URI for discover tag feed");
            return !string.IsNullOrEmpty(maxId)
                ? new UriBuilder(instaUri) { Query = $"max_id={maxId}" }.Uri
                : instaUri;
        }

        public static Uri GetLogoutUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGOUT, out var instaUri))
                throw new Exception("Cant create URI for user logout");
            return instaUri;
        }

        public static Uri GetExploreUri(string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.EXPLORE, out var instaUri))
                throw new Exception("Cant create URI for explore posts");
            var query = string.Empty;
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) { Query = query };
            return uriBuilder.Uri;
        }

        public static Uri GetDirectSendMessageUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_TEXT_BROADCAST, out var instaUri))
                throw new Exception("Cant create URI for sending message");
            return instaUri;
        }
        public static Uri GetDirectSendPhotoUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DIRECT_BROADCAST_UPLOAD_PHOTO, out var instaUri))
                throw new Exception("Cant create URI for sending photo to direct");
            return instaUri;
        }
        public static Uri GetDirectInboxUri(string NextId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_INBOX, out var instaUri))
                throw new Exception("Cant create URI for get inbox");
            return !string.IsNullOrEmpty(NextId)
                ? new UriBuilder(instaUri) { Query = $"visual_message_return_type=unseen&persistentBadging=true&use_unified_inbox=true&cursor={NextId}" }.Uri
                 : new UriBuilder(instaUri) { Query = "visual_message_return_type=unseen&persistentBadging=true&use_unified_inbox=true" }.Uri;
                //: instaUri;
            //        return instaUri
            ////GET /api/v1/direct_v2/inbox/?visual_message_return_type=unseen&persistentBadging=true&use_unified_inbox=true
            //.AddQueryParameterIfNotEmpty("visual_message_return_type", "unseen")
            //.AddQueryParameterIfNotEmpty("persistentBadging", "true")
            //.AddQueryParameterIfNotEmpty("use_unified_inbox", "true")
            //.AddQueryParameterIfNotEmpty("cursor", NextId);
        }
        public static Uri GetDirectPendingInboxUri(string NextId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_PENDING_INBOX, out var instaUri))
                throw new Exception("Cant create URI for get pending inbox");
            return !string.IsNullOrEmpty(NextId)
                ? new UriBuilder(instaUri) { Query = $"cursor={NextId}" }.Uri
                : instaUri;
        }
        public static Uri GetDirectInboxThreadUri(string threadId, string NextId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_DIRECT_THREAD, threadId),
                    out var instaUri)) throw new Exception("Cant create URI for get inbox thread by id");
            return !string.IsNullOrEmpty(NextId)
                ? new UriBuilder(instaUri) { Query = $"cursor={NextId}" }.Uri
                : instaUri;
        }
        public static Uri GetApprovePendingDirectRequestUri(string threadId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_DIRECT_THREAD_APPROVE, threadId),
                    out var instaUri)) throw new Exception("Cant create URI for approve inbox thread");
            return instaUri;
        }
        public static Uri GetApprovePendingMultipleDirectRequestUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_THREAD_APPROVE_MULTIPLE,
                    out var instaUri)) throw new Exception("Cant create URI for approve multiple inbox threads");
            return instaUri;
        }
        public static Uri GetDeclineAllPendingDirectRequestsUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_THREAD_DECLINEALL,
                    out var instaUri)) throw new Exception("Cant create URI for decline all pending direct requests");
            return instaUri;
        }
        public static Uri GetDeclinePendingDirectRequestUri(string threadId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_DIRECT_THREAD_DECLINE, threadId),
                    out var instaUri)) throw new Exception("Cant create URI for decline pending direct request");
            return instaUri;
        }
        public static Uri GetDeclineMultplePendingDirectRequestsUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_THREAD_DECLINE_MULTIPLE,
                    out var instaUri)) throw new Exception("Cant create URI for decline all pending direct requests");
            return instaUri;
        }
        public static Uri GetUserTagsUri(long userPk, string rankToken, string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_TAGS, userPk),
                out var instaUri))
                throw new Exception("Cant create URI for get user tags");
            var query = $"rank_token={rankToken}&ranked_content=true";
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) { Query = query };
            return uriBuilder.Uri;
        }

        public static Uri GetRecentRecipientsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_RECENT_RECIPIENTS, out var instaUri))
                throw new Exception("Cant create URI (get recent recipients)");
            return instaUri;
        }

        public static Uri GetRankedRecipientsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_RANKED_RECIPIENTS, out var instaUri))
                throw new Exception("Cant create URI (get ranked recipients)");
            return instaUri;
        }

        public static Uri GetRecentActivityUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_RECENT_ACTIVITY, out var instaUri))
                throw new Exception("Cant create URI (get recent activity)");
            var query = $"activity_module=all";
            var uriBuilder = new UriBuilder(instaUri) { Query = query };
            return uriBuilder.Uri;
        }

        public static Uri GetFollowingRecentActivityUri(string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_FOLLOWING_RECENT_ACTIVITY, out var instaUri))
                throw new Exception("Cant create URI (get following recent activity");
            var query = string.Empty;
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) { Query = query };
            return uriBuilder.Uri;
        }

        public static Uri GetUnLikeMediaUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.UNLIKE_MEDIA, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for unlike media");
            return instaUri;
        }

        public static Uri GetLikeMediaUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIKE_MEDIA, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for like media");
            return instaUri;
        }

        public static Uri GetMediaCommentsUri(string mediaId, string nextMaxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_COMMENTS, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media comments");
            return !string.IsNullOrEmpty(nextMaxId)
                ? new UriBuilder(instaUri) { Query = $"can_support_threading=true&max_id={nextMaxId}" }.Uri
                : new UriBuilder(instaUri) { Query = $"can_support_threading=true" }.Uri;
        }
        public static Uri GetMediaCommentsMinIdUri(string mediaId, string nextMinId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_COMMENTS, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media comments");
            return !string.IsNullOrEmpty(nextMinId)
                ? new UriBuilder(instaUri) { Query = $"can_support_threading=true&min_id={nextMinId}" }.Uri
                : new UriBuilder(instaUri) { Query = $"can_support_threading=true" }.Uri;
        }
        public static Uri GetMediaInlineCommentsUri(string mediaId, string targetCommentId, string nextMaxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_INLINE_COMMENTS, mediaId,targetCommentId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media comments replies with max id");
            return !string.IsNullOrEmpty(nextMaxId)
                //? new UriBuilder(instaUri) { Query = $"min_id={nextId}" }.Uri
                ? new UriBuilder(instaUri) { Query = $"max_id={nextMaxId}" }.Uri
                : instaUri;
        }
        public static Uri GetMediaInlineCommentsWithMinIdUri(string mediaId, string targetCommentId, string nextMinId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_INLINE_COMMENTS, mediaId, targetCommentId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media comment replies with min id");
            return !string.IsNullOrEmpty(nextMinId)
                ? new UriBuilder(instaUri) { Query = $"min_id={nextMinId}" }.Uri
                : instaUri;
        }
        public static Uri GetMediaLikersUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_LIKERS, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }

        public static Uri GetFollowUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FOLLOW_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }

        public static Uri GetUnFollowUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.UNFOLLOW_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }


        public static Uri GetBlockUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.BLOCK_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }

        public static Uri GetUnBlockUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.UNBLOCK_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }


        public static Uri GetUriSetAccountPrivate()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SET_ACCOUNT_PRIVATE, out var instaUri))
                throw new Exception("Cant create URI for set account private");
            return instaUri;
        }

        public static Uri GetUriSetAccountPublic()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SET_ACCOUNT_PUBLIC, out var instaUri))
                throw new Exception("Cant create URI for set account public");
            return instaUri;
        }

        public static Uri GetPostCommetUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.POST_COMMENT, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for posting comment");
            return instaUri;
        }

        public static Uri GetAllowMediaCommetsUri(string mediaId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.ALLOW_MEDIA_COMMENTS, mediaId),
                    out var instaUri))
                throw new Exception("Cant create URI to allow comments on media");
            return instaUri;
        }

        public static Uri GetDisableMediaCommetsUri(string mediaId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DISABLE_MEDIA_COMMENTS, mediaId),
                    out var instaUri))
                throw new Exception("Cant create URI to disable comments on media");
            return instaUri;
        }
        public static Uri GetMediaCommetLikersUri(string mediaId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_COMMENT_LIKERS, mediaId),
                    out var instaUri))
                throw new Exception("Cant create URI to media comments likers");
            return instaUri;
        }
        public static Uri GetReportCommetUri(string mediaId, string commentId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_REPORT_COMMENT, mediaId, commentId),
                    out var instaUri))
                throw new Exception("Cant create URI for report comment");
            return instaUri;
        }
        public static Uri GetDeleteCommentUri(string mediaId, string commentId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DELETE_COMMENT, mediaId, commentId),
                    out var instaUri))
                throw new Exception("Cant create URI for delete comment");
            return instaUri;
        }
        public static Uri GetUploadVideoUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.UPLOAD_VIDEO, out var instaUri))
                throw new Exception("Cant create URI for upload video");
            return instaUri;
        }
        public static Uri GetUploadPhotoUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.UPLOAD_PHOTO, out var instaUri))
                throw new Exception("Cant create URI for upload photo");
            return instaUri;
        }

        public static Uri GetMediaConfigureUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.MEDIA_CONFIGURE, out var instaUri))
                throw new Exception("Cant create URI for configuring media");
            return instaUri;
        }

        public static Uri GetMediaAlbumConfigureUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.MEDIA_ALBUM_CONFIGURE, out var instaUri))
                throw new Exception("Cant create URI for configuring media album");
            return instaUri;
        }

        public static Uri GetStoryFeedUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_STORY_TRAY, out var instaUri))
                throw new Exception("Can't create URI for getting story tray");
            return instaUri;
        }

        public static Uri GetUserStoryUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_STORY, userId),
                out var instaUri))
                throw new Exception("Can't create URI for getting user's story");
            return instaUri;
        }

        public static Uri GetStoryConfigureUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.STORY_CONFIGURE, out var instaUri))
                throw new Exception("Can't create URI for configuring story media");
            return instaUri;
        }
        public static Uri GetVideoStoryConfigureUri(bool isVideo = false)
        {
            if (!Uri.TryCreate(BaseInstagramUri, isVideo ? InstaApiConstants.STORY_CONFIGURE_VIDEO : InstaApiConstants.STORY_CONFIGURE_VIDEO2, out var instaUri))
                throw new Exception("Can't create URI for configuring story media");
            return instaUri;
        }
        public static Uri GetChangePasswordUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CHANGE_PASSWORD, out var instaUri))
                throw new Exception("Can't create URI for changing password");
            return instaUri;
        }

        public static Uri GetDeleteMediaUri(string mediaId, InstaMediaType mediaType)
        {
            if (!Uri.TryCreate(BaseInstagramUri,
                string.Format(InstaApiConstants.DELETE_MEDIA, mediaId, mediaType.ToString().ToUpper()), out var instaUri))
                throw new Exception("Can't create URI for deleting media");
            return instaUri;
        }
        public static Uri GetDeleteStoryMediaUri(string mediaId, InstaSharingType mediaType)
        {
            if (!Uri.TryCreate(BaseInstagramUri,
                string.Format(InstaApiConstants.DELETE_MEDIA, mediaId, mediaType.ToString().ToUpper()), out var instaUri))
                throw new Exception("Can't create URI for deleting media story");
            return instaUri;
        }
        public static Uri GetEditMediaUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.EDIT_MEDIA, mediaId),
                out var instaUri))
                throw new Exception("Can't create URI for editing media");
            return instaUri;
        }

        public static Uri GetUserLikeFeedUri(string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.LIKE_FEED, out var instaUri))
                throw new Exception("Can't create URI for getting like feed");
            var query = string.Empty;
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) { Query = query };
            return uriBuilder.Uri;
        }

        public static Uri GetUserFriendshipUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Concat(InstaApiConstants.FRIENDSHIPSTATUS, userId, "/"),
                out var instaUri))
                throw new Exception("Can't create URI for getting friendship status");
            return instaUri;
        }

        public static Uri GetUserReelFeedUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.USER_REEL_FEED, userId),
                out var instaUri))
                throw new Exception("Can't create URI for getting user reel feed");
            return instaUri;
        }

        public static Uri GetCollectionUri(long collectionId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_COLLECTION, collectionId),
                out var instaUri))
                throw new Exception("Can't create URI for getting collection");
            return instaUri;
        }

        public static Uri GetEditCollectionUri(long collectionId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.EDIT_COLLECTION, collectionId),
                out var instaUri))
                throw new Exception("Can't create URI for editing collection");
            return instaUri;
        }

        public static Uri GetCollectionsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_LIST_COLLECTIONS,
                out var instaUri))
                throw new Exception("Can't create URI for getting collections");
            return instaUri;
        }

        public static Uri GetCreateCollectionUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CREATE_COLLECTION,
                out var instaUri))
                throw new Exception("Can't create URI for creating collection");
            return instaUri;
        }

        public static Uri GetDeleteCollectionUri(long collectionId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DELETE_COLLECTION, collectionId),
                out var instaUri))
                throw new Exception("Can't create URI for deleting collection");
            return instaUri;
        }

        public static Uri GetMediaIdFromUrlUri(Uri uri)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_MEDIAID, uri.AbsoluteUri),
                out var instaUri))
                throw new Exception("Can't create URI for getting media id");
            return instaUri;
        }

        public static Uri GetShareLinkFromMediaId(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_SHARE_LINK, mediaId),
                out var instaUri))
                throw new Exception("Can't create URI for getting share link");
            return instaUri;
        }






        public static Uri GetRequestForEditProfileUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_REQUEST_PROFILE_EDIT, out var instaUri))
                throw new Exception("Cant create URI for request editing profile");
            return instaUri;
        }

        public static Uri GetEditProfileUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_EDIT_PROFILE, out var instaUri))
                throw new Exception("Cant create URI for edit profile");
            return instaUri;
        }

        public static Uri GetProfileSetPhoneAndNameUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SET_PHONE_AND_NAME, out var instaUri))
                throw new Exception("Cant create URI for sets phone and number");
            return instaUri;
        }

        public static Uri GetRemoveProfilePictureUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_REMOVE_PROFILE_PICTURE, out var instaUri))
                throw new Exception("Cant create URI for remove profile picture");
            return instaUri;
        }

        public static Uri GetChangeProfilePictureUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_CHANGE_PROFILE_PICTURE, out var instaUri))
                throw new Exception("Cant create URI for change profile picture");
            return instaUri;
        }

        public static Uri GetStorySettingsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USER_REEL_SETTINGS, out var instaUri))
                throw new Exception("Cant create URI for story settings");
            return instaUri;
        }

        public static Uri GetSetReelSettingsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USER_SET_REEL_SETTINGS, out var instaUri))
                throw new Exception("Cant create URI for set reel settings");
            return instaUri;
        }

        public static Uri GetCheckUsernameUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USER_CHECK_USERNAME, out var instaUri))
                throw new Exception("Cant create URI for check username");
            return instaUri;
        }

        public static Uri GetDisableSmsTwoFactorUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_DISABLE_SMS_TWO_FACTOR, out var instaUri))
                throw new Exception("Cant create URI for disable sms two factor");
            return instaUri;
        }

        public static Uri GetSendTwoFactorEnableSmsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SEND_TWO_FACTOR_ENABLE_SMS, out var instaUri))
                throw new Exception("Cant create URI for send two factor enable sms");
            return instaUri;
        }

        public static Uri GetEnableSmsTwoFactorUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_ENABLE_SMS_TWO_FACTOR, out var instaUri))
                throw new Exception("Cant create URI for enable sms two factor");
            return instaUri;
        }

        public static Uri GetAccountSecurityInfoUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SECURITY_INFO, out var instaUri))
                throw new Exception("Cant create URI for accounts security info");
            return instaUri;
        }

        public static Uri GetAccountSendConfirmEmailUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SEND_CONFIRM_EMAIL, out var instaUri))
                throw new Exception("Cant create URI for accounts send confirm email");
            return instaUri;
        }

        public static Uri GetAccountSendSmsCodeUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SEND_SMS_CODE, out var instaUri))
                throw new Exception("Cant create URI for accounts send sms code");
            return instaUri;
        }

        public static Uri GetAccountVerifySmsCodeUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_VERIFY_SMS_CODE, out var instaUri))
                throw new Exception("Cant create URI for accounts verify sms code");
            return instaUri;
        }

        public static Uri GetAccountSetPresenseDisabledUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SET_PRESENCE_DISABLED, out var instaUri))
                throw new Exception("Cant create URI for accounts set presence disabled");
            return instaUri;
        }

        public static Uri GetAccountGetCommentFilterUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_GET_COMMENT_FILTER, out var instaUri))
                throw new Exception("Cant create URI for accounts get comment filter");
            return instaUri;
        }

        public static Uri GetRecentSearchUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.FBSEARCH_RECENT_SEARCHES, out var instaUri))
                throw new Exception("Cant create URI for facebook recent searches");
            return instaUri;
        }

        public static Uri GetClearSearchHistoryUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.FBSEARCH_CLEAR_SEARCH_HISTORY, out var instaUri))
                throw new Exception("Cant create URI for clear search history");
            return instaUri;
        }

        public static Uri GetSuggestedSearchUri(API.Processors.DiscoverSearchType searchType)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FBSEARCH_SUGGESTED_SEARCHS, searchType.ToString().ToLower()), out var instaUri))
                throw new Exception("Cant create URI for suggested search");
            return instaUri;
        }
        public static Uri GetProfileSearchUri(string query, int count)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FBSEARCH_PROFILE_SEARCH, query, count), out var instaUri))
                throw new Exception("Cant create URI for profile search");
            return instaUri;
        }
        public static Uri GetDiscoverPeopleUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DISCOVER_AYML, out var instaUri))
                throw new Exception("Cant create URI for discover people");
            return instaUri;
        }

        public static Uri GetSearchUserUri(string text, int count = 30)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.USER_SEARCH,
                TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalSeconds, text, count), out var instaUri))
                throw new Exception("Cant create URI for search user");
            return instaUri;
        }


        public static Uri GetAcceptFriendshipUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FRIENDSHIPS_APPROVE, userId), out var instaUri))
                throw new Exception("Cant create URI for accept friendship");
            return instaUri;
        }

        public static Uri GetDenyFriendshipUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FRIENDSHIPS_IGNORE, userId), out var instaUri))
                throw new Exception("Cant create URI for deny friendship");
            return instaUri;
        }
        public static Uri GetFriendshipPendingRequestsUri(string rankToken)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FRIENDSHIPS_PENDING_REQUESTS, rankToken), out var instaUri))
                throw new Exception("Cant create URI for friendship pending requests");
            return instaUri;
        }
        public static Uri GetLiveHeartbeatAndViewerCountUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_HEARTBEAT_AND_GET_VIEWER_COUNT, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for live heartbeat and get viewer count");
            return instaUri;
        }

        public static Uri GetLiveFinalViewerListUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_GET_FINAL_VIEWER_LIST, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for get final viewer list");
            return instaUri;
        }

        public static Uri GetLiveNotifyToFriendsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.LIVE_GET_LIVE_PRESENCE, out var instaUri))
                throw new Exception("Cant create URI for get live presence");
            return instaUri;
        }

        public static Uri GetSuggestedBroadcastsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.LIVE_GET_SUGGESTED_BROADCASTS, out var instaUri))
                throw new Exception("Cant create URI for get suggested broadcasts");
            return instaUri;
        }

        public static Uri GetDiscoverTopLiveUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DISCOVER_TOP_LIVE, out var instaUri))
                throw new Exception("Cant create URI for discover top live");
            return instaUri;
        }

        public static Uri GetDiscoverTopLiveStatusUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DISCOVER_TOP_LIVE_STATUS, out var instaUri))
                throw new Exception("Cant create URI for discover top live status");
            return instaUri;
        }

        public static Uri GetBroadcastInfoUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_INFO, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for get broadcast info");
            return instaUri;
        }

        public static Uri GetBroadcastViewerListUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_GET_VIEWER_LIST, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for get broadcast viewer list");
            return instaUri;
        }

        public static Uri GetPostLiveViewersListUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_GET_POST_LIVE_VIEWERS_LIST, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for get post live viewer list");
            return instaUri;
        }

        public static Uri GetBroadcastPostCommentUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_COMMENT, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast comments");
            return instaUri;
        }

        public static Uri GetBroadcastPinCommentUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_PIN_COMMENT, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast pin comment");
            return instaUri;
        }

        public static Uri GetBroadcastUnPinCommentUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_UNPIN_COMMENT, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast unpin comments");
            return instaUri;
        }

        public static Uri GetBroadcastCommentUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_GET_COMMENT, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast get comments");
            return instaUri;
        }

        public static Uri GetBroadcastPostLiveCommentUri(string broadcastId, int startingOffset, string encodingTag)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_GET_POST_LIVE_COMMENT, broadcastId, startingOffset, encodingTag), out var instaUri))
                throw new Exception("Cant create URI for broadcast post live comment");
            return instaUri;
        }

        public static Uri GetBroadcastEnableCommenstUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_UNMUTE_COMMENTS, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast enable comments");
            return instaUri;
        }

        public static Uri GetBroadcastDisableCommenstUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_MUTE_COMMENTS, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast disable comments");
            return instaUri;
        }

        public static Uri GetLikeLiveUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_LIKE, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for like live");
            return instaUri;
        }

        public static Uri GetLiveLikeCountUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_GET_LIKE_COUNT, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for live like count");
            return instaUri;
        }

        public static Uri GetBroadcastPostLiveLikesUri(string broadcastId, int startingOffset, string encodingTag)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_POST_LIVE_LIKES, broadcastId, startingOffset, encodingTag), out var instaUri))
                throw new Exception("Cant create URI for broadcast post live likes");
            return instaUri;
        }

        public static Uri GetBroadcastAddToPostLiveUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_ADD_TO_POST_LIVE, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast add to post live");
            return instaUri;
        }

        public static Uri GetBroadcastDeletePostLiveUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_DELETE_POST_LIVE, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast delete post live");
            return instaUri;
        }

        public static Uri GetBroadcastCreateUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.LIVE_CREATE, out var instaUri))
                throw new Exception("Cant create URI for broadcast create");
            return instaUri;
        }

        public static Uri GetBroadcastStartUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_START, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast start");
            return instaUri;
        }

        public static Uri GetBroadcastEndUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_END, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast end");
            return instaUri;
        }
        public static Uri GetBroadcastJoinRequestsUri(string broadcastId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIVE_GET_JOIN_REQUESTS, broadcastId), out var instaUri))
                throw new Exception("Cant create URI for broadcast join requests");
            return instaUri;
        }
        public static Uri GetFacebookSignUpUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.FB_FACEBOOK_SIGNUP, out var instaUri))
                throw new Exception("Cant create URI for facebook sign up url");
            return instaUri;
        }
        public static Uri GetChallengeRequireFirstUri(string apiPath, string guid, string deviceId)
        {
            if (!apiPath.EndsWith("/"))
                apiPath = apiPath + "/";
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.API_SUFFIX + apiPath +
                $"?guid={guid}&device_id={deviceId}", out var instaUri))
                throw new Exception("Cant create URI for challenge require url");
            return instaUri;
        }
        public static Uri GetChallengeRequireUri(string apiPath)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.API_SUFFIX + apiPath, out var instaUri))
                throw new Exception("Cant create URI for challenge require url");
            return instaUri;
        }
        public static Uri GetResetChallengeRequireUri(string apiPath)
        {
            apiPath = apiPath.Replace("/challenge/", "/challenge/reset/");
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.API_SUFFIX + apiPath, out var instaUri))
                throw new Exception("Cant create URI for challenge require url");
            return instaUri;
        }
        public static Uri GetCheckPhoneNumberUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_CHECK_PHONE_NUMBER, out var instaUri))
                throw new Exception("Cant create URI for check phone number");
            return instaUri;
        }
        public static Uri GetSignUpSMSCodeUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SEND_SIGNUP_SMS_CODE, out var instaUri))
                throw new Exception("Cant create URI for send signup sms code");
            return instaUri;
        }
        public static Uri GetValidateSignUpSMSCodeUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_VALIDATE_SIGNUP_SMS_CODE, out var instaUri))
                throw new Exception("Cant create URI for validate signup sms code");
            return instaUri;
        }
        public static Uri GetUsernameSuggestionsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_USERNAME_SUGGESTIONS, out var instaUri))
                throw new Exception("Cant create URI for username suggestions");
            return instaUri;
        }
        public static Uri GetOnboardingStepsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DYNAMIC_ONBOARDING_GET_STEPS, out var instaUri))
                throw new Exception("Cant create URI for dynamic onboarding get steps");
            return instaUri;
        }
        public static Uri GetCreateValidatedUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_CREATE_VALIDATED, out var instaUri))
                throw new Exception("Cant create URI for accounbts create validated");
            return instaUri;
        }
        public static Uri GetRankRecipientsByUserUri(string username)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_RANK_RECIPIENTS_BY_USERNAME, username), out var instaUri))
                throw new Exception("Cant create URI for get rank recipients by username");
            return instaUri;
        }
        public static Uri GetParticipantRecipientUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_PARTICIPANTS_RECIPIENT_USER, userId), out var instaUri))
                throw new Exception("Cant create URI for get participants recipient user");
            return instaUri;
        }
        public static Uri GetShareUserUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_SHARE_USER, out var instaUri))
                throw new Exception("Cant create URI for share user");
            return instaUri;
        }
        public static Uri GetStoryMediaInfoUploadUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.STORY_MEDIA_INFO_UPLOAD, out var instaUri))
                throw new Exception("Cant create URI for story media info");
            return instaUri;
        }
        public static Uri GetStoryUploadVideoUri(string uploadId, int fileHashCode)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.STORY_UPLOAD_VIDEO, uploadId, fileHashCode), out var instaUri))
                throw new Exception("Cant create URI for story upload video");
            return instaUri;
        }
        public static Uri GetStoryUploadPhotoUri(string uploadId, int fileHashCode)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.STORY_UPLOAD_PHOTO, uploadId, fileHashCode), out var instaUri))
                throw new Exception("Cant create URI for story upload photo");
            return instaUri;
        }
        public static Uri GetDirectConfigVideoUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DIRECT_BROADCAST_CONFIGURE_VIDEO, out var instaUri))
                throw new Exception("Cant create URI for direct config video");
            return instaUri;
        }
        public static Uri GetStoryShareUri(string mediaType)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.STORY_SHARE, mediaType), out var instaUri))
                throw new Exception("Cant create URI for story share");
            return instaUri;
        }
        public static Uri GetSeenMediaUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SEEN_MEDIA, out var instaUri))
                throw new Exception("Cant create URI for seen media");
            return instaUri;
        }
        public static Uri GetSeenMediaStoryUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SEEN_MEDIA_STORY, out var instaUri))
                throw new Exception("Cant create URI for seen media story");
            return instaUri;
        }
        public static Uri GetDirectThreadSeenUri(string threadId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DIRECT_THREAD_ITEM_SEEN, threadId), out var instaUri))
                throw new Exception("Cant create URI for story share");
            return instaUri;
        }
        public static Uri GetDirectThreadUpdateTitleUri(string threadId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DIRECT_THREAD_UPDATE_TITLE, threadId), out var instaUri))
                throw new Exception("Cant create URI for update thread title");
            return instaUri;
        }
        public static Uri GetMuteDirectThreadUri(string threadId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DIRECT_THREAD_MUTE, threadId), out var instaUri))
                throw new Exception("Cant create URI for mute direct thread");
            return instaUri;
        }
        public static Uri GetUnMuteDirectThreadUri(string threadId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DIRECT_THREAD_UNMUTE, threadId), out var instaUri))
                throw new Exception("Cant create URI for unmute direct thread");
            return instaUri;
        }
        public static Uri GetSendDirectProfileUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DIRECT_BROADCAST_PROFILE, out var instaUri))
                throw new Exception("Cant create URI for send profile to direct thread");
            return instaUri;
        }
        public static Uri GetHighlightFeedsUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.HIGHLIGHT_TRAY, userId), out var instaUri))
                throw new Exception("Cant create URI for highlight feeds");
            return instaUri;
        }
        public static Uri GetHighlightEditUri(string highlightId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.HIGHLIGHT_EDIT_REEL, highlightId), out var instaUri))
                throw new Exception("Cant create URI for highlight edit reel");
            return instaUri;
        }

        public static Uri GetHighlightCreateUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.HIGHLIGHT_CREATE_REEL, out var instaUri))
                throw new Exception("Cant create URI for highlight create reel");
            return instaUri;
        }
        public static Uri GetRegenerateTwoFactorBackUpCodeUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_REGEN_BACKUP_CODES, out var instaUri))
                throw new Exception("Cant create URI for regenerate two factor backup codes");
            return instaUri;
        }
        public static Uri GetSendDirectLinkUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DIRECT_BROADCAST_LINK, out var instaUri))
                throw new Exception("Cant create URI for send link to direct thread");
            return instaUri;
        }
        public static Uri GetSendDirectLocationUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DIRECT_BROADCAST_LOCATION, out var instaUri))
                throw new Exception("Cant create URI for send location to direct thread");
            return instaUri;
        }
        public static Uri GetSetBiographyUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SET_BIOGRAPHY, out var instaUri))
                throw new Exception("Cant create URI for set biography");
            return instaUri;
        }
        public static Uri GetAccount2FALoginAgainUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_2FA_LOGIN_AGAIN, out var instaUri))
                throw new Exception("Cant create URI for Account 2FA Login Again");
            return instaUri;
        }
        public static Uri GetAccountRecoveryEmailUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_SEND_RECOVERY_EMAIL, out var instaUri))
                throw new Exception("Cant create URI for Account Recovery Email");
            return instaUri;
        }
        public static Uri GetAccountRecoverPhoneUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOOKUP_PHONE, out var instaUri))
                throw new Exception("Cant create URI for Account Recovery phone");
            return instaUri;
        }
        public static Uri GetUserSearchByLocationUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.FBSEARCH_TOPSEARCH_FALT, out var instaUri))
                throw new Exception("Cant create URI for user search by location");
            return instaUri;
        }
        public static Uri GetLikeDirectMessageUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DIRECT_BROADCAST_REACTION, out var instaUri))
                throw new Exception("Cant create URI for like direct message");
            return instaUri;
        }
        public static Uri GetIGTVGuideUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.IGTV_TV_GUIDE, out var instaUri))
                throw new Exception("Cant create URI for igtv tv guide");
            return instaUri;
        }
        public static Uri GetIGTVChannelUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.IGTV_CHANNEL, out var instaUri))
                throw new Exception("Cant create URI for igtv channel");
            return instaUri;
        }
        public static Uri GetIGTVSearchUri(string query)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.IGTV_SEARCH, query), out var instaUri))
                throw new Exception("Cant create URI for igtv search");
            return instaUri;
        } 
        public static Uri GetIGTVSuggestedSearchesUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.IGTV_SUGGESTED_SEARCHES, out var instaUri))
                throw new Exception("Cant create URI for igtv suggested searches");
            return instaUri;
        }
        public static Uri GetMediaConfigureToIGTVUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.MEDIA_CONFIGURE_TO_IGTV, out var instaUri))
                throw new Exception("Cant create URI for media configure igtv");
            return instaUri;
        }
        public static Uri GetLikeCommentUri(string commentId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIKE_COMMENT, commentId),
                    out var instaUri))
                throw new Exception("Cant create URI for like comment");
            return instaUri;
        }
        public static Uri GetUnLikeCommentUri(string commentId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.UNLIKE_COMMENT, commentId),
                    out var instaUri))
                throw new Exception("Cant create URI for unlike comment");
            return instaUri;
        }
        public static Uri GetDeleteMultipleCommentsUri(string mediaId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DELETE_MULTIPLE_COMMENT, mediaId),
                    out var instaUri))
                throw new Exception("Cant create URI for delete multiple comments");
            return instaUri;
        }
        public static Uri GetFullUserInfoUri(long userId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.USERS_FULL_DETAIL_INFO, userId),
                    out var instaUri))
                throw new Exception("Cant create URI for full user info");
            return instaUri;
        }

        public static Uri GetMediaShareUri(InstaMediaType mediaType)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DIRECT_BROADCAST_MEDIA_SHARE, mediaType.ToString().ToLower()),
                    out var instaUri))
                throw new Exception("Cant create URI for media share");
            return instaUri;
        }
        public static Uri GetLeaveThreadUri(string threadId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DIRECT_THREAD_LEAVE, threadId),
                    out var instaUri))
                throw new Exception("Cant create URI for leave group thread");
            return instaUri;
        }
        public static Uri GetRequestForDownloadDataUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.DYI_REQUEST_DOWNLOAD_DATA, out var instaUri))
                throw new Exception("Cant create URI for request for download data.");
            return instaUri;
        }
    }
}