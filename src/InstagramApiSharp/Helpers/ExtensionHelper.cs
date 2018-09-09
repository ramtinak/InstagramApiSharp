using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.API;

namespace InstagramApiSharp
{
    internal static class ExtensionHelper
    {
        public static string GenerateUserAgent(this AndroidDevice deviceInfo)
        {
            if (deviceInfo == null)
                return InstaApiConstants.USER_AGENT_DEFAULT;
            return string.Format(InstaApiConstants.USER_AGENT, deviceInfo.Dpi, deviceInfo.Resolution, deviceInfo.HardwareManufacturer,
                deviceInfo.DeviceModelIdentifier, deviceInfo.FirmwareBrand, deviceInfo.HardwareModel,
                InstaApiConstants.IG_APP_VERSION, deviceInfo.AndroidVersion.APILevel,
                deviceInfo.AndroidVersion.VersionNumber, InstaApiConstants.IG_APP_API_VERSION);
        }
        public static string EncodeList(this string[] listOfValues, bool appendQuotation = true)
        {
            return EncodeList(listOfValues.ToList(), appendQuotation);
        }
        public static string EncodeList(this List<string> listOfValues, bool appendQuotation = true)
        {
            if (!appendQuotation)
                return string.Join(",", listOfValues);
            var list = new List<string>();
            foreach (var item in listOfValues)
                list.Add(item.Encode());
            return string.Join(",", list);
        }
        public static string Encode(this string content)
        {
            return "\"" + content + "\"";
        }
    }
}
