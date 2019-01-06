/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaAudio
    {
        public string AudioSource { get; set; }

        private double _duration;
        public double Duration { get => _duration; set { _duration = value; DurationTs = System.TimeSpan.FromMilliseconds(value); } }

        public TimeSpan DurationTs { get; set; }

        public float[] WaveformData { get; set; }

        public int WaveformSamplingFrequencyHz { get; set; }
    }
}
