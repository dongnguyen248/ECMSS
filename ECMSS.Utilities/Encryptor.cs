using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ECMSS.Utilities
{
    public static class Encryptor
    {
        private const string SECRET_KEY = @"?(_qV~{SIASSUzm)x1mQ*d`s00@.-35TQh9PNX9Q+!@^YrL,`dVEt.qp_)r|Kv'";

        public static string Encrypt(string plainText)
        {
            if (plainText == null)
            {
                return null;
            }
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            byte[] secretBytes = Encoding.UTF8.GetBytes(SECRET_KEY);
            secretBytes = SHA256.Create().ComputeHash(secretBytes);
            byte[] bytesEncrypted = Encrypt(bytesToBeEncrypted, secretBytes);
            return Convert.ToBase64String(bytesEncrypted);
        }

        public static string Decrypt(string encryptedText)
        {
            if (encryptedText == null)
            {
                return null;
            }
            byte[] bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            byte[] secretBytes = Encoding.UTF8.GetBytes(SECRET_KEY);
            secretBytes = SHA256.Create().ComputeHash(secretBytes);
            byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted, secretBytes);
            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Encrypt(byte[] bytesToEncrypted, byte[] secretBytes)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(secretBytes, saltBytes, 1000);
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToEncrypted, 0, bytesToEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToDecrypted, byte[] secretBytes)
        {
            byte[] decryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(secretBytes, saltBytes, 1000);
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToDecrypted, 0, bytesToDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
    }
}