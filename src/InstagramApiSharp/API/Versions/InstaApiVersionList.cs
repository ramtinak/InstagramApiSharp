/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Enums;
using System.Collections.Generic;
using System.Linq;

namespace InstagramApiSharp.API.Versions
{
    internal class InstaApiVersionList
    {
        public static InstaApiVersionList GetApiVersionList() => new InstaApiVersionList();

        public Dictionary<InstaApiVersionType, InstaApiVersion> ApiVersions()
        {
            return new Dictionary<InstaApiVersionType, InstaApiVersion>
            {
                {
                    InstaApiVersionType.Version35,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "95414346",
                         AppVersion = "35.0.0.20.96",
                         Capabilities = "3brTBw==",
                         SignatureKey = "be01114435207c0a0b11a5cf68faeb82ec4eee37c52e8429af5fff6b54b80b28"
                    }
                },
                {
                    InstaApiVersionType.Version44,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "107092322",
                         AppVersion = "44.0.0.9.93",
                         Capabilities = "3brTPw==",
                         SignatureKey = "25f955cc0c8f080a0592aa1fd2572d60afacd5f3c03090cf47ca409068b0d2e1"
                    }
                },
                {
                    InstaApiVersionType.Version61,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "107092322",
                         AppVersion = "61.0.0.19.86",
                         Capabilities = "3brTPw==",
                         SignatureKey = "25f955cc0c8f080a0592aa1fd2572d60afacd5f3c03090cf47ca409068b0d2e1"
                    }
                },
                {
                    InstaApiVersionType.Version64,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "125398467",
                         AppVersion = "64.0.0.14.96",
                         Capabilities = "3brTvw==",
                         SignatureKey = "ac5f26ee05af3e40a81b94b78d762dc8287bcdd8254fe86d0971b2aded8884a4"
                    }
                },
                {
                    InstaApiVersionType.Version74,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "125398467",
                         AppVersion = "74.0.0.21.99",
                         Capabilities = "3brTvw==",
                         SignatureKey = "ac5f26ee05af3e40a81b94b78d762dc8287bcdd8254fe86d0971b2aded8884a4"
                    }
                },
                {
                    InstaApiVersionType.Version76,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "138226743",
                         AppVersion = "76.0.0.15.395",
                         Capabilities = "3brTvw==",
                         SignatureKey = "19ce5f445dbfd9d29c59dc2a78c616a7fc090a8e018b9267bc4240a30244c53b"
                    }
                },
                {
                    InstaApiVersionType.Version86,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "147375143",
                         AppVersion = "86.0.0.24.87",
                         Capabilities = "3brTvw==",
                         SignatureKey = "19ce5f445dbfd9d29c59dc2a78c616a7fc090a8e018b9267bc4240a30244c53b"
                    }
                },
                {
                    InstaApiVersionType.Version107,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "168361634",
                         AppVersion = "107.0.0.27.121",
                         Capabilities = "3brTvw==",
                         SignatureKey = "c36436a942ea1dbb40d7f2d7d45280a620d991ce8c62fb4ce600f0a048c32c11"
                    }
                },
                {
                    InstaApiVersionType.Version126,
                    new InstaApiVersion
                    {
                         AppApiVersionCode = "195435560",
                         AppVersion = "126.0.0.25.121",
                         Capabilities = "3brTvwM=",
                         SignatureKey = "8e496c87a09d5e922f6e33df3f399ce298ddbd6f7d6d038417047cc6474a3542",
                         BloksVersionId = "e538d4591f238824118bfcb9528c8d005f2ea3becd947a3973c030ac971bb88e"
                    }
                }
            };
        }

        public InstaApiVersion GetApiVersion(InstaApiVersionType versionType)
        {
            return (from apiVer in ApiVersions()
                    where apiVer.Key == versionType
                    select apiVer.Value).FirstOrDefault();
        }
    }
}
