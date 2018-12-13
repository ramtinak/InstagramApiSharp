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
    public class InstaAudio
    {
        public string AudioSource { get; set; }

        public double Duration { get; set; }

        public float[] WaveformData { get; set; }

        public int WaveformSamplingFrequencyHz { get; set; }
    }
}
