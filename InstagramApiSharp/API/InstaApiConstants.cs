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

        public const string USER_AGENT =
            "Instagram 35.0.0.20.96  Android (24/7.0; {0}; {1}; {2}; {3}; {4}; {5}; en_US)";
        //{dpi}; {resolution}; {devicebrand}; {devicemodelindentifier}; {FirmwareBrand}; {hardwaremodel}; en_US
        ////"Instagram 35.0.0.20.96 Android (24/7.0; 480dpi; 1080x1812; HUAWEI/HONOR; PRA-LA1; HWPRA-H; hi6250; en_US; 95414346)";
        public const string USER_AGENT_DEFAULT =
        "Instagram 35.0.0.20.96  Android (24/7.0; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_US)";
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
        public const string GET_DIRECT_THREAD_DECLINEALL = API_SUFFIX + "/direct_v2/threads/decline_all/";
        public const string GET_DIRECT_INBOX = API_SUFFIX + "/direct_v2/inbox/";
        public const string GET_DIRECT_PENDING_INBOX = API_SUFFIX + "/direct_v2/pending_inbox/";
        public const string GET_DIRECT_TEXT_BROADCAST = API_SUFFIX + "/direct_v2/threads/broadcast/text/";
        public const string GET_RECENT_ACTIVITY = API_SUFFIX + "/news/inbox/";
        public const string GET_FOLLOWING_RECENT_ACTIVITY = API_SUFFIX + "/news/";
        public const string LIKE_MEDIA = API_SUFFIX + "/media/{0}/like/";
        public const string UNLIKE_MEDIA = API_SUFFIX + "/media/{0}/unlike/";
        public const string MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/comments/";
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
        public const string UPLOAD_PHOTO = API_SUFFIX + "/upload/photo/";
        public const string UPLOAD_VIDEO = API_SUFFIX + "/upload/video/";
        public const string MEDIA_CONFIGURE = API_SUFFIX + "/media/configure/";
        public const string MEDIA_ALBUM_CONFIGURE = API_SUFFIX + "/media/configure_sidecar/";
        public const string DELETE_MEDIA = API_SUFFIX + "/media/{0}/delete/?media_type={1}";
        public const string EDIT_MEDIA = API_SUFFIX + "/media/{0}/edit_media/";
        public const string GET_STORY_TRAY = API_SUFFIX + "/feed/reels_tray/";
        public const string GET_USER_STORY = API_SUFFIX + "/feed/user/{0}/reel_media/";
        public const string STORY_CONFIGURE = API_SUFFIX + "/media/configure_to_reel/";
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

        public const string USER_CHECK_EMAIL = API_SUFFIX + "/users/check_email/";
        public const string USER_REEL_SETTINGS = API_SUFFIX + "/users/reel_settings/";
        public const string USER_SET_REEL_SETTINGS = API_SUFFIX + "/users/set_reel_settings/";
        public const string USER_CHECK_USERNAME = API_SUFFIX + "/users/check_username/";
        public const string USER_SEARCH = API_SUFFIX + "/users/search/?timezone_offset={0}&q={1}&count={2}";


        public const string FBSEARCH_RECENT_SEARCHES = API_SUFFIX + "/fbsearch/recent_searches/";
        public const string FBSEARCH_CLEAR_SEARCH_HISTORY = API_SUFFIX + "/fbsearch/clear_search_history";
        public const string FBSEARCH_SUGGESTED_SEARCHS = API_SUFFIX + "/fbsearch/suggested_searches/?type={0}";


        public const string DISCOVER_AYML = API_SUFFIX + "/discover/ayml/";
        public const string DISCOVER_TOP_LIVE = API_SUFFIX + "/discover/top_live/";
        public const string DISCOVER_TOP_LIVE_STATUS = API_SUFFIX + "/discover/top_live_status/";


        public const string FRIENDSHIPS_APPROVE = API_SUFFIX + "/friendships/approve/{0}";
        public const string FRIENDSHIPS_IGNORE = API_SUFFIX + "/friendships/ignore/{0}";
        public const string FRIENDSHIPS_PENDING_REQUESTS = API_SUFFIX + "/friendships/pending/?rank_mutual=0&rank_token={0}";



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

        public const string DYNAMIC_ONBOARDING_GET_STEPS = API_SUFFIX + "/dynamic_onboarding/get_steps/";

        public const string FB_FACEBOOK_SIGNUP = API_SUFFIX + "/fb/facebook_signup/";

        public static readonly Uri BaseInstagramUri = new Uri(BASE_INSTAGRAM_API_URL);
    }
}