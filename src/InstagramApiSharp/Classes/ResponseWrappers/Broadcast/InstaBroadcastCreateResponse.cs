/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using Newtonsoft.Json;
namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaBroadcastCreateResponse
    {
        [JsonProperty("broadcast_id")]
        public long BroadcastId { get; set; }
        [JsonProperty("upload_url")]
        public string UploadUrl { get; set; }
        [JsonProperty("max_time_in_seconds")]
        public int MaxTimeInSeconds { get; set; }
        [JsonProperty("speed_test_ui_timeout")]
        public int SpeedTestUiTimeout { get; set; }
        [JsonProperty("stream_network_speed_test_payload_chunk_size_in_bytes")]
        public int StreamNnetworkSpeedTestPayloadChunkSizeInBytes { get; set; }
        [JsonProperty("stream_network_speed_test_payload_size_in_bytes")]
        public int StreamNetworkSpeedTestPayloadSizeInBytes { get; set; }
        [JsonProperty("stream_network_speed_test_payload_timeout_in_seconds")]
        public int StreamNetworkSpeedTestPayloadTimeoutInSeconds { get; set; }
        [JsonProperty("speed_test_minimum_bandwidth_threshold")]
        public int SpeedTestMinimumBandwidthThreshold { get; set; }
        [JsonProperty("speed_test_retry_max_count")]
        public int SpeedTestRetryMaxCount { get; set; }
        [JsonProperty("speed_test_retry_time_delay")]
        public float SpeedTestRetryTimeDelay { get; set; }
        [JsonProperty("disable_speed_test")]
        public int DisableSpeedTest { get; set; }
        [JsonProperty("stream_video_allow_b_frames")]
        public int StreamVideoAllowBFrames { get; set; }
        [JsonProperty("stream_video_width")]
        public int StreamVideoWidth { get; set; }
        [JsonProperty("stream_video_bit_rate")]
        public int StreamVideoBitRate { get; set; }
        [JsonProperty("stream_video_fps")]
        public int StreamVideoFps { get; set; }
        [JsonProperty("stream_audio_bit_rate")]
        public int StreamAudioBitRate { get; set; }
        [JsonProperty("stream_audio_sample_rate")]
        public int StreamAudioSampleRate { get; set; }
        [JsonProperty("stream_audio_channels")]
        public int StreamAudioChannels { get; set; }
        [JsonProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
        [JsonProperty("broadcaster_update_frequency")]
        public int BroadcasterUpdateFrequency { get; set; }
        [JsonProperty("stream_video_adaptive_bitrate_config")]
        public string StreamVideoAdaptiveBitrateConfig { get; set; }
        [JsonProperty("stream_network_connection_retry_count")]
        public int StreamNetworkConnectionRetryCount { get; set; }
        [JsonProperty("stream_network_connection_retry_delay_in_seconds")]
        public int StreamNetworkConnectionRetryDelayInSeconds { get; set; }
        [JsonProperty("connect_with_1rtt")]
        public int ConnectWith1rtt { get; set; }
        [JsonProperty("allow_resolution_change")]
        public int AllowResolutionChange { get; set; }
        [JsonProperty("avc_rtmp_payload")]
        public int AvcRtmpPayload { get; set; }
        [JsonProperty("pass_thru_enabled")]
        public int PassThruEnabled { get; set; }
        [JsonProperty("live_trace_enabled")]
        public int LiveTraceEnabled { get; set; }
        [JsonProperty("live_trace_sample_interval_in_seconds")]
        public int LiveTraceSampleIntervalInSeconds { get; set; }
        [JsonProperty("live_trace_sampling_source")]
        public int LiveTraceSamplingSource { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
