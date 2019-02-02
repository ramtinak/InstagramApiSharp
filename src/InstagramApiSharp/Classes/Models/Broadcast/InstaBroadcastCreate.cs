/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBroadcastCreate
    {
        public long BroadcastId { get; set; }

        public string UploadUrl { get; set; }

        public int MaxTimeInSeconds { get; set; }

        public int SpeedTestUiTimeout { get; set; }

        public int StreamNnetworkSpeedTestPayloadChunkSizeInBytes { get; set; }

        public int StreamNetworkSpeedTestPayloadSizeInBytes { get; set; }

        public int StreamNetworkSpeedTestPayloadTimeoutInSeconds { get; set; }

        public int SpeedTestMinimumBandwidthThreshold { get; set; }

        public int SpeedTestRetryMaxCount { get; set; }

        public float SpeedTestRetryTimeDelay { get; set; }

        public int DisableSpeedTest { get; set; }

        public int StreamVideoAllowBFrames { get; set; }

        public int StreamVideoWidth { get; set; }

        public int StreamVideoBitRate { get; set; }

        public int StreamVideoFps { get; set; }

        public int StreamAudioBitRate { get; set; }

        public int StreamAudioSampleRate { get; set; }

        public int StreamAudioChannels { get; set; }

        public int HeartbeatInterval { get; set; }

        public int BroadcasterUpdateFrequency { get; set; }

        public string StreamVideoAdaptiveBitrateConfig { get; set; }

        public int StreamNetworkConnectionRetryCount { get; set; }

        public int StreamNetworkConnectionRetryDelayInSeconds { get; set; }

        public int ConnectWith1rtt { get; set; }

        public int AllowResolutionChange { get; set; }

        public int AvcRtmpPayload { get; set; }

        public int PassThruEnabled { get; set; }

        public int LiveTraceEnabled { get; set; }

        public int LiveTraceSampleIntervalInSeconds { get; set; }

        public int LiveTraceSamplingSource { get; set; }
    }
}
