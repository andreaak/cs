using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApp1
{
    public class Cipher
    {
        public static string UrlSafeEncrypt(string text, string password, string salt)
        {
            var base64 = Encrypt(text, password, salt);
            return base64.Replace("/", "_").Replace("+", "-");
        }

        public static string UrlSafeDecrypt(string text, string password, string salt)
        {
            var base64origin = text.Replace("_", "/").Replace("-", "+");
            return Decrypt(base64origin, password, salt);
        }

        public static string EncryptForAzure(string text, EncryptFor encryptForType)
        {
            var base64 = Encrypt(text, encryptForType);
            return base64.Replace('/', '_');
        }

        public static string DecryptFromAzure(string encrypted, EncryptFor encryptForType)
        {
            var base64 = encrypted.Replace('_', '/');
            return Decrypt(base64, encryptForType);
        }

        public static string Encrypt(string text, EncryptFor encryptForType)
        {
            var values = GetValues(encryptForType);

            return Encrypt<RijndaelManaged>(text, values.Item1, values.Item2);
        }

        public static string Decrypt(string encrypted, EncryptFor encryptForType)
        {
            var values = GetValues(encryptForType);

            return Decrypt<RijndaelManaged>(encrypted, values.Item1, values.Item2);
        }

        public static string Encrypt(string text, string password, string salt)
        {
            return Encrypt<RijndaelManaged>(text, password, salt);
        }

        public static string Decrypt(string encrypted, string password, string salt)
        {
            return Decrypt<RijndaelManaged>(encrypted, password, salt);
        }

        public static string Encrypt<T>(string value, string password, string salt)
            where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new T();
            algorithm.Padding = PaddingMode.PKCS7;

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            var transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (var buffer = new MemoryStream())
            {
                using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public static string Decrypt<T>(string text, string password, string salt)
            where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new RijndaelManaged();
            algorithm.Padding = PaddingMode.PKCS7;

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

            using (var buffer = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        private static Tuple<string, string> GetValues(EncryptFor encryptForType)
        {
            switch (encryptForType)
            {
                case EncryptFor.DeliveryValue:
                    return Tuple.Create(EncryptionValues.DeliveryValues.Pass, EncryptionValues.DeliveryValues.Salt);

                case EncryptFor.Any:
                    return Tuple.Create(EncryptionValues.AnyValues.Pass, EncryptionValues.AnyValues.Salt);

                default:
                    return Tuple.Create(string.Empty, string.Empty);
            }
        }
    }

    public enum EncryptFor
    {
        Any = 0,
        DeliveryValue = 1
    }

    internal class EncryptionValues
    {
        internal static class DeliveryValues
        {
            public const string Pass = "rcDeliverypassw0rd";
            public const string Salt = "aq0tSD8zflPKRPPS";
        }

        internal static class AnyValues
        {
            public const string Pass = "anyType1passw0rd";
            public const string Salt = "at0tSD8zflPKRPPS";
        }
    }


}