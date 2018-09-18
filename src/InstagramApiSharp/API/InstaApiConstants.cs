using System;

namespace InstagramApiSharp.API
{
    internal static class InstaApiConstants
    {
        public const string MAX_MEDIA_ID_POSTFIX = "/media/?max_id=";
        public const string HEADER_MAX_ID = "max_id";
        public const string MEDIA = "/media/";
        public const string P_SUFFIX = "p/";
        public const string CSRFTOKEN = "csrftoken";

        public const string HEADER_XCSRF_TOKEN = "X-CSRFToken";
        public const string HEADER_X_INSTAGRAM_AJAX = "X-Instagram-AJAX";
        public const string HEADER_X_REQUESTED_WITH = "X-Requested-With";
        public const string HEADER_XML_HTTP_REQUEST = "XMLHttpRequest";
        /// <summary>
        ///     For 35.0.0.20.96 verison
        /// </summary>
        public const string IG_APP_API_VERSION = "95414346";
        /// <summary>
        ///     InstagramApiSharp is based on 35.0.0.20.96 version but user agent is 61.0.0.19.86
        /// </summary>
        public const string IG_APP_VERSION = "61.0.0.19.86"/*"35.0.0.20.96"*/;
        public const string USER_AGENT =
            "Instagram {6} Android ({7}/{8}; {0}; {1}; {2}; {3}; {4}; {5}; en_US; {9})";
        public const string USER_AGENT_DEFAULT =
        "Instagram 61.0.0.19.86 Android (24/7.0; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_US; 95414346)";
        ////"Instagram 12.0.0.7.91 Android (23/6.0.1; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_NZ)";
        public const string HEADER_USER_AGENT = "User-Agent";

        public const string HEADER_QUERY = "q";
        public const string HEADER_RANK_TOKEN = "rank_token";
        public const string HEADER_COUNT = "count";
        public const string HEADER_EXCLUDE_LIST = "exclude_list";

        public const string FB_ACCESS_TOKEN = "EAABwzLixnjYBADcFesqNbHwRoDFmpBpEVZB2hcOAhMh1A3gGyxoW82CZBXKUEAYDAfNrA2Ntt4Pf21mXIT5Bv2CYIY3OhWJZCChIxAopXyOYQq4KkZBUsRL3deG7550cL9qiJaPEwlnK9pTQCWSUok5ZBZBNaUyW78eiQeTJCfBOQxYbjNG8CU";
        public const string
            IG_SIGNATURE_KEY =
              "be01114435207c0a0b11a5cf68faeb82ec4eee37c52e8429af5fff6b54b80b28";    
            //"b4946d296abf005163e72346a6d33dd083cadde638e6ad9c5eb92e381b35784a"; //4749bda4fc1f49372dae3d79db339ce4959cfbbe

        public const string HEADER_IG_SIGNATURE = "signed_body";
        public const string IG_SIGNATURE_KEY_VERSION = "4"; //5
        public const string HEADER_IG_SIGNATURE_KEY_VERSION = "ig_sig_key_version";
        public const string IG_CAPABILITIES = "3brTBw==";//"3boBAA==";
        public const string HEADER_IG_CAPABILITIES = "X-IG-Capabilities";
        public const string IG_CONNECTION_TYPE = "WIFI";
        public const string HEADER_IG_CONNECTION_TYPE = "X-IG-Connection-Type";
        public const string ACCEPT_LANGUAGE = "en-US";
        public const string HEADER_ACCEPT_LANGUAGE = "Accept-Language";
        public const string ACCEPT_ENCODING = "gzip, deflate, sdch";
        public const string HEADER_ACCEPT_ENCODING = "gzip, deflate, sdch";
        public const string TIMEZONE = "Pacific/Auckland";
        public const string HEADER_PHONE_ID = "phone_id";
        public const string HEADER_TIMEZONE = "timezone_offset";
        public const string HEADER_XGOOGLE_AD_IDE = "X-Google-AD-ID";
        public const string HEADER_IG_APP_ID = "X-IG-App-ID";
        public const string IG_APP_ID = "567067343352427";

        public const string COMMENT_BREADCRUMB_KEY = "iN4$aGr0m";
        public const int TIMEZONE_OFFSET = 43200;

        public const string INSTAGRAM_URL = "https://i.instagram.com";
        public const string API = "/api";
        public const string API_SUFFIX = API + API_VERSION;
        public const string API_VERSION = "/v1";
        public const string BASE_INSTAGRAM_API_URL = INSTAGRAM_URL + API_SUFFIX + "/";


        public const string CURRENTUSER = API_SUFFIX + "/accounts/current_user?edit=true";
        public const string SEARCH_TAGS = API_SUFFIX + "/tags/search/?q={0}&count={1}";
        public const string GET_TAG_INFO = API_SUFFIX + "/tags/{0}/info/";
        public const string SEARCH_USERS = API_SUFFIX + "/users/search";
        public const string GET_USER_INFO_BY_ID = API_SUFFIX + "/users/{0}/info/";
        public const string GET_USER_INFO_BY_USERNAME = API_SUFFIX + "/users/{0}/usernameinfo/";
        public const string ACCOUNTS_LOGIN = API_SUFFIX + "/accounts/login/";
        public const string ACCOUNTS_CREATE = API_SUFFIX + "/accounts/create/";
        public const string ACCOUNTS_2FA_LOGIN = API_SUFFIX + "/accounts/two_factor_login/";
        public const string ACCOUNTS_2FA_LOGIN_AGAIN = API_SUFFIX + "/accounts/send_two_factor_login_sms/";
        public const string ACCOUNTS_LOOKUP_PHONE = API_SUFFIX + "/users/lookup_phone/";
        public const string ACCOUNTS_SEND_RECOVERY_EMAIL = API_SUFFIX + "/accounts/send_recovery_flow_email/";
        public const string CHANGE_PASSWORD = API_SUFFIX + "/accounts/change_password/";
        public const string ACCOUNTS_LOGOUT = API_SUFFIX + "/accounts/logout/";
        public const string EXPLORE = API_SUFFIX + "/discover/explore/";
        public const string TIMELINEFEED = API_SUFFIX + "/feed/timeline";
        public const string USEREFEED = API_SUFFIX + "/feed/user/";
        public const string GET_USER_TAGS = API_SUFFIX + "/usertags/{0}/feed/";
        public const string GET_MEDIA = API_SUFFIX + "/media/{0}/info/";
        public const string GET_USER_FOLLOWERS = API_SUFFIX + "/friendships/{0}/followers/?rank_token={1}";
        public const string GET_USER_FOLLOWING = API_SUFFIX + "/friendships/{0}/following/?rank_token={1}";
        public const string GET_TAG_FEED = API_SUFFIX + "/feed/tag/{0}";
        public const string GET_RANKED_RECIPIENTS = API_SUFFIX + "/direct_v2/ranked_recipients";

        public const string GET_LIST_COLLECTIONS = API_SUFFIX + "/collections/list/";
        public const string GET_COLLECTION = API_SUFFIX + "/feed/collection/{0}/";
        public const string CREATE_COLLECTION = API_SUFFIX + "/collections/create/";
        public const string EDIT_COLLECTION = API_SUFFIX + "/collections/{0}/edit/";
        public const string DELETE_COLLECTION = API_SUFFIX + "/collections/{0}/delete/";
        public const string COLLECTION_CREATE_MODULE = API_SUFFIX + "collection_create";
        public const string FEED_SAVED_ADD_TO_COLLECTION_MODULE = "feed_saved_add_to_collection";

        public const string GET_MEDIAID = API_SUFFIX + "/oembed/?url={0}";
        public const string GET_SHARE_LINK = API_SUFFIX + "/media/{0}/permalink/";

        public const string GET_DIRECT_SHARE_USER = API_SUFFIX + "/direct_v2/threads/broadcast/profile/";
        public const string GET_RANK_RECIPIENTS_BY_USERNAME = API_SUFFIX + "/direct_v2/ranked_recipients/?mode=raven&show_threads=true&query={0}&use_unified_inbox=true";
        public const string GET_PARTICIPANTS_RECIPIENT_USER = API_SUFFIX + "/direct_v2/threads/get_by_participants/?recipient_users=[{0}]";
        public const string GET_RECENT_RECIPIENTS = API_SUFFIX + "/direct_share/recent_recipients/";
        public const string GET_DIRECT_THREAD = API_SUFFIX + "/direct_v2/threads/{0}";
        public const string GET_DIRECT_THREAD_APPROVE = API_SUFFIX + "/direct_v2/threads/{0}/approve/";
        public const string GET_DIRECT_THREAD_APPROVE_MULTIPLE = API_SUFFIX + "/direct_v2/threads/approve_multiple/";
        public const string GET_DIRECT_THREAD_DECLINE = API_SUFFIX + "/direct_v2/threads/{0}/decline/";
        public const string GET_DIRECT_THREAD_DECLINEALL = API_SUFFIX + "/direct_v2/threads/decline_all/";        
        public const string GET_DIRECT_THREAD_DECLINE_MULTIPLE = API_SUFFIX + "/direct_v2/threads/decline_multiple/";        
        public const string GET_DIRECT_INBOX = API_SUFFIX + "/direct_v2/inbox/";
        public const string GET_DIRECT_PENDING_INBOX = API_SUFFIX + "/direct_v2/pending_inbox/";
        public const string GET_DIRECT_TEXT_BROADCAST = API_SUFFIX + "/direct_v2/threads/broadcast/text/";
        public const string DIRECT_BROADCAST_UPLOAD_PHOTO = API_SUFFIX + "/direct_v2/threads/broadcast/upload_photo/";
        public const string DIRECT_BROADCAST_CONFIGURE_VIDEO = API_SUFFIX + "/direct_v2/threads/broadcast/configure_video/";
        public const string DIRECT_BROADCAST_PROFILE = API_SUFFIX + "/direct_v2/threads/broadcast/profile/";
        public const string DIRECT_BROADCAST_LINK = API_SUFFIX + "/direct_v2/threads/broadcast/link/";
        public const string DIRECT_BROADCAST_LOCATION = API_SUFFIX + "/direct_v2/threads/broadcast/location/";
        public const string DIRECT_BROADCAST_REACTION = API_SUFFIX + "/direct_v2/threads/broadcast/reaction/";
        public const string DIRECT_BROADCAST_MEDIA_SHARE = API_SUFFIX + "/direct_v2/threads/broadcast/media_share/?media_type={0}";
        public const string DIRECT_BROADCAST_REEL_SHARE = API_SUFFIX + "/direct_v2/threads/broadcast/reel_share/";

        public const string DIRECT_THREAD_ITEM_SEEN = API_SUFFIX + "/direct_v2/visual_threads/{0}/item_seen/";
        public const string DIRECT_THREAD_UPDATE_TITLE = API_SUFFIX + "/direct_v2/threads/{0}/update_title/";
        public const string DIRECT_THREAD_MUTE = API_SUFFIX + "/direct_v2/threads/{0}/mute/";
        public const string DIRECT_THREAD_UNMUTE = API_SUFFIX + "/direct_v2/threads/{0}/unmute/";
        public const string DIRECT_THREAD_LEAVE = API_SUFFIX + "/direct_v2/threads/{0}/leave/";
        /// <summary>
        /// post data:
        /// <para>use_unified_inbox=true</para>
        /// <para>recipient_users= user ids split with comma.: ["user id1","user id2","..."]</para>
        /// </summary>
        public const string DIRECT_CREATE_GROUP = API_SUFFIX + "/direct_v2/create_group_thread/";
        /// <summary>
        /// post data: _uid ro post nakon
        /// <para>use_unified_inbox=true</para>
        /// </summary>
        public const string DIRECT_THREAD_HIDE = API_SUFFIX + "/direct_v2/threads/{0}/hide/";
        public const string DIRECT_SHARE = API_SUFFIX + "/direct_share/inbox/";

        public const string GET_RECENT_ACTIVITY = API_SUFFIX + "/news/inbox/";
        public const string GET_FOLLOWING_RECENT_ACTIVITY = API_SUFFIX + "/news/";
        public const string LIKE_MEDIA = API_SUFFIX + "/media/{0}/like/";
        public const string UNLIKE_MEDIA = API_SUFFIX + "/media/{0}/unlike/";
        public const string MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/comments/?can_support_threading=true";
        public const string MEDIA_INLINE_COMMENTS = API_SUFFIX + "/media/{0}/comments/{1}/inline_child_comments/";

        public const string MEDIA_LIKERS = API_SUFFIX + "/media/{0}/likers/";
        public const string FOLLOW_USER = API_SUFFIX + "/friendships/create/{0}/";
        public const string UNFOLLOW_USER = API_SUFFIX + "/friendships/destroy/{0}/";
        public const string BLOCK_USER = API_SUFFIX + "/friendships/block/{0}/";
        public const string UNBLOCK_USER = API_SUFFIX + "/friendships/unblock/{0}/";
        public const string SET_ACCOUNT_PRIVATE = API_SUFFIX + "/accounts/set_private/";
        public const string SET_ACCOUNT_PUBLIC = API_SUFFIX + "/accounts/set_public/";
        public const string POST_COMMENT = API_SUFFIX + "/media/{0}/comment/";
        public const string ALLOW_MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/enable_comments/";
        public const string DISABLE_MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/disable_comments/";
        public const string MEDIA_COMMENT_LIKERS = API_SUFFIX + "/media/{0}/comment_likers/";
        public const string MEDIA_REPORT_COMMENT = API_SUFFIX + "/media/{0}/comment/{1}/flag/";
        public const string DELETE_COMMENT = API_SUFFIX + "/media/{0}/comment/{1}/delete/";
        public const string DELETE_MULTIPLE_COMMENT = API_SUFFIX + "/media/{0}/comment/bulk_delete/";
        public const string LIKE_COMMENT = API_SUFFIX + "/media/{0}/comment_like/";
        public const string UNLIKE_COMMENT = API_SUFFIX + "/media/{0}/comment_unlike/";
        public const string UPLOAD_PHOTO = API_SUFFIX + "/upload/photo/";
        public const string UPLOAD_VIDEO = API_SUFFIX + "/upload/video/";
        public const string MEDIA_CONFIGURE = API_SUFFIX + "/media/configure/";
        public const string MEDIA_ALBUM_CONFIGURE = API_SUFFIX + "/media/configure_sidecar/";
        public const string DELETE_MEDIA = API_SUFFIX + "/media/{0}/delete/?media_type={1}";
        public const string EDIT_MEDIA = API_SUFFIX + "/media/{0}/edit_media/";
        public const string SEEN_MEDIA = API_SUFFIX + "/media/seen/";
        public const string SEEN_MEDIA_STORY = SEEN_MEDIA + "?reel=1&live_vod=0";
        public const string GET_STORY_TRAY = API_SUFFIX + "/feed/reels_tray/";
        public const string GET_USER_STORY = API_SUFFIX + "/feed/user/{0}/reel_media/";
        public const string STORY_CONFIGURE = API_SUFFIX + "/media/configure_to_reel/";
        public const string STORY_CONFIGURE_VIDEO = API_SUFFIX + "/media/configure_to_story/?video=1";
        public const string STORY_CONFIGURE_VIDEO2 = API_SUFFIX + "/media/configure_to_story/";
        public const string STORY_SHARE = API_SUFFIX + "/direct_v2/threads/broadcast/story_share/?media_type={0}";
        public const string LOCATION_SEARCH = API_SUFFIX + "/location_search/";
        public const string FRIENDSHIPSTATUS = API_SUFFIX + "/friendships/show/";
        public const string LIKE_FEED = API_SUFFIX + "/feed/liked/";
        public const string USER_REEL_FEED = API_SUFFIX + "/feed/user/{0}/reel_media/";


        public const string ACCOUNTS_CHECK_PHONE_NUMBER = API_SUFFIX + "/accounts/check_phone_number/";
        public const string ACCOUNTS_SEND_SIGNUP_SMS_CODE = API_SUFFIX + "/accounts/send_signup_sms_code/";
        public const string ACCOUNTS_VALIDATE_SIGNUP_SMS_CODE = API_SUFFIX + "/accounts/validate_signup_sms_code/";
        public const string ACCOUNTS_USERNAME_SUGGESTIONS = API_SUFFIX + "/accounts/username_suggestions/";
        public const string ACCOUNTS_CREATE_VALIDATED = API_SUFFIX + "/accounts/create_validated/";
        public const string ACCOUNTS_REQUEST_PROFILE_EDIT = API_SUFFIX + "/accounts/current_user/?edit=true";
        public const string ACCOUNTS_EDIT_PROFILE = API_SUFFIX + "/accounts/edit_profile/";
        public const string ACCOUNTS_SET_PHONE_AND_NAME = API_SUFFIX + "/accounts/set_phone_and_name/";
        public const string ACCOUNTS_REMOVE_PROFILE_PICTURE = API_SUFFIX + "/accounts/remove_profile_picture/";
        public const string ACCOUNTS_CHANGE_PROFILE_PICTURE = API_SUFFIX + "/accounts/change_profile_picture/";
        public const string ACCOUNTS_DISABLE_SMS_TWO_FACTOR = API_SUFFIX + "/accounts/disable_sms_two_factor/";
        public const string ACCOUNTS_SEND_TWO_FACTOR_ENABLE_SMS = API_SUFFIX + "/accounts/send_two_factor_enable_sms/";
        public const string ACCOUNTS_ENABLE_SMS_TWO_FACTOR = API_SUFFIX + "/accounts/enable_sms_two_factor/";
        public const string ACCOUNTS_SECURITY_INFO = API_SUFFIX + "accounts/account_security_info/";
        public const string ACCOUNTS_SEND_CONFIRM_EMAIL = API_SUFFIX + "/accounts/send_confirm_email/";
        public const string ACCOUNTS_SEND_SMS_CODE = API_SUFFIX + "/accounts/send_sms_code/";
        public const string ACCOUNTS_VERIFY_SMS_CODE = API_SUFFIX + "/accounts/verify_sms_code/";
        public const string ACCOUNTS_SET_PRESENCE_DISABLED = API_SUFFIX + "/accounts/set_presence_disabled/";
        public const string ACCOUNTS_GET_COMMENT_FILTER = API_SUFFIX + "/accounts/get_comment_filter/";
        public const string ACCOUNTS_REGEN_BACKUP_CODES = API_SUFFIX + "/accounts/regen_backup_codes/";
        public const string ACCOUNTS_SET_BIOGRAPHY = API_SUFFIX + "/accounts/set_biography/";
        public const string ACCOUNTS_READ_MSISDN_HEADER = API_SUFFIX + "/accounts/read_msisdn_header/";
        public const string ACCOUNTS_CONTACT_POINT_PREFILL = API_SUFFIX + "/accounts/contact_point_prefill/";

        public const string USER_CHECK_EMAIL = API_SUFFIX + "/users/check_email/";
        public const string USER_REEL_SETTINGS = API_SUFFIX + "/users/reel_settings/";
        public const string USER_SET_REEL_SETTINGS = API_SUFFIX + "/users/set_reel_settings/";
        public const string USER_CHECK_USERNAME = API_SUFFIX + "/users/check_username/";
        public const string USER_SEARCH = API_SUFFIX + "/users/search/?timezone_offset={0}&q={1}&count={2}";


        public const string FBSEARCH_RECENT_SEARCHES = API_SUFFIX + "/fbsearch/recent_searches/";
        public const string FBSEARCH_CLEAR_SEARCH_HISTORY = API_SUFFIX + "/fbsearch/clear_search_history";
        public const string FBSEARCH_SUGGESTED_SEARCHS = API_SUFFIX + "/fbsearch/suggested_searches/?type={0}";
        public const string FBSEARCH_PROFILE_SEARCH = API_SUFFIX + "/fbsearch/profile_link_search/?q={0}&count={1}";
        public const string FBSEARCH_TOPSEARCH_FALT = API_SUFFIX + "/fbsearch/topsearch_flat/";
        public const string FBSEARCH_GET_HIDDEN_SEARCH_ENTITIES = API_SUFFIX + "/fbsearch/get_hidden_search_entities/";
        /// <summary>
        /// get nearby places
        /// </summary>
        public const string FBSEARCH_PLACES = API_SUFFIX + "/fbsearch/places/?timezone_offset=43200&lat={0}&lng={1}";
        /// <summary>
        /// get search places
        /// </summary>
        public const string FBSEARCH_PLACES_QUERY = API_SUFFIX + "/fbsearch/places/?timezone_offset=43200&lat={0}&lng={1}&query={2}&rank_token={3}";
        /// <summary>
        /// post data:
        /// <para>section=suggested</para>
        /// <para>user=["1 user id"]</para>
        /// </summary>
        public const string FBSEARCH_HIDE_SEARCH_ENTITIES = API_SUFFIX + "/fbsearch/hide_search_entities/";

        public const string FBSEARCH_TOPSEARCH = API_SUFFIX + "/fbsearch/topsearch/";


        public const string DISCOVER_AYML = API_SUFFIX + "/discover/ayml/";
        public const string DISCOVER_TOP_LIVE = API_SUFFIX + "/discover/top_live/";
        public const string DISCOVER_TOP_LIVE_STATUS = API_SUFFIX + "/discover/top_live_status/";

        public const string DISCOVER_CHAINING = API_SUFFIX + "/discover/chaining/?target_id={0}";

        public const string FRIENDSHIPS_APPROVE = API_SUFFIX + "/friendships/approve/{0}/";
        public const string FRIENDSHIPS_IGNORE = API_SUFFIX + "/friendships/ignore/{0}/";
        public const string FRIENDSHIPS_PENDING_REQUESTS = API_SUFFIX + "/friendships/pending/?rank_mutual=0&rank_token={0}";
        /// <summary>
        /// get friendship status
        /// <para>post data:</para>
        /// <para>include_reel_info = 0</para>
        /// <para>user_ids = user ids(PK) split with comma</para>
        /// </summary>
        public const string FRIENDSHIPS_SHOW_MANY = API_SUFFIX + "/friendships/show_many/";
        /// <summary>
        /// mute friend reel
        /// <para>{0}: user id</para>
        /// <para>post data:</para>
        /// <para>tray_position=13</para>
        /// <para>reel_type=story</para>
        /// </summary>
        public const string FRIENDSHIPS_MUTE_FRIEND_REEL = API_SUFFIX + "/friendships/mute_friend_reel/{0}/";
        /// <summary>
        /// post method
        /// </summary>
        public const string FRIENDSHIPS_FAVORITE = API_SUFFIX + "/friendships/favorite/{0}/";
        /// <summary>
        /// post method
        /// </summary>
        public const string FRIENDSHIPS_UNFAVORITE = API_SUFFIX + "/friendships/unfavorite/{0}/";
        /// <summary>
        /// unhide your stories from a specific user
        /// </summary>
        public const string FRIENDSHIPS_UNBLOCK_FRIEND_REEL = API_SUFFIX + "/friendships/unblock_friend_reel/{0}";
        /// <summary>
        /// hide your stories from specific users
        /// </summary>
        public const string FRIENDSHIPS_SET_REEL_BLOCK_STATUS = API_SUFFIX + "/friendships/set_reel_block_status/";
        /// <summary>
        /// get blocked users from self stories
        /// </summary>
        public const string FRIENDSHIPS_BLOCKED_REEL = API_VERSION + "/friendships/blocked_reels/";
        public const string FRIENDSHIPS_REMOVE_FOLLOWER = API_SUFFIX + "/friendships/remove_follower/";

        public const string LIVE_HEARTBEAT_AND_GET_VIEWER_COUNT = API_SUFFIX + "/live/{0}/heartbeat_and_get_viewer_count/";
        public const string LIVE_GET_FINAL_VIEWER_LIST = API_SUFFIX + "/live/{0}/get_final_viewer_list/";
        public const string LIVE_GET_LIVE_PRESENCE = API_SUFFIX + "/live/get_live_presence/?presence_type=30min";
        public const string LIVE_GET_SUGGESTED_BROADCASTS = API_SUFFIX + "/live/get_suggested_broadcasts/";
        public const string LIVE_INFO = API_SUFFIX + "/live/{0}/info/";
        public const string LIVE_GET_VIEWER_LIST = API_SUFFIX + "/live/{0}/get_viewer_list/";
        public const string LIVE_GET_POST_LIVE_VIEWERS_LIST = API_SUFFIX + "/live/{0}/get_post_live_viewers_list/";
        public const string LIVE_COMMENT = API_SUFFIX + "/live/{0}/comment/";
        public const string LIVE_PIN_COMMENT = API_SUFFIX + "/live/{0}/pin_comment/";
        public const string LIVE_UNPIN_COMMENT = API_SUFFIX + "/live/{0}/unpin_comment/";
        public const string LIVE_GET_COMMENT = API_SUFFIX + "/live/{0}/get_comment/";
        public const string LIVE_GET_POST_LIVE_COMMENT = API_SUFFIX + "/live/{0}/get_post_live_comments/?starting_offset={1}&encoding_tag={2}";
        public const string LIVE_UNMUTE_COMMENTS = API_SUFFIX + "/live/{0}/unmute_comment/";
        public const string LIVE_MUTE_COMMENTS = API_SUFFIX + "/live/{0}/mute_comment/";
        public const string LIVE_LIKE = API_SUFFIX + "/live/{0}/like/";
        public const string LIVE_GET_LIKE_COUNT = API_SUFFIX + "/live/{0}/get_like_count/";
        public const string LIVE_POST_LIVE_LIKES = API_SUFFIX + "/live/{0}/get_post_live_likes/?starting_offset={1}&encoding_tag={2}";
        public const string LIVE_ADD_TO_POST_LIVE = API_SUFFIX + "/live/{0}/add_to_post_live/";
        public const string LIVE_DELETE_POST_LIVE = API_SUFFIX + "/live/{0}/delete_post_live/";
        public const string LIVE_CREATE = API_SUFFIX + "/live/create/";
        public const string LIVE_START = API_SUFFIX + "/live/{0}/start/";
        public const string LIVE_END = API_SUFFIX + "/live/{0}/end_broadcast/";
        public const string LIVE_GET_JOIN_REQUESTS = API_SUFFIX + "/live/{0}/get_join_requests/";

        public const string STORY_MEDIA_INFO_UPLOAD = API_SUFFIX + "/media/mas_opt_in_info/";
        public const string STORY_UPLOAD_VIDEO = INSTAGRAM_URL + "/rupload_igvideo/{0}_0_{1}";
        public const string STORY_UPLOAD_PHOTO = INSTAGRAM_URL + "/rupload_igphoto/{0}_0_{1}";
        public const string DYNAMIC_ONBOARDING_GET_STEPS = API_SUFFIX + "/dynamic_onboarding/get_steps/";

        public const string HIGHLIGHT_TRAY = API_SUFFIX + "/highlights/{0}/highlights_tray/";
        public const string HIGHLIGHT_EDIT_REEL = API_SUFFIX + "/highlights/{0}/edit_reel/";
        public const string HIGHLIGHT_CREATE_REEL = API_SUFFIX + "/highlights/create_reel/";
        public const string HIGHLIGHT_DELETE_REEL = API_SUFFIX + "/highlights/{0}/delete_reel/";
        public const string FB_FACEBOOK_SIGNUP = API_SUFFIX + "/fb/facebook_signup/";


        public const string IGTV_TV_GUIDE = API_SUFFIX + "/igtv/tv_guide/";
        public const string IGTV_CHANNEL = API_SUFFIX + "/igtv/channel/";
        public const string IGTV_SEARCH = API_SUFFIX + "/igtv/search/?query={0}";
        public const string IGTV_SUGGESTED_SEARCHES = API_SUFFIX + "/igtv/suggested_searches/";
        public const string MEDIA_CONFIGURE_TO_IGTV = API_SUFFIX + "/media/configure_to_igtv/";


        public const string FRIENDSHIP_AUTOCOMPLETE_USER_LIST = API_SUFFIX + "/friendships/autocomplete_user_list/";


        public const string NOTIFICATION_BADGE = API_SUFFIX + "/notifications/badge/";
        public const string PUSH_REGISTER = API_SUFFIX + "/push/register/";

        /// <summary>
        /// queries:
        /// <para>visited = [{"id":"TAG ID","type":"hashtag"}]</para>
        /// <para>related_types = ["location","hashtag"]</para>
        /// </summary>
        public const string TAG_RELATED = API_SUFFIX + "/tags/{0}/related/";
        /// <summary>
        /// post params:
        /// <para>"action":"click"</para>
        /// </summary>
        public const string NEWS_LOG = API_SUFFIX + "/news/log/";
        /// <summary>
        /// get
        /// </summary>
        public const string USERS_FOLLOWING_TAG_INFO = API_SUFFIX + "/users/{0}/following_tags_info/";
        /// <summary>
        /// user id 
        /// </summary>
        public const string USERS_FULL_DETAIL_INFO = API_SUFFIX + "/users/{0}/full_detail_info/";
        /// <summary>
        /// get
        /// </summary>
        public const string FEED_SAVED = API_SUFFIX + "/feed/saved/";
        /// <summary>
        /// {0} = rank token <<<<< this endpoint is deprecated
        /// </summary>
        public const string FEED_POPULAR = API_VERSION + "/feed/popular/?people_teaser_supported=1&rank_token={0}&ranked_content=true";
        /// <summary>
        /// {0} media id, {1} user pk
        /// <para>post data:</para>
        /// <para>module_name=single_feed_profile</para>
        /// </summary>
        public const string MEDIA_SAVE = API_SUFFIX + "/media/{0}_{1}/save/";
        /// <summary>
        /// {0} media id, {1} user pk
        /// <para>post data:</para>
        /// <para>module_name=single_feed_profile</para>
        /// </summary>
        public const string MEDIA_UNSAVE = API_SUFFIX + "/media/{0}_{1}/unsave/";
        /// <summary>
        ///  get media infos
        ///  <para>get method | queries:</para>
        ///  <para>_uuid</para>
        ///  <para>_csrftoken</para>
        ///  <para>media_ids= split with comma</para>
        ///  <para>ranked_content=true</para>
        ///  <para>include_inactive_reel=true</para>
        /// </summary>
        public const string MEDIA_INFOS = API_SUFFIX + "/media/infos/";

        public const string MEGAPHONE_LOG = API_SUFFIX + "/megaphone/log/";

        public const string QE_EXPOSE = API_SUFFIX + "/qe/expose/";
        /// <summary>
        /// {0} => external id
        /// </summary>
        public const string LOCATIONS_INFO = API_SUFFIX + "/locations/{0}/info/";
        /// <summary>
        /// {0} => external id
        /// </summary>
        public const string LOCATIONS_RELEATED = API_SUFFIX + "/locations/{0}/related/";

        public const string LANGUAGE_TRANSLATE = API_SUFFIX + "/language/translate/";
        public const string LANGUAGE_TRANSLATE_MULTIPLE = API_SUFFIX + "/language/bulk_translate/";

        public const string DYI_REQUEST_DOWNLOAD_DATA = API_SUFFIX + "/dyi/request_download_data/";


        public static readonly Uri BaseInstagramUri = new Uri(BASE_INSTAGRAM_API_URL);
    }
}