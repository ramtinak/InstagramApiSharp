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
                    DeviceGuid = new Guid("202d7022-3533-4450-91bd-0344112e0deb"),
                    PhoneGuid = new Guid("5b971484-ad0f-41fa-8886-313e9e91f5b9"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("202d7022-3533-4450-91bd-0344112e0deb")),
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
                    DeviceGuid = new Guid("82c2dbb7-35fc-4544-8b6f-4d8606ea1f7f"),
                    PhoneGuid = new Guid("97dd4f8a-af3f-4cfe-8be3-c34c38110346"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("82c2dbb7-35fc-4544-8b6f-4d8606ea1f7f")),
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
                    DeviceGuid = new Guid("cf6e9c5f-5ad8-4507-8de5-958c4b398010"),
                    PhoneGuid = new Guid("e64b4dd2-0368-40f1-9168-723ddd7460c2"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("cf6e9c5f-5ad8-4507-8de5-958c4b398010")),
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
                    DeviceGuid = new Guid("a91cd29b-2070-4c4e-b4cb-35335b2a38dc"),
                    PhoneGuid = new Guid("3e90b5f5-23c3-4fd1-b9ba-8e090a1fa397"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("a91cd29b-2070-4c4e-b4cb-35335b2a38dc")),
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
                    DeviceGuid = new Guid("505cbe9d-487c-49d4-8f2c-b1cc166d1094"),
                    PhoneGuid = new Guid("9ade42fb-09de-4931-8526-8f7c1bd3ce2a"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("505cbe9d-487c-49d4-8f2c-b1cc166d1094")),
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
                    DeviceGuid = new Guid("d13d1596-0983-4e59-825f-bd7cd559106b"),
                    PhoneGuid = new Guid("141023a2-153b-4e92-ae64-893553eaa9db"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("d13d1596-0983-4e59-825f-bd7cd559106b")),
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
                    DeviceGuid = new Guid("5ccdd80f-389e-4156-b070-fddab5fb7ed9"),
                    PhoneGuid = new Guid("17c27d7a-788d-4430-bcb0-6ae605ef0b01"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("5ccdd80f-389e-4156-b070-fddab5fb7ed9")),
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
                    DeviceGuid = new Guid("7c020baa-3810-48a3-b991-35b83b2e1b31"),
                    PhoneGuid = new Guid("a115bd4b-e782-483b-96a8-157ec0f2803a"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("7c020baa-3810-48a3-b991-35b83b2e1b31")),
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
                    DeviceGuid = new Guid("e428e21e-f105-4201-8287-b8bd9bd8727f"),
                    PhoneGuid = new Guid("2a6e43a7-0204-4b76-89a6-d7d17303e5f7"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("e428e21e-f105-4201-8287-b8bd9bd8727f")),
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
                    DeviceGuid = new Guid("78178fef-aa0c-4691-9c00-16482c25ce24"),
                    PhoneGuid = new Guid("aaeb4dfb-a93d-4bd6-9147-1a3aaee60510"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("78178fef-aa0c-4691-9c00-16482c25ce24")),
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
                    DeviceGuid = new Guid("d22d08e6-6856-4c6a-8748-124471796564"),
                    PhoneGuid = new Guid("36fe0448-6404-423b-a11f-95528f0f5120"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("d22d08e6-6856-4c6a-8748-124471796564")),
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
                    DeviceGuid = new Guid("95363fcc-9b6d-4ef3-b7d7-9d4d1bf94602"),
                    PhoneGuid = new Guid("d685d651-082c-425b-872b-d0907604649a"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("95363fcc-9b6d-4ef3-b7d7-9d4d1bf94602")),
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
                    DeviceGuid = new Guid("bccfcc1c-8188-42fa-a14e-e238c847c358"),
                    PhoneGuid = new Guid("8afad275-4fca-49e6-a5e0-3b2bbfe6e9f2"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("bccfcc1c-8188-42fa-a14e-e238c847c358")),
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
                    DeviceGuid = new Guid("6548b691-0b6f-4179-b9a8-d4ced7cc1708"),
                    PhoneGuid = new Guid("af872704-92d7-49c8-a5bf-41e5c65a07b4"),
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
                    DeviceGuid = new Guid("dde2038c-4f1c-465a-982d-9c844fd2b80a"),
                    PhoneGuid = new Guid("d8d75d13-a124-4304-a935-0247ed1656cb"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("dde2038c-4f1c-465a-982d-9c844fd2b80a")),
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
                    DeviceGuid = new Guid("fe46d44b-e00c-4f1b-9718-7c4dae2160cc"),
                    PhoneGuid = new Guid("0bcbf7e0-a73f-4424-8c70-c2d38ae42d5d"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("fe46d44b-e00c-4f1b-9718-7c4dae2160cc")),
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
                    DeviceGuid = new Guid("2c4ae214-c037-486c-a335-76a1f6973445"),
                    PhoneGuid = new Guid("7fb2eb38-04ab-4c51-bd0c-694c7da2187e"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("2c4ae214-c037-486c-a335-76a1f6973445")),
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
                    DeviceGuid = new Guid("7f585e77-becf-4137-bf1f-84ab72e35eb4"),
                    PhoneGuid = new Guid("28484284-e646-4a29-88fc-76c2666d5ab3"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("7f585e77-becf-4137-bf1f-84ab72e35eb4")),
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
                    DeviceGuid = new Guid("c319490f-6f09-467b-b2a5-6f1db13348e9"),
                    PhoneGuid = new Guid("849a7ae1-cf94-4dd5-a977-a2f3e8363e66"),
                    DeviceId =
                        ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("c319490f-6f09-467b-b2a5-6f1db13348e9")),
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
                    DeviceGuid = new Guid("be897499-c663-492e-a125-f4c8d3785ebf"),
                    PhoneGuid = new Guid("7b72321f-dd9a-425e-b3ee-d4aaf476ec52"),
                    DeviceId = ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("be897499-c663-492e-a125-f4c8d3785ebf")),
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
                    DeviceGuid = new Guid("726ba564-e9e4-40da-985e-eaf2790cf35c"),
                    PhoneGuid = new Guid("40167788-a864-4f86-8b38-c1ac4fa543a5"),
                    DeviceId = ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("726ba564-e9e4-40da-985e-eaf2790cf35c")),
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
                    DeviceGuid = new Guid("eee33f71-41cd-40ef-8f82-9cdbb29012d7"),
                    PhoneGuid = new Guid("48cdf398-784d-470e-a54c-8e211b56f710"),
                    DeviceId = ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("eee33f71-41cd-40ef-8f82-9cdbb29012d7")),
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
                    DeviceGuid = new Guid("43691b4d-4fcf-46c9-9c1f-a65a38c4ecc2"),
                    PhoneGuid = new Guid("e17e7301-5ff0-4819-88da-27b320a79d4e"),
                    DeviceId = ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("43691b4d-4fcf-46c9-9c1f-a65a38c4ecc2")),
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

        public static AndroidDevice GetById(string deviceId)
        {
            return (from androidAndroidDeviceSet in AndroidAndroidDeviceSets
                where androidAndroidDeviceSet.Value.DeviceId == deviceId
                select androidAndroidDeviceSet.Value).FirstOrDefault();
        }
    }
}