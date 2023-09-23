/*  
 *  
 *  
 *  All endpoints and headers is here
 *  
 *  
 *        IRANIAN DEVELOPERS
 *        
 *        
 *               2018
 *  
 *  
 */

using Newtonsoft.Json.Linq;
using System;

namespace InstagramApiSharp.API
{
    /// <summary>
    ///     Place of every endpoints, headers and other constants and variables.
    /// </summary>
    internal static class InstaApiConstants
    {
        #region New

        public const string CONSENT_GET_SIGNUP_CONFIG = API_SUFFIX + "/consent/get_signup_config/";
        public const string ACCOUNTS_SEND_VERIFY_EMAIL = API_SUFFIX + "/accounts/send_verify_email/";
        public const string ACCOUNTS_CHECK_CONFIRMATION_CODE = API_SUFFIX + "/accounts/check_confirmation_code/";
        public const string SI_FETCH_HEADERS = API_SUFFIX + "/si/fetch_headers/";
        public const string CONSENT_CHECK_AGE_ELIGIBILITY = API_SUFFIX + "/consent/check_age_eligibility/";
        public const string SIGNUP_EXPERIMENTS_CONFIGS = "ig_android_video_raven_streaming_upload_universe,ig_android_vc_explicit_intent_for_notification,ig_android_shopping_checkout_signaling,ig_stories_ads_delivery_rules,ig_shopping_checkout_improvements_universe,ig_business_new_value_prop_universe,ig_android_mqtt_cookie_auth_memcache_universe,ig_android_product_tag_suggestions,ig_android_gallery_grid_controller_folder_cache,ig_android_suggested_users_background,ig_android_stories_music_search_typeahead,ig_android_direct_mutation_manager_media_3,ig_android_ads_bottom_sheet_report_flow,ig_android_login_onetap_upsell_universe,ig_fb_graph_differentiation,ig_android_shopping_bag_null_state_v1,ig_camera_android_feed_effect_attribution_universe,ig_android_test_not_signing_address_book_unlink_endpoint,ig_android_stories_share_extension_video_segmentation,ig_android_search_nearby_places_universe,ig_graph_management_production_h2_2019_holdout_universe,ig_android_vc_migrate_to_bluetooth_v2_universe,ig_ei_option_setting_universe,ig_android_test_remove_button_main_cta_self_followers_universe,instagram_ns_qp_prefetch_universe,ig_android_camera_leak,ig_android_wellbeing_support_frx_live_reporting,ig_android_separate_empty_feed_su_universe,ig_stories_rainbow_ring,ig_android_zero_rating_carrier_signal,ig_explore_2019_h1_destination_cover,ig_android_explore_recyclerview_universe,ig_android_image_pdq_calculation,ig_camera_android_subtle_filter_universe,ig_android_whats_app_contact_invite_universe,ig_android_direct_add_member_dialog_universe,ig_android_xposting_reel_memory_share_universe,ig_android_viewpoint_stories_public_testing,ig_graph_management_h2_2019_universe,ig_android_photo_creation_large_width,ig_android_save_all,ig_android_video_upload_hevc_encoding_universe,instagram_shopping_hero_carousel_visual_variant_consolidation,ig_android_vc_face_effects_universe,ig_android_fbpage_on_profile_side_tray,ig_android_ttcp_improvements,ig_android_igtv_refresh_tv_guide_interval,ig_shopping_bag_universe,ig_android_recyclerview_binder_group_enabled_universe,ig_android_video_exoplayer_2,ig_rn_branded_content_settings_approval_on_select_save,ig_android_account_insights_shopping_content_universe,ig_branded_content_tagging_approval_request_flow_brand_side_v2,ig_android_render_thread_memory_leak_holdout,ig_threads_clear_notifications_on_has_seen,ig_android_xposting_dual_destination_shortcut_fix,ig_android_wellbeing_support_frx_profile_reporting,ig_android_show_create_content_pages_universe,ig_android_camera_reduce_file_exif_reads,ig_android_disk_usage_logging_universe,ig_android_stories_blacklist,ig_payments_billing_address,ig_android_fs_new_gallery_hashtag_prompts,ig_android_video_product_specific_abr,ig_android_sidecar_segmented_streaming_universe,ig_camera_android_gyro_senser_sampling_period_universe,ig_android_xposting_feed_to_stories_reshares_universe,ig_android_stories_layout_universe,ig_emoji_render_counter_logging_universe,ig_android_wellbeing_support_frx_feed_posts_reporting,ig_android_sharedpreferences_qpl_logging,ig_android_vc_cpu_overuse_universe,ig_android_image_upload_quality_universe,ig_android_invite_list_button_redesign_universe,ig_android_react_native_email_sms_settings_universe,ig_android_enable_zero_rating,ig_android_direct_leave_from_group_message_requests,ig_android_unfollow_reciprocal_universe,ig_android_publisher_stories_migration,aymt_instagram_promote_flow_abandonment_ig_universe,ig_android_whitehat_options_universe,ig_android_stories_context_sheets_universe,ig_android_stories_vpvd_container_module_fix,ig_android_delete_ssim_compare_img_soon,instagram_android_profile_follow_cta_context_feed,ig_android_personal_user_xposting_destination_fix,ig_android_stories_boomerang_v2_universe,ig_android_direct_message_follow_button,ig_android_video_raven_passthrough,ig_android_vc_cowatch_universe,ig_shopping_insights_wc_copy_update_android,android_camracore_fbaudio_integration_ig_universe,ig_stories_ads_media_based_insertion,ig_android_analytics_background_uploader_schedule,ig_android_explore_reel_loading_state,ig_android_wellbeing_timeinapp_v1_universe,ig_end_of_feed_universe,ig_android_mainfeed_generate_prefetch_background,ig_android_feed_ads_ppr_universe,ig_android_igtv_browse_long_press,ig_xposting_mention_reshare_stories,ig_threads_sanity_check_thread_viewkeys,ig_android_vc_shareable_moments_universe,ig_android_igtv_stories_preview,ig_android_shopping_product_metadata_on_product_tiles_universe,ig_android_stories_quick_react_gif_universe,ig_android_video_qp_logger_universe,ig_android_stories_weblink_creation,ig_android_story_bottom_sheet_top_clips_mas,ig_android_frx_highlight_cover_reporting_qe,ig_android_vc_capture_universe,ig_android_optic_face_detection,ig_android_save_to_collections_flow,ig_android_direct_segmented_video,ig_android_stories_video_prefetch_kb,ig_android_direct_mark_as_read_notif_action,ig_android_not_interested_secondary_options,ig_android_product_breakdown_post_insights,ig_inventory_connections,ig_android_canvas_cookie_universe,ig_android_video_streaming_upload_universe,ig_android_wellbeing_support_frx_stories_reporting,ig_android_smplt_universe,ig_android_vc_missed_call_call_back_action_universe,ig_cameracore_android_new_optic_camera2,ig_android_partial_share_sheet,ig_android_secondary_inbox_universe,ig_android_fbc_upsell_on_dp_first_load,ig_android_stories_sundial_creation_universe,saved_collections_cache_universe,ig_android_show_self_followers_after_becoming_private_universe,ig_android_music_story_fb_crosspost_universe,ig_android_payments_growth_promote_payments_in_payments,ig_carousel_bumped_organic_impression_client_universe,ig_android_business_attribute_sync,ig_biz_post_approval_nux_universe,ig_camera_android_bg_processor,ig_android_ig_personal_account_to_fb_page_linkage_backfill,ig_android_dropframe_manager,ig_android_ad_stories_scroll_perf_universe,ig_android_persistent_nux,ig_android_crash_fix_detach_from_gl_context,ig_android_tango_cpu_overuse_universe,ig_android_direct_wellbeing_message_reachability_settings,ig_android_edit_location_page_info,ig_android_unfollow_from_main_feed_v2,ig_android_stories_project_eclipse,ig_direct_android_bubble_system,ig_android_self_story_setting_option_in_menu,ig_android_frx_creation_question_responses_reporting,ig_android_li_session_chaining,ig_android_create_mode_memories_see_all,ig_android_feed_post_warning_universe,ig_mprotect_code_universe,ig_android_video_visual_quality_score_based_abr,ig_explore_2018_post_chaining_account_recs_dedupe_universe,ig_android_view_info_universe,ig_android_camera_upsell_dialog,ig_android_business_transaction_in_stories_consumer,ig_android_dead_code_detection,ig_android_stories_video_seeking_audio_bug_fix,ig_android_qp_kill_switch,ig_android_new_follower_removal_universe,ig_android_feed_post_sticker,ig_android_business_cross_post_with_biz_id_infra,ig_android_inline_editing_local_prefill,ig_android_reel_tray_item_impression_logging_viewpoint,ig_android_story_bottom_sheet_music_mas,ig_android_video_abr_universe,ig_android_unify_graph_management_actions,ig_android_vc_cowatch_media_share_universe,ig_challenge_general_v2,ig_android_place_signature_universe,ig_android_direct_inbox_cache_universe,ig_android_business_promote_tooltip,ig_android_wellbeing_support_frx_hashtags_reporting,ig_android_wait_for_app_initialization_on_push_action_universe,ig_android_direct_aggregated_media_and_reshares,ig_camera_android_facetracker_v12_universe,ig_android_story_bottom_sheet_clips_single_audio_mas,ig_android_fb_follow_server_linkage_universe,igqe_pending_tagged_posts,ig_sim_api_analytics_reporting,ig_android_self_following_v2_universe,ig_android_interest_follows_universe,ig_android_direct_view_more_qe,ig_android_audience_control,ig_android_memory_use_logging_universe,ig_android_branded_content_tag_redesign_organic,ig_camera_android_paris_filter_universe,ig_android_igtv_whitelisted_for_web,ig_rti_inapp_notifications_universe,ig_android_vc_join_timeout_universe,ig_android_share_publish_page_niverse,ig_direct_max_participants,ig_commerce_platform_ptx_bloks_universe,ig_android_video_raven_bitrate_ladder_universe,ig_android_live_realtime_comments_universe,ig_android_recipient_picker,ig_android_graphql_survey_new_proxy_universe,ig_android_music_browser_redesign,ig_android_disable_manual_retries,ig_android_qr_code_nametag,ig_android_purx_native_checkout_universe,ig_android_fs_creation_flow_tweaks,ig_android_apr_lazy_build_request_infra,ig_android_business_transaction_in_stories_creator,ig_cameracore_android_new_optic_camera2_galaxy,ig_android_wellbeing_support_frx_igtv_reporting,ig_android_branded_content_appeal_states,ig_android_claim_location_page,ig_android_location_integrity_universe,ig_video_experimental_encoding_consumption_universe,ig_android_biz_story_to_fb_page_improvement,ig_shopping_checkout_improvements_v2_universe,ig_android_direct_thread_target_queue_universe,ig_android_save_to_collections_bottom_sheet_refactor,ig_android_branded_content_insights_disclosure,ig_android_create_mode_tap_to_cycle,ig_android_fb_profile_integration_universe,ig_android_shopping_bag_optimization_universe,ig_android_create_page_on_top_universe,android_ig_cameracore_aspect_ratio_fix,ig_android_feed_auto_share_to_facebook_dialog,ig_android_skip_button_content_on_connect_fb_universe,ig_android_igtv_explore2x2_viewer,ig_android_network_perf_qpl_ppr,ig_android_wellbeing_support_frx_comment_reporting,ig_android_insights_post_dismiss_button,ig_xposting_biz_feed_to_story_reshare,ig_android_user_url_deeplink_fbpage_endpoint,ig_android_comment_warning_non_english_universe,ig_android_wellbeing_support_frx_cowatch_reporting,ig_android_stories_question_sticker_music_format,ig_promote_interactive_poll_sticker_igid_universe,ig_android_feed_cache_update,ig_pacing_overriding_universe,ig_explore_reel_ring_universe,ig_android_explore_peek_redesign_universe,ig_android_igtv_pip,ig_graph_evolution_holdout_universe,ig_android_wishlist_reconsideration_universe,ig_android_sso_use_trustedapp_universe,ig_android_stories_music_lyrics,ig_android_camera_formats_ranking_universe,ig_android_direct_multi_upload_universe,ig_android_stories_music_awareness_universe,ig_explore_2019_h1_video_autoplay_resume,ig_android_video_upload_quality_qe1,ig_android_expanded_xposting_upsell_directly_after_sharing_story_universe,ig_android_country_code_fix_universe,ig_android_stories_music_overlay,ig_android_multi_thread_sends,ig_android_render_output_surface_timeout_universe,ig_android_emoji_util_universe_3,ig_android_shopping_pdp_post_purchase_sharing,ig_branded_content_settings_unsaved_changes_dialog,ig_android_realtime_mqtt_logging,ig_android_rainbow_hashtags,ig_android_create_mode_templates,ig_android_direct_block_from_group_message_requests,ig_android_live_subscribe_user_level_universe,ig_android_video_call_finish_universe,ig_android_viewpoint_occlusion,ig_biz_growth_insights_universe,ig_android_logged_in_delta_migration,ig_android_push_reliability_universe,ig_android_self_story_button_non_fbc_accounts,ig_android_stories_gallery_video_segmentation,ig_android_explore_discover_people_entry_point_universe,ig_android_action_sheet_migration_universe,ig_android_live_webrtc_livewith_params,ig_camera_android_effect_metadata_cache_refresh_universe,ig_android_xposting_upsell_directly_after_sharing_to_story,ig_android_vc_codec_settings,ig_android_appstate_logger,ig_android_dual_destination_quality_improvement,ig_prefetch_scheduler_backtest,ig_android_ads_data_preferences_universe,ig_payment_checkout_cvv,ig_android_vc_background_call_toast_universe,ig_android_fb_link_ui_polish_universe,ig_android_qr_code_scanner,ig_disable_fsync_universe,mi_viewpoint_viewability_universe,ig_android_live_egl10_compat,ig_android_camera_gyro_universe,ig_android_video_upload_transform_matrix_fix_universe,ig_android_fb_url_universe,ig_android_reel_raven_video_segmented_upload_universe,ig_android_fb_sync_options_universe,ig_android_stories_gallery_sticker_universe,ig_android_recommend_accounts_destination_routing_fix,ig_android_enable_automated_instruction_text_ar,ig_traffic_routing_universe,ig_storiesallow_camera_actions_while_recording,ig_shopping_checkout_mvp_experiment,ig_android_video_fit_scale_type_igtv,ig_android_wellbeing_support_frx_friction_process_education,ig_android_direct_state_observer,ig_android_igtv_player_follow_button,ig_android_arengine_remote_scripting_universe,ig_android_page_claim_deeplink_qe,ig_android_logging_metric_universe_v2,ig_android_xposting_newly_fbc_people,ig_android_recognition_tracking_thread_prority_universe,ig_android_contact_point_upload_rate_limit_killswitch,ig_android_optic_photo_cropping_fixes,ig_android_qpl_class_marker,ig_camera_android_gallery_search_universe,ig_android_sso_kototoro_app_universe,ig_android_vc_cowatch_config_universe,ig_android_profile_thumbnail_impression,ig_android_fs_new_gallery,ig_android_media_remodel,ig_camera_android_share_effect_link_universe,ig_android_igtv_autoplay_on_prepare,ig_android_ads_rendering_logging,ig_shopping_size_selector_redesign,ig_android_image_exif_metadata_ar_effect_id_universe,ig_android_optic_new_architecture,ig_android_external_gallery_import_affordance,ig_search_hashtag_content_advisory_remove_snooze,ig_android_on_notification_cleared_async_universe,ig_android_direct_new_gallery,ig_payment_checkout_info";

        #endregion New

        #region B Url endpoints

        public const string MULTIPLE_ACCOUNTS_GET_ACCOUNT_FAMILY = API_SUFFIX + "/multiple_accounts/get_account_family/";
        public const string ZR_TOKEN_RESULT = API_SUFFIX + "/zr/token/result/";
        public const string NUX_NEW_ACCOUNT_NUX_SEEN = API_SUFFIX + "/nux/new_account_nux_seen/";

        #endregion B Url endpoints

        #region Main
        public const string RESPONSE_HEADER_IG_PASSWORD_ENC_PUB_KEY = "ig-set-password-encryption-pub-key";
        public const string RESPONSE_HEADER_IG_PASSWORD_ENC_KEY_ID = "ig-set-password-encryption-key-id";
        public const string RESPONSE_HEADER_IG_SET_X_MID = "ig-set-x-mid";
        public const string RESPONSE_HEADER_X_IG_ORIGIN_REGION = "x-ig-origin-region";
        public const string HEADER_IG_U_DIRECT_REGION_HINT = "ig-u-ig-direct-region-hint";
        public const string HEADER_IG_U_SHBID = "ig-u-shbid";
        public const string HEADER_IG_U_SHBTS = "ig-u-shbts";
        public const string HEADER_IG_U_DS_USER_ID = "ig-u-ds-user-id";
        public const string HEADER_IG_U_RUR = "ig-u-rur";
        public const string COOKIES_IG_DIRECT_REGION_HINT = "ig_direct_region_hint";
        public const string COOKIES_SHBTS = "shbts";
        public const string COOKIES_SHBID = "shbid";
        public const string COOKIES_RUR = "rur";

        public const string RESPONSE_HEADER_U_DIRECT_REGION_HINT = "ig-set-ig-u-ig-direct-region-hint";
        public const string RESPONSE_HEADER_U_SHBID = "ig-set-ig-u-shbid";
        public const string RESPONSE_HEADER_U_SHBTS = "ig-set-ig-u-shbts";
        public const string RESPONSE_HEADER_U_RUR = "ig-set-ig-u-rur";

        public const string HEADER_BEARER_IGT_2_VALUE = "Bearer IGT:2:";

        public const string HEADER_X_IG_BLOKS_PANORAMA_ENABLED = "X-Bloks-Is-Panorama-Enabled";
        public const string HEADER_X_IG_MAPPED_LOCALE = "X-IG-Mapped-Locale";
        public const string HEADER_X_IG_TIGON_RETRY = "X-Tigon-Is-Retry";
        public const string HEADER_X_FB_HTTP_IP = "X-FB-CLIENT-IP";
        public const string HEADER_X_FB_SERVER_CLUSTER = "X-FB-SERVER-CLUSTER";

        public const string ACCEPT_ENCODING2 = "gzip, deflate";
        public const string HEADER_PIGEON_SESSION_ID = "X-Pigeon-Session-Id";
        public const string HEADER_PIGEON_RAWCLINETTIME = "X-Pigeon-Rawclienttime";
        public const string HEADER_X_IG_CONNECTION_SPEED = "X-IG-Connection-Speed";
        public const string HEADER_X_IG_BANDWIDTH_SPEED_KBPS = "X-IG-Bandwidth-Speed-KBPS";
        public const string HEADER_X_IG_BANDWIDTH_TOTALBYTES_B = "X-IG-Bandwidth-TotalBytes-B";
        public const string HEADER_X_IG_BANDWIDTH_TOTALTIME_MS = "X-IG-Bandwidth-TotalTime-MS";
        public const string HEADER_X_FB_HTTP_ENGINE = "X-FB-HTTP-Engine";
        public const string HEADER_X_IG_APP_LOCALE = "X-IG-App-Locale";
        public const string HEADER_X_IG_DEVICE_LOCALE = "X-IG-Device-Locale";
        public const string HEADER_X_MID = "X-MID";
        public const string HEADER_AUTHORIZATION = "Authorization";
        public const string HEADER_RESPONSE_AUTHORIZATION = "ig-set-authorization";
        public const string HEADER_X_WWW_CLAIM = "X-IG-WWW-Claim";
        public const string HEADER_X_WWW_CLAIM_DEFAULT = "0";
        public const string HEADER_RESPONSE_X_WWW_CLAIM = "x-ig-set-www-claim";
        public const string HEADER_X_FB_TRIP_ID = "X-FB-TRIP-ID";
        public const string COOKIES_MID = "mid";
        public const string COOKIES_DS_USER_ID = "ds_user_id";
        public const string COOKIES_SESSION_ID = "sessionid";
        public const string HEADER_IG_APP_STARTUP_COUNTRY = "X-IG-App-Startup-Country";
        public static string HEADER_IG_APP_STARTUP_COUNTRY_VALUE = "US"; // IR
        public const string HEADER_X_IG_EXTENDED_CDN_THUMBNAIL_CACHE_BUSTING_VALUE = "X-IG-Extended-CDN-Thumbnail-Cache-Busting-Value";
        public const string HEADER_X_IG_BLOKS_VERSION_ID = "X-Bloks-Version-Id";
        public const string HEADER_X_IG_BLOKS_IS_LAYOUT_RTL = "X-Bloks-Is-Layout-RTL";
        public const string HEADER_X_IG_BLOKS_ENABLE_RENDERCODE = "X-Bloks-Enable-RenderCore";
        public const string HEADER_X_IG_DEVICE_ID = "X-IG-Device-ID";
        public const string HEADER_X_IG_ANDROID_ID = "X-IG-Android-ID";
        public const string CURRENT_BLOKS_VERSION_ID = "e538d4591f238824118bfcb9528c8d005f2ea3becd947a3973c030ac971bb88e";
        public const string HOST = "Host";
        public const string HOST_URI = "i.instagram.com";
        public const string ACCEPT_ENCODING = "gzip, deflate, sdch";
        public const string API = "/api";
        public const string API_SUFFIX = API + API_VERSION;
        public const string API_SUFFIX_V2 = API + API_VERSION_V2;
        public const string API_VERSION = "/v1";
        public const string API_VERSION_V2 = "/v2";
        public const string BASE_INSTAGRAM_API_URL = INSTAGRAM_URL + API_SUFFIX + "/";
        public const string COMMENT_BREADCRUMB_KEY = "iN4$aGr0m";
        public const string CSRFTOKEN = "csrftoken";
        public const string HEADER_ACCEPT_ENCODING = "gzip, deflate, sdch";
        public const string HEADER_ACCEPT_LANGUAGE = "Accept-Language";
        public const string HEADER_COUNT = "count";
        public const string HEADER_EXCLUDE_LIST = "exclude_list";
        public const string HEADER_IG_APP_ID = "X-IG-App-ID";
        public const string HEADER_IG_CAPABILITIES = "X-IG-Capabilities";
        public const string HEADER_IG_CONNECTION_TYPE = "X-IG-Connection-Type";
        public const string HEADER_IG_SIGNATURE = "signed_body";
        public const string HEADER_IG_SIGNATURE_KEY_VERSION = "ig_sig_key_version";
        public const string HEADER_MAX_ID = "max_id";
        public const string HEADER_PHONE_ID = "phone_id";
        public const string HEADER_QUERY = "q";
        public const string HEADER_RANK_TOKEN = "rank_token";
        public const string HEADER_TIMEZONE = "timezone_offset";
        public const string HEADER_USER_AGENT = "User-Agent";
        public const string HEADER_X_INSTAGRAM_AJAX = "X-Instagram-AJAX";
        public const string HEADER_X_REQUESTED_WITH = "X-Requested-With";
        public const string HEADER_XCSRF_TOKEN = "X-CSRFToken";
        public const string HEADER_XGOOGLE_AD_IDE = "X-Google-AD-ID";
        public const string HEADER_XML_HTTP_REQUEST = "XMLHttpRequest";
        public const string IG_APP_ID = "567067343352427";
        public const string IG_CONNECTION_TYPE = "WIFI";
        public const string IG_SIGNATURE_KEY_VERSION = "4";
        public const string INSTAGRAM_URL = "https://i.instagram.com";
        public const string INSTAGRAM_B_URL = "https://b.i.instagram.com";
        public const string P_SUFFIX = "p/";
        public const string SUPPORTED_CAPABALITIES_HEADER = "supported_capabilities_new";

        public static string TIMEZONE = "Asia/Tehran";

        public static int TIMEZONE_OFFSET = 16200;

        public const string USER_AGENT =
                                    "Instagram {6} Android ({7}/{8}; {0}; {1}; {2}; {3}; {4}; {5}; en_US; {9})";
        public const string USER_AGENT_DEFAULT =
        "Instagram 126.0.0.25.121 Android (24/7.0; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_US; 195435560)";
        public static readonly JArray SupportedCapabalities = new JArray
        {
            new JObject
            {
                {"name","SUPPORTED_SDK_VERSIONS"},
                {"value","45.0,46.0,47.0,48.0,49.0,50.0,51.0,52.0,53.0,54.0,55.0,56.0,57.0,58.0,59.0,60.0,61.0," +
                    "62.0,63.0,64.0,65.0,66.0,67.0,68.0,69.0,70.0,71.0,72.0,73.0,74.0,75.0,76.0,77.0,78.0,79.0,80.0"}
            },
            new JObject
            {
                {"name","FACE_TRACKER_VERSION"},
                {"value","12"}
            },
            new JObject
            {
                {"name","COMPRESSION"},
                {"value","ETC2_COMPRESSION"}
            },
            new JObject
            {
                {"name","WORLD_TRACKER"},
                {"value","WORLD_TRACKER_ENABLED"}
            }
        };
        public const string LOGIN_EXPERIMENTS_CONFIGS = "ig_android_fci_onboarding_friend_search,ig_android_device_detection_info_upload,ig_android_account_linking_upsell_universe,ig_android_direct_main_tab_universe_v2,ig_android_sign_in_help_only_one_account_family_universe,ig_android_sms_retriever_backtest_universe,ig_android_direct_add_direct_to_android_native_photo_share_sheet,ig_android_spatial_account_switch_universe,ig_growth_android_profile_pic_prefill_with_fb_pic_2,ig_account_identity_logged_out_signals_global_holdout_universe,ig_android_prefill_main_account_username_on_login_screen_universe,ig_android_login_identifier_fuzzy_match,ig_android_mas_remove_close_friends_entrypoint,ig_android_shared_email_reg_universe,ig_android_video_render_codec_low_memory_gc,ig_android_custom_transitions_universe,ig_android_push_fcm,multiple_account_recovery_universe,ig_android_show_login_info_reminder_universe,ig_android_email_fuzzy_matching_universe,ig_android_one_tap_aymh_redesign_universe,ig_android_direct_send_like_from_notification,ig_android_suma_landing_page,ig_android_prefetch_debug_dialog,ig_android_smartlock_hints_universe,ig_android_black_out,ig_activation_global_discretionary_sms_holdout,ig_android_video_ffmpegutil_pts_fix,ig_android_multi_tap_login_new,ig_save_smartlock_universe,ig_android_caption_typeahead_fix_on_o_universe,ig_android_enable_keyboardlistener_redesign,ig_android_nux_add_email_device,ig_android_direct_remove_view_mode_stickiness_universe,ig_android_hide_contacts_list_in_nux,ig_android_new_users_one_tap_holdout_universe,ig_android_ingestion_video_support_hevc_decoding,ig_android_mas_notification_badging_universe,ig_android_secondary_account_in_main_reg_flow_universe,ig_android_secondary_account_creation_universe,ig_android_account_recovery_auto_login,ig_android_pwd_encrytpion,ig_android_bottom_sheet_keyboard_leaks,ig_android_sim_info_upload,ig_android_shorten_sac_for_one_eligible_main_account_universe,ig_android_mobile_http_flow_device_universe,ig_android_hide_fb_button_when_not_installed_universe,ig_android_targeted_one_tap_upsell_universe,ig_android_gmail_oauth_in_reg,ig_android_vc_interop_use_test_igid_universe,ig_android_notification_unpack_universe,ig_android_quickcapture_keep_screen_on,ig_android_registration_confirmation_code_universe,ig_android_device_based_country_verification,ig_android_log_suggested_users_cache_on_error,ig_android_reg_modularization_universe,ig_android_device_verification_separate_endpoint,ig_android_universe_noticiation_channels,ig_android_account_linking_universe,ig_android_hsite_prefill_new_carrier,ig_android_one_login_toast_universe,ig_android_retry_create_account_universe,ig_android_family_apps_user_values_provider_universe,ig_android_reg_nux_headers_cleanup_universe,ig_android_get_cookie_with_concurrent_session_universe,ig_android_device_info_foreground_reporting,ig_android_shortcuts_2019,ig_android_device_verification_fb_signup,ig_android_onetaplogin_optimization,ig_android_passwordless_account_password_creation_universe,ig_android_black_out_toggle_universe,ig_video_debug_overlay,ig_android_ask_for_permissions_on_reg,ig_assisted_login_universe,ig_android_security_intent_switchoff,ig_android_device_info_job_based_reporting,ig_android_add_account_button_in_profile_mas_universe,ig_android_passwordless_auth,ig_android_direct_main_tab_account_switch,ig_android_recovery_one_tap_holdout_universe,ig_android_modularized_dynamic_nux_universe,ig_android_sac_follow_from_other_accounts_nux_universe,ig_android_fb_account_linking_sampling_freq_universe,ig_android_fix_sms_read_lollipop,ig_android_access_flow_prefill";

        public static string ACCEPT_LANGUAGE = "en-US";

        public const string FACEBOOK_LOGIN_URI = "https://m.facebook.com/v2.3/dialog/oauth?access_token=&client_id=124024574287414&e2e={0}&scope=email&default_audience=friends&redirect_uri=fbconnect://success&display=touch&response_type=token,signed_request&return_scopes=true";
        public const string FACEBOOK_TOKEN = "https://graph.facebook.com/me?access_token={0}&fields=id,is_employee,name";
        public const string FACEBOOK_TOKEN_PICTURE = "https://graph.facebook.com/me?access_token={0}&fields=picture";

        public const string FACEBOOK_USER_AGENT = "Mozilla/5.0 (Linux; Android {0}; {1} Build/{2}; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/69.0.3497.100 Mobile Safari/537.36";
        public const string FACEBOOK_USER_AGENT_DEFAULT = "Mozilla/5.0 (Linux; Android 7.0; PRA-LA1 Build/HONORPRA-LA1; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/69.0.3497.100 Mobile Safari/537.36";

        public const string WEB_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 OPR/57.0.3098.116";

        public const string ERROR_OCCURRED = "Oops, an error occurred";

        public static readonly Uri BaseInstagramUri = new Uri(BASE_INSTAGRAM_API_URL);

        #endregion Main

        #region Account endpoints constants

        public const string ACCOUNTS_2FA_LOGIN = API_SUFFIX + "/accounts/two_factor_login/";
        public const string ACCOUNTS_2FA_LOGIN_AGAIN = API_SUFFIX + "/accounts/send_two_factor_login_sms/";
        public const string ACCOUNTS_CHANGE_PROFILE_PICTURE = API_SUFFIX + "/accounts/change_profile_picture/";
        public const string ACCOUNTS_CHECK_PHONE_NUMBER = API_SUFFIX + "/accounts/check_phone_number/";
        public const string ACCOUNTS_CONTACT_POINT_PREFILL = API_SUFFIX + "/accounts/contact_point_prefill/";
        public const string ACCOUNTS_CREATE = API_SUFFIX + "/accounts/create/";
        public const string ACCOUNTS_CREATE_VALIDATED = API_SUFFIX + "/accounts/create_validated/";
        public const string ACCOUNTS_DISABLE_SMS_TWO_FACTOR = API_SUFFIX + "/accounts/disable_sms_two_factor/";
        public const string ACCOUNTS_EDIT_PROFILE = API_SUFFIX + "/accounts/edit_profile/";
        public const string ACCOUNTS_ENABLE_SMS_TWO_FACTOR = API_SUFFIX + "/accounts/enable_sms_two_factor/";
        public const string ACCOUNTS_GET_COMMENT_FILTER = API_SUFFIX + "/accounts/get_comment_filter/";
        public const string ACCOUNTS_LOGIN = API_SUFFIX + "/accounts/login/";
        public const string ACCOUNTS_LOGOUT = API_SUFFIX + "/accounts/logout/";
        public const string ACCOUNTS_READ_MSISDN_HEADER = API_SUFFIX + "/accounts/read_msisdn_header/";
        public const string ACCOUNTS_REGEN_BACKUP_CODES = API_SUFFIX + "/accounts/regen_backup_codes/";
        public const string ACCOUNTS_REMOVE_PROFILE_PICTURE = API_SUFFIX + "/accounts/remove_profile_picture/";
        public const string ACCOUNTS_REQUEST_PROFILE_EDIT = API_SUFFIX + "/accounts/current_user/?edit=true";
        public const string ACCOUNTS_SECURITY_INFO = API_SUFFIX + "/accounts/account_security_info/";
        public const string ACCOUNTS_SEND_CONFIRM_EMAIL = API_SUFFIX + "/accounts/send_confirm_email/";
        public const string ACCOUNTS_SEND_RECOVERY_EMAIL = API_SUFFIX + "/accounts/send_recovery_flow_email/";
        public const string ACCOUNTS_SEND_SIGNUP_SMS_CODE = API_SUFFIX + "/accounts/send_signup_sms_code/";
        public const string ACCOUNTS_SEND_SMS_CODE = API_SUFFIX + "/accounts/send_sms_code/";
        public const string ACCOUNTS_SEND_TWO_FACTOR_ENABLE_SMS = API_SUFFIX + "/accounts/send_two_factor_enable_sms/";
        public const string ACCOUNTS_SET_BIOGRAPHY = API_SUFFIX + "/accounts/set_biography/";
        public const string ACCOUNTS_SET_PHONE_AND_NAME = API_SUFFIX + "/accounts/set_phone_and_name/";
        public const string ACCOUNTS_SET_PRESENCE_DISABLED = API_SUFFIX + "/accounts/set_presence_disabled/";
        public const string ACCOUNTS_UPDATE_BUSINESS_INFO = API_SUFFIX + "/accounts/update_business_info/";
        public const string ACCOUNTS_USERNAME_SUGGESTIONS = API_SUFFIX + "/accounts/username_suggestions/";
        public const string ACCOUNTS_VALIDATE_SIGNUP_SMS_CODE = API_SUFFIX + "/accounts/validate_signup_sms_code/";
        public const string ACCOUNTS_VERIFY_SMS_CODE = API_SUFFIX + "/accounts/verify_sms_code/";
        public const string CHANGE_PASSWORD = API_SUFFIX + "/accounts/change_password/";
        public const string CURRENTUSER = API_SUFFIX + "/accounts/current_user/?edit=true";
        public const string SET_ACCOUNT_PRIVATE = API_SUFFIX + "/accounts/set_private/";
        public const string SET_ACCOUNT_PUBLIC = API_SUFFIX + "/accounts/set_public/";
        public const string ACCOUNTS_CONVERT_TO_PERSONAL = API_SUFFIX + "/accounts/convert_to_personal/";
        public const string ACCOUNTS_CREATE_BUSINESS_INFO = API_SUFFIX + "/accounts/create_business_info/";
        public const string ACCOUNTS_GET_PRESENCE = API_SUFFIX + "/accounts/get_presence_disabled/";
        public const string ACCOUNTS_GET_BLOCKED_COMMENTERS = API_SUFFIX + "/accounts/get_blocked_commenters/";
        public const string ACCOUNTS_SET_BLOCKED_COMMENTERS = API_SUFFIX + "/accounts/set_blocked_commenters/";

        #endregion Account endpoint constants

        #region Business endpoints constants

        /// <summary>
        /// /api/v1/business/instant_experience/get_ix_partners_bundle/?signed_body=b941ff07b83716087710019790b3529ab123c8deabfb216e056651e9cf4b4ca7.{}&ig_sig_key_version=4
        /// </summary>
        public const string BUSINESS_INSTANT_EXPERIENCE = API_SUFFIX + "/business/instant_experience/get_ix_partners_bundle/?signed_body={0}&ig_sig_key_version={1}";

        public const string BUSINESS_SET_CATEGORY = API_SUFFIX + "/business/account/set_business_category/";
        public const string BUSINESS_VALIDATE_URL = API_SUFFIX + "/business/instant_experience/ix_validate_url/";
        public const string BUSINESS_BRANDED_GET_SETTINGS = API_SUFFIX + "/business/branded_content/get_whitelist_settings/";
        public const string BUSINESS_BRANDED_USER_SEARCH = API_SUFFIX + "/users/search/?q={0}&count={1}&branded_content_creator_only=true";
        public const string BUSINESS_BRANDED_UPDATE_SETTINGS = API_SUFFIX + "/business/branded_content/update_whitelist_settings/";
        public const string BUSINESS_CONVERT_TO_BUSINESS_ACCOUNT = API_SUFFIX + "/business_conversion/get_business_convert_social_context/";

        #endregion Business endpoints constants

        #region Collection endpoints constants

        public const string COLLECTION_CREATE_MODULE = API_SUFFIX + "/collection_create/";
        public const string CREATE_COLLECTION = API_SUFFIX + "/collections/create/";
        public const string DELETE_COLLECTION = API_SUFFIX + "/collections/{0}/delete/";
        public const string EDIT_COLLECTION = API_SUFFIX + "/collections/{0}/edit/";
        public const string FEED_SAVED_ADD_TO_COLLECTION_MODULE = "feed_saved_add_to_collection";
        public const string GET_LIST_COLLECTIONS = API_SUFFIX + "/collections/list/";

        #endregion Collection endpoints constants

        #region Consent endpoints constants

        public const string CONSENT_NEW_USER_FLOW = API_SUFFIX + "/consent/new_user_flow/";
        public const string CONSENT_NEW_USER_FLOW_BEGINS = API_SUFFIX + "/consent/new_user_flow_begins/";

        #endregion Consent endpoints constants

        #region Direct endpoints constants

        public const string DIRECT_BROADCAST_CONFIGURE_VIDEO = API_SUFFIX + "/direct_v2/threads/broadcast/configure_video/";
        public const string DIRECT_BROADCAST_LINK = API_SUFFIX + "/direct_v2/threads/broadcast/link/";
        public const string DIRECT_BROADCAST_THREAD_LIKE = API_SUFFIX + "/direct_v2/threads/broadcast/like/";
        public const string DIRECT_BROADCAST_LOCATION = API_SUFFIX + "/direct_v2/threads/broadcast/location/";
        public const string DIRECT_BROADCAST_MEDIA_SHARE = API_SUFFIX + "/direct_v2/threads/broadcast/media_share/?media_type={0}";
        public const string DIRECT_BROADCAST_PROFILE = API_SUFFIX + "/direct_v2/threads/broadcast/profile/";
        public const string DIRECT_BROADCAST_REACTION = API_SUFFIX + "/direct_v2/threads/broadcast/reaction/";
        public const string DIRECT_BROADCAST_REEL_SHARE = API_SUFFIX + "/direct_v2/threads/broadcast/reel_share/";
        public const string DIRECT_BROADCAST_UPLOAD_PHOTO = API_SUFFIX + "/direct_v2/threads/broadcast/upload_photo/";
        public const string DIRECT_BROADCAST_HASHTAG = API_SUFFIX + "/direct_v2/threads/broadcast/hashtag/";
        public const string DIRECT_BROADCAST_LIVE_VIEWER_INVITE = API_SUFFIX + "/direct_v2/threads/broadcast/live_viewer_invite/";
        /// <summary>
        /// post data:
        /// <para>use_unified_inbox=true</para>
        /// <para>recipient_users= user ids split with comma.: ["user id1","user id2","..."]</para>
        /// </summary>
        public const string DIRECT_CREATE_GROUP = API_SUFFIX + "/direct_v2/create_group_thread/";

        public const string DIRECT_PRESENCE = API_SUFFIX + "/direct_v2/get_presence/";
        public const string DIRECT_SHARE = API_SUFFIX + "/direct_share/inbox/";
        public const string DIRECT_STAR = API_SUFFIX + "/direct_v2/threads/{0}/label/";
        public const string DIRECT_THREAD_HIDE = API_SUFFIX + "/direct_v2/threads/{0}/hide/";
        public const string DIRECT_THREAD_ADD_USER = API_SUFFIX + "/direct_v2/threads/{0}/add_user/";
        /// <summary>
        ///  deprecated
        /// </summary>
        public const string DIRECT_THREAD_ITEM_SEEN = API_SUFFIX + "/direct_v2/visual_threads/{0}/item_seen/";
        public const string DIRECT_THREAD_SEEN = API_SUFFIX + "/direct_v2/threads/{0}/items/{1}/seen/";
        public const string DIRECT_THREAD_LEAVE = API_SUFFIX + "/direct_v2/threads/{0}/leave/";
        public const string DIRECT_THREAD_MUTE = API_SUFFIX + "/direct_v2/threads/{0}/mute/";
        public const string DIRECT_THREAD_UNMUTE = API_SUFFIX + "/direct_v2/threads/{0}/unmute/";
        public const string DIRECT_THREAD_UPDATE_TITLE = API_SUFFIX + "/direct_v2/threads/{0}/update_title/";
        public const string DIRECT_UNSTAR = API_SUFFIX + "/direct_v2/threads/{0}/unlabel/";
        public const string GET_DIRECT_INBOX = API_SUFFIX + "/direct_v2/inbox/";
        public const string GET_DIRECT_PENDING_INBOX = API_SUFFIX + "/direct_v2/pending_inbox/";
        public const string GET_DIRECT_SHARE_USER = API_SUFFIX + "/direct_v2/threads/broadcast/profile/";
        public const string GET_DIRECT_TEXT_BROADCAST = API_SUFFIX + "/direct_v2/threads/broadcast/text/";
        public const string GET_DIRECT_THREAD = API_SUFFIX + "/direct_v2/threads/{0}/";
        public const string GET_DIRECT_THREAD_APPROVE = API_SUFFIX + "/direct_v2/threads/{0}/approve/";
        public const string GET_DIRECT_THREAD_APPROVE_MULTIPLE = API_SUFFIX + "/direct_v2/threads/approve_multiple/";
        public const string GET_DIRECT_THREAD_DECLINE = API_SUFFIX + "/direct_v2/threads/{0}/decline/";
        public const string GET_DIRECT_THREAD_DECLINE_MULTIPLE = API_SUFFIX + "/direct_v2/threads/decline_multiple/";
        public const string GET_DIRECT_THREAD_DECLINEALL = API_SUFFIX + "/direct_v2/threads/decline_all/";
        public const string GET_PARTICIPANTS_RECIPIENT_USER = API_SUFFIX + "/direct_v2/threads/get_by_participants/?recipient_users=[{0}]";
        public const string GET_RANK_RECIPIENTS_BY_USERNAME = API_SUFFIX + "/direct_v2/ranked_recipients/?mode=raven&show_threads=true&query={0}&use_unified_inbox=true";
        public const string GET_RANKED_RECIPIENTS = API_SUFFIX + "/direct_v2/ranked_recipients/";
        public const string GET_RECENT_RECIPIENTS = API_SUFFIX + "/direct_share/recent_recipients/";
        public const string STORY_SHARE = API_SUFFIX + "/direct_v2/threads/broadcast/story_share/?media_type={0}";
        public const string DIRECT_THREAD_DELETE_MESSAGE = API_SUFFIX + "/direct_v2/threads/{0}/items/{1}/delete/"; 

        #endregion Direct endpoints constants

        #region Discover endpoints constants

        public const string DISCOVER_AYML = API_SUFFIX + "/discover/ayml/";
        public const string DISCOVER_CHAINING = API_SUFFIX + "/discover/chaining/?target_id={0}";
        public const string DISCOVER_EXPLORE = API_SUFFIX + "/discover/explore/";
        public const string DISCOVER_TOPICAL_EXPLORE = API_SUFFIX + "/discover/topical_explore/";
        public const string DISCOVER_FETCH_SUGGESTION_DETAILS = API_SUFFIX + "/discover/fetch_suggestion_details/?target_id={0}&chained_ids={1}";
        public const string DISCOVER_TOP_LIVE = API_SUFFIX + "/discover/top_live/";
        public const string DISCOVER_TOP_LIVE_STATUS = API_SUFFIX + "/discover/top_live_status/";

        #endregion Discover endpoints constants
        
        #region FBSearch endpoints constants

        public const string FBSEARCH_CLEAR_SEARCH_HISTORY = API_SUFFIX + "/fbsearch/clear_search_history";
        public const string FBSEARCH_GET_HIDDEN_SEARCH_ENTITIES = API_SUFFIX + "/fbsearch/get_hidden_search_entities/";
        /// <summary>
        /// post data:
        /// <para>section=suggested</para>
        /// <para>user=["1 user id"]</para>
        /// </summary>
        public const string FBSEARCH_HIDE_SEARCH_ENTITIES = API_SUFFIX + "/fbsearch/hide_search_entities/";

        /// <summary>
        /// get nearby places
        /// </summary>
        public const string FBSEARCH_PLACES = API_SUFFIX + "/fbsearch/places/";
        
        public const string FBSEARCH_PROFILE_SEARCH = API_SUFFIX + "/fbsearch/profile_link_search/?q={0}&count={1}";
        public const string FBSEARCH_RECENT_SEARCHES = API_SUFFIX + "/fbsearch/recent_searches/";
        public const string FBSEARCH_SUGGESTED_SEARCHS = API_SUFFIX + "/fbsearch/suggested_searches/?type={0}";
        public const string FBSEARCH_TOPSEARCH = API_SUFFIX + "/fbsearch/topsearch/";
        public const string FBSEARCH_TOPSEARCH_FALT = API_SUFFIX + "/fbsearch/topsearch_flat/";
        public const string FBSEARCH_TOPSEARCH_FALT_PARAMETER = API_SUFFIX + "/fbsearch/topsearch_flat/?rank_token={0}&timezone_offset={1}&query={2}&context={3}";

        #endregion FBSearch endpoints constants

        #region FB endpoints constants

        public const string FB_ENTRYPOINT_INFO = API_SUFFIX + "/fb/fb_entrypoint_info/";
        public const string FB_FACEBOOK_SIGNUP = API_SUFFIX + "/fb/facebook_signup/";
        public const string FB_GET_INVITE_SUGGESTIONS = API_SUFFIX + "/fb/get_invite_suggestions/";

        #endregion FB endpoints constants

        #region Feed endpoints constants

        public const string FEED_ONLY_ME_FEED = API_SUFFIX + "/feed/only_me_feed/";
        /// <summary>
        /// {0} = rank token <<<<< this endpoint is deprecated
        /// </summary>
        public const string FEED_POPULAR = API_VERSION + "/feed/popular/?people_teaser_supported=1&rank_token={0}&ranked_content=true";

        public const string FEED_PROMOTABLE_MEDIA = API_SUFFIX + "/feed/promotable_media/";
        public const string FEED_REEL_MEDIA = API_SUFFIX + "/feed/reels_media/";
        public const string FEED_SAVED = API_SUFFIX + "/feed/saved/";
        public const string GET_COLLECTION = API_SUFFIX + "/feed/collection/{0}/";
        public const string GET_STORY_TRAY = API_SUFFIX + "/feed/reels_tray/";
        public const string GET_TAG_FEED = API_SUFFIX + "/feed/tag/{0}";
        public const string GET_USER_STORY = API_SUFFIX + "/feed/user/{0}/reel_media/";
        public const string GET_USER_TAGS = API_SUFFIX + "/usertags/{0}/feed/";
        public const string LIKE_FEED = API_SUFFIX + "/feed/liked/";
        public const string TIMELINEFEED = API_SUFFIX + "/feed/timeline/";
        public const string USER_REEL_FEED = API_SUFFIX + "/feed/user/{0}/reel_media/";
        public const string USEREFEED = API_SUFFIX + "/feed/user/{0}/";

        #endregion Feed endpoints constants

        #region Friendship endpoints constants

        public const string FRIENDSHIPS_APPROVE = API_SUFFIX + "/friendships/approve/{0}/";
        public const string FRIENDSHIPS_AUTOCOMPLETE_USER_LIST = API_SUFFIX + "/friendships/autocomplete_user_list/";
        public const string FRIENDSHIPS_BLOCK_USER = API_SUFFIX + "/friendships/block/{0}/";
  
        public const string FRIENDSHIPS_FOLLOW_USER = API_SUFFIX + "/friendships/create/{0}/";
        public const string FRIENDSHIPS_IGNORE = API_SUFFIX + "/friendships/ignore/{0}/";


        public const string FRIENDSHIPS_PENDING_REQUESTS = API_SUFFIX + "/friendships/pending/?rank_mutual=0&rank_token={0}";
        public const string FRIENDSHIPS_REMOVE_FOLLOWER = API_SUFFIX + "/friendships/remove_follower/{0}/";
        /// <summary>
        /// hide your stories from specific users
        /// </summary>
        public const string FRIENDSHIPS_SET_REEL_BLOCK_STATUS = API_SUFFIX + "/friendships/set_reel_block_status/";

        public const string FRIENDSHIPS_SHOW_MANY = API_SUFFIX + "/friendships/show_many/";


        public const string FRIENDSHIPS_UNBLOCK_USER = API_SUFFIX + "/friendships/unblock/{0}/";


        public const string FRIENDSHIPS_FAVORITE = API_SUFFIX + "/friendships/favorite/{0}/";
        public const string FRIENDSHIPS_UNFAVORITE = API_SUFFIX + "/friendships/unfavorite/{0}/";
        public const string FRIENDSHIPS_FAVORITE_FOR_STORIES = API_SUFFIX + "/friendships/favorite_for_stories/{0}/";
        public const string FRIENDSHIPS_UNFAVORITE_FOR_STORIES = API_SUFFIX + "/friendships/unfavorite_for_stories/{0}/";
        public const string FRIENDSHIPS_UNFOLLOW_USER = API_SUFFIX + "/friendships/destroy/{0}/";
        public const string FRIENDSHIPS_USER_FOLLOWERS = API_SUFFIX + "/friendships/{0}/followers/?rank_token={1}";
        public const string FRIENDSHIPS_USER_FOLLOWERS_MUTUALFIRST = API_SUFFIX + "/friendships/{0}/followers/?rank_token={1}&rank_mutual={2}";
        public const string FRIENDSHIPS_USER_FOLLOWING = API_SUFFIX + "/friendships/{0}/following/?rank_token={1}";
        public const string FRIENDSHIPSTATUS = API_SUFFIX + "/friendships/show/";
        public const string FRIENDSHIPS_MARK_USER_OVERAGE = API_SUFFIX + "/friendships/mark_user_overage/{0}/feed/";
        public const string FRIENDSHIPS_MUTE_POST_STORY = API_SUFFIX + "/friendships/mute_posts_or_story_from_follow/";
        public const string FRIENDSHIPS_UNMUTE_POST_STORY = API_SUFFIX + "/friendships/unmute_posts_or_story_from_follow/";
        public const string FRIENDSHIPS_BLOCK_FRIEND_REEL = API_SUFFIX + "/friendships/block_friend_reel/{0}/";
        public const string FRIENDSHIPS_UNBLOCK_FRIEND_REEL = API_SUFFIX + "/friendships/unblock_friend_reel/{0}/";
        public const string FRIENDSHIPS_MUTE_FRIEND_REEL = API_SUFFIX + "/friendships/mute_friend_reel/{0}/";
        public const string FRIENDSHIPS_UNMUTE_FRIEND_REEL = API_SUFFIX + "/friendships/unmute_friend_reel/{0}/";
        public const string FRIENDSHIPS_BLOCKED_REEL = API_SUFFIX + "/friendships/blocked_reels/";
        public const string FRIENDSHIPS_BESTIES = API_SUFFIX + "/friendships/besties/";
        public const string FRIENDSHIPS_BESTIES_SUGGESTIONS = API_SUFFIX + "/friendships/bestie_suggestions/";
        public const string FRIENDSHIPS_SET_BESTIES = API_SUFFIX + "/friendships/set_besties/";

        #endregion Friendships endpoints constants

        #region Graphql, insights [statistics] endpoints constants

        public const string GRAPH_QL = API_SUFFIX + "/ads/graphql/";
        public const string GRAPH_QL_STATISTICS = GRAPH_QL + "?locale={0}&vc_policy=insights_policy&surface={1}";
        public const string INSIGHTS_MEDIA = API_SUFFIX + "/insights/account_organic_insights/?show_promotions_in_landing_page=true&first={0}";
        public const string INSIGHTS_MEDIA_SINGLE = API_SUFFIX + "/insights/media_organic_insights/{0}?{1}={2}";

        #endregion Graphql, insights [statistics] endpoints constants

        #region Highlight endpoints constants

        public const string HIGHLIGHT_CREATE_REEL = API_SUFFIX + "/highlights/create_reel/";
        public const string HIGHLIGHT_DELETE_REEL = API_SUFFIX + "/highlights/{0}/delete_reel/";
        public const string HIGHLIGHT_EDIT_REEL = API_SUFFIX + "/highlights/{0}/edit_reel/";
        public const string HIGHLIGHT_TRAY = API_SUFFIX + "/highlights/{0}/highlights_tray/";

        #endregion Highlight endpoints constants

        #region IgTv (instagram tv) endpoints constants

        public const string IGTV_CHANNEL = API_SUFFIX + "/igtv/channel/";
        public const string IGTV_SEARCH = API_SUFFIX + "/igtv/search/?query={0}";
        public const string IGTV_SUGGESTED_SEARCHES = API_SUFFIX + "/igtv/suggested_searches/";
        public const string IGTV_TV_GUIDE = API_SUFFIX + "/igtv/tv_guide/";
        public const string MEDIA_CONFIGURE_TO_IGTV = API_SUFFIX + "/media/configure_to_igtv/";

        #endregion IgTv (instagram tv) endpoints constants

        #region Language endpoints constants

        public const string LANGUAGE_TRANSLATE = API_SUFFIX + "/language/translate/?id={0}&type=3";
        public const string LANGUAGE_TRANSLATE_COMMENT = API_SUFFIX + "/language/bulk_translate/?comment_ids={0}";

        #endregion Language endpoints constants

        #region Live endpoints constants

        public const string LIVE_ADD_TO_POST_LIVE = API_SUFFIX + "/live/{0}/add_to_post_live/";
        public const string LIVE_COMMENT = API_SUFFIX + "/live/{0}/comment/";
        public const string LIVE_CREATE = API_SUFFIX + "/live/create/";
        public const string LIVE_DELETE_POST_LIVE = API_SUFFIX + "/live/{0}/delete_post_live/";
        public const string LIVE_END = API_SUFFIX + "/live/{0}/end_broadcast/";
        public const string LIVE_GET_COMMENT = API_SUFFIX + "/live/{0}/get_comment/";
        public const string LIVE_GET_COMMENT_LASTCOMMENTTS = API_SUFFIX + "/live/{0}/get_comment/?last_comment_ts={1}";
        public const string LIVE_GET_FINAL_VIEWER_LIST = API_SUFFIX + "/live/{0}/get_final_viewer_list/";
        public const string LIVE_GET_JOIN_REQUESTS = API_SUFFIX + "/live/{0}/get_join_requests/";
        public const string LIVE_GET_LIKE_COUNT = API_SUFFIX + "/live/{0}/get_like_count/";
        public const string LIVE_GET_LIVE_PRESENCE = API_SUFFIX + "/live/get_live_presence/?presence_type=30min";
        public const string LIVE_GET_POST_LIVE_COMMENT = API_SUFFIX + "/live/{0}/get_post_live_comments/?starting_offset={1}&encoding_tag={2}";
        public const string LIVE_GET_POST_LIVE_VIEWERS_LIST = API_SUFFIX + "/live/{0}/get_post_live_viewers_list/";
        public const string LIVE_GET_SUGGESTED_BROADCASTS = API_SUFFIX + "/live/get_suggested_broadcasts/";
        public const string LIVE_GET_VIEWER_LIST = API_SUFFIX + "/live/{0}/get_viewer_list/";
        public const string LIVE_HEARTBEAT_AND_GET_VIEWER_COUNT = API_SUFFIX + "/live/{0}/heartbeat_and_get_viewer_count/";
        public const string LIVE_INFO = API_SUFFIX + "/live/{0}/info/";
        public const string LIVE_LIKE = API_SUFFIX + "/live/{0}/like/";
        public const string LIVE_MUTE_COMMENTS = API_SUFFIX + "/live/{0}/mute_comment/";
        public const string LIVE_PIN_COMMENT = API_SUFFIX + "/live/{0}/pin_comment/";
        public const string LIVE_POST_LIVE_LIKES = API_SUFFIX + "/live/{0}/get_post_live_likes/?starting_offset={1}&encoding_tag={2}";
        public const string LIVE_START = API_SUFFIX + "/live/{0}/start/";
        public const string LIVE_UNMUTE_COMMENTS = API_SUFFIX + "/live/{0}/unmute_comment/";
        public const string LIVE_UNPIN_COMMENT = API_SUFFIX + "/live/{0}/unpin_comment/";

        #endregion Live endpoints constants

        #region Location endpoints constants
        /// <summary>
        /// It seems deprecated and can't get feeds, only stories will recieve
        /// </summary>
        public const string LOCATION_FEED = API_SUFFIX + "/feed/location/{0}/";
        public const string LOCATION_SECTION = API_SUFFIX + "/locations/{0}/sections/";

        public const string LOCATION_SEARCH = API_SUFFIX + "/location_search/";

        public const string LOCATIONS_INFO = API_SUFFIX + "/locations/{0}/info/";
        /// <summary>
        /// {0} => external id, NOT WORKING
        /// </summary>
        public const string LOCATIONS_RELEATED = API_SUFFIX + "/locations/{0}/related/";

        #endregion Location endpoints constants

        #region Media endpoints constants

        public const string ALLOW_MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/enable_comments/";
        public const string DELETE_COMMENT = API_SUFFIX + "/media/{0}/comment/{1}/delete/";
        public const string DELETE_MEDIA = API_SUFFIX + "/media/{0}/delete/?media_type={1}";
        public const string DELETE_MULTIPLE_COMMENT = API_SUFFIX + "/media/{0}/comment/bulk_delete/";
        public const string DISABLE_MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/disable_comments/";
        public const string EDIT_MEDIA = API_SUFFIX + "/media/{0}/edit_media/";
        public const string GET_MEDIA = API_SUFFIX + "/media/{0}/info/";
        public const string GET_SHARE_LINK = API_SUFFIX + "/media/{0}/permalink/";
        public const string LIKE_COMMENT = API_SUFFIX + "/media/{0}/comment_like/";
        public const string LIKE_MEDIA = API_SUFFIX + "/media/{0}/like/";
        public const string MAX_MEDIA_ID_POSTFIX = "/media/?max_id=";
        public const string MEDIA = "/media/";
        public const string MEDIA_ALBUM_CONFIGURE = API_SUFFIX + "/media/configure_sidecar/";
        public const string MEDIA_COMMENT_LIKERS = API_SUFFIX + "/media/{0}/comment_likers/";
        public const string MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/comments/?can_support_threading=true";
        public const string MEDIA_CONFIGURE = API_SUFFIX + "/media/configure/";
        public const string MEDIA_CONFIGURE_VIDEO = API_SUFFIX + "/media/configure/?video=1";
        public const string MEDIA_UPLOAD_FINISH = API_SUFFIX + "/media/upload_finish/?video=1";
        public const string MEDIA_INFOS = API_SUFFIX + "/media/infos/?_uuid={0}&media_ids={1}&ranked_content=true&include_inactive_reel=true";
        public const string MEDIA_CONFIGURE_NAMETAG = API_SUFFIX + "/media/configure_to_nametag/";
        public const string MEDIA_INLINE_COMMENTS = API_SUFFIX + "/media/{0}/comments/{1}/inline_child_comments/";
        public const string MEDIA_LIKERS = API_SUFFIX + "/media/{0}/likers/";
        public const string MEDIA_REPORT = API_SUFFIX + "/media/{0}/flag_media/";
        public const string MEDIA_REPORT_COMMENT = API_SUFFIX + "/media/{0}/comment/{1}/flag/";
        public const string MEDIA_SAVE = API_SUFFIX + "/media/{0}/save/";
        public const string MEDIA_UNSAVE = API_SUFFIX + "/media/{0}/unsave/";

        public const string MEDIA_VALIDATE_REEL_URL = API_SUFFIX + "/media/validate_reel_url/";
        public const string POST_COMMENT = API_SUFFIX + "/media/{0}/comment/";
        public const string SEEN_MEDIA = API_SUFFIX + "/media/seen/";
        public const string SEEN_MEDIA_STORY = API_SUFFIX_V2 + "/media/seen/?reel=1&live_vod=0";
        public const string STORY_CONFIGURE = API_SUFFIX + "/media/configure_to_reel/";
        public const string STORY_CONFIGURE_VIDEO = API_SUFFIX + "/media/configure_to_story/?video=1";
        public const string STORY_CONFIGURE_VIDEO2 = API_SUFFIX + "/media/configure_to_story/";
        public const string STORY_MEDIA_INFO_UPLOAD = API_SUFFIX + "/media/mas_opt_in_info/";
        public const string UNLIKE_COMMENT = API_SUFFIX + "/media/{0}/comment_unlike/";
        public const string UNLIKE_MEDIA = API_SUFFIX + "/media/{0}/unlike/";
        public const string MEDIA_STORY_VIEWERS = API_SUFFIX + "/media/{0}/list_reel_media_viewer/";
        public const string MEDIA_BLOCKED = API_SUFFIX + "/media/blocked/";
        public const string MEDIA_ARCHIVE = API_SUFFIX + "/media/{0}/only_me/";
        public const string MEDIA_UNARCHIVE = API_SUFFIX + "/media/{0}/undo_only_me/";
        public const string MEDIA_STORY_POLL_VOTERS = API_SUFFIX + "/media/{0}/{1}/story_poll_voters/";
        public const string MEDIA_STORY_POLL_VOTE = API_SUFFIX + "/media/{0}/{1}/story_poll_vote/";
        public const string MEDIA_STORY_SLIDER_VOTE = API_SUFFIX + "/media/{0}/{1}/story_slider_vote/";
        public const string MEDIA_STORY_QUESTION_RESPONSE = API_SUFFIX + "/media/{0}/{1}/story_question_response/";
        public const string MEDIA_STORY_COUNTDOWNS = API_SUFFIX + "/media/story_countdowns/";
        public const string MEDIA_FOLLOW_COUNTDOWN = API_SUFFIX + "/media/{0}/follow_story_countdown/";
        public const string MEDIA_UNFOLLOW_COUNTDOWN = API_SUFFIX + "/media/{0}/unfollow_story_countdown/";

        #endregion Media endpoints constants

        #region News endpoints constants

        public const string GET_FOLLOWING_RECENT_ACTIVITY = API_SUFFIX + "/news/";
        public const string GET_RECENT_ACTIVITY = API_SUFFIX + "/news/inbox/";
        /// <summary>
        /// post params:
        /// <para>"action":"click"</para>
        /// </summary>
        public const string NEWS_LOG = API_SUFFIX + "/news/log/";

        #endregion News endpoints constants

        #region Notification endpoints constants

        public const string NOTIFICATION_BADGE = API_SUFFIX + "/notifications/badge/";
        public const string PUSH_REGISTER = API_SUFFIX + "/push/register/";

        #endregion Notification endpoints constants

        #region Shopping endpoints constants

        public const string USER_SHOPPABLE_MEDIA = API_SUFFIX + "/feed/user/{0}/shoppable_media/";

        public const string COMMERCE_PRODUCT_INFO = API_SUFFIX + "/commerce/products/{0}/?media_id={1}&device_width={2}";

        #endregion Shopping endpoints constants

        #region Tags endpoints constants

        public const string GET_TAG_INFO = API_SUFFIX + "/tags/{0}/info/";
        public const string SEARCH_TAGS = API_SUFFIX + "/tags/search/?q={0}&count={1}";
        public const string TAG_FOLLOW = API_SUFFIX + "/tags/follow/{0}/";
        public const string TAG_RANKED = API_SUFFIX + "/tags/{0}/ranked_sections/";
        public const string TAG_RECENT = API_SUFFIX + "/tags/{0}/recent_sections/";
        public const string TAG_SECTION = API_SUFFIX + "/tags/{0}/sections/";
        /// <summary>
        /// queries:
        /// <para>visited = [{"id":"TAG ID","type":"hashtag"}]</para>
        /// <para>related_types = ["location","hashtag"]</para>
        /// </summary>
        public const string TAG_RELATED = API_SUFFIX + "/tags/{0}/related/";

        public const string TAG_STORY = API_SUFFIX + "/tags/{0}/story/";
        public const string TAG_SUGGESTED = API_SUFFIX + "/tags/suggested/";
        public const string TAG_UNFOLLOW = API_SUFFIX + "/tags/unfollow/{0}/";

        #endregion Tags endpoints constants

        #region Users endpoints constants

        public const string ACCOUNTS_LOOKUP_PHONE = API_SUFFIX + "/users/lookup_phone/";
        public const string GET_USER_INFO_BY_ID = API_SUFFIX + "/users/{0}/info/";
        public const string GET_USER_INFO_BY_USERNAME = API_SUFFIX + "/users/{0}/usernameinfo/";
        public const string SEARCH_USERS = API_SUFFIX + "/users/search/";
        public const string USERS_CHECK_EMAIL = API_SUFFIX + "/users/check_email/";
        public const string USERS_CHECK_USERNAME = API_SUFFIX + "/users/check_username/";
        public const string USERS_LOOKUP = API_SUFFIX + "/users/lookup/";
        public const string USERS_NAMETAG_CONFIG = API_SUFFIX + "/users/nametag_config/";
        public const string USERS_REEL_SETTINGS = API_SUFFIX + "/users/reel_settings/";
        public const string USERS_REPORT = API_SUFFIX + "/users/{0}/flag_user/";
        public const string USERS_SEARCH = API_SUFFIX + "/users/search/?timezone_offset={0}&q={1}&count={2}";
        public const string USERS_SET_REEL_SETTINGS = API_SUFFIX + "/users/set_reel_settings/";
        public const string USERS_FOLLOWING_TAG_INFO = API_SUFFIX + "/users/{0}/following_tags_info/";
        public const string USERS_FULL_DETAIL_INFO = API_SUFFIX + "/users/{0}/full_detail_info/";
        public const string USERS_NAMETAG_LOOKUP = API_SUFFIX + "/users/nametag_lookup/";
        public const string USERS_BLOCKED_LIST = API_SUFFIX + "/users/blocked_list/";
        public const string USERS_ACCOUNT_DETAILS = API_SUFFIX + "/users/{0}/account_details/";

        #endregion Users endpoints constants

        #region Upload endpoints constants

        public const string UPLOAD_PHOTO = INSTAGRAM_URL + "/rupload_igphoto/{0}_0_{1}";
        public const string UPLOAD_PHOTO_OLD = API_SUFFIX + "/upload/photo/";
        public const string UPLOAD_VIDEO = INSTAGRAM_URL + "/rupload_igvideo/{0}_0_{1}";
        public const string UPLOAD_VIDEO_OLD = API_SUFFIX + "/upload/video/";

        #endregion Upload endpoints constants

        #region Other endpoints constants

        public const string ADDRESSBOOK_LINK = API_SUFFIX + "/address_book/link/?include=extra_display_name,thumbnails";
        public const string ARCHIVE_REEL_DAY_SHELLS = API_SUFFIX + "/archive/reel/day_shells/?include_cover=0";
        public const string DYI_REQUEST_DOWNLOAD_DATA = API_SUFFIX + "/dyi/request_download_data/";
        public const string DYI_CHECK_DATA_STATE = API_SUFFIX + "/dyi/check_data_state/";
        public const string DYNAMIC_ONBOARDING_GET_STEPS = API_SUFFIX + "/dynamic_onboarding/get_steps/";
        public const string OEMBED = API_SUFFIX + "/oembed/?url={0}";
        public const string MEGAPHONE_LOG = API_SUFFIX + "/megaphone/log/";

        public const string QE_EXPOSE = API_SUFFIX + "/qe/expose/";

        public const string CHALLENGE = API_SUFFIX + "/challenge/";

        public const string LAUNCHER_SYNC = API_SUFFIX + "/launcher/sync/";
        public const string ACCOUNTS_GET_PREFILL_CANDIDATES = API_SUFFIX + "/accounts/get_prefill_candidates/";
        public const string QE_SYNC = API_SUFFIX + "/qe/sync/";
        public const string LAUNCHER_MOBILE_CONFIG = API_SUFFIX + "/launcher/mobileconfig/";
        #endregion Other endpoints constants

        #region Web endpoints constants

        public static string WEB_ADDRESS = "https://www.instagram.com";
        public static string WEB_ACCOUNTS = "/accounts";
        public static string WEB_ACCOUNT_DATA = WEB_ACCOUNTS + "/access_tool";
        public static string WEB_CURRENT_FOLLOW_REQUESTS = WEB_ACCOUNT_DATA + "/current_follow_requests";
        public static string WEB_FORMER_EMAILS = WEB_ACCOUNT_DATA + "/former_emails";
        public static string WEB_FORMER_PHONES = WEB_ACCOUNT_DATA + "/former_phones";
        public static string WEB_FORMER_USERNAMES = WEB_ACCOUNT_DATA + "/former_usernames";
        public static string WEB_FORMER_FULL_NAMES = WEB_ACCOUNT_DATA + "/former_full_names";
        public static string WEB_FORMER_BIO_TEXTS = WEB_ACCOUNT_DATA + "/former_bio_texts";
        public static string WEB_FORMER_BIO_LINKS = WEB_ACCOUNT_DATA + "/former_links_in_bio";


        public static string WEB_CURSOR = "__a=1&cursor={0}";

        public static readonly Uri InstagramWebUri = new Uri(WEB_ADDRESS);
        #endregion
    }
}
