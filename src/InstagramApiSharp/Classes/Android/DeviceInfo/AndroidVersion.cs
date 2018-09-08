using System;
using System.Collections.Generic;
using System.Linq;

namespace InstagramApiSharp.Classes.Android.DeviceInfo
{
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
                APILevel = "23"
            },
            new AndroidVersion
            {
                Codename = "Nougat",
                VersionNumber = "7.1",
                APILevel = "25"
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
    }
}