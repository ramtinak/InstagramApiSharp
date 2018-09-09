using System;
using System.Collections.Generic;
using System.Linq;

namespace InstagramApiSharp.Classes.Android.DeviceInfo
{
    [Serializable]
    public class AndroidVersion
    {
        public static readonly List<AndroidVersion> AndroidVersions = new List<AndroidVersion>
        {
            new AndroidVersion
            {
                Codename = "Ice Cream Sandwich",
                VersionNumber = "4.0",
                APILevel = "14"
            },
            new AndroidVersion
            {
                Codename = "Ice Cream Sandwich",
                VersionNumber = "4.0.3",
                APILevel = "15"
            },
            new AndroidVersion
            {
                Codename = "Jelly Bean",
                VersionNumber = "4.1",
                APILevel = "16"
            },
            new AndroidVersion
            {
                Codename = "Jelly Bean",
                VersionNumber = "4.2",
                APILevel = "17"
            },
            new AndroidVersion
            {
                Codename = "Jelly Bean",
                VersionNumber = "4.3",
                APILevel = "18"
            },
            new AndroidVersion
            {
                Codename = "KitKat",
                VersionNumber = "4.4",
                APILevel = "19"
            },
            new AndroidVersion
            {
                Codename = "KitKat",
                VersionNumber = "5.0",
                APILevel = "21"
            },
            new AndroidVersion
            {
                Codename = "Lollipop",
                VersionNumber = "5.1",
                APILevel = "22"
            },
            new AndroidVersion
            {
                Codename = "Marshmallow",
                VersionNumber = "6.0",
                APILevel = "23"
            },
            new AndroidVersion
            {
                Codename = "Nougat",
                VersionNumber = "7.0",
                APILevel = "24"
            },
            new AndroidVersion
            {
                Codename = "Nougat",
                VersionNumber = "7.1",
                APILevel = "25"
            },
            new AndroidVersion
            {
                Codename = "Oreo",
                VersionNumber = "8.0",
                APILevel = "26"
            },
            new AndroidVersion
            {
                Codename = "Oreo",
                VersionNumber = "8.1",
                APILevel = "27"
            },
            new AndroidVersion
            {
                Codename = "Pie",
                VersionNumber = "9.0",
                APILevel = "27"
            }
        };

        private AndroidVersion()
        {
        }

        public string Codename { get; set; }
        public string VersionNumber { get; set; }
        public string APILevel { get; set; }

        public static AndroidVersion FromString(string versionString)
        {
            var version = new Version(versionString);
            foreach (var androidVersion in AndroidVersions)
                if (version.CompareTo(new Version(androidVersion.VersionNumber)) == 0 ||
                    version.CompareTo(new Version(androidVersion.VersionNumber)) > 0 &&
                    androidVersion != AndroidVersions.Last() &&
                    version.CompareTo(new Version(AndroidVersions[AndroidVersions.IndexOf(androidVersion) + 1]
                        .VersionNumber)) < 0)
                    return androidVersion;
            return null;
        }

        static Random Rnd = new Random();
        private static AndroidVersion LastAndriodVersion;
        public static AndroidVersion GetRandomAndriodVersion()
        {
            TryLabel:
            var randomDeviceIndex = Rnd.Next(0, AndroidVersions.Count);
            var androidVersion = AndroidVersions.ElementAt(randomDeviceIndex);
            if (LastAndriodVersion != null)
                if (androidVersion.APILevel == LastAndriodVersion.APILevel)
                    goto TryLabel;
            return LastAndriodVersion = androidVersion;
        }
        public static AndroidVersion GetAndroidVersion(string apiLevel)
        {
            if (string.IsNullOrEmpty(apiLevel)) return null;

            return AndroidVersions.FirstOrDefault(api => api.APILevel == apiLevel);
        }
    }
}