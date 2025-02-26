using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace PasswordManager.API.Encryption
{
    public class RsaEncryptionStrategy : IEncryptionStrategy, IDisposable
    {
        // Stocker le fichier de clés dans le répertoire de base de l'application
        private static readonly string KeyFilePath = Path.Combine(AppContext.BaseDirectory, "rsa_keys.dat");

        private readonly RSA _rsa;
        private readonly IDataProtector _protector;

        public RsaEncryptionStrategy(IDataProtectionProvider dataProtectionProvider)
        {
            // Créer un protector pour chiffrer/déchiffrer les clés
            _protector = dataProtectionProvider.CreateProtector("PasswordManager.RsaEncryptionStrategy");

            // Créer l'instance RSA avec une taille de 2048 bits
            _rsa = RSA.Create(2048);

            if (File.Exists(KeyFilePath))
            {
                // Charger les clés chiffrées depuis le fichier
                string encryptedXml = File.ReadAllText(KeyFilePath, Encoding.UTF8);
                string xmlKeys = _protector.Unprotect(encryptedXml);
                _rsa.FromXmlString(xmlKeys);
            }
            else
            {
                // Générer une nouvelle paire de clés et la sauvegarder de façon sécurisée
                string xmlKeys = _rsa.ToXmlString(true); // inclut la clé privée
                string encryptedXml = _protector.Protect(xmlKeys);
                File.WriteAllText(KeyFilePath, encryptedXml, Encoding.UTF8);
            }
        }

        public string Encrypt(string plainText)
        {
            var data = Encoding.UTF8.GetBytes(plainText);
            var encryptedData = _rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedData);
        }

        public string Decrypt(string cipherText)
        {
            var data = Convert.FromBase64String(cipherText);
            var decryptedData = _rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public void Dispose()
        {
            _rsa?.Dispose();
        }
    }
}
