using System;
using InstagramApiSharp.API;
using InstagramApiSharp.Helpers;
using Newtonsoft.Json;
using InstagramApiSharp.API.Versions;
namespace InstagramApiSharp.Classes.Android.DeviceInfo
{
    public class ApiRequestMessage
    {
        private string _phoneId;

        [JsonProperty("jazoest")]
        public string Jazoest { get; set; }
        [JsonProperty("country_codes")]
        public string CountryCodes { get; set; } = "[{\"country_code\":\"1\",\"source\":[\"default\"]},{\"country_code\":\"1\",\"source\":[\"uig_via_phone_id\"]}]";
        [JsonProperty("phone_id")]
        public string PhoneId { get { return _phoneId; } set { _phoneId = value; Jazoest = ExtensionHelper.GenerateJazoest(value); } }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("adid")]
        public string AdId { get; set; }
        [JsonProperty("guid")]
        public Guid Guid { get; set; }
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
        [JsonProperty("_uuid")]
        public string Uuid => Guid.ToString();
        [JsonProperty("google_tokens")]
        public string GoogleTokens { get; set; } = "[]";
        [JsonIgnore()/*JsonProperty("password")*/]
        public string Password { get; set; }
        [JsonProperty("enc_password")]
        public string EncPassword { get; set; }
        [JsonProperty("login_attempt_count")]
        public string LoginAttemptCount { get; set; } = "1";
        public static ApiRequestMessage CurrentDevice { get; private set; }
        internal string GetMessageString()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
        internal string GetChallengeMessageString(string csrfToken)
        {
            var api = new ApiRequestMessage
            {
                DeviceId = DeviceId,
                Guid = Guid,
                LoginAttemptCount = "0",
                Password = Password,
                PhoneId = PhoneId,
                Username = Username,
                AdId = AdId,
                CountryCodes = CountryCodes,
            };
            var json = JsonConvert.SerializeObject(api);
            return json;
        }
        internal string GenerateSignature(InstaApiVersion apiVersion, string signatureKey, out string deviceid)
        {
            if (string.IsNullOrEmpty(signatureKey))
                signatureKey = apiVersion.SignatureKey;
            var res = CryptoHelper.CalculateHash(signatureKey,
                JsonConvert.SerializeObject(this));
            deviceid = DeviceId;
            return res;
        }
        internal string GenerateChallengeSignature(InstaApiVersion apiVersion, string signatureKey,string csrfToken, out string deviceid)
        {
            if (string.IsNullOrEmpty(signatureKey))
                signatureKey = apiVersion.SignatureKey;
            var api = new ApiRequestMessage
            {
                DeviceId = DeviceId,
                Guid = Guid,
                LoginAttemptCount = "0",
                Password = Password,
                PhoneId = PhoneId,
                Username = Username,
                AdId = AdId,
                CountryCodes = CountryCodes,
            };
            var res = CryptoHelper.CalculateHash(signatureKey,
                JsonConvert.SerializeObject(api));
            deviceid = DeviceId;
            return res;
        }
        internal bool IsEmpty()
        {
            if (string.IsNullOrEmpty(PhoneId)) return true;
            if (string.IsNullOrEmpty(DeviceId)) return true;
            if (Guid.Empty == Guid) return true;
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
            var u = (uploadId * ExtensionHelper.Rnd.Next(1111, 99999)) - ExtensionHelper.Rnd.Next(1000, 99999);
            return $"{u}{ExtensionHelper.Rnd.Next(33333, 9999999)}";
        }
        //internal static string GenerateRandomUploadIdOLD()
        //{
        //    var total = GenerateUploadId();
        //    var uploadId = total + rnd.Next(11111, 99999).ToString("D5");
        //    return uploadId;
        //}
        internal static string GenerateRandomUploadId()
        {
            return GenerateUploadId();
        }
        public static ApiRequestMessage FromDevice(AndroidDevice device)
        {
            var requestMessage = new ApiRequestMessage
            {
                PhoneId = device.PhoneGuid.ToString(),
                Guid = device.DeviceGuid,
                DeviceId = device.DeviceId
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