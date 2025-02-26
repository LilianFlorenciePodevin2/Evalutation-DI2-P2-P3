// EncryptionService.cs
using System;
using PasswordManager.API.Encryption;

namespace PasswordManager.API.Services
{
    public class EncryptionService
    {
        private readonly AesEncryptionStrategy _aesStrategy;
        private readonly RsaEncryptionStrategy _rsaStrategy;

        public EncryptionService(AesEncryptionStrategy aesStrategy, RsaEncryptionStrategy rsaStrategy)
        {
            _aesStrategy = aesStrategy;
            _rsaStrategy = rsaStrategy;
        }

        public string Encrypt(string plainText, string appType)
        {
            if (appType.Equals("Grand public", StringComparison.OrdinalIgnoreCase))
                return _aesStrategy.Encrypt(plainText);
            else if (appType.Equals("Professionnelle", StringComparison.OrdinalIgnoreCase))
                return _rsaStrategy.Encrypt(plainText);
            else
                throw new Exception("Type d'application inconnu");
        }

        public string Decrypt(string cipherText, string appType)
        {
            if (appType.Equals("Grand public", StringComparison.OrdinalIgnoreCase))
                return _aesStrategy.Decrypt(cipherText);
            else if (appType.Equals("Professionnelle", StringComparison.OrdinalIgnoreCase))
                return _rsaStrategy.Decrypt(cipherText);
            else
                throw new Exception("Type d'application inconnu");
        }
    }
}
