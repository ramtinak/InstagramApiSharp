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
    public class InstaAudioResponse
    {
        [JsonProperty("audio_src")]
        public string AudioSource { get; set; }
        [JsonProperty("duration")]
        public double Duration { get; set; }
        [JsonProperty("waveform_data")]
        public float[] WaveformData { get; set; }
        [JsonProperty("waveform_sampling_frequency_hz")]
        public int WaveformSamplingFrequencyHz { get; set; }
    }
}
