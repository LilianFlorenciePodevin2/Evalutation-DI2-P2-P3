// AesEncryptionStrategy.cs
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.API.Encryption
{
    public class AesEncryptionStrategy : IEncryptionStrategy
    {
        // Clé et IV fixes pour cet exemple (16 octets pour AES-128)
        private readonly byte[] _key = Encoding.UTF8.GetBytes("0123456789abcdef");
        private readonly byte[] _iv = Encoding.UTF8.GetBytes("abcdef9876543210");

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
            {
                writer.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            var buffer = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }
    }
}
