using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WorkOrder.Shared.Extensions
{
    public static class SecurityExtensions
    {
        private static string Salt => "f6ab5485f60746c6a063ace24dbae94b";
        private static string Key => Settings.SecurityKey;

        public static string Encrypt(this string str)
        {
            return GenerateEncryotion(str).Trim();
        }

        private static string GenerateEncryotion(string str)
        {
            return Encrypt(GetHashKey(Key), str);
        }

        private static byte[] GetHashKey(string hashKey)
        {
            // Initialize
            var encoder = new UTF8Encoding();
            // Get the salt
            var salt = !string.IsNullOrEmpty(Salt) ? Salt : Guid.NewGuid().ToString().Replace("-", "");
            var saltBytes = encoder.GetBytes(salt);
            // Setup the hasher
            var rfc = new Rfc2898DeriveBytes(hashKey, saltBytes);
            // Return the key
            return rfc.GetBytes(16);
        }

        private static string Encrypt(byte[] key, string dataToEncrypt)
        {
            // Initialize
            var encryptor = new AesManaged();
            // Set the key
            encryptor.Key = key;
            encryptor.IV = key;
            // create a memory stream
            using (var encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (var encrypt =
                    new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    var utfD1 = Encoding.UTF8.GetBytes(dataToEncrypt);
                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    // Return the encrypted data
                    return Convert.ToBase64String(encryptionStream.ToArray());
                }
            }
        }
    }
}
