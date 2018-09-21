using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Android.DeviceInfo
{
    public class AndroidVersionList
    {
        public static AndroidVersionList GetVersionList() => new AndroidVersionList();

        public List<AndroidVersion> AndroidVersions()
        {
            return new List<AndroidVersion>
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
        }
    }
}
