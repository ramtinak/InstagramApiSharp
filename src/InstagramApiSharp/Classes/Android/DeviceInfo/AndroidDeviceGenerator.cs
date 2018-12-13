using System;
using System.Collections.Generic;
using System.Linq;

namespace InstagramApiSharp.Classes.Android.DeviceInfo
{
    public class AndroidDeviceGenerator
    {
        private static readonly List<string> DevicesNames = new List<string>
        {
            AndroidDevices.LG_OPTIMUS_G,
            AndroidDevices.NEXUS7_GEN2,
            AndroidDevices.NEXUS7_GEN1,
            AndroidDevices.HTC10,
            AndroidDevices.GALAXY6,
            AndroidDevices.GALAXY5,
            AndroidDevices.LG_OPTIMUS_F6,
            AndroidDevices.NEXUS_5X,
            AndroidDevices.NEXUS_5,
            AndroidDevices.GALAXY_S7_EDGE,
            AndroidDevices.GALAXY_S4,
            AndroidDevices.NEXUS_6P,
            AndroidDevices.GALAXY_TAB,
            AndroidDevices.GALAXY_TAB3,
            AndroidDevices.SAMSUNG_NOTE3,
            AndroidDevices.NEXUS4_CHROMA,
            AndroidDevices.SONY_Z3_COMPACT,
            AndroidDevices.XPERIA_Z5,
            AndroidDevices.HONOR_8LITE,
            AndroidDevices.XIAOMI_MI_4W,
            AndroidDevices.HTC_ONE_PLUS
        };

        public static Dictionary<string, AndroidDevice> AndroidAndroidDeviceSets = new Dictionary<string, AndroidDevice>
        {
            {
                "lg-optimus-g",
                new AndroidDevice
                {
                    AndroidBoardName = "geehrc",
                    AndroidBootloader = "MAKOZ10f",
                    DeviceBrand = "LGE",
                    DeviceModel = "LG-LS970",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "cm_ls970",
                    FirmwareBrand = "cm_ls970",
                    FirmwareFingerprint = "google/occam/mako:4.2.2/JDQ39/573038:user/release-keys",
                    FirmwareTags = "test-keys",
                    FirmwareType = "userdebug",
                    HardwareManufacturer = "LGE",
                    HardwareModel = "LG-LS970",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "nexus7gen2",
                new AndroidDevice
                {
                    AndroidBoardName = "flo",
                    AndroidBootloader = "FLO-04.07",
                    DeviceBrand = "google",
                    DeviceModel = "Nexus 7",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "razor",
                    FirmwareBrand = "razor",
                    FirmwareFingerprint = "google/razor/flo:6.0.1/MOB30P/2960889:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "asus",
                    HardwareModel = "Nexus 7",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "nexus7gen1",
                new AndroidDevice
                {
                    AndroidBoardName = "grouper",
                    AndroidBootloader = "4.23",
                    DeviceBrand = "google",
                    DeviceModel = "Nexus 7",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "nakasi",
                    FirmwareBrand = "nakasi",
                    FirmwareFingerprint = "google/nakasi/grouper:5.1.1/LMY47V/1836172:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "asus",
                    HardwareModel = "Nexus 7",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "htc10",
                new AndroidDevice
                {
                    AndroidBoardName = "msm8996",
                    AndroidBootloader = "1.0.0.0000",
                    DeviceBrand = "HTC",
                    DeviceModel = "HTC 10",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "pmewl_00531",
                    FirmwareBrand = "pmewl_00531",
                    FirmwareFingerprint = "htc/pmewl_00531/htc_pmewl:6.0.1/MMB29M/770927.1:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "HTC",
                    HardwareModel = "HTC 10",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "galaxy6",
                new AndroidDevice
                {
                    AndroidBoardName = "universal7420",
                    AndroidBootloader = "G920FXXU3DPEK",
                    DeviceBrand = "samsung",
                    DeviceModel = "zeroflte",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "SM-G920F",
                    FirmwareBrand = "zerofltexx",
                    FirmwareFingerprint = "samsung/zerofltexx/zeroflte:6.0.1/MMB29K/G920FXXU3DPEK:user/release-keys",
                    FirmwareTags = "dev-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "samsungexynos7420",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "galaxy-s5-gold",
                new AndroidDevice
                {
                    AndroidBoardName = "MSM8974",
                    AndroidBootloader = "G900FXXU1CPEH",
                    DeviceBrand = "samsung",
                    DeviceModel = "SM-G900F",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "kltexx",
                    FirmwareBrand = "kltexx",
                    FirmwareFingerprint = "samsung/kltexx/klte:6.0.1/MMB29M/G900FXXU1CPEH:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SM-G900F",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "lg-optimus-f6",
                new AndroidDevice
                {
                    AndroidBoardName = "f6t",
                    AndroidBootloader = "1.0.0.0000",
                    DeviceBrand = "lge",
                    DeviceModel = "LG-D500",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "f6_tmo_us",
                    FirmwareBrand = "f6_tmo_us",
                    FirmwareFingerprint = "lge/f6_tmo_us/f6:4.1.2/JZO54K/D50010h.1384764249:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "LGE",
                    HardwareModel = "LG-D500",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "nexus-5x",
                new AndroidDevice
                {
                    AndroidBoardName = "bullhead",
                    AndroidBootloader = "BHZ10k",
                    DeviceBrand = "google",
                    DeviceModel = "Nexus 5X",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "bullhead",
                    FirmwareBrand = "bullhead",
                    FirmwareFingerprint = "google/bullhead/bullhead:6.0.1/MTC19T/2741993:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "LGE",
                    HardwareModel = "Nexus 5X",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "galaxy-s7-edge",
                new AndroidDevice
                {
                    AndroidBoardName = "msm8996",
                    AndroidBootloader = "G935TUVU3APG1",
                    DeviceBrand = "samsung",
                    DeviceModel = "SM-G935T",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "hero2qltetmo",
                    FirmwareBrand = "hero2qltetmo",
                    FirmwareFingerprint =
                        "samsung/hero2qltetmo/hero2qltetmo:6.0.1/MMB29M/G935TUVU3APG1:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SM-G935T",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "xperia-z5",
                new AndroidDevice
                {
                    AndroidBoardName = "msm8994",
                    AndroidBootloader = "s1",
                    DeviceBrand = "Sony",
                    DeviceModel = "E6653",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "E6653",
                    FirmwareBrand = "E6653",
                    FirmwareFingerprint = "Sony/E6653/E6653:6.0.1/32.2.A.0.224/456768306:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "Sony",
                    HardwareModel = "E6653",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "galaxy-s4",
                new AndroidDevice
                {
                    AndroidBoardName = "MSM8960",
                    AndroidBootloader = "I337MVLUGOH1",
                    DeviceBrand = "samsung",
                    DeviceModel = "SGH-I337M",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "jfltevl",
                    FirmwareBrand = "jfltevl",
                    FirmwareFingerprint = "samsung/jfltevl/jfltecan:5.0.1/LRX22C/I337MVLUGOH1:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SGH-I337M",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "nexus-6p",
                new AndroidDevice
                {
                    AndroidBoardName = "angler",
                    AndroidBootloader = "angler-03.52",
                    DeviceBrand = "google",
                    DeviceModel = "Nexus 6P",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "angler",
                    FirmwareBrand = "angler",
                    FirmwareFingerprint = "google/angler/angler:6.0.1/MTC19X/2960136:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "Huawei",
                    HardwareModel = "Nexus 6P",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "sony-z3-compact",
                new AndroidDevice
                {
                    AndroidBoardName = "MSM8974",
                    AndroidBootloader = "s1",
                    DeviceBrand = "docomo",
                    DeviceModel = "SO-02G",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "SO-02G",
                    FirmwareBrand = "SO-02G",
                    FirmwareFingerprint = "docomo/SO-02G/SO-02G:5.0.2/23.1.B.1.317/2161656255:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "Sony",
                    HardwareModel = "SO-02G",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "galaxy-tab3",
                new AndroidDevice
                {
                    AndroidBoardName = "smdk4x12",
                    AndroidBootloader = "T310UEUCOI1",
                    DeviceBrand = "samsung",
                    DeviceId = "8525f5d8201f78b5",
                    DeviceModel = "SM-T310",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "lt01wifiue",
                    FirmwareBrand = "lt01wifiue",
                    FirmwareFingerprint = "samsung/lt01wifiue/lt01wifi:4.4.2/KOT49H/T310UEUCOI1:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SM-T310",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "nexus5",
                new AndroidDevice
                {
                    AndroidBoardName = "hammerhead",
                    AndroidBootloader = "HHZ20b",
                    DeviceBrand = "google",
                    DeviceModel = "Nexus 5",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "hammerhead",
                    FirmwareBrand = "hammerhead",
                    FirmwareFingerprint = "google/hammerhead/hammerhead:6.0.1/MOB30M/2862625:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "LGE",
                    HardwareModel = "Nexus 5",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "galaxy-note-edge",
                new AndroidDevice
                {
                    AndroidBoardName = "APQ8084",
                    AndroidBootloader = "N915W8VLU1CPE2",
                    DeviceBrand = "samsung",
                    DeviceModel = "SM-N915W8",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "tbltecan",
                    FirmwareBrand = "tbltecan",
                    FirmwareFingerprint = "samsung/tbltecan/tbltecan:6.0.1/MMB29M/N915W8VLU1CPE2:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SM-N915W8",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                "nexus4-chroma",
                new AndroidDevice
                {
                    AndroidBoardName = "MAKO",
                    AndroidBootloader = "MAKOZ30f",
                    DeviceBrand = "google",
                    DeviceModel = "Nexus 4",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "occam",
                    FirmwareBrand = "occam",
                    FirmwareFingerprint = "google/occam/mako:6.0.1/MOB30Y/3067468:user/release-keys",
                    FirmwareTags = "test-keys",
                    FirmwareType = "userdebug",
                    HardwareManufacturer = "LGE",
                    HardwareModel = "Nexus 4",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                AndroidDevices.SAMSUNG_NOTE3,
                new AndroidDevice
                {
                    AndroidBoardName = "MSM8974",
                    AndroidBootloader = "N900PVPUEOK2",
                    DeviceBrand = "samsung",
                    DeviceModel = "SM-N900P",
                    DeviceModelBoot = "qcom",
                    DeviceModelIdentifier = "cm_hltespr",
                    FirmwareBrand = "cm_hltespr",
                    FirmwareFingerprint = "samsung/hltespr/hltespr:5.0/LRX21V/N900PVPUEOH1:user/release-keys",
                    FirmwareTags = "test-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SM-N900P",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                AndroidDevices.GALAXY_TAB,
                new AndroidDevice
                {
                    AndroidBoardName = "universal5420",
                    AndroidBootloader = "T705XXU1BOL2",
                    DeviceBrand = "samsung",
                    DeviceModel = "Samsung Galaxy Tab S 8.4 LTE",
                    DeviceModelBoot = "universal5420",
                    DeviceModelIdentifier = "LRX22G.T705XXU1BOL2",
                    FirmwareBrand = "Samsung Galaxy Tab S 8.4 LTE",
                    FirmwareFingerprint = "samsung/klimtltexx/klimtlte:5.0.2/LRX22G/T705XXU1BOL2:user/release-keys",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SM-T705",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1440x2560",
                    Dpi = "640dpi"
                }
            },
            {
                AndroidDevices.HONOR_8LITE,
                new AndroidDevice
                {
                    AndroidBoardName = "HONOR",
                    DeviceBrand = "HUAWEI",
                    HardwareManufacturer = "HUAWEI",
                    DeviceModel = "PRA-LA1",
                    DeviceModelIdentifier = "PRA-LA1",
                    FirmwareBrand = "HWPRA-H",
                    HardwareModel = "hi6250",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1080x1812",
                    Dpi = "480dpi",
                    FirmwareFingerprint = "HUAWEI/HONOR/PRA-LA1:7.0/hi6250/95414346:user/release-keys",
                    AndroidBootloader = "4.23",
                    DeviceModelBoot = "qcom",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user"
                }
            },
            {
                AndroidDevices.XIAOMI_MI_4W,
                new AndroidDevice
                {
                    AndroidBoardName = "MI",
                    DeviceBrand = "Xiaomi",
                    HardwareManufacturer = "Xiaomi",
                    DeviceModel = "MI-4W",
                    DeviceModelIdentifier = "4W",
                    FirmwareBrand = "4W",
                    HardwareModel = "cancro",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1080x1920",
                    Dpi = "480dpi",
                    FirmwareFingerprint = "Xiaomi/MI/4W:7.1/cancro/95414346:user/release-keys",
                    AndroidBootloader = "4.23",
                    DeviceModelBoot = "qcom",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user"
                }
            },
            {
                AndroidDevices.XIAOMI_HM_1SW,
                new AndroidDevice
                {
                    AndroidBoardName = "HM",
                    DeviceBrand = "Xiaomi",
                    HardwareManufacturer = "Xiaomi",
                    DeviceModel = "HM-1SW",
                    DeviceModelIdentifier = "1SW",
                    FirmwareBrand = "1SW",
                    HardwareModel = "armani",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "720x1280",
                    Dpi = "320dpi",
                    FirmwareFingerprint = "Xiaomi/HM/1SW:6.0/cancro/95414346:user/release-keys",
                    AndroidBootloader = "4.23",
                    DeviceModelBoot = "qcom",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user"
                }
            },
            {
                AndroidDevices.HTC_ONE_PLUS,
                new AndroidDevice
                {
                    AndroidBoardName = "One",
                    DeviceBrand = "Htc",
                    HardwareManufacturer = "Htc",
                    DeviceModel = "One-Plus",
                    DeviceModelIdentifier = "Plus",
                    FirmwareBrand = "Plus",
                    HardwareModel = "A3010",
                    DeviceGuid = Guid.NewGuid(),
                    PhoneGuid = Guid.NewGuid(),
                    Resolution = "1080x1920",
                    Dpi = "380dpi",
                    FirmwareFingerprint = "Htc/One/Plus:6.0/cancro/95414346:user/release-keys",
                    AndroidBootloader = "4.23",
                    DeviceModelBoot = "qcom",
                    FirmwareTags = "release-keys",
                    FirmwareType = "user"
                }
            }
        };

        static Random rnd = new Random();
        private static AndroidDevice LastDevice;
        public static AndroidDevice GetRandomAndroidDevice()
        {
            TryLabel:
            var randomDeviceIndex = rnd.Next(0, DevicesNames.Count);
            var device = AndroidAndroidDeviceSets.ElementAt(randomDeviceIndex).Value;
            device.PhoneGuid = Guid.NewGuid();
            device.DeviceGuid = Guid.NewGuid();
            device.DeviceId = ApiRequestMessage.GenerateDeviceIdFromGuid(device.DeviceGuid);
            if (LastDevice != null)
                if (device.DeviceId == LastDevice.DeviceId)
                    goto TryLabel;
            LastDevice = device;
            return device;
        }

        public static AndroidDevice GetByName(string name)
        {
            return AndroidAndroidDeviceSets[name];
        }

        //public static AndroidDevice GetById(string deviceId)
        //{
        //    return (from androidAndroidDeviceSet in AndroidAndroidDeviceSets
        //            where androidAndroidDeviceSet.Value.DeviceId == deviceId
        //            select androidAndroidDeviceSet.Value).FirstOrDefault();
        //}
    }
}