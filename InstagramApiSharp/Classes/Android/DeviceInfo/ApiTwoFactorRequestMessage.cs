using InstagramApiSharp.API;
using InstagramApiSharp.Helpers;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Android.DeviceInfo
{
    internal class ApiTwoFactorRequestMessage
    {
        internal ApiTwoFactorRequestMessage(string verificationCode, string username, string deviceId,
            string twoFactorIdentifier)
        {
            verification_code = verificationCode;
            this.username = username;
            device_id = deviceId;
            two_factor_identifier = twoFactorIdentifier;
        }

        public string verification_code { get; set; }
        public string username { get; set; }
        public string device_id { get; set; }
        public string two_factor_identifier { get; set; }


        internal string GenerateSignature(string signatureKey)
        {
            if (string.IsNullOrEmpty(signatureKey))
                signatureKey = InstaApiConstants.IG_SIGNATURE_KEY;
            return CryptoHelper.CalculateHash(signatureKey,
                JsonConvert.SerializeObject(this));
        }

        internal string GetMessageString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}