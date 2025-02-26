namespace PasswordManager.API.Encryption
{
    public interface IEncryptionStrategy
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
