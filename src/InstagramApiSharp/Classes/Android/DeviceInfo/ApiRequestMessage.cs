using System;
using InstagramApiSharp.API;
using InstagramApiSharp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Classes.Android.DeviceInfo
{
    internal class ApiRequestChallengeMessage : ApiRequestMessage
    {
        [JsonProperty("_csrftoken")]
        public string CsrtToken { get; set; }
    }
    public class ApiRequestMessage
    {
        static Random rnd = new Random();

        public string phone_id { get; set; }
        public string username { get; set; }
        public Guid guid { get; set; }
        public string device_id { get; set; }
        public string password { get; set; }
        public string login_attempt_count { get; set; } = "0";
        public string adid { get; set; }
        public static ApiRequestMessage CurrentDevice { get; private set; }
        internal string GetMessageString()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
        internal string GetChallengeMessageString(string csrfToken)
        {
            var api = new ApiRequestChallengeMessage
            {
                CsrtToken = csrfToken,
                device_id = device_id,
                guid = guid,
                login_attempt_count = "1",
                password = password,
                phone_id = phone_id,
                username = username,
                adid = adid
            };
            var json = JsonConvert.SerializeObject(api);
            return json;
        }
        internal string GetMessageStringForChallengeVerificationCodeSend(int Choice = 1)
        {
            return JsonConvert.SerializeObject(new { choice = Choice.ToString(), _csrftoken = "ReplaceCSRF", guid, device_id });
        }
        internal string GetChallengeVerificationCodeSend(string verify)
        {
            return JsonConvert.SerializeObject(new { security_code = verify.ToString(), _csrftoken = "ReplaceCSRF", guid, device_id });
        }
        internal string GenerateSignature(string signatureKey, out string deviceid)
        {
            if (string.IsNullOrEmpty(signatureKey))
                signatureKey = InstaApiConstants.IG_SIGNATURE_KEY;
            var res = CryptoHelper.CalculateHash(signatureKey,
                JsonConvert.SerializeObject(this));
            deviceid = device_id;
            return res;
        }
        internal string GenerateChallengeSignature(string signatureKey,string csrfToken, out string deviceid)
        {
            if (string.IsNullOrEmpty(signatureKey))
                signatureKey = InstaApiConstants.IG_SIGNATURE_KEY;
            var api = new ApiRequestChallengeMessage
            {
                CsrtToken = csrfToken,
                device_id = device_id,
                guid = guid,
                login_attempt_count = "1",
                password = password,
                phone_id = phone_id,
                username = username,
                adid = adid
            };
            var res = CryptoHelper.CalculateHash(signatureKey,
                JsonConvert.SerializeObject(api));
            deviceid = device_id;
            return res;
        }
        internal bool IsEmpty()
        {
            if (string.IsNullOrEmpty(phone_id)) return true;
            if (string.IsNullOrEmpty(device_id)) return true;
            if (Guid.Empty == guid) return true;
            return false;
        }

        internal static string GenerateDeviceId()
        {
            return GenerateDeviceIdFromGuid(Guid.NewGuid());
        }

        internal static string GenerateUploadId()
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            var uploadId = (long) timeSpan.TotalSeconds;
            return uploadId.ToString();
        }
        internal static string GenerateRandomUploadId()
        {
            var total = GenerateUploadId();
            var uploadId = total + rnd.Next(11111, 99999).ToString("D5");
            return uploadId;
        }
        public static ApiRequestMessage FromDevice(AndroidDevice device)
        {
            var requestMessage = new ApiRequestMessage
            {
                phone_id = device.PhoneGuid.ToString(),
                guid = device.DeviceGuid,
                device_id = device.DeviceId
            };
            return requestMessage;
        }

        public static string GenerateDeviceIdFromGuid(Guid guid)
        {
            var hashedGuid = CryptoHelper.CalculateMd5(guid.ToString());
            return $"android-{hashedGuid.Substring(0, 16)}";
        }
    }
}