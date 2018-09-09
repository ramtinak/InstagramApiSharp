using System;
using System.Collections.Generic;
using System.Net.Http;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApiSharp.Helpers
{
    internal class HttpHelper
    {
        public static HttpRequestMessage GetDefaultRequest(HttpMethod method, Uri uri, AndroidDevice deviceInfo)
        {
            var userAgent = deviceInfo.GenerateUserAgent();
            var request = new HttpRequestMessage(method, uri);
            request.Headers.Add(InstaApiConstants.HEADER_ACCEPT_LANGUAGE, InstaApiConstants.ACCEPT_LANGUAGE);
            request.Headers.Add(InstaApiConstants.HEADER_IG_CAPABILITIES, InstaApiConstants.IG_CAPABILITIES);
            request.Headers.Add(InstaApiConstants.HEADER_IG_CONNECTION_TYPE, InstaApiConstants.IG_CONNECTION_TYPE);
            request.Headers.Add(InstaApiConstants.HEADER_USER_AGENT, userAgent);
            request.Headers.Add(InstaApiConstants.HEADER_IG_APP_ID, InstaApiConstants.IG_APP_ID);
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_XGOOGLE_AD_IDE,
                deviceInfo.GoogleAdId.ToString()));
            return request;
        }
        public static HttpRequestMessage GetDefaultRequest(HttpMethod method, Uri uri, AndroidDevice deviceInfo, Dictionary<string, string> data)
        {
            var request = GetDefaultRequest(HttpMethod.Post, uri, deviceInfo);
            request.Content = new FormUrlEncodedContent(data);
            return request;
        }
        public static HttpRequestMessage GetSignedRequest(HttpMethod method,
            Uri uri,
            AndroidDevice deviceInfo,
            Dictionary<string, string> data)
        {
            var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                JsonConvert.SerializeObject(data));
            var payload = JsonConvert.SerializeObject(data);
            var signature = $"{hash}.{payload}";

            var fields = new Dictionary<string, string>
            {
                {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION}
            };
            var request = GetDefaultRequest(HttpMethod.Post, uri, deviceInfo);
            request.Content = new FormUrlEncodedContent(fields);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
            return request;
        }


        public static HttpRequestMessage GetSignedRequest(HttpMethod method,
            Uri uri,
            AndroidDevice deviceInfo,
            Dictionary<string, int> data)
        {
            var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                JsonConvert.SerializeObject(data));
            var payload = JsonConvert.SerializeObject(data);
            var signature = $"{hash}.{payload}";

            var fields = new Dictionary<string, string>
            {
                {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION}
            };
            var request = GetDefaultRequest(HttpMethod.Post, uri, deviceInfo);
            request.Content = new FormUrlEncodedContent(fields);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
            return request;
        }



        public static HttpRequestMessage GetSignedRequest(HttpMethod method,
            Uri uri,
            AndroidDevice deviceInfo,
            Dictionary<string, object> data)
        {
            var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                JsonConvert.SerializeObject(data));
            var payload = JsonConvert.SerializeObject(data);
            var signature = $"{hash}.{payload}";

            var fields = new Dictionary<string, string>
            {
                {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION}
            };
            var request = GetDefaultRequest(HttpMethod.Post, uri, deviceInfo);
            request.Content = new FormUrlEncodedContent(fields);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
            return request;
        }

        public static HttpRequestMessage GetSignedRequest(HttpMethod method,
            Uri uri,
            AndroidDevice deviceInfo,
            JObject data)
        {
            var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                data.ToString(Formatting.None));
            var payload = data.ToString(Formatting.None);
            var signature = $"{hash}.{payload}";
            var fields = new Dictionary<string, string>
            {
                {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION}
            };
            var request = GetDefaultRequest(HttpMethod.Post, uri, deviceInfo);
            request.Content = new FormUrlEncodedContent(fields);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
            return request;
        }

    }
}