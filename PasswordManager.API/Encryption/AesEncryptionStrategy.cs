using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.API.Encryption
{
    public class AesEncryptionStrategy : IEncryptionStrategy
    {
        // Clé et IV pour AES (à sécuriser et externaliser)
        private readonly byte[] _key = Encoding.UTF8.GetBytes("1234567890123456");
        private readonly byte[] _iv = Encoding.UTF8.GetBytes("6543210987654321");

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
