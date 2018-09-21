using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using InstagramApiSharp.API;

namespace InstagramApiSharp.Helpers
{
    internal class CryptoHelper
    {
        public static string ByteToString(byte[] buff)
        {
            return buff.Aggregate("", (current, item) => current + item.ToString("X2"));
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string CalculateMd5(string message)
        {
            var encoding = Encoding.UTF8;

            using (var md5 = MD5.Create())
            {
                var hashed = md5.ComputeHash(encoding.GetBytes(message));
                var hash = BitConverter.ToString(hashed).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static string CalculateHash(string key, string message)
        {
            var encoding = Encoding.UTF8;

            //Reference http://en.wikipedia.org/wiki/Secure_Hash_Algorithm
            //SHA256 block size is 512 bits => 64 bytes.
            const int HashBlockSize = 64;

            var keyBytes = encoding.GetBytes(key);
            var opadKeySet = new byte[HashBlockSize];
            var ipadKeySet = new byte[HashBlockSize];


            if (keyBytes.Length > HashBlockSize) keyBytes = GetHash(keyBytes);

            // This condition is independent of previous
            // condition. If previous was true
            // we still need to execute this to make keyBytes same length
            // as blocksize with 0 padded if its less than block size
            if (keyBytes.Length < HashBlockSize)
            {
                var newKeyBytes = new byte[HashBlockSize];
                keyBytes.CopyTo(newKeyBytes, 0);
                keyBytes = newKeyBytes;
            }


            for (var i = 0; i < keyBytes.Length; i++)
            {
                opadKeySet[i] = (byte) (keyBytes[i] ^ 0x5C);
                ipadKeySet[i] = (byte) (keyBytes[i] ^ 0x36);
            }

            var hash = GetHash(ByteConcat(opadKeySet,
                GetHash(ByteConcat(ipadKeySet, encoding.GetBytes(message)))));

            // Convert to standard hex string 
            return hash.Select(a => a.ToString("x2"))
                .Aggregate((a, b) => $"{a}{b}");
        }

        public static byte[] GetHash(byte[] bytes)
        {
            using (var hash = SHA256.Create())
            {
                return hash.ComputeHash(bytes);
            }
        }

        public static byte[] ByteConcat(byte[] left, byte[] right)
        {
            if (null == left) return right;

            if (null == right) return left;

            var newBytes = new byte[left.Length + right.Length];
            left.CopyTo(newBytes, 0);
            right.CopyTo(newBytes, left.Length);

            return newBytes;
        }

        public static string GetCommentBreadCrumbEncoded(string text)
        {
            const string key = InstaApiConstants.COMMENT_BREADCRUMB_KEY;

            var date = Convert.ToInt64(DateTimeHelper.GetUnixTimestampMilliseconds(DateTime.Now));
            var rnd = new Random(DateTime.Now.Millisecond);
            var msgSize = text.Length;
            var term = rnd.Next(2, 3) * 1000 + msgSize * rnd.Next(15, 20) * 100;
            var textChangeDeviceEventCount = Math.Round((decimal) msgSize / rnd.Next(2, 3), 0);
            if (textChangeDeviceEventCount == 0) textChangeDeviceEventCount = 1;
            var data = $"{msgSize} {term} {textChangeDeviceEventCount} {date}";

            var keyByte = Encoding.UTF8.GetBytes(key);
            string dataEncoded;
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                dataEncoded = ByteToString(hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(data)));
            }

            return $"{Base64Encode(dataEncoded)}\n{Base64Encode(data)}\n";
        }
    }
}